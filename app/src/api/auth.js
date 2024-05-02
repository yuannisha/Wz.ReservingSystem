// import { ObjectToFormData } from '@/utils/createFormData'
import requests from './ajax'
// 登录注册接口

const URL = {
    user_login: 'CustomUser/Login',
    user_register: 'CustomUser/Register',
    reset_password: 'CustomUser/ResetPassword',
    get_reservingRecordings:'ReservingOperation/GetRecordingsByIDNumber',
    cancle_reservingRecording:'ReservingOperation/CancleReserving',
    get_previewInformation:'ReservingOperation/GetPreviewInformation',
    get_timesByDate:'ReservingOperation/GetTimesByDate',
    reserving:'ReservingOperation/Reserving',
    delete_reserving:'ReservingOperation/DeleteReserving'
}

// 用户登录
export const login = (IDnumber,Password) => {
    return requests.post(URL.user_login,IDnumber,Password)
}

// 用户注册
export const register = (Name,IDnumber,Class,Password) => {
    return requests.post(URL.user_register, Name,IDnumber,Class,Password)
}

// // 管理员登录
// export const adminLogin = (data) => {
//     return requests.post(URL.admin_login, ObjectToFormData(data))
// }

// 用户重置密码
export const resetPassword = (IDnumber,NewPassword) => {
    return requests.post(URL.reset_password, IDnumber,NewPassword)
}

// 用户预约琴房
export const reserving = (Name,IDnumber,BuildingAndFloor,Classroom,ReservingTime) => {
    return requests.post(URL.reserving, Name,IDnumber,BuildingAndFloor,Classroom,ReservingTime)
}

// 用户获取时间段
export const getTimesByDate = (Date) => {
    return requests.post(URL.get_timesByDate, Date)
}

// 用户获取预览信息
export const getPreviewInformation = () => {
    return requests.post(URL.get_previewInformation)
}

// 用户获取记录
export const getRecordingsByIDNumber = (IDNumber) => {
    return requests.post(URL.get_reservingRecordings, IDNumber)
}

// 用户取消预约
export const cancleReserving = (Id,IDNumber) => {
    return requests.post(URL.cancle_reservingRecording, Id, IDNumber)
}


// 用户删除预约记录
export const deleteReserving = (Id,IDNumber) => {
    return requests.post(URL.delete_reserving, Id, IDNumber)
}