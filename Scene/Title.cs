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
    class Title 
    {
        private bool isEndFlag;

        public Title()
        {
            isEndFlag = false;
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();           
        }

        public void Initialize()
        {
            isEndFlag = false;
        }

        public Scene Next()
        {
            Scene nextScene = Scene.GamePlay;
            return nextScene;
        }
    }
    


}
