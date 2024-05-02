<template>
  <div class="layout">
    <div class="Img">
      <img :src="img" alt="">
    </div>
    <div class="right_part">
      <h2 class="title">欢迎来到
        <em>琴房预约注册</em>
        , 很高兴您来到这里
      </h2>
      <div class="inputbox">
        <input type="text" v-model="form.name" placeholder="请输入姓名">
      </div>
      <div class="inputbox">
        <input type="text" v-model="form.account" placeholder="请输入学号(20200032xxx)">
      </div>
      <div class="inputbox">
        <input type="text" v-model="form.classNumber" placeholder="请输入班级">
      </div>
      <div class="inputbox">
        <input type="password" v-model="form.password" placeholder="密码">
      </div>
      <div class="inputbox">
        <input type="password" v-model="form.repassword" placeholder="确认密码">
      </div>
      <button @click="finishRegister">注册</button>
      <div class="nav-footer">
        <p class="manager" @click="$router.replace('/login')">已有账号?前往登录</p>
      </div>
    </div>
  </div>
</template>

<script>
// import requests from "@/api/ajax";
import { registerCheck } from "@/utils/InfoCheck"
import { register } from '@/api/auth'
export default {
  data: function () {
    return {
      form: {
        name: "",
        account: "",
        classNumber: "",
        password: "",
        repassword: ""
      },
      img: require('@/assets/register-bg.png')
    };
  },
  methods: {
    async finishRegister() {
      let { form } = this;
      if (registerCheck(form)) {
        let formData = new FormData();
        formData.append('name', form.name);
        formData.append('iDNumber', form.account);
        formData.append('class', form.classNumber);
        formData.append('password', form.repassword);

        var res = await register(formData);
        if(res.successfullyOrNot)
        {
          this.$message({
                      message: res.tips + ",即将跳转至登录页！",
                      type: "success"
                    })
                    setTimeout(() => {
                      this.$router.push('/login');
                    }, 1500)
        }else{
          this.$message({
                      message: res.tips,
                      type: "warning"
                    })
        }

        // this.$router.push('/login')
        /*         requests.post('/userregister', formData).then(res => {
                  if (res.status == 0) {
                    this.$message({
                      message: "注册成功,即将跳转至登录页",
                      type: "success"
                    })
                    setTimeout(() => {
                      this.$router.push('/login');
                    }, 1500)
                  } else {
                    this.$message({
                      message: res.message,
                      type: "warning"
                    })
                  }
                }) */
      }
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
      margin-top: 10px;
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