import Api from "./api";

const getAll = async ()=>{
    const {data} = await Api.get(`Nutritions/all`);
    return data;
}
// export type Nutrition = {
//     id : number;
//     name: string;

//   }
export const getByUserNutrition = async (id: string) => {
    const response = await Api.get(`Nutritions/Get/${id}`);
    return response;
}

export const getNutrition = async (id: string) => {
    const response = await Api.get(`Nutritions/${id}`);
    return response;
}
export const createNutrition= async (data: string) => {
    const response = await Api.post(`Nutritions/new`, data);
    return response;
}
export const editNutrition= async (data: string) => {
    const response = await Api.post(`Nutritions/edit/`, data);
    return response;
}

export const deleteNutrition = async (id:number) => {
    const response = await Api.remove(`Nutritions/Delete?nutritionId=${id}`);
    return response;
}
export default {
    getAll,
    getNutrition,
    getByUserNutrition
};