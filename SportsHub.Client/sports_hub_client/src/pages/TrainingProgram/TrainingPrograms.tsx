import "./TrainingPrograms.css";
import React, { useEffect, useState } from 'react';
import jwt from 'jwt-decode';
import AuthStore from '../../stores/AuthStore';
import TrainingProgram from "../../models/TrainingProgram";
import { getAllTrainingProgram, getRecommendedTrainingProgram, getSuggestionsTrainingProgram, getNewSuggestionsTrainingProgram } from "../../api/trainingProgramApi";
import TrainingProgramCard from "./ProgramCard";
import { Button, Select, Tooltip } from "antd";
import { Pagination } from 'antd';
import {
    ReloadOutlined,
  } from '@ant-design/icons';
import jwt_decode from "jwt-decode";

const { Option } = Select;

export default function TrainingProgramsPage () {
    const [loading, setLoading] = useState(true);
    const token = AuthStore.getToken() as string;
    const [programs, setPrograms] = useState<Array<TrainingProgram>>();
    const user:any = jwt(token);
    const [size, setSize] = useState(3);
    const [page, setPage] = useState(1);
    
    const [userRole, setUser] = useState<string[]>();
    const [canEdit, setCanEdit] = useState(false);
    const [isRecommendationMode, setRecommendationMode] = useState(false);

    const fetchData = async () => {
        // const user:any = jwt(token);
        setLoading(true);
        await getRecommendedTrainingProgram(user.nameid).then(response => {
            setPrograms(response.data);
            setLoading(false);
          });
      };

      const fetchAllData = async () => {
        // const user:any = jwt(token);
        setLoading(true);
        await getAllTrainingProgram().then(response => {
            setPrograms(response.data);
            setLoading(false);
          });
      };

      const fetchSuggestionsData = async () => {
        // const user:any = jwt(token);
        setLoading(true);
        await getSuggestionsTrainingProgram(user.nameid, 3).then(response => {
            setPrograms(response.data);
          }).catch(x => {
            console.log(x)
            setPrograms([])
          });
          setLoading(false);
      };
      
      const fetchNewSuggestionsData = async () => {
        // const user:any = jwt(token);
        setLoading(true);
        await getNewSuggestionsTrainingProgram(user.nameid, 3).then(response => {
            setPrograms(response.data);
          }).catch(x => {
            console.log(x)
            setPrograms([])
          });
        setLoading(false);
      };



    useEffect(() => {
        let decodedJwt = jwt_decode(token) as any;
        let roles = decodedJwt['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] as string[];
        setUser(roles);
        setCanEdit(roles.includes("Admin"));
        fetchAllData();
        // setLoading(false);
      }, []);

      const handleChange = (value: any) => {
        setPage(1);
        if(value == "recommended")
        {
            fetchSuggestionsData();
            setRecommendationMode(true);
        }
        else if(value == "by my sport kind")
        {
            setRecommendationMode(false);
            fetchData();
        }
        else if(value == "all")
        {
            setRecommendationMode(false);
            fetchAllData();
        }
      };

      return(
          <>
          <div>
          <div className="profileHeader">
          <h2>Training programs</h2>
          </div>
          <div className="programSelect">
              {canEdit ?
                  <div className="new">
                      <Button href="/training-program/new">Create new</Button>
                  </div>: null}
                  {isRecommendationMode ?
                  <div className="refresh">
                    <Tooltip placement="top" title="Refresh my recommendations">
                        <ReloadOutlined onClick={fetchNewSuggestionsData}/>
                    </Tooltip>
                  </div>: null
                  }
            <Select defaultValue="all" style={{ width: 150, textAlign:"center", float:"right" }} onChange={handleChange}>
                <Option value="all">All</Option>
                <Option value="by my sport kind">By my sport kind</Option>
                <Option value="recommended">Recommended</Option>
            </Select>
          </div>
          {
              loading ? null : typeof programs == 'undefined' || programs?.length==0 ? <div className="noPrograms">There are no recommended training programs for you</div> : <>
              {programs?.slice((page-1)*size, page*size).map(p => {
                  return <TrainingProgramCard key={p.id} program={p}/>
              })}
              <div className="Pagination">
              <Pagination
                total={programs?.length}
                showTotal={total => `Total ${total} items`}
                defaultPageSize={size}
                defaultCurrent={page}
                onChange={(p,pSize) => {
                    setPage(p);
                    setSize(pSize ?? 3);
                }}
                />
                </div>
                </>
          }
          </div>
          </>
      );
}