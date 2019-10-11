using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Toss.Device;

namespace Toss.Actor
{
    class GameObjectManager
    {
        private List<GameObject> gameObjectsList;   //プレイヤーグループ
        private List<GameObject> addGameObjects;     //追加するキャラクターリスト

        private Map map;  //マップ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GameObjectManager()
        {
            Initialize();    //初期化
        }
        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            //
            if (gameObjectsList != null)
            {
                gameObjectsList.Clear();   //初期化
            }
            else
            {
                gameObjectsList = new List<GameObject>();
            }
            if (addGameObjects != null)
            {
                addGameObjects.Clear();
            }
            else
            {
                addGameObjects = new List<GameObject>();
            }
        }
        /// <summary>
        /// ゲームオブジェクトの追加
        /// </summary>
        /// <param name="gameObject">追加するゲームオブジェクト</param>
        public void Add(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }
            addGameObjects.Add(gameObject);
        }
        /// <summary>
        /// マップの追加
        /// </summary>
        /// <param name="map">追加するマップ</param>
        public void Add(Map map)
        {
            if (map == null)
            {
                return;
            }
            this.map = map;
        }
        /// <summary>
        /// マップとの当たり判定
        /// </summary>
        private void hitToMap()
        {
            if (map == null)
            {
                return;
            }
            //全てのオブジェクトとマップとのヒット通知
            foreach (var obj in gameObjectsList)
            {
                map.Hit(obj);
            }
        }
        /// <summary>
        /// ゲームオブジェクトとのヒット通知
        /// </summary>
        private void hitToGameObject()
        {
            //ゲームオブジェクトリストを繰り返し
            foreach (var c1 in gameObjectsList)
            {
                //同じゲームオブジェクトリストを繰り返し
                foreach (var c2 in gameObjectsList)
                {
                   
                    //衝突判定
                    if (c1.IsCollusion(c2))
                    {
                        //ヒット通知
                        c1.Hit(c2);
                        c2.Hit(c1);
                    }
                }
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //キャラ更新
            foreach (var c in gameObjectsList)
            {
                c.Update(gameTime);
            }
            //キャラクタの追加
            foreach (var c in addGameObjects)
            {
                gameObjectsList.Add(c);
            }

            addGameObjects.Clear();
            //当たり判定
            hitToMap();
            hitToGameObject();
        }
        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            foreach (var c in gameObjectsList)
            {
                c.Draw(renderer);
            }
        }
        /// <summary>
        /// ゲームオブジェクトの追加
        /// </summary>
        /// <param name="gameObject"></param>
        public void AddGameObject(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }
            addGameObjects.Add(gameObject);
        }
        /// <summary>
        /// マップ全体のサイズの取得
        /// </summary>
        /// <returns></returns>
        public Vector2 MapSize()
        {
            return MapSize();
        }
    }
}
