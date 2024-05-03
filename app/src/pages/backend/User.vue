<template>
  <div class="userInfo">
    <div class="userform">
      <h2>个人信息</h2>
      <div class="user">
        <div class="avatar">
          <input type="file" @change="handleChangeImg" accept="image/*">
          <img :src="userInfo.avatar || baseImg">
        </div>
        <div class="p">
          <span class="account">学号：</span>
          <input type="text" disabled :value="userInfo.account" />
        </div>
        <div class="p">
          <span class="phone">姓名：</span>
          <input type="text" disabled :value="userInfo.name" />
        </div>
        <div class="p">
          <span class="phone">班级：</span>
          <input type="text" disabled :value="userInfo.classNumber" />
        </div>
      </div>
      <div class="buttons">
        <button @click="updateUser">修改个人信息</button>
        <button @click="exitUser">退出登录</button>
      </div>
    </div>
    <div class="book-form">
      <h2 style="text-align: left;">最近一次预约信息</h2>
      <div class="book">
        <div class="p">
          <span class="name">姓名：</span>
          <p>{{ bookInfo.name || '暂无信息' }}</p>
        </div>
        <!-- <div class="p">
          <span class="phone">联系方式：</span>
          <p>{{ bookInfo.phone || '暂无信息' }}</p>
        </div> -->
        <div class="p">
          <span class="address">预约地点：</span>
          <p>{{ bookInfo.address || '暂无信息' }}</p>
        </div>
        <div class="p">
          <span class="classroom">预约教室：</span>
          <p>{{ bookInfo.classroom || '暂无信息' }}</p>
        </div>
        <div class="p">
          <span class="time">预约时间段：</span>
          <p>{{ bookInfo.time || '暂无信息' }}</p>
        </div>
        <div class="p">
          <span class="stauts">预约状态：</span>
          <p>{{ bookInfo.status || '暂无信息' }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import requests from "@/api/ajax";
import { getImage } from "@/api/uploadImg.js";
export default {
  data() {
    return {
      userInfo: {
        name: '',
        classNumber: '',
        account: ''
      },
      bookInfo: {
        name: '',
        address: '',
        classroom: '',
        time: '',
        status: ''
      },
      baseImg: 'https://ts1.cn.mm.bing.net/th/id/R-C.40d473e3c46836d841a218c5ab489916?rik=aCPOnQd2iMcp%2bA&riu=http%3a%2f%2fimg.crcz.com%2fallimg%2f201910%2f26%2f1572070977116973.jpg&ehk=YJz19Yijg4yUsNpIB68ygCcT58WAoUpV7A3f7q9JDhc%3d&risl=&pid=ImgRaw&r=0&sres=1&sresct=1',
    }
  },
  mounted() {
    const storedObject = JSON.parse(localStorage.getItem('MyInformation'));
    console.log(storedObject)
    this.userInfo.name = storedObject.name;
    this.userInfo.classNumber = storedObject.class;
    this.userInfo.account = storedObject.idNumber;
    this.bookInfo.name = storedObject.name;
    this.bookInfo.address = storedObject.buildingAndFloor;
    this.bookInfo.classroom = storedObject.classroom;
    this.bookInfo.time = storedObject.reservingTime;
    if(storedObject.reservingTime != null)
      this.bookInfo.status = storedObject.reservingStatus === 0 ? "已预约" : "已取消";
    else if(storedObject.reservingStatus == "暂无信息")
    this.bookInfo.status = storedObject.reservingStatus
  },
  methods: {
    // 退出登录
    exitUser() {
      if (confirm("确定退出登录?")) {
        // removeToken();
        // 删除所有的项
        localStorage.clear();
        this.$message({
          type: "success",
          duration: 1000,
          message: "退出成功"
        })
        this.$router.replace("/login");
      }
    },
    // 修改个人信息
    updateUser() {
      this.$router.push("/back/reset")
    },
    //修改图片
    handleChangeImg(e) {
      var file = e.target.files[0]
      if (file.size / 1024 / 1024 >= 3) {
        this.$message({
          message: "图片过大，请更换图片",
          type: "warning",
          duration: 2000
        })
        return;
      }
      if (file != undefined) {
        let loading = this.$loading({
          lock: true,//lock的修改符--默认是false
          text: '上传中',//显示在加载图标下方的加载文案
          spinner: 'el-icon-loading',//自定义加载图标类名
          background: 'rgba(0, 0, 0, 0.7)',//遮罩层颜色
        })
        getImage(file).then(res => {
          this.userInfo.avatar = res;
          this.$forceUpdate()
          if (res) this.updateAvatar(res);
        }).finally(e => {
          loading.close();
        })
      }
    },
    // 更新用户头像
    updateAvatar(avatar) {
      /*       requests.patch('/update/avatar', { avatar, account: this.form.account }).then(res => {
              console.log(res)
              if (res.status == 0) setToken(res.token);
            }) */
    },
  },
};
</script>

<style scoped lang="less">
button {
  cursor: pointer;
}

.userInfo {
  background: url(https://tse3-mm.cn.bing.net/th/id/OIP-C.vGgzy70AFXAhCnmDMBnyZwHaNL?rs=1&pid=ImgDetMain) no-repeat;
  background-size: 40% 100%;
  background-position: right 280px;
  margin: auto;
  height: 100%;
  width: 900px;

  .userform {
    float: left;
    width: 50%;
    height: 100%;

    .user {
      width: 100%;
      height: 350px;
      display: flex;
      justify-content: center;
      flex-direction: column;

      .avatar {
        position: relative;
        margin-left: 140px;

        input {
          position: absolute;
          z-index: 999;
          opacity: 0;
          left: 0;
          top: 0;
          height: 100%;
          width: 100%;
          cursor: pointer;
        }

        img {
          height: 80px;
          width: 80px;
          border-radius: 50%;
        }
      }

      .p {
        height: 30px;
        display: flex;
        margin: 10px 0;

        span {
          color: rgb(158, 124, 91);
          font-size: 16px;
          text-align: left;
          width: 6em;
        }

        input {
          padding: 0 20px;
          height: 100%;
          width: 200px;
          font-size: 14px;
          white-space: nowrap;
          overflow: auto;
        }
      }
    }

  }

  h2 {
    width: 100%;
    height: 50px;
    line-height: 50px;
    font-size: 18px;
  }

  .buttons {
    display: flex;

    button {
      background-color: rgb(158, 124, 91);
      color: #fff;
      font-size: 16px;
      border-radius: 10px;
      padding: 10px 20px;
      margin-left: 30px;
      border: none;
    }
  }

  .book-form {
    box-sizing: border-box;
    padding-left: 50px;
    float: left;
    width: 50%;
    height: 100%;

    .book {
      margin-top: 80px;
      margin-bottom: 27px;
      width: 100%;
      display: flex;
      flex-direction: column;
      align-items: flex-start;

      .p {
        margin: 15px 0;
        display: flex;
        font-size: 18px;

        span {
          color: rgb(158, 124, 91);
          font-size: 16px;
          text-align: left;
          width: 6em;
        }

        p {
          font-size: 14px;
          width: 200px;
          border-bottom: 1px solid #000;
        }
      }
    }

    .register {
      margin-top: -100px;
      width: 100%;
      height: 100%;
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;

      p {
        margin-bottom: 20px;
        font-size: 16px;
      }

      button {
        border: none;
        border-radius: 5px;
        padding: 10px 30px;
        background-color: green;
        color: #fff;
        font-weight: 600;
      }
    }
  }
}
</style>