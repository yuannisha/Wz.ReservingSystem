import { Message } from "element-ui";
// 对注册的密码, 账号进行验证
export function registerCheck({ account, password, repassword, name, classNumber }) {
  // 正则表达式密码 包括字母，数字，不许有空格，8位以上,16位以下
  let regPassword = /^(?=\S*[a-z])(?=\S*\d)\S{8,16}$/;
  var regAccount = /^202003[0-9]{4}$/;
  if (!password || !account || !repassword || !name || !classNumber) {
    Message({
      message: "输入不能为空",
      type: "warning",
    });
    return false;
  } else if (!regPassword.test(password)) {
    Message({
      message: "密码必须包含字母，数字，不许有空格,8位以上",
      type: "warning",
    });
    return false;
  } else if (!regAccount.test(account)) {
    Message({
      message: "账号格式有误",
      type: "warning",
    });
    return false;
  } else if (repassword !== password) {
    Message.warning("两次密码不同请重新输入");
    return false;
  }
  return true;
}
