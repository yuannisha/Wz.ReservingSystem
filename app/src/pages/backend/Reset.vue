<template>
    <div class="bg">
        <div class="form" ref="form">
            <div class="part_01">
                <h2>密码重置</h2>
                <p>
                    <em>*</em>
                    新密码:
                </p>
                <el-input type="text" name="password" v-model="form.password" placeholder="请输入密码" />
                <p>
                    <em>*</em>
                    密码:
                </p>
                <el-input type="text" name="password1" v-model="form.repassword" placeholder="请再次输入密码" />
            </div>

            <button class="primaryKey" :disabled="Applied" @click="confirmUpdate">确定修改</button>
        </div>
    </div>
</template>
  
<script>
import { resetPassword } from '@/api/auth'
export default {
    data() {
        return {
            form: {
                account: "",
                password: "",
                repassword: ""
            }
        };
    },
    mounted() {

    },
    methods: {
        // 确定修改
        async confirmUpdate() {
            var { password, repassword } = this.form;
            if (repassword !== password) {
                this.$message({
                    message: "两次密码不同",
                    type: 'warning'
                })
                return;
            }
            const storedObject = JSON.parse(localStorage.getItem('MyInformation'));

            let formData = new FormData();
        formData.append('IDNumber', storedObject.idNumber);
        formData.append('NewPassword', this.form.repassword);

            var res = await resetPassword(formData)

            if(res.successfullyOrNot)
        {
          this.$message({
                      message: res.tips + "即将跳转至登录页！",
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
            /*             resetPassword(this.form).then(res => {
                            if (res.status == 0) {
                                this.Applied = true
                                this.$message({
                                    message: '修改成功,即将跳转至登录页',
                                    type: 'success'
                                })
                                window.setTimeout(() => {
                                    this.$router.replace("/login")
                                    // 关闭消息提示框
                                    this.$message.close()
                                }, 3000)
                            }
                            else {
                                this.$message({
                                    message: res.message,
                                    type: 'error'
                                })
                            }
                        }) */
        }
    },
};
</script>
  
<style scoped lang="less">
.bg {
    min-width: 1100px;
    background: url('~@/assets/info-bg.png') no-repeat;
    background-size: 100% 100%;
    width: 100%;
}

.form {
    min-width: 1100px;
    margin: auto;
    width: 80%;
    margin-left: 100px;

    h2 {
        position: relative;
        color: #505458;
        padding: 40px;
        font-size: 18px;
        font-weight: 600;

        &::before {
            content: "";
            display: inline-block;
            width: 4px;
            height: 20px;
            background: #cc0000;
            position: absolute;
            left: 0;
            top: 50%;
            margin-top: -10px;
        }
    }

    p {
        font-size: 15px;
        font-weight: 600;

        em {
            color: red;
        }
    }

    .el-input {
        display: block;
        margin-bottom: 20px;
        width: 35%;
    }

    .el-select {
        display: block;
        margin-bottom: 20px;
        width: 35%;
    }


    .primaryKey {
        margin-bottom: 100px;
        width: 35%;
        height: 40px;
        margin-top: 20px;
        background-color: #409eff;
        border: 1px solid #409eff;
        border-radius: 4px;
        color: white;
        font-size: 14px;
        cursor: pointer;
    }
}
</style>