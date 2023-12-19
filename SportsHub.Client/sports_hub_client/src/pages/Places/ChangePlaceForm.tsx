import React, { useState } from 'react';
import { Modal, Form, Input, Radio } from 'antd';
import jwt from 'jwt-decode';
import AuthStore from '../../stores/AuthStore';
import userApi from '../../api/UserApi';
import { Button } from '@mui/material';

interface Values {
  Location: string;
}

interface CollectionCreateFormProps {
  visible: boolean;
  onCreate: (values: Values) => void;
  onCancel: () => void;
}

const CollectionCreateForm: React.FC<CollectionCreateFormProps> = ({
  visible,
  onCreate,
  onCancel,
}) => {
  const [form] = Form.useForm();
  return (
    <Modal
      visible={visible}
      title="Edit your location"
      okText="Confirm"
      cancelText="Cancel"
      onCancel={onCancel}
      onOk={() => {
        form
          .validateFields()
          .then(values => {
            form.resetFields();
            onCreate(values);
          })
          .catch(info => {
            console.log('Validate Failed:', info);
          });
      }}
    >
      <Form
        form={form}
        layout="vertical"
        name="form_in_modal"
      >
        <Form.Item
          name="location"
          label="New location"
          rules={[{ required: true, message: 'Please input your new location!' }]}
        >
          <Input />
        </Form.Item>
      </Form>
    </Modal>
  );
};

type Props = {
    fetchData: () => Promise<void>;
  };
const ChangePlaceForm = (props:Props) => {
  const [visible, setVisible] = useState(false);
  
  const onCreate = async (values: any) => {
    // console.log('Received values of form: ', values);
    const token = AuthStore.getToken() as string;
    const user: any = jwt(token);
    await userApi.getById(user.nameid).then(async response => {
        let userInfo = response.data;
        userInfo.location = values.location;
        debugger;
        await userApi.updateUser(userInfo). then(x => {
            setVisible(false);
            props.fetchData();
        });
    })

  };

  return (
    <div>
      <Button
        variant="outlined"
        color="success"
        onClick={() => {
          setVisible(true);
        }}
      >
        Change location
      </Button>
      <CollectionCreateForm
        visible={visible}
        onCreate={onCreate}
        onCancel={() => {
          setVisible(false);
        }}
      />
    </div>
  );
};

export default ChangePlaceForm;