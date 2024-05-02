<template>
  <el-menu :default-active="index" class="el-menu-vertical-demo" background-color="#b7b7b7a8" text-color="#5f6135"
    active-text-color="#fff">
    <h3>琴房预约系统</h3>
    <el-menu-item index="1" @click="goNextPage('/back/home')">
      <i class="el-icon-s-home"></i>
      <span slot="title">首页</span>
    </el-menu-item>
    <el-submenu index="2">
      <template slot="title">
        <i class="el-icon-user-solid"></i>
        <span>个人中心</span>
      </template>
      <el-menu-item-group>
        <el-menu-item index="2-1" @click="$router.push(`/back/user`)">个人信息</el-menu-item>
        <el-menu-item index="2-2" @click="$router.push(`/back/reset`)">重置密码</el-menu-item>
      </el-menu-item-group>
    </el-submenu>
    <el-submenu index="3">
      <template slot="title">
        <i class="el-icon-s-cooperation"></i>
        <span>预约中心</span>
      </template>
      <el-menu-item-group>
        <el-menu-item index="3-1" @click="$router.push(`/back/order`)">前往预约</el-menu-item>
        <el-menu-item index="3-2" @click="$router.push(`/back/order/detail`)">我的预约</el-menu-item>
      </el-menu-item-group>
    </el-submenu>
    <el-menu-item index="4" @click="goNextPage('/back/info')">
      <i class="el-icon-data-line"></i>
      <span slot="title">信息公告</span>
    </el-menu-item>
  </el-menu>
</template>

<script>
import { getUserInfo } from '@/utils/userInfo'
export default {
  data() {
    return {
      index: "1",
      userInfo: {}
    }
  },
  methods: {
    goNextPage(path) {
      this.$router.push(path);
    }
  },
  mounted() {
    switch (this.$route.path) {
      case '/back/home':
        this.index = "1"
        break
      case '/back/user':
        this.index = "2-1"
        break
      case '/back/reset':
        this.index = '2-2'
        break
      case "/back/order":
        this.index = "3-1"
        break
      case "/back/order/detail":
        this.index = "3-2"
        break
      case "/back/info":
        this.index = "4"
        break
    }
    this.userInfo = getUserInfo();
  }
};
</script>

<style scoped lang="less">
.el-menu-vertical-demo:not(.el-menu--collapse) {
  width: 200px;
  min-height: 400px;
}

.el-menu {
  height: 100vh;
  background-image: url("https://ts1.cn.mm.bing.net/th/id/R-C.8504280f17892c23308099c3c4e1524c?rik=BACjjFUXqOCUbw&riu=http%3a%2f%2fa4.att.hudong.com%2f12%2f62%2f01300000207463121776624693441.jpg&ehk=mBF2AkE2vcsM7GRK782kvb7LKlzue1SrCajAGgn7ob4%3d&risl=&pid=ImgRaw&r=0");

  h3 {
    text-align: center;
    line-height: 48px;
    color: #5f6135;
  }
}
</style>