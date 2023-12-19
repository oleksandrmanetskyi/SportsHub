import "./News.css";
import React, { useEffect, useState } from 'react';
import jwt from 'jwt-decode';
import AuthStore from '../../stores/AuthStore';
import TrainingProgram from "../../models/TrainingProgram";
// import  getAll  from "../../api/NewsApi";
import NewsCard from "./NewsCard";
import { Button, Select } from "antd";
import { Pagination } from 'antd';
import jwt_decode from "jwt-decode";
import NewsApi, { getBySportKind } from '../../api/newsApi';
import userApi from '../../api/UserApi';
import News from "../../models/News";
const { Option } = Select;

export default function NewsPage () {
    const [loading, setLoading] = useState(true);
    const token = AuthStore.getToken() as string;
    const [News, setNews] = useState<Array<News>>();
    const user:any = jwt(token);
    const [size, setSize] = useState(4);
    const [page, setPage] = useState(1);
    
    const [userRole, setUser] = useState<string[]>();
    const [canEdit, setCanEdit] = useState(false);

    // const fetchData = async () => {
    //     // const user:any = jwt(token);
    //     // setLoading(true);
    //     await getRecommendedTrainingProgram(user.nameid).then(response => {
    //         setNews(response.data);
    //         setLoading(false);
    //       });
    //   };

      const fetchAllData = async () => {
        // const user:any = jwt(token);
        // setLoading(true);
        debugger
        await NewsApi.getAll().then(response => {
            setNews(response);
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

      const fetchData = async () => {
        // const user:any = jwt(token);
        setLoading(true);
        var resp = await userApi.getById(user.nameid);
        debugger
        await getBySportKind(resp.data.sportKindId ?? 0).then(response => {
          setNews(response.data);
            setLoading(false);
          });
      };

      const handleChange = (value: any) => {
        setPage(1);
        if(value == "by my sport kind")
        {
            fetchData();
        }
        else if(value == "all")
        {
            fetchAllData();
        }
      };

      return(
          <>
          <div>
          <div className="profileHeader">
          <h2>News</h2>
          </div>
          <div className="NewsSelect">
              {canEdit ?
                  <div className="new">
                      <Button href="/News/new">Create new</Button>
                  </div>: null}
            <Select defaultValue="all" style={{ width: 150, textAlign:"center", float:"right" }} onChange={handleChange}>
                <Option value="all">All</Option>
                <Option value="by my sport kind">By my sport kind</Option>
            </Select>
          </div>
          {
              loading ? null : typeof News == 'undefined' || News?.length==0 ? <div className="noNews">There are no news</div> : <>
              <div className="allNews">
              {News?.slice((page-1)*size, page*size).map(p => {
                  return <NewsCard key={p.id} News={p}/>
              })}
              </div>
              <div className="Pagination">
              <Pagination
                total={News?.length}
                showTotal={total => `Total ${total} items`}
                defaultPageSize={size}
                defaultCurrent={page}
                onChange={(p,pSize) => {
                    setPage(p);
                    setSize(pSize ?? 4);
                }}
                />
                </div>
                </>
          }
          </div>
          </>
      );
}