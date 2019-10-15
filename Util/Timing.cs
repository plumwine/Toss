using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Toss.Actor;

namespace Toss.Util
{


    class Timing
    {
        //タイミングランク
        private TimingRank timingRank;
        private Timer timer;
        private bool timerStart;    //カウントを始める

        //コンストラクタ
        public Timing()
        {
            timer = new CountDownTimer(5.0f);  //タイミングを5秒内でやる
            timingRank = TimingRank.NULL;      //最初はNULL
            timerStart = false;
        }

        //更新
        public void Update(GameTime gameTime)
        {
            //タイマーが開始されていなかったらリターン
            if (!timerStart) return;

            timer.Update(gameTime);
            if(timer.IsTime())
            {
                timerStart = false;
            }
        }

        //タイミングをチェックする
        public TimingRank GetTiming()
        {
            float currntTime = timer.Now() / 5;
            if (currntTime >= 0.75f) timingRank = TimingRank.Fast;                               //1   ～0.75
            if (currntTime >= 0.26f && currntTime <= 0.75f) timingRank = TimingRank.Good;        //0.75～0.26
            if (currntTime >= 0.2f && currntTime <= 0.26f) timingRank = TimingRank.Excellent;    //0.26～0.2
            if (currntTime <= 0.2f) timingRank = TimingRank.Slow;                                //0.2未満
            
            return timingRank;

        }

        //タイマー開始を制御する
        public void SetStart()
        {
            timerStart = true;
        }





    }
}
