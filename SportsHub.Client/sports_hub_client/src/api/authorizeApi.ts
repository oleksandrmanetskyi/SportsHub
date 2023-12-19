import Api from "./api";
import AuthStore from '../stores/AuthStore';
import openNotificationWithIcon from "../components/Notifications/Notification";

export default class AuthorizeApi {

  static isSignedIn(): boolean {
    return !!AuthStore.getToken();
  }

  login = async (data: any) => {
    const response = await Api.post("Auth/signin", data)
      .then(response => {
        if (response.data.token !== null) {
          //debugger
          AuthStore.setToken(response.data.token);
          return "";
        }
      })
      .catch(error => {
        if (error.response.status === 400) {
          console.log(error)
          //debugger
          
          openNotificationWithIcon('error', error.response.data);
        }
      })
    return response;
  };


  register = async (data: any) => {
    const response = await Api.post("Auth/signup", data)
      .then(response => {
        openNotificationWithIcon('success', response.data.value);
        console.log(response.data.token);
        if (response.data.token !== null) {
          AuthStore.setToken(response.data.token);
        }
      })
      .catch(error => {
        if (error.response.status === 400) {
          openNotificationWithIcon('error', error.response.data.value);
        }
      });
    return response;
  };

  logout = async () => {
    AuthStore.removeToken();
  };


}
