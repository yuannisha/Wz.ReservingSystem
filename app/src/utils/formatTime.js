export default (timeStamp) => {
    var time = new Date(timeStamp);

    var year = time.getFullYear();
    var month = time.getMonth() + 1;
    var date = time.getDate();
    var hours = time.getHours();
    var minutes = time.getMinutes();
    var sec = time.getSeconds();
    return (
        year + '-' +
        month.toString().padStart(2, "0") +
        "-" +
        date.toString().padStart(2, "0") + " " +
        hours + ":" + minutes + ':' + sec
    );
};

export const timestampToDate = (timeStamp = Date.now()) => {
    let time = new Date(timeStamp)
    let year = time.getFullYear()
    let month = time.getMonth() + 1
    let date = time.getDate()

    return year + '-' + month.toString().padStart(2, "0") +
        "-" + date.toString().padStart(2, "0")
}

export const getWeekDay = (str) => {
    let time = new Date(str)
    let num = time.getDay()
    switch (num) {
        case 1:
            return '星期一'
        case 2:
            return '星期二'
        case 3:
            return '星期三'
        case 4:
            return '星期四'
        case 5:
            return '星期五'
        case 6:
            return '星期六'
        case 0:
            return '星期日'
    }
}