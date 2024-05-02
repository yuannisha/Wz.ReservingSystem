<template>
  <div>
    <div class="bgboard">
      <div class="homebody">
        <div class="swiper">
          <el-carousel trigger="click" :interval="4000" height="380px" autoplay>
            <el-carousel-item v-for="item in swipers" :key="item.id">
              <a href="#">
                <img :src="item.url" />
                <div class="title">SS
                  <p>
                    音乐似光照亮生命
                  </p>
                </div>
              </a>
            </el-carousel-item>
          </el-carousel>
        </div>
        <div class="info">
          <div class="title">
            <b class="iconfont icon-xinxi"></b>
            <h1>信息动态</h1>
            <div class="more" style="cursor: pointer;">更多...</div>
          </div>
          <div class="content">
            <ul>
              <li v-for="item in news" :key="item.id">
                <a :data-id="item.id" :href="item.href" target="_blank">{{ item.title }}</a>
                <span>{{ item.time }}</span>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { Infos } from '@/assets/JS/static.js'
export default {
  data() {
    return {
      swipers: [
        { id: 0, url: require('@/assets/swiper-1.png') },
        { id: 1, url: require('@/assets/swiper-2.png') },
      ],
      news: []
    };
  },
  methods: {
  },
  mounted() {
    this.news = [...Infos[0]]
    this.news.length > 7 ? this.news.length = 7 : ''
  }
};
</script>

<style scoped lang="less">
.bgboard {
  background: url("./images/bg.png") no-repeat;
  background-position: bottom;
  background-size: cover;
  width: 100%;
  height: 600px;

  .homebody {
    min-width: 900px;
    width: 100%;
    height: 100%;
    margin: auto;

    .swiper {
      float: left;
      width: 55%;
      height: 100%;
      display: flex;
      align-items: center;
      justify-content: center;

      .el-carousel {
        position: relative;
        width: 660px;

        img {
          width: 660px;
          height: 380px;
          background-size: cover;
        }

        .title {
          position: absolute;
          background-color: rgba(0, 0, 0, .3);
          width: 100%;
          height: 40px;
          line-height: 40px;
          padding: 0 20px;
          left: 0;
          bottom: 0;

          p {
            max-width: 45%;
            color: #fff;
            font-weight: 600;
            font-size: 16px;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
          }
        }
      }
    }

    .info {
      float: left;
      padding-left: 20px;
      width: 40%;
      height: 100%;
      display: flex;
      align-items: center;
      justify-content: center;
      flex-direction: column;

      .title {
        position: relative;
        height: 50px;
        width: 100%;
        border-bottom: 1px dashed #a0a0a0;
        display: flex;

        b {
          font-size: 40px;
          color: green;
        }

        h1 {
          margin-left: 10px;
          font-size: 26px;
        }

        .more {
          position: absolute;
          right: 20px;
          top: 50%;
          transform: translateY(-50%);
          padding: 10px 20px;
          border-radius: 30px;
          background-color: red;
          color: #fff;
        }
      }

      .content {
        width: 100%;
        height: 330px;
        margin: 0 20px;

        ul {
          height: 100%;
          width: 100%;
          display: flex;
          flex-direction: column;

          li {
            cursor: pointer;
            display: flex;
            align-items: center;
            justify-content: space-between;
            position: relative;
            text-align: center;
            width: 100%;
            height: calc(100% / 7);

            &::before {
              content: "";
              width: 4px;
              height: 4px;
              background-color: rgb(158, 124, 91);
              position: absolute;
              left: 0;
              top: 50%;
              margin-top: -2px;
            }

            a {
              font-size: 16px;
              max-width: 63%;
              overflow: hidden;
              white-space: nowrap;
              text-overflow: ellipsis;
              padding: 0 20px;
            }

            span {
              font-size: 16px;
              color: #676666;
            }
          }
        }
      }
    }
  }
}

.homefoot {
  width: 100%;
  height: 350px;
  min-width: 960px;

  .title {
    margin: auto;
    padding: 20px 0;
    height: 20%;
    width: 80%;
    text-align: center;
  }

  .content {
    cursor: pointer;
    margin: auto;
    height: 80%;
    width: 85%;

    .type {
      float: left;
      display: flex;
      align-items: center;
      justify-content: center;
      flex-direction: column;
      width: 33.3%;
      height: 100%;
      border-left: 1px dashed #ccc;

      &:first-child {
        border: none;
      }

      .imgs {
        margin-bottom: 20px;

        img {
          height: 90px;
          width: 110px;
        }
      }

      .detail {
        display: flex;
        align-items: center;
        flex-direction: column;

        em {
          font-size: 18px;
          margin-bottom: 10px;
        }

        span {
          max-width: 80%;
          font-size: 12px;
          text-align: center;
        }
      }
    }
  }


}
</style>