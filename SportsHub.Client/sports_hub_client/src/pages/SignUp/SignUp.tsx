import React, { useEffect, useState } from 'react';
import { Form, Input, Button, Select } from 'antd';
import styles from './SignUp.module.css';
import { checkEmail, checkNameSurName, checkPassword } from './verification';
import AuthorizeApi from '../../api/authorizeApi';
import { useHistory } from 'react-router-dom';
import { emptyInput, minLength } from "../../components/Notifications/Messages"
import AuthStore from '../../stores/AuthStore';
import sportKindApi, { SportKind } from '../../api/sportKindApi';

let authService = new AuthorizeApi();

export default function () {
  const [form] = Form.useForm();
  const history = useHistory();
  const [available, setAvailabe] = useState(true);
  const [isLoaded, setLoaded] = useState(false);
  const [sportKinds, setSportKinds] = useState<Array<SportKind>>(Array<SportKind>());

  const validationSchema = {
    Location: [
      { required: true, message: emptyInput() },
    ],
    SportKindId: [
      { required: true, message: emptyInput() },
    ],
    Email: [
      { required: true, message: emptyInput() },
      { validator: checkEmail }
    ],
    Password: [
      { required: true, message: emptyInput() },
      { validator: checkPassword }
    ],
    Name: [
      { required: true, message: emptyInput() },
      { validator: checkNameSurName }
    ],
    SurName: [
      { required: true, message: emptyInput() },
      { validator: checkNameSurName }
    ],
    ConfirmPassword: [
      { required: true, message: emptyInput() },
      { min: 8, message: minLength(8) },
    ],
  };

  const handleSubmit = async (values: any) => {
    setAvailabe(false);
    console.log(values);
    const send = {ConfirmPassword: values.ConfirmPassword,
    Email: values.Email,
    Location: values.Location,
    Name: values.Name,
    Password: values.Password,
    SportKindId:JSON.parse(values.SportKindId).id,
    SurName: values.SurName}
    console.log(send);
    debugger;
    await authService.register(send);
    const token = AuthStore.getToken() as string;
    history.push(`/`);
    window.location.reload();
    setAvailabe(true);
  };

  const initialValues = {
    Email: '',
    Name: '',
    SurName: '',
    Password: '',
    ConfirmPassword: '',
    SportKindId:'',
    Location:''
  };
  const fetchData = async () => {
    setLoaded(false);
    const res: Array<any> = await sportKindApi.getAllSportKinds();
    const res2:Array<SportKind> = res.map(x=>{
      return {id: x.id,name : x.name};
    })
    setSportKinds(res2);
    setLoaded(true);
  };
  useEffect(() => {
    fetchData();
  }, []);
  return (<>
    { isLoaded==true ?
    <div className={styles.mainContainerSignUp}>
      <h1 className={styles.pageTitle}>Sign Up</h1>
      <Form
        name="SignUpForm"
        initialValues={initialValues}
        form={form}
        onFinish={handleSubmit}
      >
        <Form.Item name="Email" rules={validationSchema.Email}>
          <Input className={styles.MyInput} placeholder="Email" />
        </Form.Item>
        <Form.Item name="Password" rules={validationSchema.Password}>
          <Input.Password visibilityToggle={true} className={styles.MyInput} placeholder="Password" />
        </Form.Item>
        <Form.Item
          name="ConfirmPassword"
          dependencies={['Password']}
          rules={[
            {
              required: true,
              message: emptyInput(),
            },
            ({ getFieldValue }) => ({
              validator(rule, value) {
                if (!value || getFieldValue('Password') === value) {
                  return Promise.resolve();
                }
                return Promise.reject(new Error('Password are not equal'));
              },
            }),
          ]}
        >
          <Input.Password visibilityToggle={true} className={styles.MyInput} placeholder="Confirm password" />
        </Form.Item>
        <Form.Item name="Name" rules={validationSchema.Name}>
          <Input className={styles.MyInput} placeholder="Name" />
        </Form.Item>
        <Form.Item name="SurName" rules={validationSchema.SurName}>
          <Input className={styles.MyInput} placeholder="Surname" />
        </Form.Item>
        <Form.Item name="SportKindId" rules={validationSchema.SportKindId}>
          {/* <Input className={styles.MyInput} placeholder="Sport kind" /> */}
          <Select
              placeholder="Sport kind"
              className={styles.selectField}
              
            >
              {sportKinds.map((o) => (
                <Select.Option key={o.id} value={JSON.stringify(o)}>
                  {o.name}
                </Select.Option>
              ))}
            </Select>
        </Form.Item>
        <Form.Item name="Location" rules={validationSchema.Location}>
          <Input className={styles.MyInput} placeholder="Location" />
        </Form.Item>
        <Form.Item>
          <Button htmlType="submit" id={styles.confirmButton} disabled={!available} loading={!available}>
            Sign Up
          </Button>
        </Form.Item>
      </Form>
    </div>:null}</>
  );
}