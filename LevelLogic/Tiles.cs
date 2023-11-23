using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourEngine;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using System.Numerics;

namespace YourGame.LevelLogic
{
    internal class Tiles : GameObject
    {
        public enum Type
        {
            Empty,
            Wall,
            Platform
        }

        Sprite image;
        Type type;
        Microsoft.Xna.Framework.Rectangle hitBox;

       
        Texture2D texture;

        public Tiles(Type type)
        {
            this.type = type;
            Microsoft.Xna.Framework.Rectangle sourceRectangle = new Microsoft.Xna.Framework.Rectangle(location: new Microsoft.Xna.Framework.Point(6, 14), size: new Microsoft.Xna.Framework.Point(21, 44));

            if (type == Type.Empty)
            {
                image = null;
                
            } else if (type == Type.Platform)
            {
                texture = YourGame.AssetManager.LoadTexture("spr_wall_speed");
                image = new Sprite(texture);
                {
                    image.OriginType = OriginType.TopLeft;
                    image.GlobalPosition = new Microsoft.Xna.Framework.Vector2(YourGame.ScreenSize.Y, 100);
                }
            } else if (type == Type.Wall)
            {
                texture = YourGame.AssetManager.LoadTexture("spr_wall_speed");
                image = new Sprite(texture);
                {
                    image.OriginType = OriginType.BottomRight;
                        
                }
            }

            this.hitBox = new Microsoft.Xna.Framework.Rectangle(new Microsoft.Xna.Framework.Point(YourGame.ScreenSize.Y, 100), new Microsoft.Xna.Framework.Point(texture.Height, texture.Width));
            

            
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if(image != null)
            {
                image.Draw(spriteBatch);
            }
        }
    }
}
