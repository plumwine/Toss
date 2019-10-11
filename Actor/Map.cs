using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Toss.Device;
using Toss.Util;

namespace Toss.Actor
{
    class Map
    {
        private List<List<GameObject>> mapList;
        private GameDevice gameDevice;

        public Map(GameDevice gameDevice)
        {
            mapList = new List<List<GameObject>>();
            this.gameDevice = gameDevice;
        }
        private List<GameObject> addBlock(int lineCnt, string[] line)
        {
            Dictionary<string, GameObject> objectDict = new Dictionary<string, GameObject>();

            List<GameObject> workList = new List<GameObject>();
            int colCnt = 0;
            foreach (var s in line)
            {
                try
                {
                    //ディクショナリから元データ取り出し、クローン機能で複製
                    GameObject work = (GameObject)objectDict[s];
                    work.SetPosition(new Vector2(colCnt * work.GetHeight(), lineCnt * work.GetWidth()));
                    workList.Add(work);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                colCnt += 1;

            }
            return workList;
            
        }
        /// <summary>
        /// CSVReaderを使ってMapの読み込み
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="path"></param>
        public void Load(string filename, string path = "./")
        {
            CSVReader csvReader = new CSVReader();
            csvReader.Read(filename, path);

            var data = csvReader.GetData();    //List<string[]>型で取得

            //1行ごとにmapListに追加していく
            for (int lineCnt = 0; lineCnt < data.Count(); lineCnt++)
            {
                mapList.Add(addBlock(lineCnt, data[lineCnt]));
            }
        }



        /// <summary>
        /// マップリストのクリア
        /// </summary>
        public void Unoad()
        {
            mapList.Clear();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            foreach (var list in mapList)　　　　　//ListはList<GameObject>型
            {
                foreach (var obj in list)          //objはGameObject型
                {
                    //objがSpaceクラスのオブジェクトなら次へ
                    if (obj is Space)
                    {
                        continue;
                    }
                    //更新
                    obj.Update(gameTime);
                }
            }
        }
        public void Hit(GameObject gameObject)
        {
            Point work = gameObject.GetRectangle().Location;      //左上の座標くぉ取得
                                                                  //配列の何行何列目にいるかを計算
            int x = work.X / 32;
            int y = work.Y / 32;
            //移動で食い込んでいる時の修正
            if (x < 1)
            {
                x = 1;
            }
            if (y < 1)
            {
                y = 1;
            }

            Range yRange = new Range(0, mapList.Count() - 1);
            Range xRange = new Range(0, mapList[0].Count() - 1);

            for (int row = y - 1; row <= (y + 1); row++)
            {
                for (int col = x - 1; col <= (x + 1); col++)
                {
                    //配列外にないなら何もしない
                    if (xRange.IsOutOfRange(col) || yRange.IsOutOfRange(row))
                    {
                        continue;
                    }

                    //その場所のオブジェクトを取得
                    GameObject obj = mapList[row][col];
                    //objがSpaceクラスのオブジェクトなら次へ
                    if (obj is Space)
                    {
                        continue;
                    }
                    //衝突判定
                    if (obj.IsCollusion(gameObject))
                    {
                        gameObject.Hit(obj);
                    }
                }
            }
        }
        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer">描画オブジェクト</param>
        public void Draw(Renderer renderer)
        {
            foreach (var m in mapList)
            {
                foreach (var ml in m)
                {
                    ml.Draw(renderer);
                }
            }
        }


        public int GetWidth()
        {
            int col = mapList[0].Count;
            int width = col * mapList[0][0].GetWidth();
            return width;
        }
        public int GetHeight()
        {
            int row = mapList[0].Count;
            int height = row * mapList[0][0].GetHeight();
            return height;
        }
    }
}
