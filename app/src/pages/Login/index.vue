<template>
  <div class="layout">
    <div class="Img">
      <img :src="img" alt="">
    </div>
    <div class="right_part">
      <h2 class="title">欢迎来到
        <em>琴房预约</em>
        , 很高兴您来到这里
      </h2>
      <div class="inputbox">
        <input type="text" v-model="form.IDNumber" placeholder="请输入学号">
      </div>
      <div class="inputbox">
        <input type="password" v-model="form.password" placeholder="密码">
      </div>
      <button @click="handleLogin">登录</button>
      <div class="nav-footer">
        <p class="manager" @click="$router.replace('/register')">还没有账号?前往注册</p>
        <!-- <label> -->
        <!-- <input type="checkbox" v-model="form.rememberMe"> 记住我 -->
        <!-- </label> -->
      </div>
    </div>
  </div>
</template>

<script>
import requests from '@/api/ajax';
import {login} from '@/api/auth'
import { setUserInfo } from '@/utils/userInfo';
export default {
  data: function () {
    return {
      form: {
        IDNumber: "",
        password: "",
        // rememberMe: false,
      },
      img: require('@/assets/login-bg.png')
    };
  },
  methods: {
    async handleLogin() {
      var { IDNumber, password } = this.form;
      if (!IDNumber || !password) {
        this.$message({
          message: "输入不能为空",
          type: "warning"
        })
        return;
      }
      let formData = new FormData();
      formData.append('IDNumber', this.form.IDNumber);
      formData.append('password', this.form.password);
      // formData.append('rememberMe', this.form.rememberMe);
      console.log(this.form)
      console.log(formData)
      var res = await login(formData);
      console.log(res)
      if(res.successfullyOrNot)
      {
        localStorage.setItem('MyInformation', JSON.stringify(res));
        this.$message({
          message: res.loginTips,
          type: "success"
        });
        setTimeout(() => {
                      this.$router.push('/back');
                    }, 1500)
      }
      else
      {
        this.$message({
          message: res.loginTips,
          type: "warning"
        })
      }
      // 登录接口
      // requests.post('/userlogin', formData).then(res => {
      //         if (res.status === 0) {
      //           this.$message({
      //             message: res.message,
      //             duration: 1500,
      //             type: 'success'
      //           })
      //           setUserInfo({ ...res.data, role: 0 });
      //           this.$router.replace("/back/home")
      //         }
      //         else {
      //           this.$message({
      //             message: res.message,
      //             type: 'error',
      //             duration: 2000
      //           })
      //         }
      //       })
    }

  },
};
</script>

<style scoped lang="less">
.layout {
  width: 100%;
  height: calc(100vh - 20px);
  min-width: 1000px;

  .Img {
    width: 36%;
    height: 100vh;
    float: left;
    overflow: hidden;

    img {
      width: 100%;
      height: 100%;
    }
  }

  .right_part {
    padding: 10% 20% 0 10%;
    box-sizing: border-box;
    width: 64%;
    float: left;

    h2 {
      width: 100%;
      font-size: 28px;
      font-weight: 400;

      em {
        color: #005a83;
        font-weight: 600;
      }
    }

    .inputbox {
      margin-top: 40px;
      width: 100%;
      height: 60px;
      line-height: 60px;

      input {
        font-size: 16px;
        width: 100%;
        height: 45px;
        box-sizing: border-box;
        border-bottom: 1px solid #111;
        padding: 0 10px;
      }
    }

    button {
      margin: 40px 0;
      width: 100%;
      height: 50px;
      font-size: 18px;
      border-radius: 20px;
      line-height: 50px;
      text-align: center;
      background-color: #005a83;
      color: #fff;
    }

    .login_tip {
      width: 100%;
      display: flex;

      span {
        cursor: pointer;
        flex: 1;
        text-decoration: underline;
        padding: 0 10px;
        color: #333232;
        font-weight: 600;

        &:last-child {
          text-align: right;
        }
      }
    }
  }
}

.nav-footer {
  display: flex;
  justify-content: space-between;

  .manager {
    cursor: pointer;

    &:hover {
      text-decoration: underline;
    }
  }
}
</style>