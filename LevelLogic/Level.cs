using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourEngine;

namespace YourGame.LevelLogic
{
    public partial class Level : GameObject
    {
        Tiles[,] tiles;
        public Level()
        {
            //LoadLevel("LevelFiles/testLevel");
            tiles = new Tiles[1, 3];

            this.AddChild(new Tiles(Tiles.Type.Platform));
            this.AddChild(new Tiles(Tiles.Type.Wall));  
        }

        protected override void EnterSelf()
        {
            base.EnterSelf();
           
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            
        }


    }
}
