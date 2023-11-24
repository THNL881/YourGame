using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourEngine;
using YourGame.Objects;

namespace YourGame.LevelLogic
{
    public partial class Level : GameObject
    {
        Tiles[,] tiles;
        Player player;
        Tiles tile = new Tiles(Tiles.Type.Elevator);
        private bool standingOnPlatform = false;
        private bool standingOnSpike = false;
        private const float gravity = 10f;
        private int i;

        public Level()
        {
            //LoadLevel("LevelFiles/testLevel");
            //tiles = new Tiles[1, 3];
            this.player = new Player();

            this.AddChild(tile);
            //this.AddChild(new Tiles(Tiles.Type.Wall));
            this.AddChild(player);
        }

        protected override void EnterSelf()
        {
            base.EnterSelf();
           
        }

        protected override void UpdateSelf(GameTime gameTime)
        {
            

            if(CollisionDetection())
            {
                HandleCollision();
            }


            if (standingOnPlatform)
            {
                this.player.Direction = new Vector2(0, 0);
            } else
            {
                
                
            }
            //Debug.WriteLine(standingOnPlatform);
            Debug.WriteLine("hitbox player size: " + this.player.hitBox);
            Debug.WriteLine("hitbox tile size: " + this.tile.hitBox);
         
            //Debug.WriteLine("niggaaaaaaaaa" + i);
            i++;
            
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            
        }

        private bool CollisionDetection()
        {
            if (this.player.hitBox.Intersects(this.tile.hitBox))
            {
                return true;
            }
                
            return false;
        }

        private void HandleCollision()
        {
            switch (tile.TileType)
            {
                case Tiles.Type.Platform:
                    standingOnPlatform = true;
                    Debug.WriteLine("touching me nigga" + i);
                    
                    this.player.gravity = 0;
                    break;
                case Tiles.Type.Spike:
                    this.RemoveChild(tile);
                    break;
                case Tiles.Type.Elevator:
                    this.player.goingUp = true;
                   
                    break;
            }
        }


    }
}
