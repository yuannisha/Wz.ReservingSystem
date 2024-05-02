<template>
  <div class="header">
    <span class="text">欢迎您！</span>
    <el-dropdown :hide-on-click="false">
      <span class="el-dropdown-link">
        <img :src="img" class="user" />
      </span>
      <el-dropdown-menu slot="dropdown">
        <el-dropdown-item @click.native="exitAccount">退出登录</el-dropdown-item>
      </el-dropdown-menu>
    </el-dropdown>
  </div>
</template>

<script>
export default {
  data() {
    return {
      img: "https://tse1-mm.cn.bing.net/th/id/OIP-C.4qK_V1EUC_O2U0i_0eQCoAHaKY?w=202&h=283&c=7&r=0&o=5&dpr=1.3&pid=1.7"
    };
  },
  methods: {
    exitAccount() {
      if (confirm("确定退出")) {
        this.$router.replace("/login");
        this.$message({
          type: "warning",
          message: "退出成功",
          duration: 1200,
        });
      }
    },
  },
};
</script>

<style scoped>
.header {
  min-width: 960px;
  padding: 0 20px;
  height: 60px;
  background-color: #303133;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.text {
  color: #fff;
  font-size: 14px;
  line-height: 60px;
}

.user {
  width: 40px;
  height: 40px;
  border-radius: 50%;
}
</style>