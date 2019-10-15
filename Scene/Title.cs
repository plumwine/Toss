using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Toss.Device;
using Toss.Def;

namespace Toss.Scene
{
    class Title :IScene
    {
        private bool isEndFlag;

        public Title()
        {
            isEndFlag = false;
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("player_kari", new Vector2(100, 100));
            renderer.DrawTexture("block", new Vector2(200, 200));
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            Scene nextScene = Scene.GamePlay;
            return nextScene;
        }

        public void Shutdown()
        {
        }

        public void Update(GameTime gameTime)
        {
            if(Input.IsKeyDown(Keys.Space))
            {
                isEndFlag = true;
            }
        }
    }
    


}
