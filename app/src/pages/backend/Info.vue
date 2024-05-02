<template>
    <div id="myInfo">
        <div class="container">
            <h1>{{ title }}</h1>
            <div class="info_body">
                <div class="tips" v-if="Infos.length == 0">暂无数据</div>
                <ul class="items">
                    <li class="item" v-for="item in Infos[page - 1]" :key="item.id">
                        <a target="_blank" :href="item.href">{{ item.title }}</a>
                        <span>{{ item.time }}</span>
                    </li>
                </ul>
                <el-row type="flex">
                    <el-pagination background layout="prev, pager, next" @current-change="handlePageChange"
                        :page-size="size" :total="size * Infos.length">
                    </el-pagination>
                </el-row>
            </div>
        </div>
    </div>
</template>

<script>
import { Infos } from '@/assets/JS/static.js'
export default {
    data() {
        return {
            Infos,
            page: 1,
            size: 12,
            count: 0,
            title: '信息公告'
        }
    },
    methods: {
        // 进行分页
        handlePageChange(e) {
            this.page = e
        }
    }

}
</script>

<style scoped lang='less'>
#myInfo {
    position: relative;
    width: 100%;
    background: url("@/assets/info-bg.png") no-repeat;
    height: 100%;
    min-width: 1000px;

    .container {
        position: absolute;
        top: 20px;
        left: 50%;
        transform: translate(-50%);
        padding: 15px;
        width: 80%;

        h1 {
            position: relative;
            height: 40px;
            line-height: 40px;
            padding-left: 30px;

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

        .info_body {
            position: relative;
            margin-top: 20px;
            height: 525px;
            width: 100%;

            .items {
                max-height: 85%;
                width: 100%;

                .item {
                    cursor: pointer;
                    position: relative;
                    display: flex;
                    padding: 10px 30px;
                    font-size: 16px;

                    &::before {
                        content: "";
                        display: inline-block;
                        width: 4px;
                        height: 4px;
                        background: #cc0000;
                        position: absolute;
                        left: 0;
                        top: 50%;
                        margin-top: -2px;
                        border-radius: 50%;
                    }

                    a {
                        width: 80%;
                        color: #313030;

                        &:hover {
                            color: #f2c9bb;
                        }
                    }

                    span {
                        width: 20%;
                        color: #666565
                    }
                }
            }

            .el-row {
                position: absolute;
                bottom: 20px;
                left: 50%;
                transform: translate(-50%);
            }

            .tips {
                position: absolute;
                font-size: 20px;
                color: #313030;
                top: 30%;
                left: 50%;
                transform: translate(-50%, -50%);
            }
        }


    }
}
</style>