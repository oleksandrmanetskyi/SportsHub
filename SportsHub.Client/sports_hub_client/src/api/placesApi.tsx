import Api from "./api";

export const getPlaces = async (id: string)=>{
    const data = await Api.get(`Places/Get/${id}`);
    return data;
}