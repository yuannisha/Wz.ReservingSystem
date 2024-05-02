<template>
  <div>
    <el-container>
      <el-aside width="200px">
        <Aside />
      </el-aside>
      <el-container>
        <el-header>
          <Header />
        </el-header>
        <el-main>
          <router-view></router-view>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script>
import Aside from "@/components/Aside";
import Header from "@/components/BackHeader";
export default {
  components: { Aside, Header },
};
</script>

<style scoped>
.el-aside {
  overflow: hidden !important;
}

.el-header {
  padding: 0 !important;
}
</style>