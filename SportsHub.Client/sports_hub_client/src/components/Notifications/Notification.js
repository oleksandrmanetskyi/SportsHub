import {notification} from 'antd';
 
const openNotificationWithIcon = (type, text, icon = null) => {
  (notification[type])({
    message: 'Notification',
    icon: icon,
    description: text
  });
};

  export default openNotificationWithIcon;