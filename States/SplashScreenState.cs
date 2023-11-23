using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YourEngine;

namespace YourGame.States
{
    /// <summary>
    /// An example of how you could make a splash screen.
    /// </summary>
    public sealed class SplashScreenState : State
    {
        private readonly SoundEffectInstance sfx;
        private readonly Timer timer;
        private readonly Sprite quanLogo;
        private Phase phase = Phase.FadeIn;

        public SplashScreenState() : base()
        {
            this.sfx = YourGame.AssetManager.LoadSoundEffect("Nintendo GBA Startup").CreateInstance();
            this.sfx.Play();

            this.timer = new Timer(timeLimitInSeconds: 1)
            {
                RestartsOnFinish = false
            };

            int randomIndex = YourGame.Random.Next(3);
            this.quanLogo = new Sprite(YourGame.AssetManager.LoadTexture("quanlogo"))
            {
                GlobalPosition = YourGame.ScreenSize.ToVector2() / 2,
                SourceRectangle = new Rectangle(
                    location: new Point(160 * randomIndex, 0),
                    size: new Point(160, 224)
                    ),
                OriginType = OriginType.Center
            };

            if (randomIndex == 2)
                this.DrawClearColor = Color.Black;

            this.AddChild(quanLogo);
        }

        private enum Phase
        {
            FadeIn,
            Hold,
            FadeOut
        }

        protected override void EnterSelf()
        {
            this.timer.Finished += this.OnTimerFinished;
        }

        protected override void UpdateSelf(GameTime gameTime)
        {
            this.timer.Update(gameTime.Delta());

            switch (this.phase)
            {
                case Phase.FadeIn:
                    this.quanLogo.Transparency = this.timer.TimeLeftToTimeLimitRatio;
                    break;
                default:
                case Phase.Hold:
                    break;
                case Phase.FadeOut:
                    this.quanLogo.Transparency = this.timer.TimeElapsedToTimeLimitRatio;
                    break;
            }

            if (YourGame.InputManager.CheckIsKeyJustPressed(Keys.Space))
                this.Skip();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            //
        }

        protected override void ExitSelf()
        {
            this.timer.Finished -= this.OnTimerFinished;
            this.sfx.Stop();
        }

        private void OnTimerFinished()
        {
            switch (this.phase)
            {
                case Phase.FadeIn:
                    // An enum is a number under the hood, so we can do this ++ trick!
                    ++this.phase;
                    this.timer.TimeLimitInSeconds = 1;
                    this.timer.Restart();
                    break;
                case Phase.Hold:
                    ++this.phase;
                    this.timer.TimeLimitInSeconds = 1.75f;
                    this.timer.Restart();
                    break;
                default:
                case Phase.FadeOut:
                    this.Skip();
                    break;
            }
        }

        private void Skip()
        {
            this.NextState = new PlayingState();
        }
    }
}
