<template>
    <div class="order">
        <h2>欢迎来到预约界面,以下是本周可预约的时间表</h2>
        <el-calendar :range="[`${start}`, `${end}`]" :first-day-of-week="day" v-if="start">
            <template #dateCell="{ data }">
                <div class="custom" @click="handleSelect(data)">
                    <p>{{ data.day.split('-').slice(1).join('-') }}</p>
                    <p>剩余可预约:{{ getCurrentDateSurplus(data) }}</p>
                    <p v-if="data?.surplus">{{ data.isSelected ? '✔️' : '' }}</p>
                    <div class="over" v-else>满</div>
                </div>
            </template>
        </el-calendar>
        <div class="layout">
            <div class="select-classroom" v-if="value">
                <h2>以下是本周可预约的教室</h2>
                <el-select v-model="selectValue">
                    <el-option v-for="item in rooms" :key="item" :value="item">{{ item }}</el-option>
                </el-select>
            </div>
            <div class="time-period" v-if="value && selectValue">
                <h2>以下是本周此教室可预约的时间段</h2>
                <ul>
                    <li :class="{ bid: item.surplus == 0, selected: selectIndex == index && item.surplus != 0 }"
                        v-for="(item, index) in times" :key="index" @click="handleSelectTimePeriod(item, index)">
                        <p>时间段:{{ item.time }}</p>
                        <p>剩余:{{ item.surplus }}</p>
                    </li>
                </ul>
            </div>
            <div class="confirm-info" v-if="value && selectValue && form.time">
                <h2>以下是您的预约信息</h2>
                <div class="p">
                    <span class="name">姓名：</span>
                    <p>{{ form.name }}</p>
                </div>
                <div class="p">
                    <span class="name">学号：</span>
                    <p>{{ form.account }}</p>
                </div>
                <div class="p">
                    <span class="address">预约地点：</span>
                    <p>{{ form.address }}</p>
                </div>
                <div class="p">
                    <span class="classroom">预约教室：</span>
                    <p>{{ form.classroom }}</p>
                </div>
                <div class="p">
                    <span class="time">预约时间段：</span>
                    <p>{{ form.date + ' ' + form.time + ' ' + form.week }}</p>
                </div>
                <el-button type="primary" @click="handleConfirmToBook">确定预约</el-button>
            </div>
        </div>
    </div>
</template>

