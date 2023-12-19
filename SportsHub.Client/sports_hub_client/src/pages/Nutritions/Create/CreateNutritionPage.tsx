import "./CreateNutritionPage.css";
import React, { useEffect, useState } from 'react';
import jwt from 'jwt-decode';
import AuthStore from '../../../stores/AuthStore';
import TrainingProgram from "../../../models/TrainingProgram";
import { createTrainingProgram, editTrainingProgram, getTrainingProgram } from "../../../api/trainingProgramApi";
import { Input, message, Pagination, Popconfirm } from 'antd';
import jwt_decode from "jwt-decode";
import { SportKind } from "../../../api/sportKindApi";
import Nutrition from "../../../models/Nutrition";
import  sportKindsApi from '../../../api/sportKindApi';
import nutritionsApi, { createNutrition, editNutrition, getNutrition } from '../../../api/nutritionsApi';
import notificationLogic from "../../../components/Notifications/Notification";

import {
    Form,
    Select,
    InputNumber,
    Switch,
    Radio,
    Slider,
    Button,
    Upload,
    Rate,
    Checkbox,
    Row,
    Col,
    Image
  } from 'antd';
  import { UploadOutlined, InboxOutlined } from '@ant-design/icons';
import { useHistory, useParams } from "react-router-dom";
import { RcFile, UploadChangeParam } from "antd/lib/upload";
import { UploadFile } from "antd/lib/upload/interface";
import { downloadImage, uploadImage } from "../../../api/imageApi";
  
  const { Option } = Select;
  const formItemLayout = {
    labelCol: { span: 6 },
    wrapperCol: { span: 14 },
  };

  type ProgramParams = {
    id: string;
  };

