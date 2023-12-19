import Api from "./api";

const getAll = async ()=>{
    const {data} = await Api.get(`Shops/all`);
    return data;
}
// export type Shops= {
//     id : number;
//     name: string;

//   }
// export const getBySportKind = async (id: number) => {
//     const response = await Api.get(`Shops/GetBySportKind?sportKindId=${id}`);
//     return response;
// }

export const getShop= async (id: number) => {
    const response = await Api.get(`Shops/Get?ShopId=${id}`);
    return response;
}
export const createShop= async (data: string) => {
    const response = await Api.post(`Shops/new`, data);
    return response;
}
export const editShop= async (data: string) => {
    const response = await Api.post(`Shops/edit/`, data);
    return response;
}

export const deleteShop= async (id:number) => {
    const response = await Api.remove(`Shops/Delete?ShopId=${id}`);
    return response;
}
export default {
    getAll,
    getShop,
};