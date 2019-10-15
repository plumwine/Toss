using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Toss.Device;

namespace Toss.Actor
{

    //当たった方向
    enum Direction
    {
        Top, Bottom, Right, Left
    }
    abstract class GameObject :ICloneable
    {

        protected string name;
        protected Vector2 position;
        protected int width;
        protected int height;
        protected GameDevice gameDevice;　　　　　      

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public GameObject(string name, Vector2 position, int width, int height,GameDevice gameDevice)
        {
            this.name = name;
            this.position = position;
            this.width = width;
            this.height = height;
            this.gameDevice = gameDevice;
        }

        /// <summary>
        /// 位置の設定
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }
        /// <summary>
        /// 位置の取得
        /// </summary>
        /// <returns></returns>
        public Vector2 GetPosition()
        {
            return position;
        }
        /// <summary>
        /// オブジェクト幅の取得
        /// </summary>
        /// <returns></returns>
        public int GetWidth()
        {
            return width;
        }
        /// <summary>
        /// オブジェクトの高さの取得
        /// </summary>
        /// <returns></returns>
        public int GetHeight()
        {
            return height;
        }
        //抽象メソッド
        public abstract object Clone();
        public abstract void Update(GameTime gameTime);   //更新
        public abstract void Hit(GameObject gameObject);  //ヒット通知

        //矩形同士の当たり判定
        public bool IsCollusion(GameObject otherObj)
        {
            float sabunX = this.position.X - otherObj.position.X;
            if (sabunX < this.width + otherObj.width) return true;
            float sabunY = this.position.Y - otherObj.position.Y;
            if (sabunY < this.width + otherObj.width) return true;

            return false;

        }

        //仮想メソッド
        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer">描画オブジェクト</param>
        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position+ gameDevice.GetDisplayModify());
        }
        //矩形の生成
        public Rectangle GetRectangle()
        {
            //短形の生成
            Rectangle area = new Rectangle();

            //位置と幅、高さを設定
            area.X = (int)position.X;
            area.Y = (int)position.Y;
            area.Height = height;
            area.Width = width;
            return area;
        }
        //どこの辺が当たっているか
        public Direction CheckDirection(GameObject otherObj)
        {
            //中心位置の取得
            Point thisCenter = this.GetRectangle().Center; 　　   //自分の中心位置
            Point otherCenter = otherObj.GetRectangle().Center;   //相手の中心位置

            //向きのベクトルを取得
            Vector2 dir = new Vector2(thisCenter.X, thisCenter.Y) -
                          new Vector2(otherCenter.X, otherCenter.Y);

            //当たっている側面をリターンする
            //x成分とy成分でどちらの方が量が多いか
            if (Math.Abs(dir.X) > Math.Abs(dir.Y))
            {
                //xの向きが正の時
                if (dir.X > 0)
                {
                    return Direction.Right;
                }
                return Direction.Left;
            }
            //y成分が大きく、正の値か？
            if (dir.Y > 0)
            {
                return Direction.Bottom;
            }
            //プレイヤーがブロックに乗った
            return Direction.Top;
        }

        /// <summary>
        /// 当たった面からの位置補正
        /// </summary>
        /// <param name="other"></param>
        public virtual void CorrectPosition(GameObject other)
        {
            //当たった面の取得
            Direction dir = this.CheckDirection(other);
            
            switch (dir)
            {
                case Direction.Top:
                    position.Y = other.GetRectangle().Top - this.height;
                    break;
                case Direction.Bottom:
                    position.Y = other.GetRectangle().Bottom;
                    break;
                case Direction.Right:
                    position.X = other.GetRectangle().Right;
                    break;
                case Direction.Left:
                    position.X = other.GetRectangle().Left - this.width;
                    break;
                default:
                    break;
            }
        }


    }
}
