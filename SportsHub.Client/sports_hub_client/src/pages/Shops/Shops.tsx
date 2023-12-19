import "./Shops.css";
import React, { useEffect, useState } from 'react';
import jwt from 'jwt-decode';
import AuthStore from '../../stores/AuthStore';
import TrainingProgram from "../../models/TrainingProgram";
// import  getAll  from "../../api/ShopApi";
import ShopCard from "./ShopCard";
import { Button, Select } from "antd";
import { Pagination } from 'antd';
import jwt_decode from "jwt-decode";
import ShopApi from '../../api/shopsApi';
import userApi from '../../api/UserApi';
import Shop from "../../models/Shop";
const { Option } = Select;

export default function ShopPage () {
    const [loading, setLoading] = useState(true);
    const token = AuthStore.getToken() as string;
    const [shops, setShops] = useState<Array<Shop>>();
    const user:any = jwt(token);
    const [size, setSize] = useState(4);
    const [page, setPage] = useState(1);
    
    const [userRole, setUser] = useState<string[]>();
    const [canEdit, setCanEdit] = useState(false);

    // const fetchData = async () => {
    //     // const user:any = jwt(token);
    //     // setLoading(true);
    //     await getRecommendedTrainingProgram(user.nameid).then(response => {
    //         setShop(response.data);
    //         setLoading(false);
    //       });
    //   };

      const fetchAllData = async () => {
        // const user:any = jwt(token);
        // setLoading(true);
        debugger
        await ShopApi.getAll().then(response => {
            setShops(response);
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

      // const fetchData = async () => {
      //   // const user:any = jwt(token);
      //   setLoading(true);
      //   await getRecommendedTrainingProgram(user.nameid).then(response => {
      //     setPrograms(response.data);
      //     setLoading(false);
      //   });
      // };

      return(
          <>
          <div>
          <div className="profileHeader">
          <h2>Shop</h2>
          </div>
          <div className="ShopsSelect">
              {canEdit ?
                  <div className="new">
                      <Button href="/Shop/new">Create new</Button>
                  </div>: null}
          </div>
          {
              loading ? null : typeof shops == 'undefined' || shops?.length==0 ? <div className="noShops">There are no shops</div> : <>
              <div className="allShops">
              {shops?.slice((page-1)*size, page*size).map(p => {
                  return <ShopCard key={p.id} Shop={p}/>
              })}
              </div>
              <div className="Pagination">
              <Pagination
                total={shops?.length}
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