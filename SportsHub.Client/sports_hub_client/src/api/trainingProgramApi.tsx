import Api from "./api";

export const getRecommendedTrainingProgram = async (userId: string)=>{
    const data = await Api.get(`TrainingProgram/recommended/${userId}`);
    return data;
}

export const getSuggestionsTrainingProgram = async (userId: string, count: number)=>{
    const data = await Api.get(`TrainingProgram/suggestions?userId=${userId}&count=${count}`);
    return data;
}

export const getNewSuggestionsTrainingProgram = async (userId: string, count: number)=>{
    const data = await Api.get(`TrainingProgram/newsuggestions?userId=${userId}&count=${count}`);
    return data;
}

export const getAllTrainingProgram = async ()=>{
    const data = await Api.get(`TrainingProgram`);
    return data;
}

export const createTrainingProgram = async (data: string) => {
    const response = await Api.post(`TrainingProgram/new`, data);
    return response;
}

export const getTrainingProgram = async (id: string) => {
    const response = await Api.get(`TrainingProgram/Get/${id}`);
    return response;
}

export const editTrainingProgram = async (id:string, data: string) => {
    const response = await Api.post(`TrainingProgram/edit/${id}`, data);
    return response;
}

export const followTrainingProgram = async (userId:string, progId: number) => {
    const response = await Api.post(`TrainingProgram/Follow/${userId}`, progId);
    return response;
}

export const unfollowTrainingProgram = async (userId:string) => {
    const response = await Api.post(`TrainingProgram/Unfollow/${userId}`);
    return response;
}

export const deleteTrainingProgram = async (id:number) => {
    const response = await Api.remove(`TrainingProgram/Delete?trainingProgramId=${id}`);
    return response;
}

export const getRating = async (userId:string, progId: number) => {
    const response = await Api.get(`TrainingProgram/getrating?userId=${userId}&trainingProgramId=${progId}`);
    return response;
}

export const Like = async (userId:string, progId: number, rating: number) => {
    const response = await Api.get(`TrainingProgram/like?userId=${userId}&trainingProgramId=${progId}&rating=${rating}`);
    return response;
}