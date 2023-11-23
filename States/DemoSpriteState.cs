using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using YourEngine;
using Microsoft.Xna.Framework.Input;

namespace YourGame.States
{
    /// <summary>
    /// A demo of Sprite.cs and miscellaneous. Intentionally no comments here - no handholding for now.
    /// Observe, question, mess around, and learn! Mainly to understand how objects are positioned.
    /// </summary>
    public sealed class DemoSpriteState : State
    {
        private readonly GameObject spritesParent;
        private readonly Sprite spriteTopLeft;
        private readonly Sprite spriteTopCenter;
        private readonly Sprite spriteTopRight;
        private readonly Sprite spriteCenterLeft;
        private readonly Sprite spriteCenter;
        private readonly Sprite spriteCenterRight;
        private readonly Sprite spriteBottomLeft;
        private readonly Sprite spriteBottomCenter;
        private readonly Sprite spriteBottomRight;
        private readonly Timer timer;
        private float uniformScale = 1;

        public DemoSpriteState() : base()
        {
            this.DrawClearColor = Color.Beige;
            Texture2D texture = YourGame.AssetManager.LoadTexture("TX Player", "Pixel Art Top Down - Basic/");
            Rectangle sourceRectangle = new Rectangle(location: new Point(6, 14), size: new Point(21, 44));

            this.spritesParent = new GameObject()
            {
                /* Uncomment the line of code below once you think you understand what is going on.
                   Once uncommented, can you explain what happens to the positioning of objects? */
                //GlobalPosition = YourGame.WorldSize.ToVector2() / 2
            };

            this.spriteTopLeft = new Sprite(texture)
            {
                OriginType = OriginType.TopLeft,
                GlobalPosition = Vector2.Zero,
                SourceRectangle = sourceRectangle
            };
            this.spriteTopCenter = new Sprite(texture)
            {
                OriginType = OriginType.TopCenter,
                GlobalPosition = Vector2.UnitX * YourGame.ScreenSize.X / 2,
                SourceRectangle = sourceRectangle
            };
            this.spriteTopRight = new Sprite(texture)
            {
                OriginType = OriginType.TopRight,
                GlobalPosition = Vector2.UnitX * YourGame.ScreenSize.X,
                SourceRectangle = sourceRectangle
            };

            this.spriteCenterLeft = new Sprite(texture)
            {
                OriginType = OriginType.CenterLeft,
                GlobalPosition = Vector2.UnitY * YourGame.ScreenSize.Y / 2,
                SourceRectangle = sourceRectangle,
            };
            this.spriteCenter = new Sprite(texture)
            {
                OriginType = OriginType.Center,
                GlobalPosition = YourGame.ScreenSize.ToVector2() / 2,
                SourceRectangle = sourceRectangle
            };
            this.spriteCenterRight = new Sprite(texture)
            {
                OriginType = OriginType.CenterRight,
                GlobalPosition = new Vector2(YourGame.ScreenSize.X, YourGame.ScreenSize.Y / 2),
                SourceRectangle = sourceRectangle
            };

            this.spriteBottomLeft = new Sprite(texture)
            {
                OriginType = OriginType.BottomLeft,
                GlobalPosition = Vector2.UnitY * YourGame.ScreenSize.Y,
                SourceRectangle = sourceRectangle
            };
            this.spriteBottomCenter = new Sprite(texture)
            {
                OriginType = OriginType.BottomCenter,
                GlobalPosition = new Vector2(YourGame.ScreenSize.X / 2, YourGame.ScreenSize.Y),
                SourceRectangle = sourceRectangle
            };
            this.spriteBottomRight = new Sprite(texture)
            {
                OriginType = OriginType.BottomRight,
                GlobalPosition = YourGame.ScreenSize.ToVector2(),
                SourceRectangle = sourceRectangle
            };

            this.spritesParent.AddChild(
                this.spriteTopLeft,
                this.spriteTopCenter,
                this.spriteTopRight,
                this.spriteCenterLeft,
                this.spriteCenter,
                this.spriteCenterRight,
                this.spriteBottomLeft,
                this.spriteBottomCenter,
                this.spriteBottomRight
                );
            this.AddChild(this.spritesParent);

            this.timer = new Timer(timeLimitInSeconds: 1);
        }

        protected override void EnterSelf()
        {
            this.timer.Finished += this.OnTimerFinished;
            this.spriteBottomLeft.IsVisibleChanged += this.OnSpriteBottomLeftVisibilityChanged;
        }

        protected override void UpdateSelf(GameTime gameTime)
        {
            // Uncomment the line of code below to not be overstimulated by the chaos!
            // Actually, uncommenting/early returning might be helpful to understand the chaos...
            // return;
            this.timer.Update(gameTime.Delta());

            this.uniformScale = YourMath.Sine(
                elapsedTime: gameTime.TotalGameTimeInSeconds(),
                amplitude: 6,
                period: 5
                ).Absolute();
            Vector2 scale = new Vector2(this.uniformScale);
            this.spriteTopLeft.Scale = scale;
            this.spriteCenter.Scale = scale;
            this.spriteBottomRight.Scale = scale;

            Vector2 globalPositionOffset = Vector2.UnitX * YourMath.Square(
                elapsedTime: gameTime.TotalGameTimeInSeconds(),
                amplitude: 64,
                period: 5
                );
            this.spriteCenter.GlobalPosition = (YourGame.ScreenSize.ToVector2() / 2) + globalPositionOffset;

            float angleOffset = 45 * gameTime.Delta();
            this.spriteTopCenter.AngleDegrees += angleOffset;
            this.spriteBottomCenter.AngleDegrees += angleOffset;

            if (YourGame.InputManager.CheckIsKeyJustPressed(Keys.Space))
                this.NextState = new DemoPlayerState();
        }

        protected override void ExitSelf()
        {
            this.timer.Finished -= this.OnTimerFinished;
            this.spriteBottomLeft.IsVisibleChanged -= this.OnSpriteBottomLeftVisibilityChanged;
        }

        private void OnTimerFinished()
        {
            int colorChannel = YourGame.Random.Next(0, 256);
            this.spriteCenter.Color = new Color(colorChannel, colorChannel, colorChannel);
            this.spriteBottomLeft.IsVisible = !this.spriteBottomLeft.IsVisible;
        }

        private void OnSpriteBottomLeftVisibilityChanged()
        {
            if (this.spriteBottomLeft.IsVisible)
                this.spritesParent.AddChild(this.spriteTopRight);
            else
                this.spritesParent.RemoveChild(this.spriteTopRight);
        }
    }
}
