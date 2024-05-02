
/**
 * 将对象转换成formdata格式
 * @param {Object} obj 
 */
export const ObjectToFormData = (obj) => {
    let formData = new FormData
    for (let key in obj) {
        formData.append(key, obj[key]);
    }
    return formData;
}