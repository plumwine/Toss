using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Toss.Device;

namespace Toss.Actor.Effects
{
    class ParticleManager
    {
        //パーティクルのリスト
        private List<Particle> particles = new List<Particle>();

        ///<summary>
        ///コントラスタ
        /// </summary>
        public ParticleManager()
        { }

        ///<summary>
        ///初期化
        /// </summary>
        public void Intialize()
        {
            particles.Clear();//リストクリア
        }

        ///<summary]>
        ///更新
       /// </summary>
       /// <param name="gameTime"></param>
       public void Update(GameTime gameTime)
        {
            //一括更新
            particles.ForEach(particles => particles.Update(gameTime));
            
        }
    }
}