export default function CreateNutritionPage () {
    const { id } = useParams<ProgramParams>();
    const history = useHistory();
    const [loading, setLoading] = useState(true);
    const token = AuthStore.getToken() as string;
    const [nutrition, setNutrition] = useState<Nutrition>(new Nutrition());
    const [fileName, setFileName] = useState<string>("default.png");
    const [imageBase64, setImageBase64] = useState<string>("");
    const [defaultImageBase64, setDefaultImageBase64] = useState<string>("");
    const [form] = Form.useForm();

    const onFinish = (values: any) => {
        //   setLoading(true);

        nutrition.name = form.getFieldValue("name");
        nutrition.description = form.getFieldValue("description");
        nutrition.imagePath = fileName;

        //   user.sportKindId = sportId;
        //   user.name = values.name;
        //   user.surName = values.surName;
        //   user.email = values.email;
        //   user.location = values.location;
        debugger
        uploadImage(fileName, imageBase64);
        if(+id)
        {
          nutrition.id = +id;
            editNutrition(JSON.stringify(nutrition)).then(() => {
                notificationLogic("success", "Nutrition is edited succesfully");
                history.push(`/nutritions/get/${id}`);
              })
              .catch(() => {
                notificationLogic("error", "Failed to edite nutrition");
              });;
        }else
        {
        createNutrition(JSON.stringify(nutrition)).then(() => {
            notificationLogic("success", "Nutrition is created succesfully");
            history.push(`/nutritions`);
          })
          .catch(() => {
            notificationLogic("error", "Failed to create nutrition");
          });;
        }
        
      };

    const fetchData = async () => {
        // await sportKindsApi.getAllSportKinds().then(r => setSportKinds(r));
        // await nutritionsApi.getAll().then(r => setNutritions(r));
        await downloadImage("").then(response => {
            setDefaultImageBase64(response.data);
          });
        setLoading(false);
      };

      const fetchEditData = async () => {
        // await sportKindsApi.getAllSportKinds().then(r => setSportKinds(r));
        // await nutritionsApi.getAll().then(r => setNutritions(r));
        debugger
        // let editId: string = id;
        await getNutrition(id).then(async r => {
            setNutrition(r.data);
            await downloadImage(r.data.imagePath).then(response => {
                setFileName(r.data.imagePath);
                let format = r.data.imagePath.split('.')[1];
                let base64 = `data:image/${format}};base64,`+ response.data;
                setImageBase64(base64);
              });
        })
        
        setLoading(false);
      };

    useEffect(() => {
        if (id) {
            fetchEditData();
          } else {
            fetchData();
          }
        // fetchData();
        // setLoading(false);
      }, [id]);

      const getBase64 = (img: Blob, callback: Function) => {
        const reader = new FileReader();
        reader.addEventListener("load", () => callback(reader.result));
        reader.readAsDataURL(img);
      };

      const handleUpload = (info: any) => {
          debugger
        if (info !== null) {
            setFileName(info.file.name);
            getBase64(info.file, (base64: any) => {
                setImageBase64(base64);
            });
            // setPhotoName(null);
          }
        else {
            setImageBase64("");
        }
      };

      const checkFormat = (file: RcFile) => {
        const isValid = file.type === 'image/png' || file.type === 'image/jpg' || file.type === 'image/jpeg';
        if (!isValid) {
        message.error(`${file.name} is not a png file`);
        }
        return isValid || Upload.LIST_IGNORE;
      }

      const onChange = (info: UploadChangeParam<UploadFile<any>>) => {
        if (info.file.status !== 'uploading') {
            console.log(info.file, info.fileList);
          }
          if (info.file.status === 'done') {
            setFileName(info.file.name);
            message.success(`${info.file.name} file uploaded successfully`);
          } else if (info.file.status === 'error') {
            message.error(`${info.file.name} file upload failed.`);
          }
        }

        const getImageBase64 = () => {
            if(imageBase64 ==="")
            {
              return `data:image/png;base64,`+ defaultImageBase64;
            }
            return imageBase64;
        }
      
      return (<>
      {
          loading ? null : <div>
              
              <Form
      name="validate_other"
      {...formItemLayout}
      form={form}
      // onFinish={onFinish}
    //   initialValues={{
    //     sportKind: sportKinds?.find(x => x.id == user?.sportKindId)?.name,
    //     name: user.name,
    //     surName: user.surName,
    //     email: user.email,
    //     location: user.location
    //   }}
    >
        {id ? <h2 className="profileHeader">Edit {nutrition.name} nutrition</h2> : <h2 className="profileHeader">Create new nutrition</h2>}
        <div className='imageUpload'>
            <div className="uploadComponent">
                <Image
                style={{marginLeft:"auto", marginRight:"auto"}}
                    width={200}
                    src={getImageBase64()}
                />
                </div>
                <div className="uploadComponent" style={{margin:"15px"}}>
                <Upload style={{marginLeft:"auto", marginRight:"auto"}} name={fileName} showUploadList={false} beforeUpload = {checkFormat} onChange = {onChange} customRequest={handleUpload}>
                    <Button icon={<UploadOutlined />}>Upload image</Button>
                </Upload>
                </div>
                </div>
                <div>
                  <p className="instruction">
                    Instruction: All required fields are marked with an asterisk *</p>
                </div>
      <Form.Item
        {...formItemLayout}
        name="name"
        label="Name"
        initialValue={nutrition.name}
        rules={[
          {
            required: true,
            message: 'Please input name',
          },
        ]}
      >
        <Input placeholder="Please input name" />
      </Form.Item>
      <Form.Item
        {...formItemLayout}
        name="description"
        label="Description"
        initialValue={nutrition.description}
        rules={[
          {
            required: true,
            message: 'Please input description',
          },
        ]}
      >
        <Input.TextArea placeholder="Please input description" allowClear showCount />
      </Form.Item>
      <Form.Item wrapperCol={{ span: 12, offset: 6 }}>
      <Popconfirm
        title="Are you sure you want to save changes?"
        onConfirm={onFinish}
        // onCancel={cancel}
        okText="Yes"
        cancelText="No"
    >
        <Button type="primary" htmlType="submit">
          Save
        </Button>
        </Popconfirm>
      </Form.Item>
    </Form>
          </div>
      }

      </>);

}