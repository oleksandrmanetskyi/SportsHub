import "./CreateTrainingProgram.css";
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
import nutritionsApi from '../../../api/nutritionsApi';
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

export default function CreateTrainingProgramPage () {
    const { id } = useParams<ProgramParams>();
    const history = useHistory();
    const [form] = Form.useForm();
    const [loading, setLoading] = useState(true);
    const token = AuthStore.getToken() as string;
    const [program, setProgram] = useState<TrainingProgram>(new TrainingProgram());
    const [sportKinds, setSportKinds] = useState<Array<SportKind>>();
    const [nutritions, setNutritions] = useState<Array<Nutrition>>();
    const [fileName, setFileName] = useState<string>("default.png");
    const [imageBase64, setImageBase64] = useState<string>("");
    const [defaultImageBase64, setDefaultImageBase64] = useState<string>("");

    const onFinish = (values: any) => {
        //   setLoading(true);

        debugger
        program.name = form.getFieldValue("name");
        program.description = form.getFieldValue("description");
        program.imagePath = fileName;
        
         let sportId = form.getFieldValue("sportKind");
          if(typeof sportId == 'string')
          {
            program.sportKindId = sportKinds?.find(x => x.name = sportId)?.id ?? 0;
          }
          else 
          {
            program.sportKindId = sportId;
          }
        let nId = form.getFieldValue("nutrition");
        if(typeof nId == 'string')
        {
          program.nutritionId = nutritions?.find(x => x.name = nId)?.id ?? null;
        }
        else 
        {
          program.nutritionId = nId;
        }
        //   user.sportKindId = sportId;
        //   user.name = values.name;
        //   user.surName = values.surName;
        //   user.email = values.email;
        //   user.location = values.location;
        debugger
        uploadImage(fileName, imageBase64);
        if(+id)
        {
            program.id = +id;
            editTrainingProgram(id, JSON.stringify(program)).then(() => {
                notificationLogic("success", "Training program is edited succesfully");
                history.push(`/training-program/get/${id}`);
              })
              .catch(() => {
                notificationLogic("error", "Failed to edite training program");
              });;
        }else
        {
        createTrainingProgram(JSON.stringify(program)).then(() => {
            notificationLogic("success", "Training program is created succesfully");
            history.push(`/training-program`);
          })
          .catch(() => {
            notificationLogic("error", "Failed to create training program");
          });;
        }
        
      };

    const fetchData = async () => {
        await sportKindsApi.getAllSportKinds().then(r => setSportKinds(r));
        await nutritionsApi.getAll().then(r => setNutritions(r));
        await downloadImage("").then(response => {
            setDefaultImageBase64(response.data);
          });
        setLoading(false);
      };

      const fetchEditData = async () => {
        await sportKindsApi.getAllSportKinds().then(r => setSportKinds(r));
        await nutritionsApi.getAll().then(r => setNutritions(r));
        debugger
        // let editId: string = id;
        await getTrainingProgram(id).then(async r => {
            setProgram(r.data);
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
        {id ? <h2 className="profileHeader">Edit {program.name} training program</h2> : <h2 className="profileHeader">Create new training program</h2>}
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
        initialValue={program.name}
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
        name="sportKind"
        label="Kind of sport"
        hasFeedback
        initialValue={sportKinds?.find(x => x.id == program.sportKindId)?.name}
        rules={[{ required: true, message: 'Please select kind of sport for training program' }]}
      >
        <Select placeholder="Select kind of sport">
            {sportKinds?.map(x => {
                return <Option key={x.id} value={x.id}>{x.name}</Option>
            })}
        </Select>
      </Form.Item>
      <Form.Item
        name="nutrition"
        label="Nutrition"
        hasFeedback
        initialValue={nutritions?.find(x => x.id == program.nutritionId)?.name}
        // rules={[{ required: true, message: 'Please select nutrition for training program' }]}
      >
        <Select placeholder="Select nutrition">
            {nutritions?.map(x => {
                return <Option key={x.id} value={x.id}>{x.name}</Option>
            })}
        </Select>
      </Form.Item>
      <Form.Item
        {...formItemLayout}
        name="description"
        label="Description"
        initialValue={program.description}
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