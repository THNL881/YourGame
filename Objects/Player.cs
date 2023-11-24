using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using YourEngine;
using YourGame.LevelLogic;

namespace YourGame.Objects
{
    /// <summary>
    /// Just know that the player looks like a tree in this example.
    /// </summary>
    public sealed class Player : GameObject
    {
        public Microsoft.Xna.Framework.Rectangle hitBox;

        public float gravity = 5.0f;
  
        private Texture2D sprite = YourGame.AssetManager.LoadTexture("spr_platform_ice");

        private Point position = new Point(100, 0);

        private Point size;

        public bool goingUp = false;

        public Player() : base()
        {
            this.Velocity = 64;

            this.Sprite = new Sprite(sprite)
            {
                //SourceRectangle = new Rectangle(location: Point.Zero, size: new Point(160, 153)),
                //OriginType = OriginType.BottomCenter,
            };

            this.AddChild(this.Sprite);
            size = new Point((int)sprite.Width, (int)sprite.Height);
            this.hitBox = new Rectangle(position, size);
            //new Rectangle(position, new Point(sprite.Width, sprite.Height));
            this.Sprite.GlobalPosition = new Vector2(100, 0);
        }

        public Sprite Sprite { get; }

        protected override void EnterSelf()
        {
            Debug.WriteLine("what u doing, entering yourself");
        }

        protected override void UpdateSelf(GameTime gameTime)
        {
            // Update direction (standard 8-way movement code).
            Vector2 direction = Vector2.Zero;
            Point pos = new Point((int)this.Sprite.GlobalDrawPosition.X, (int)this.Sprite.GlobalDrawPosition.Y);

            //if (YourGame.InputManager.CheckIsKeyPressed(Keys.W))
            //    direction -= Vector2.UnitY;

            if (YourGame.InputManager.CheckIsKeyPressed(Keys.A))
                direction -= Vector2.UnitX;

            //if (YourGame.InputManager.CheckIsKeyPressed(Keys.S))
            //    direction += Vector2.UnitY;

            if (YourGame.InputManager.CheckIsKeyPressed(Keys.D))
                direction += Vector2.UnitX;
           if (!goingUp)
            {
                direction.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            } else if (goingUp)
            {
                direction.Y -= gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
               

            this.Direction = direction;

            this.hitBox = new Rectangle(pos, size);

            //Point offset = new Point(0, (int)this.Sprite.GlobalPosition.Y);
            //this.hitBox.Offset(offset);
            //Debug.WriteLine("globaldrawcoord: " + this.Sprite.GlobalDrawPosition);


            //position.X = (int)this.Sprite.GlobalPosition.X;
            //position.Y = (int)this.Sprite.GlobalPosition.Y; 
            //position.X = (int)direction.X;
            //position.Y = (int)direction.Y;


            // Update angle.
            //this.Sprite.AngleDegrees = YourMath.Sine(
            //    elapsedTime: gameTime.TotalGameTimeInSeconds(),
            //    amplitude: 11.25f,
            //    period: 3
            //    );

            // Update scale.
            //this.Sprite.Scale = new Vector2(YourMath.Sine(
            //    elapsedTime: gameTime.TotalGameTimeInSeconds(),
            //    amplitude: 1.25f,
            //    period: 3
            //    ).Clamp(1, 1.25f));



            //this.Sprite.GlobalPosition = Vector2.Clamp(value1: Vector2.Zero, max: new Vector2(YourGame.ScreenSize.X, YourGame.ScreenSize.Y), min: Vector2.Zero);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if(Sprite != null)
            {
                Sprite.Draw(spriteBatch);
            } else
            {
                Debug.WriteLine("sprite is null");
            }
        }

        protected override void ExitSelf()
        {
            //
        }
    }
}
