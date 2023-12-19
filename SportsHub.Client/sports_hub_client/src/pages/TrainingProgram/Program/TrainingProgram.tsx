import "./TrainingProgram.css";
import React, { useEffect, useState } from 'react';
import jwt from 'jwt-decode';
import AuthStore from '../../../stores/AuthStore';
import { deleteTrainingProgram, followTrainingProgram, getAllTrainingProgram, getRecommendedTrainingProgram, getTrainingProgram, unfollowTrainingProgram, getRating, Like } from "../../../api/trainingProgramApi";
import { Button, Popconfirm, Select } from "antd";
import { Pagination } from 'antd';
import jwt_decode from "jwt-decode";
import TrainingProgram from "../../../models/TrainingProgram";
import { useHistory, useParams } from "react-router-dom";
import { downloadImage } from "../../../api/imageApi";
import { Image } from 'antd';
import userApi from '../../../api/UserApi';
import notificationLogic from "../../../components/Notifications/Notification";
import { QuestionCircleOutlined } from '@ant-design/icons';
import { Rating } from "@mui/material";

type ProgramParams = {
    id: string;
  };
export default function TrainingProgramPage () {
    const { id } = useParams<ProgramParams>();
    const history = useHistory();
    const [loading, setLoading] = useState(true);
    const [program, setProgram] = useState<TrainingProgram>();
    const [imageBase64, setImageBase64] = useState<string>("");
    const [loadingButton, setLoadingButton] = useState(false);
    const [isFollowed, setFollowed] = useState(false);
    const token = AuthStore.getToken() as string;
    const user:any = jwt(token);
    const [canEdit, setCanEdit] = useState(false);
    const [rating, setRating] = useState<number | null>(null);

    const Unfollow = async () => 
    {
        setLoadingButton(true);
        await unfollowTrainingProgram(user.nameid);
        setFollowed(false);
        setLoadingButton(false);
    }

    const Follow = async () => 
    {
        setLoadingButton(true);
        await followTrainingProgram(user.nameid, program?.id as number);
        setFollowed(true);
        setLoadingButton(false);
    }

    const Delete = async () => 
    {
        await deleteTrainingProgram(program?.id as number).then(r => {
                notificationLogic("success", "Training program is deleted succesfully");
                history.push(`/training-program`);
              })
              .catch(() => {
                notificationLogic("error", "Failed to delete training program");
              });
        
    }

    const fetchUserInfo = async () => {
        await userApi.getById(user.nameid).then(response => {
            let follows = response.data.trainingProgramId as string == id;
            setFollowed(follows);
          })
      }

      const fetchRatingInfo = async () => {
        await getRating(user.nameid, +id).then(response => {
            let ratingRes = response.data as number;
            setRating(ratingRes);
          })
      }

      const updateRatingInfo = async (score: number) => {
        await Like(user.nameid, +id, score).then(response => {
            setRating(score);
          })
      }

    const fetchData = async () => {
        await getTrainingProgram(id).then( async r => {
            setProgram(r.data);
            await downloadImage(r.data.imagePath).then(response => {
                let format;
                if (r.data.imagePath === null)
                {
                    format = "png";
                }
                else
                {
                    format = r.data.imagePath.split('.')[1];
                }
                let base64 = `data:image/${format}};base64,`+ response.data;
                setImageBase64(base64);
              });
              await fetchRatingInfo();
        });
        await fetchUserInfo();
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
            <div>
                <h2 className="profileHeader">{program?.name}</h2>
                <div className="imageAndDescription">
                    <div>
                        <div style={{marginTop:"37px", display:"flex", justifyContent:"center"}}>
                            <Image
                                // width={400}
                                style={{maxWidth:"400px", minWidth:"200px"}}
                                src={imageBase64}
                            />
                        </div>
                    <div className="buttons">
                        <div className="followButton">
                    {isFollowed ? 
                    <Button type="primary" loading={loadingButton} onClick={Unfollow}>
                        Unfollow
                    </Button>
                    :
                    <Button type="primary" loading={loadingButton} onClick={Follow}>
                        Follow
                    </Button>
                    }</div>
                    <div className="rating-style">
                        <Rating
                            name="simple-controlled"
                            value={rating}
                            onChange={(event, newValue) => {
                                updateRatingInfo(newValue ?? 0);
                            }}
                            />
                    </div>
                    { canEdit ?
                    <div className="adminButtons">
                    <Button style={{marginRight:"5px"}} type="dashed" href={`/training-program/edit/${program?.id}`}>
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
                    <div><h3 style={{textAlign:"center"}}>Description</h3>
                        {/* <div className="description"> */}
                            
                            <p className="description">{program?.description}</p>
                        {/* </div> */}
                    </div>
                </div>
                
            
            </div>}
        
        </>
      );

}
