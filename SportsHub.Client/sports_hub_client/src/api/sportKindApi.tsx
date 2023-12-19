import Api from "./api";

const getAllSportKinds = async ()=>{
    const {data} = await Api.get(`SportKind/all`);
    return data;
}
export type SportKind = {
    id : number;
    name: string;
  }

export default {
    getAllSportKinds
};