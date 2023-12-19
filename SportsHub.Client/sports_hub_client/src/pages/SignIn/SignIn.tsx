import React, { useState } from "react";
import { Form, Input, Button, Checkbox } from "antd";
import styles from "./SignIn.module.css";
import { checkEmail, checkPassword } from "../SignUp/verification";
import AuthorizeApi from '../../api/authorizeApi';
import { useHistory } from 'react-router-dom';
import jwt from 'jwt-decode';
import AuthStore from '../../stores/AuthStore';
import{emptyInput} from "../../components/Notifications/Messages"
import { AutoComplete } from 'antd';

const { Option } = AutoComplete;

let authService = new AuthorizeApi();
let user: any;

export default function () {
  const [form] = Form.useForm();
  const history = useHistory();

  const initialValues = {
    Email: "",
    Password: "",
    RememberMe: true,
  };

  const validationSchema = {
    Email: [
      { required: true, message: emptyInput() },
      { validator: checkEmail },
    ],
    Password: [
      { required: true, message: emptyInput() },
      { validator: checkPassword }
    ],
  };

  const handleSubmit = async (values: any) => {
    debugger
    const res = await authService.login(values);
    if(res!==undefined)
    {
      const token = AuthStore.getToken() as string;
    user = jwt(token);
    history.push(`/`);
  
    window.location.reload();
    }
    // const token = AuthStore.getToken() as string;
    // user = jwt(token);
    // history.push(`/`);
  
    // window.location.reload();
  };
  
  const [result, setResult] = useState<string[]>([]);

  const handleSearch = (value: string) => {
    let res: string[] = [];
    if (!value || value.indexOf('@') >= 0) {
      res = [];
    } else {
      res = ['gmail.com', 'lnu.edu.com', 'ukr.net'].map(domain => `${value}@${domain}`);
    }
    setResult(res);
  };

  return (<>
    <div className={styles.mainContainer}>
    <h1 className={styles.pageTitle}>Log In</h1>
      <Form
        name="SignInForm"
        initialValues={initialValues}
        form={form}
        onFinish={handleSubmit}
      >
        <Form.Item name="Email" rules={validationSchema.Email}>
          <AutoComplete className={styles.SignInInput} onSearch={handleSearch} placeholder="Email">
            {result.map((email: string) => (
              <Option key={email} value={email}>
                {email}
              </Option>
            ))}
          </AutoComplete>
          {/* <Input
            className={styles.SignInInput}
            placeholder="Email"
          /> */}
        </Form.Item>
        <Form.Item name="Password" rules={validationSchema.Password} >
          <Input.Password
            visibilityToggle={true}
            className={styles.SignInInput}
            placeholder="Password"
          />
        </Form.Item>
        <Form.Item name="remember" valuePropName="checked">
          <Checkbox className={styles.rememberMe}>Remember me?</Checkbox>
        </Form.Item>
        <Form.Item>
          <Button htmlType="submit" id={styles.confirmButton}>
            Log In
          </Button>
        </Form.Item>
      </Form>
    </div>
    </>
  );
}
