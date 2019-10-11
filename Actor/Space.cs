using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Toss.Actor
{
    class Space : GameObject
    {
        public Space(Vector2 position)
            : base("", position, 64, 64)
        {
        }

        public override void Hit(GameObject gameObject)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
