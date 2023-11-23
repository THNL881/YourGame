using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using YourEngine;
using YourGame.Objects;

namespace YourGame.States
{
    /// <summary>
    /// A demo of a custom GameObject class (Player.cs). Can you spot the player?
    /// </summary>
    public sealed class DemoPlayerState : State
    {
        private const int MaxWorldDimension = 1000000;
        private readonly Sprite backgroundSprite;
        private readonly Player player;
        private readonly List<Sprite> treeSprites;
        private readonly Timer timer;
        private readonly Song ambience;

        public DemoPlayerState() : base()
        {
            ambience = YourGame.AssetManager.LoadSong("SM64Birds");

            this.DrawClearColor = Color.CornflowerBlue;

            this.backgroundSprite = new Sprite(texture: YourGame.AssetManager.LoadTexture("TX Tileset Grass", "Pixel Art Top Down - Basic/"))
            {
                BaseDrawLayer = 0
            };
            this.player = new Player()
            {
                GlobalPosition = YourGame.ScreenSize.ToVector2() / 2,
            };

            this.AddChild(this.backgroundSprite);
            this.AddChild(this.player);

            Texture2D treeTexture = YourGame.AssetManager.LoadTexture("TX Plant", "Pixel Art Top Down - Basic/");
            const int treeAmount = 16;
            this.treeSprites = new List<Sprite>();

            for (int i = 0; i < treeAmount; ++i)
            {
                Sprite treeSprite = new Sprite(treeTexture)
                {
                    SourceRectangle = new Rectangle(location: new Point(288, 16), size: new Point(96, 135)),
                    OriginType = OriginType.BottomCenter,
                    GlobalPosition = new Vector2(
                        x: YourGame.Random.Next(64, YourGame.ScreenSize.X - 64),
                        y: YourGame.Random.Next(64, YourGame.ScreenSize.Y - 64)
                        ),
                };
                this.treeSprites.Add(treeSprite);
                this.AddChild(treeSprite);
            }

            this.timer = new Timer(timeLimitInSeconds: 5);
        }

        protected override void EnterSelf()
        {
            MediaPlayer.Play(ambience);
            MediaPlayer.IsRepeating = true;
            this.timer.Finished += this.OnTimerFinished;
        }

        protected override void UpdateSelf(GameTime gameTime)
        {
            this.timer.Update(gameTime.Delta());

            this.player.Sprite.BaseDrawLayer = this.CalculateYSort(this.player.Sprite.GlobalPosition.Y);

            foreach (Sprite treeSprite in this.treeSprites)
                treeSprite.BaseDrawLayer = this.CalculateYSort(treeSprite.GlobalPosition.Y);

            if (YourGame.InputManager.CheckIsKeyJustPressed(Keys.Space))
                this.NextState = new SplashScreenState();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            //
        }

        protected override void ExitSelf()
        {
            MediaPlayer.Stop();
            this.timer.Finished -= this.OnTimerFinished;
        }

        private float CalculateYSort(float yGlobalPosition)
        {
            // A demo of how Y-sort could work.
            // If you want Y-sort to work with the game tree automatically, then you need
            // to think of how you can make GameObject instances "inherit" their DrawLayer somehow
            // to deal with nesting.
            return yGlobalPosition * (1f / MaxWorldDimension);
        }


        private void OnTimerFinished()
        {
            foreach (Sprite treeSprite in this.treeSprites)
                treeSprite.GlobalPosition = new Vector2(
                    x: YourGame.Random.Next(64, YourGame.ScreenSize.X - 64),
                    y: YourGame.Random.Next(64, YourGame.ScreenSize.Y - 64)
                    );
        }
    }
}
