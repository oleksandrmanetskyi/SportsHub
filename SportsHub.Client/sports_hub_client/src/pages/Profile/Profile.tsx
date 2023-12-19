import React, { useEffect, useState } from 'react';
import jwt from 'jwt-decode';
import AuthStore from '../../stores/AuthStore';
import "./Profile.css"
import userApi from '../../api/UserApi';
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
    Input,
    Popconfirm,
  } from 'antd';
  import { UploadOutlined, InboxOutlined } from '@ant-design/icons';
import { SportKind } from '../../api/sportKindApi';
import  sportKindsApi from '../../api/sportKindApi';


  const { Option } = Select;

const formItemLayout = {
  labelCol: { span: 6 },
  wrapperCol: { span: 14 },
};


export default function Profile (){
  const [loading, setLoading] = useState(true);
  const [form] = Form.useForm();
  const [user, setUser] = useState<any | undefined>();
  const [sportKinds, setSportKinds] = useState<Array<SportKind>>();

  const fetchUserInfo = async () => {
    const token = AuthStore.getToken() as string;
    const user:any = jwt(token);
    await userApi.getById(user.nameid).then(response => {
        setUser(response.data);
        setLoading(false);
      })
  }
  const fetchData = async () => {
    await sportKindsApi.getAllSportKinds().then(r => setSportKinds(r));
    await fetchUserInfo();
  };

  const onFinish = (values: any) => {
    //   setLoading(true);
    console.log(form)
    debugger
      let sportId = user.sportKindId;
      let spK = form.getFieldValue("sportKind")
      if(typeof spK == 'number')
      {
        sportId = spK;
      }
      
      user.sportKindId = sportId;
      user.name = form.getFieldValue("name");
      user.surName = form.getFieldValue("surName");
      user.email = form.getFieldValue("email");
      user.location = form.getFieldValue("location");
    userApi.updateUser(user).then(r => fetchUserInfo());
    
  };

  useEffect(() => {
    fetchData();
    // setLoading(false);
  }, []);

  return (<>{ loading==true ? null :
    <Form
      name="validate_other"
      {...formItemLayout}
      // onFinish={onFinish}
      form={form}
      initialValues={{
        sportKind: sportKinds?.find(x => x.id == user?.sportKindId)?.name,
        name: user.name,
        surName: user.surName,
        email: user.email,
        location: user.location
      }}
    >
        <h2 className="profileHeader ant-form-text">{user.name} {user.surName}</h2>
        <div>
                  <p className="instruction">
                    Instruction: All required fields are marked with an asterisk *</p>
                </div>
      <Form.Item
        name="sportKind"
        label="Your kind of sport"
        hasFeedback
        rules={[{ required: true, message: 'Please select your favourite kind of sport' }]}
      >
        <Select placeholder="Select kind of sport">
            {sportKinds?.map(x => {
                return <Option key={x.id} value={x.id}>{x.name}</Option>
            })}
        </Select>
      </Form.Item>
      <Form.Item
        {...formItemLayout}
        name="name"
        label="Name"
        rules={[
          {
            required: true,
            message: 'Please input your name',
          },
        ]}
      >
        <Input placeholder="Please input your name" />
      </Form.Item>
      <Form.Item
        {...formItemLayout}
        name="surName"
        label="Surname"
        rules={[
          {
            required: true,
            message: 'Please input your surname',
          },
        ]}
      >
        <Input placeholder="Please input your surname" />
      </Form.Item>
      <Form.Item
        {...formItemLayout}
        name="email"
        label="E-mail"
        rules={[
            {
              type: 'email',
              message: 'The input is not valid E-mail',
            },
            {
              required: true,
              message: 'Please input your E-mail',
            },
          ]}
      >
        <Input placeholder="Please input your email" />
      </Form.Item>
      <Form.Item
        {...formItemLayout}
        name="location"
        label="Location"
        rules={[
          {
            required: true,
            message: 'Please input your location',
          },
        ]}
      >
        <Input placeholder="Please input your location" />
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
    }
    </>
  );
}