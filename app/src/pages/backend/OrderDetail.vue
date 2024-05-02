<template>
    <div style="min-width: 960px">
        <h2>我的预约记录</h2>
        <el-table :data="tableData" style="width: 100%;" height="486" stripe border>
            <el-table-column label="操作" width="200" v-slot="{ row }" fixed="right">
                <template>
                    <el-button type="danger" v-if="row.reservingStatus == '已预约'" size="mini" @click="handleUpdateStatus(row)">
                        取消预约
                    </el-button>
                    <el-button type="danger" size="mini" @click="delRecord(row.id, row.index)">
                        删除
                    </el-button>
                </template>
            </el-table-column>
            <el-table-column prop="index" label="序号" width="50" show-overflow-tooltip>
            </el-table-column>
            <el-table-column prop="name" label="姓名" show-overflow-tooltip>
            </el-table-column>
            <!-- <el-table-column prop="classNumber" label="班级" show-overflow-tooltip>
            </el-table-column> -->
            <el-table-column prop="idNumber" label="学号" show-overflow-tooltip>
            </el-table-column>
            <el-table-column prop="buildingAndFloor" label="地址" show-overflow-tooltip>
            </el-table-column>
            <el-table-column prop="classroom" label="教室" show-overflow-tooltip>
            </el-table-column>
            <el-table-column prop="reservingTime" width="200" label="预约时间" show-overflow-tooltip>
            </el-table-column>
            <el-table-column prop="reservingWeekday" width="100" label="预约天" show-overflow-tooltip>
            </el-table-column>
            <el-table-column prop="reservingStatus" label="预约状态" show-overflow-tooltip>
            </el-table-column>
        </el-table>
        <el-row type="flex" justify="center" style="margin-top: 50px">
            <el-pagination background layout="prev, pager, next" @current-change="handleChange" :page-size="pagesize"
                :total="count" hide-on-single-page>
            </el-pagination>
        </el-row>
    </div>
</template>

<script>
const reg = /^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{2,10}$/;
import { getRecordingsByIDNumber , cancleReserving , deleteReserving} from '@/api/auth'
export default {
    data() {
        return {
            tableData: [
                
            ],
            page: 1,
            pagesize: 8,
            count: 0,
            currentRow: {},
            idNumber:""
        }
    },
    async mounted() {
        // this.getData()
        await this.getData();
    },
    methods: {
        // 页面发生变化
        handleChange(e = 1) {
            this.page = e;
            this.getData()
        },
        // 选中添加或删除
        async handleUpdateStatus(rowData) {
            if (confirm(`确定取消`))
            {
                let formData = new FormData();
                formData.append('id', rowData.id);
                formData.append('iDNumber', this.idNumber);
                var res = await cancleReserving(formData)
            //     this.tableData = res.getRecordingsOutputDto;
            //     this.tableData = res.getRecordingsOutputDto;
            //     let index = 1 ;
            //     this.tableData.forEach(x=>{
            //     x.index = index++;
            //     x.reservingStatus = x.reservingStatus === 0 ? "已预约" : "已取消";
            // });
            //     rowData.reservingStatus = '已取消';
            await this.getData();
            }
                
        },
        // 删除
        async delRecord(id, rowIndex) {
            if (!confirm(`确定删除第${rowIndex}条预约记录吗`)) return
            // this.tableData.splice(index - 1, 1)
            let formData = new FormData();
            formData.append('id', id);
            formData.append('iDNumber', this.idNumber);
            var res = await deleteReserving(formData)
            // this.tableData = res.getRecordingsOutputDto;
            // this.tableData = res.getRecordingsOutputDto;
            // let index = 1 ;
            // this.tableData.forEach(x=>{
            //     x.index = index++;
            //     x.reservingStatus = x.reservingStatus === 0 ? "已预约" : "已取消";
            // });
            await this.getData();
            this.$message.success('删除成功')

            /*             delRecord(id).then(res => {
                            if (res.status == 0) {
                                this.$message.success('删除成功')
                                this.handleChange(this.page)
                            } else {
                                this.$message.warning('删除失败')
                            }
                        }) */
        },
        // 初始化页面
        async getData() {
            // let { pagesize, page } = this
            // getAllRecord(page, pagesize).then(res => {
            //     res.data.forEach((v, i) => v.index = i + 1)
            //     this.tableData = res.data
            // })
            const storedObject = JSON.parse(localStorage.getItem('MyInformation'));
            this.idNumber = storedObject.idNumber;
            console.log(storedObject.idNumber +  " 在这里");
            let formData = new FormData();
            formData.append('iDNumber', this.idNumber);
            var res = await getRecordingsByIDNumber(formData);
            this.tableData = res.getRecordingsOutputDto;
            let index = 1 ;
            this.tableData.forEach(x=>{
                x.index = index++;
                x.reservingStatus = x.reservingStatus === 0 ? "已预约" : "已取消";
            });
            console.log(this.tableData)
        }
    }
}
</script>

<style scoped lang='less'>
.el-input {
    width: 250px;
    margin-right: 10px;
}

.name,
.code {
    margin: 10px 0;

    em {
        color: red;
    }

    label {
        display: inline-block;
        width: 75px;
        color: #042c5c;
    }
}

/deep/.el-dialog__header {
    background-color: #ddd;
}
</style>