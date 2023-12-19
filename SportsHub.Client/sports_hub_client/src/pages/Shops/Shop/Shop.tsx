import "./Shop.css";
import React, { useEffect, useState } from 'react';
import jwt from 'jwt-decode';
import AuthStore from '../../../stores/AuthStore';
import { deleteTrainingProgram, followTrainingProgram, getAllTrainingProgram, getRecommendedTrainingProgram, getTrainingProgram, unfollowTrainingProgram } from "../../../api/trainingProgramApi";
import { Button, Popconfirm, Select } from "antd";
import { Pagination } from 'antd';
import jwt_decode from "jwt-decode";
import TrainingProgram from "../../../models/TrainingProgram";
import { useHistory, useParams } from "react-router-dom";
import { downloadImage } from "../../../api/imageApi";
import { Image } from 'antd';
import userApi from '../../../api/UserApi';
import notificationLogic from "../../../components/Notifications/Notification";
import Shop from "../../../models/Shop";
import { deleteShop, getShop } from "../../../api/shopsApi";
import { QuestionCircleOutlined } from '@ant-design/icons';

type Params = {
    id: string;
  };
export default function ShopPage () {
    const { id } = useParams<Params>();
    const history = useHistory();
    const [loading, setLoading] = useState(true);
    const [Shop, setShop] = useState<Shop>();
    const [imageBase64, setImageBase64] = useState<string>("");
    const [loadingButton, setLoadingButton] = useState(false);
    const [isFollowed, setFollowed] = useState(false);
    const token = AuthStore.getToken() as string;
    const user:any = jwt(token);
    const [canEdit, setCanEdit] = useState(false);


    const Delete = async () => 
    {
        await deleteShop(Shop?.id as number).then(r => {
                notificationLogic("success", "Shop is deleted succesfully");
                history.push(`/Shops`);
              })
              .catch(() => {
                notificationLogic("error", "Failed to delete Shop");
              });
        
    }

    // const fetchUserInfo = async () => {
    //     await userApi.getById(user.nameid).then(response => {
    //         let follows = response.data.trainingProgramId as string == id;
    //         setFollowed(follows);
    //       })
    //   }

    const fetchData = async () => {
        await getShop(+id).then( async r => {
            setShop(r.data);
            await downloadImage(r.data.imagePath).then(response => {
                let format = r.data.imagePath.split('.')[1];
                let base64 = `data:image/${format}};base64,`+ response.data;
                setImageBase64(base64);
              });
        });
        // await fetchUserInfo();
        setLoading(false);
      };

    useEffect(() => {
        let decodedJwt = jwt_decode(token) as any;
        let roles = decodedJwt['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] as string[];
        setCanEdit(roles.includes("Admin"));
        fetchData();
        // setLoading(false);
      }, [id]);

      return(
        <>
        {loading ? null : 
            <div style={{display:"flex", justifyContent:"center"}}>
                {/* <div>
                <h2 className="profileHeader">{Shop?.name}</h2>
                </div> */}
                <div className="imageAndDescription2">
                <h2 className="profileHeader">{Shop?.name}</h2>
                    <div>
                        <div style={{maxWidth:"500px", margin:"auto"}}>
                            <Image
                                width={"100%"}
                                src={imageBase64}
                            />
                        </div>
                    <div className="buttons">
                        {/* <div className="followButton"> */}
                    {/* {isFollowed ? 
                    <Button type="primary" loading={loadingButton} onClick={Unfollow}>
                        Unfollow
                    </Button>
                    :
                    <Button type="primary" loading={loadingButton} onClick={Follow}>
                        Follow
                    </Button>
                    }</div> */}
                    { canEdit ?
                    <div className="adminButtons">
                    <Button style={{marginRight:"5px"}} type="dashed" href={`/Shop/edit/${Shop?.id}`}>
                        Edit
                    </Button>
                    <Popconfirm placement="top" title="Are you sure you want to perform this action?" icon={<QuestionCircleOutlined style={{ color: 'red' }}/>} onConfirm={Delete} okText="Yes" cancelText="No">
                    <Button style={{marginLeft:"5px"}} type="dashed" danger>
                        Delete
                    </Button>
                    </Popconfirm>
                    </div> : null}
                </div>
                    </div>
                    <div style={{marginTop:"20px"}} ><h3 style={{textAlign:"center"}}>Location : {Shop?.location}</h3>
                    </div>
                </div>
                
            
            </div>}
        
        </>
      );

}
