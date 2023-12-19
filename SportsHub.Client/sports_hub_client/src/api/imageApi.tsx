import Api from "./api";

export const downloadImage = async (name: string)=>{
    const data = await Api.post(`Images/download`, JSON.stringify(name ?? ""));
    return data;
}

export const uploadImage = async (name: string, base64: string)=>{
    const data = await Api.post(`Images/upload`, JSON.stringify({ name: name, base64: base64 }));
    return data;
}

export const deleteImage = async (name: string)=>{
    const data = await Api.remove(`Images/delete`, JSON.stringify(name));
    return data;
}