using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Toss.Device;
using Toss.Util;

namespace Toss.Actor
{
    //ボタンタイミングの評価
    enum TimingRank
    {
        NULL,
        Fast,
        Slow,
        Good,
        Excellent
    }
    //プレイヤーのAnimetion
    enum PlayerAnim
    {
        Idle,
        Run,
        Throw
    }


    class Player : GameObject
    {
        private float barrageSpeed;  //連打によるスピード値
        private TimingRank tRank;    //タイミング評価
        private PlayerAnim pAnim;    //プレイヤーのAnimetion
        private Vector2 velocity;    //移動量
        private bool isGameSet;      //ゲームが終了しているか(種目が終わったか)
        private Timing timing;       //タイミングクラス

        private bool isStop;         //止まるか
        private bool isThrow;        //投げるか


        public Player(Vector2 position,GameDevice gameDevice)
            :base("player_kari",position,64,64,gameDevice)
        {
            this.position = position;
        }

        public Player(Player other)
            : this(other.position, other.gameDevice)
        { }


        public override object Clone()
        {
            return new Player(this);
        }

        //初期化
        public void Initialize()
        {
            barrageSpeed = 0;          //最初は止まっているので0
            tRank = TimingRank.NULL;   //最初は評価無し
            isGameSet = false;　       //最初はゲームが終了していない状態
            pAnim = PlayerAnim.Run;    //最初はIdle状態
            timing = new Timing();
            isStop = false;
            isThrow = false;
            
        }
        public override void Hit(GameObject gameObject)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            PlayerMove(gameTime);
            
        }

        public override void Draw(Renderer renderer)
        {
            //プレイヤーのAnimetion管理
            switch (pAnim)
            {
                case PlayerAnim.Idle:
                    renderer.DrawTexture(name, position + gameDevice.GetDisplayModify());
                    break;
                case PlayerAnim.Run:
                    renderer.DrawTexture(name, position + gameDevice.GetDisplayModify());
                    break;
                case PlayerAnim.Throw:
                    renderer.DrawTexture(name, position + gameDevice.GetDisplayModify());
                    break;
                default:
                    break;
            }

        }
        //プレイヤーの行動管理
        private void PlayerMove(GameTime gameTime)
        {
            //ゲームがスタートしていない状態なら何もしない
            if (isGameSet) return;

            //プレイヤーのAnimetion管理
            switch (pAnim)
            {
                case PlayerAnim.Idle:
                    IdleAnim();
                    break;
                case PlayerAnim.Run:
                    Barrage();
                    PlayerStop(gameTime);
                    break;
                case PlayerAnim.Throw:
                    ThrowAnim();
                    break;
                default:
                    break;
            }
            
        }

        /// <summary>
        /// プレイヤーの連打
        /// </summary>
        public void Barrage()
        {
            if (isStop) return;
            //スペースが押されたら barrageSpeedを加算
            if (Input.IsKeyDown(Keys.Space))
            {
                barrageSpeed++;
            }
            velocity.X = barrageSpeed;
            position += velocity;
        }

        //プレイヤーの移動をとめてアニメーションを遷移
        public void PlayerStop(GameTime gameTime)
        {
            if (isThrow) return;
            isStop = true;
            timing.SetStart();
            timing.Update(gameTime);
            if(Input.IsKeyDown(Keys.Space))
            {
                tRank = timing.GetTiming();
                //移動量を0にする
                barrageSpeed = 0;
                pAnim = PlayerAnim.Throw;  //アニメーションを投げに遷移
                isThrow = true;
            }
            

        }
        //アイドル状態
        public void IdleAnim()
        {
            //スペースが押されて、移動を開始したら、アニメーションをRunに変える
            if(Input.IsKeyDown(Keys.Space))
            {
                pAnim = PlayerAnim.Run;
            }
        }
        //投げ状態
        public void ThrowAnim()
        {

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
