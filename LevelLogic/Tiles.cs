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
            Platform,
            Spike,
            Elevator
        }

        Sprite image;
        Type type; 
        Texture2D texture;
        public Microsoft.Xna.Framework.Rectangle hitBox;
        private Microsoft.Xna.Framework.Point position;

       

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
                    image.GlobalPosition = new Microsoft.Xna.Framework.Vector2(100, YourGame.ScreenSize.Y - texture.Height);
                }
            } else if (type == Type.Wall)
            {
                texture = YourGame.AssetManager.LoadTexture("spr_wall_speed");
                image = new Sprite(texture);
                {
                    image.OriginType = OriginType.BottomRight;
                }
            } else if (type == Type.Spike)
            {
                texture = YourGame.AssetManager.LoadTexture("spr_platform_hot");
                image = new Sprite(texture);
                {
                    image.GlobalPosition = new Microsoft.Xna.Framework.Vector2(100, YourGame.ScreenSize.Y - texture.Height);
                }
            } else if (type == Type.Elevator)
            {
                texture = YourGame.AssetManager.LoadTexture("spr_wall");
                image = new Sprite(texture);
                {
                    image.GlobalPosition = new Microsoft.Xna.Framework.Vector2(100, YourGame.ScreenSize.Y - texture.Height);
                }
            }

            position = new Microsoft.Xna.Framework.Point(100, YourGame.ScreenSize.Y - texture.Height);

            this.hitBox = new Microsoft.Xna.Framework.Rectangle(position, new Microsoft.Xna.Framework.Point(texture.Width, texture.Height));
        }

        public Type TileType { get { return type; } }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if(image != null)
            {
                image.Draw(spriteBatch);
            }
            
        }

       
    }
}
