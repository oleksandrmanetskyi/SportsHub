import axios from 'axios';
import BASE_URL from '../config';
import api from './api';

const getById = async (id: string | undefined) => {
    const response = await axios.get(`${`${BASE_URL}User/`}${id}`);

    return response;
};
const updateUser = async (data: any) => {
    return api.post(`Accounts/edit/`, data).catch((error) => {
      throw new Error(error);
    });
  }
export default {
    getById,
    updateUser
};