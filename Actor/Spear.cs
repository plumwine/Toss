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
    //斜方投射を使ったやりクラス
    class Spear : GameObject
    {
        private float acceleration;       //加速値
        //private float score;              //スコア
        private bool isThrow;             //投げるかどうか
        private float angleDig;           //先端方向
        private  Vector2 Force;           //外力（N）
        private  Vector2 Acc;             //加速度（m/ss）
        private  Vector2 Vel;             //速度（m/s）
        private float gravity;            //重力
        private  float Mass;              //質量（kg）
        private Player player;            //プレイヤーの情報を取得


        public Spear( Vector2 position, GameDevice gameDevice,Player player)
            : base("Arrow", position,64,32, gameDevice)
        {
            this.position = position;
            this.player = player;
        }
        public Spear(Spear other)
            : this(other.position, other.gameDevice, other.player)
        { }
        public override object Clone()
        {
            return new Spear(this);
        }

        //初期化
        public void Initialize()
        {
            isThrow = false;
            acceleration = 0.0f;
            gravity = 9.8f;
            Acc = Vector2.Zero;
            Vel = Vector2.Zero;
            Mass = 1.0f;
            angleDig = 0.0f;
            Force = new Vector2(0, gravity * Mass);
        }

       

        public override void Hit(GameObject gameObject)
        {
            if(gameObject is Block)
            {
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.IsKeyDown(Keys.Space))
            {
                SetAccele(45);
                isThrow = true;
            }
            if (!isThrow) return;
            ThrowingSpear(gameTime);
        }

        public override void Draw(Renderer renderer)
        {
            //槍を投げていない状態ならプレイヤーについていく
            if (isThrow)
                renderer.DrawTexture(name, position, MathHelper.ToRadians(angleDig), new Vector2(32, 16));
            else
            {
                renderer.DrawTexture(name, player.GetPosition() + gameDevice.GetDisplayModify());
            }
                


        }

        //加速値と角度を入れる
        public void SetAccele(float angle)
        {
            //プレイヤーの加速値を入れる
            acceleration = 50.0f;//テスト,player.GetBarrageSpeed();
            angle *= -(float)Math.PI / 180; 　　//ラジアン変換
            Vel = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) *acceleration;
        }

        //投げられているとき
        public void ThrowingSpear(GameTime gameTime)
        {
            // 並走運動（外力を加速度に変える）
            Acc = Force / Mass;  //加速度（m/ss）
            Vel += Acc *  (float)gameTime.ElapsedGameTime.TotalSeconds*10;     //速度（m/s）
            position += Vel *  (float)gameTime.ElapsedGameTime.TotalSeconds*10;     //変位（m）
            angleDig = Vel.Y;       //先端方向
            //外力を重力加速度のみにして、次の繰り返しへ
            Force = new Vector2(0, gravity * Mass);
        }
        
        public void SetIsThrow()
        {
            isThrow = true;
        }
    }
}
