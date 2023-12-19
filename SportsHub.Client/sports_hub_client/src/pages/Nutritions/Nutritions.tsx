import "./Nutritions.css";
import React, { useEffect, useState } from 'react';
import jwt from 'jwt-decode';
import AuthStore from '../../stores/AuthStore';
import TrainingProgram from "../../models/TrainingProgram";
import  getAll  from "../../api/nutritionsApi";
import NutritionCard from "./NutritionCard";
import { Button, Select } from "antd";
import { Pagination } from 'antd';
import jwt_decode from "jwt-decode";
import nutritionsApi from '../../api/nutritionsApi';
const { Option } = Select;

export default function NutritionsPage () {
    const [loading, setLoading] = useState(true);
    const token = AuthStore.getToken() as string;
    const [Nutritions, setNutritions] = useState<Array<TrainingProgram>>();
    const user:any = jwt(token);
    const [size, setSize] = useState(8);
    const [page, setPage] = useState(1);
    
    const [userRole, setUser] = useState<string[]>();
    const [canEdit, setCanEdit] = useState(false);

    // const fetchData = async () => {
    //     // const user:any = jwt(token);
    //     // setLoading(true);
    //     await getRecommendedTrainingProgram(user.nameid).then(response => {
    //         setNutritions(response.data);
    //         setLoading(false);
    //       });
    //   };

      const fetchAllData = async () => {
        // const user:any = jwt(token);
        // setLoading(true);
        debugger
        await nutritionsApi.getAll().then(response => {
            setNutritions(response);
            setLoading(false);
          });
      };



    useEffect(() => {
        let decodedJwt = jwt_decode(token) as any;
        let roles = decodedJwt['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] as string[];
        debugger
        setUser(roles);
        setCanEdit(roles.includes("Admin"));
        fetchAllData();
        // setLoading(false);
      }, []);

    //   const handleChange = (value: any) => {
    //     if(value == "recomended")
    //     {
    //         fetchData();
    //     }
    //     else if(value == "all")
    //     {
    //         fetchAllData();
    //     }
    //   };

      return(
          <>
          <div>
          <div className="profileHeader">
          <h2>Nutritions</h2>
          </div>
          <div className="Nutritionselect">
              {canEdit ?
                  <div className="new">
                      <Button href="/nutritions/new">Create new</Button>
                  </div>: null}
            {/* <Select defaultValue="Recomended" style={{ width: 150, textAlign:"center", float:"right" }} onChange={handleChange}>
                <Option value="recomended">Recomended</Option>
                <Option value="all">All</Option>
            </Select> */}
          </div>
          {
              loading ? null : typeof Nutritions == 'undefined' || Nutritions?.length==0 ? <div className="noNutritions">There are no nutritions</div> : <>
              <div className="allNutritions">
              {Nutritions?.slice((page-1)*size, page*size).map(p => {
                  return <NutritionCard key={p.id} nutrition={p}/>
              })}
              </div>
              <div className="Pagination">
              <Pagination
                total={Nutritions?.length}
                showTotal={total => `Total ${total} items`}
                defaultPageSize={size}
                defaultCurrent={page}
                onChange={(p,pSize) => {
                    setPage(p);
                    setSize(pSize ?? 8);
                }}
                />
                </div>
                </>
          }
          </div>
          </>
      );
}