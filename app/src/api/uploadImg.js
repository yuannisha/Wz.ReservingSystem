import axios from "axios"

//更换头像
export const getImage = (file) => {
    const forms = new FormData()
    forms.append('smfile', file)
    return new Promise((resolove, rejcet) => {
        axios.post('/uploadImage/upload', forms, {
            headers: {
                "Authorization": "NggJQ1LeWSO3WV7eh3bBAAqi0P3ZgjnP",
                'Content-Type': 'multipart/form-data'
            }
        }).then(res => {
            resolove(res.data.images || res.data.data.url)
        })
    })
}