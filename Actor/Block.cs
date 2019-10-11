using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Toss.Actor
{
    class Block : GameObject
    {
        public Block(Vector2 position)
            :base("",position,64,64)
        {

        }


        public override void Hit(GameObject gameObject)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
