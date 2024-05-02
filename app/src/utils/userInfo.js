const KEY = 'userInfo';

export const getUserInfo = () => {
    let userInfo = JSON.parse(localStorage.getItem(KEY)) || {};
    return userInfo
}

export const setUserInfo = (data) => {
    localStorage.setItem(KEY, JSON.stringify(data));
}

export const removeUserInfo = () => {
    localStorage.removeItem(KEY);
}