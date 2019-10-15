using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Toss.Device;

namespace Toss.Actor
{
    enum TimingRank
    {
        NULL,
        Fast,
        Slow,
        Good,
        Excellent
    }

    class Player : GameObject
    {
        private float barrageSpeed;  //連打によるスピード値
        private TimingRank tRank;    //タイミング評価

        private Vector2 velocity;

        private bool isGameSet;       //ゲームが終了しているか(種目が終わったか)
        

        public Player(Vector2 position,GameDevice gameDevice)
            :base("player_kari",position,64,64,gameDevice)
        {
            this.position = position;
        }

        public Player(Player other)
            : this(other.position, other.gameDevice)
        { }
        //初期化
        public void Initialize()
        {
            barrageSpeed = 0;          //最初は止まっているので0
            tRank = TimingRank.NULL;   //最初は評価無し
            isGameSet = false;
            
        }

        public override object Clone()
        {
            return new Player(this);   
        }

        public override void Hit(GameObject gameObject)
        {

        }

        public override void Update(GameTime gameTime)
        {
            PlayerMove(gameTime);
            Barrage();
        }

        private void PlayerMove(GameTime gameTime)
        {
            if (isGameSet) return;
            
            velocity.X = barrageSpeed;

            position += velocity;
        }

        /// <summary>
        /// プレイヤーの連打
        /// </summary>
        public void Barrage()
        {
            //スペースが押されたら barrageSpeedを加算
            if (Input.IsKeyDown(Keys.Space))
            {
                barrageSpeed++;
            }
        }
        //タイミングのランクを返す
        public TimingRank GetRank()
        {
            return tRank;
        }
        //プレイヤーのスピードを与える
        public float GetBarrageSpeed()
        {
            return barrageSpeed;
        }
        //プレイヤーの仕事が終わっているか
        public bool GetGameSet()
        {
            return isGameSet;
        }
        //プレイヤー仕事が終わっていることを伝える
        public void SetGameSet()
        {
            isGameSet = false;
        }
    }
}
