import Api from "./api";

const getAll = async ()=>{
    const {data} = await Api.get(`News/all`);
    return data;
}
// export type News= {
//     id : number;
//     name: string;

//   }
export const getBySportKind = async (id: number) => {
    const response = await Api.get(`News/GetBySportKind?sportKindId=${id}`);
    return response;
}

export const getNews= async (id: number) => {
    const response = await Api.get(`News/Get?newsId=${id}`);
    return response;
}
export const createNews= async (data: string) => {
    const response = await Api.post(`News/new`, data);
    return response;
}
export const editNews= async (data: string) => {
    const response = await Api.post(`News/edit/`, data);
    return response;
}

export const deleteNews= async (id:number) => {
    const response = await Api.remove(`News/Delete?newsId=${id}`);
    return response;
}
export default {
    getAll,
    getNews,
    getBySportKind
};