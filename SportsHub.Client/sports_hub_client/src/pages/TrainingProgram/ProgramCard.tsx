import "./ProgramCard.css";
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

type Props = {
    program: TrainingProgram
}


export default function TrainingProgramCard (props: Props) {
    const [loading, setLoading] = useState(true);
    const [image, setImage] = useState<string>("");

    const fetchData = async () => {
        await downloadImage(props.program.imagePath ?? "").then(response => {
            setImage(response.data);
            setLoading(false);
          });
      };

      const createImage = (base64: string) =>
      {
        let format;
        if (props.program.imagePath === null)
        {
            format = "png";
        }
        else
        {
            format = props.program.imagePath.split('.')[1];
        }
        
        return `data:image/${format}};base64,`+base64;
      }

    useEffect(() => {
        fetchData();
        // setLoading(false);
      }, []);

      return(
          <>
          <Card sx={{justifyContent: "space-around", display: "flex", margin:"40px 80px" }}>
              {loading ? <Skeleton variant="rectangular" width={20} height={100} /> :
              <CardMedia
                component="img"
                height="140"
                image={createImage(image)}
                alt={"Image to " + props.program.name}
                sx={{maxWidth:"200px"}}
            /> }
            {/* <CardMedia
                component="img"
                height="140"
                image="/static/images/cards/contemplative-reptile.jpg"
                alt="green iguana"
                sx={{maxWidth:"200px"}}
            /> */}
            <CardContent>
                <Typography gutterBottom variant="h5" component="div">
                {props.program.name}
                </Typography>
                <Typography sx={{maxWidth:"200px"}} variant="body2" color="text.secondary">
                {props.program.description.substring(0,30)}{props.program.description.length>30?"...":""}
                </Typography>
            </CardContent>
            <CardActions>
                <Button size="small" href={`/training-program/get/${props.program.id}`}>View More</Button>
            </CardActions>
            </Card>
          </>
      );
}