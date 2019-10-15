using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Toss.Device;
using Toss.Util;

namespace Toss.Actor.Effects
{
    class ParticleBlue : Particle
    {
           private Timer timer;//制 限 時 間
           public ParticleBlue(string name, Vector2 position, Vector2 velocity, IParticleMediator mediator) 
            : base(name, position, velocity, mediator)
        {
            var random = GameDevice.Instance().GetRandom();
            timer = new CountDownTimer(random.Next(1, 3));
        }
        public ParticleBlue(IParticleMediator mediator) : base(mediator)
        {
            var random = GameDevice.Instance().GetRandom();
            timer = new CountDownTimer(random.Next(1, 3));
            name = "particleBlue";
        }

        public override void Update(GameTime gameTime)
        {
            //親クラスで更新
            base.Update(gameTime);

            timer.Update(gameTime);

        }

    }
}