<script>
import { timestampToDate, getWeekDay } from '@/utils/formatTime'
import { timePeriod, getTotalNumber, classroom } from '@/assets/JS/static'
import { getPreviewInformation , getTimesByDate , reserving} from '@/api/auth'
const DAY_TIMESTAMP = 1000 * 60 * 60 * 24//一天的时间戳 秒 分钟 小时 天
export default {
    data() {
        return {
            start: '',
            end: '',
            step: 6,// 数据从0开始 6 相当于跨度为7天
            value: "",
            selectValue: '',
            selectIndex: -1,
            day: "",
            currentIndex: -1,
            orderList: getTotalNumber(),
            timePeriod,
            classroom,
            form: {
                name: '',
                account: '',
                date: '',
                time: '',
                address: '',
                classroom: '',
                week: ''
            },
            originData:null,
            daysGroup:null,
            restSumsAndWeekDayGroup:null,
            rooms:null,
            times:null,
            timesByDateGroup:null
        }
    },
    async mounted() {
        await this.getdata(); 
        const storedObject = JSON.parse(localStorage.getItem('MyInformation'));
        this.form.name = storedObject.name;
        this.form.account = storedObject.idNumber;
        this.daysGroup = Object.keys(this.originData);
        this.restSumsAndWeekDayGroup = Object.values(this.originData);
        let timestamp = Date.now() + DAY_TIMESTAMP
        this.start = timestampToDate(timestamp)//转换日期
        this.end = timestampToDate(timestamp + DAY_TIMESTAMP * this.step)
        let time = new Date(timestamp)
        console.log(this.start)
        console.log(this.end)
        console.log(time.getDay() == 0 ? 7 : time.getDay())
        this.day = time.getDay() == 0 ? 7 : time.getDay()
        console.log(this.day+"在这里")
    },
    watch: {
        async selectValue(val) {
            if (val) {
                // let index = this.classroom.indexOf(val)
                console.log(val)
                console.log(this.timesByDateGroup)
                // this.timePeriod.forEach((v, i) => v.surplus = this.orderList[this.currentIndex].data[index][i])
                this.times = this.timesByDateGroup[val]
                console.log(this.times)
                this.form.classroom = val
                this.selectIndex = -1
                this.form.time = ''
                let tt = this.form.classroom[2];
                switch(tt){
                    case '5':
                        this.form.address = '创新楼五楼'
                        return
                    case '6':
                        this.form.address = '创新楼六楼'
                        return
                    case '7':
                        this.form.address = '创新楼七楼'
                        return
                }
            }
        },
        value(val) {
            if (this.selectValue && val) {
                let index = this.classroom.indexOf(this.selectValue)
                this.timePeriod.forEach((v, i) => v.surplus = this.orderList[this.currentIndex].data[index][i])
                this.selectIndex = -1
                this.form.time = ''
            }
        }
    },
    methods: {
        // 获取当前日期剩余数量
        getCurrentDateSurplus(data) {
            let time1 = new Date(this.start)
            let time2 = new Date(data.day)
            let day = (time2 - time1) / DAY_TIMESTAMP
            // console.log(day + "123465")
            // data.surplus = this.orderList[day].surplus
            // data.index = day
            // return data.surplus
            data.surplus = this.restSumsAndWeekDayGroup[day].restSums;
            return data.surplus
        },
        // 获取当前点击的索引
        async handleSelect(item) {
            console.log(item)
            // var dateForRequest = item.day.replace("2024-","")
            // console.log(dateForRequest)
            let formData = new FormData();
            formData.append('date', item.day);
            var res = await getTimesByDate(formData);
            console.log(res)
            this.timesByDateGroup = res;
            console.log(this.timesByDateGroup)
            this.rooms = Object.keys(res);
            console.log(this.rooms)

            this.form.date = item.day
            this.value = item.day
            this.form.week = getWeekDay(item.day)
            this.currentIndex = item.index
        },
        handleSelectTimePeriod(item, index) {
            if (item.surplus == 0) {
                this.$message.warning('此时间段已被其他同学预约啦!')
                return;
            }
            this.form.time = item.time
            this.selectIndex = index
        },
        // 确定预约
        async handleConfirmToBook() {
            if (!confirm(`确认预约`)) return;

            let formData = new FormData();
            formData.append('Name', this.form.name);
            formData.append('IDNumber', this.form.account);
            formData.append('BuildingAndFloor', this.form.address);
            formData.append('Classroom', this.form.classroom);
            formData.append('ReservingTime', this.form.date + ' ' + this.form.time + ' ' + this.form.week);
            var res = await reserving(formData)
            if(res.successfullyOrNot)
        {
          this.$message({
                      message: res.tips,
                      type: "success"
                    })
                    setTimeout(() => {
                      this.$router.push('/back');
                    }, 1500)
        }else{
          this.$message({
                      message: res.tips,
                      type: "warning"
                    })
        }
        },
        //获取数据
        async getdata(){
            var res = await getPreviewInformation();
            this.originData = res;
            console.log(this.originData)
        }
    }
}
</script>

<style scoped lang='less'>
.stopSelect {
    cursor: not-allowed;
}

.bid {
    background: #848484;
    cursor: not-allowed !important;
}

.selected {
    background-color: #3b6f9e !important;
    color: #fff;
    font-weight: 600;
}

.layout {
    display: flex;

    .select-classroom {
        margin-right: 40px;
    }
}

.order {
    min-width: 900px;

    h2 {
        padding: 20px;
    }
}

.time-period {
    width: 400px;
    margin-right: 40px;

    ul {
        width: 100%;
        display: flex;
        flex-wrap: wrap;

        li {
            cursor: pointer;
            padding: 20px 10px;
            margin: 10px;
            width: 40%;
            text-align: center;
            border: 1px solid #ddd;
            box-sizing: border-box;
        }
    }
}

.confirm-info {
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
            width: 230px;
            color: #8a8a8a;
            border-bottom: 1px solid #000;
        }

        input {
            border-bottom: 1px solid;
            font-size: 14px;
            width: 200px;
        }
    }
}



.custom {
    position: relative;
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;

    .over {
        position: absolute;
        left: 50%;
        transform: translate(-50%);
        width: 50px;
        height: 50px;
        border: 3px solid rgb(165, 55, 55);
        font-weight: 600;
        font-size: 22px;
        border-radius: 50%;
        display: flex;
        align-items: center;
        justify-content: center;
        color: rgb(165, 55, 55);
    }
}
</style>