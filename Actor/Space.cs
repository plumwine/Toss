﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Toss.Device;

namespace Toss.Actor
{
    class Space : GameObject
    {
        public Space(Vector2 position,GameDevice gameDevice)
            : base("", position, 64, 64,gameDevice)
        {
        }
        public Space(Space other)
            : this(other.position, other.gameDevice)
        { }

        public override object Clone()
        {
            return new Space(this);
        }

        public override void Hit(GameObject gameObject)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(Renderer renderer)
        {
            //スペースなので表示なし
        }
    }
}
