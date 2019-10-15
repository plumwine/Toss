using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Toss.Actor;
using Toss.Device;
using Toss.Util;

namespace Toss.Scene
{
    class Gameplay : IScene
    {
        private bool isEndFlag;
        private Player player;
        private Map map;
        private GameObjectManager objectManager;
        private Spear spear;

        public void Initialize()
        {
            objectManager = new GameObjectManager();
            objectManager.Initialize();
            isEndFlag = false;
            
            map = new Map(GameDevice.Instance());
            map.Load("stagetest.csv");
            player = new Player(new Vector2(64, 700), GameDevice.Instance());
            player.Initialize();
            spear = new Spear(player.GetPosition(), GameDevice.Instance(), player);
            spear.Initialize();
            objectManager.Add(map);
            objectManager.Add(player);
            objectManager.Add(spear);

        }
        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            map.Draw(renderer);
            objectManager.Draw(renderer);
            
            renderer.End();
        }


        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            Scene nextScene = Scene.Ending;
            return nextScene;
        }

        public void Shutdown()
        {
        }

        public void Update(GameTime gameTime)
        {
            objectManager.Update(gameTime);
            map.Update(gameTime);
        }
    }
}
