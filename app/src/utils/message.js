
const channel = '97.63';

const bc = new BroadcastChannel(channel);

// 发送信息
export const sendMessage = (data) => {
    bc.postMessage(data);
}

// 监听信息
export const listenMessage = (callback) => {
    bc.addEventListener('message', (e) => {
        callback && callback(e.data);
    })
}






