import "./NutritionCard.css";
import React, { useEffect, useState } from 'react';
import TrainingProgram from "../../models/TrainingProgram";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Skeleton from "@mui/material/Skeleton";
import { downloadImage } from "../../api/imageApi";
import Nutrition from "../../models/Nutrition";
import { CardActionArea } from "@mui/material";
import { useHistory } from "react-router-dom";
import Tooltip from '@mui/material/Tooltip';

type Props = {
    nutrition: Nutrition
}


export default function TrainingProgramCard (props: Props) {
    const [loading, setLoading] = useState(true);
    const [image, setImage] = useState<string>("");
    const history = useHistory();
    
    const fetchData = async () => {
        await downloadImage(props.nutrition.imagePath ?? "").then(response => {
            setImage(response.data);
            setLoading(false);
          });
      };

      const createImage = (base64: string) =>
      {
        let format;
        if (props.nutrition.imagePath === null)
        {
            format = "png";
        }
        else
        {
            format = props.nutrition.imagePath.split('.')[1];
        }
        
        return `data:image/${format}};base64,`+base64;
      }

    useEffect(() => {
        fetchData();
        // setLoading(false);
      }, []);

      return(
          <>
          <Card sx={{display: "inline-block", margin:"5px 22px", maxWidth:"200px"}}
            // sx={{justifyContent: "space-around", display: "flex", margin:"40px 80px" }}
          >
              <CardActionArea onClick={(x)=>history.push(`/nutritions/get/${props.nutrition.id}`)}>
              {loading ? <Skeleton variant="rectangular" width={20} height={100} /> :
              <CardMedia
                component="img"
                height="140"
                image={createImage(image)}
                alt={"Image to " + props.nutrition.name}
                sx={{maxWidth:"200px", minWidth:"200px"}}
            /> }
            {/* <CardMedia
                component="img"
                height="140"
                image="/static/images/cards/contemplative-reptile.jpg"
                alt="green iguana"
                sx={{maxWidth:"200px"}}
            /> */}
            <CardContent>
                <Tooltip title={props.nutrition.name}>
                <Typography sx={{textAlign:"center"}} gutterBottom variant="h5" component="div">
                {props.nutrition.name.substring(0,15)}{props.nutrition.name.length>15?"...":""}
                </Typography>
                </Tooltip>
                {/* <Typography sx={{maxWidth:"200px"}} variant="body2" color="text.secondary">
                {props.nutrition.description.substring(0,30)}{props.nutrition.description.length>30?"...":""}
                </Typography> */}
            </CardContent>
            </CardActionArea>
            </Card>
          </>
      );
}