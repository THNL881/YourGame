using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using YourEngine;
using YourGame.States;

namespace YourGame
{
    /// <summary>
    /// Welcome to your game class. This class is merely responsible for technical stuff and updating the game tree.
    /// The game tree contains your game logic and is where you will spend most of your time instead.
    /// Carefully view the contents of this class before you dive into GameTree.cs.
    /// </summary>
    public class YourGame : Game
    {
        /// 1920 x 1080 (width x height) or aspect ratio 16:9 is very common, so let us stick to this.
        private static readonly Point MaxScreenSize = new Point(1920, 1080);
        private readonly GraphicsDeviceManager graphicsDeviceManager;
        private SpriteBatch spriteBatch;

        public YourGame() : base()
        {
            this.Window.Title = "Your Game"; // <-- This should be the first thing you want to edit ;-).
            this.graphicsDeviceManager = new GraphicsDeviceManager(game: this);
            this.graphicsDeviceManager.IsFullScreen = false;
            this.CorrectScreenSize();
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }
        /// <summary>
        /// The literal size of your game screen. I recommend dividing the MaxScreenSize.X and .Y
        /// by a common denominator. An important concept here is that your window coordinates do
        /// not necessarily have to correspond one-to-one with your game world coordinates.
        /// </summary>
        public static Point TrueScreenSize => new Point(MaxScreenSize.X / 2, MaxScreenSize.Y / 2);
        /// <summary>
        /// What size of your world fits in your game screen.
        /// If the bottom right coordinates of your screen are (960, 540) and your world size
        /// is half the screen, then those same coordinates would map to (960 / 2, 540 / 2).
        /// Do not set this value greater than TrueScreenSize.
        /// </summary>
        public static Point ScreenSize => new Point(TrueScreenSize.X / 2, TrueScreenSize.Y / 2);
        /// <summary>
        /// Useful for checking whether the mouse is inside the screen, for example.
        /// </summary>
        public static Rectangle ViewportBounds { get; private set; } = Rectangle.Empty;
        public static InputManager InputManager { get; } = new InputManager();
        public static AssetManager AssetManager { get; private set; } = null;
        /// <summary>
        /// Just a Random instance that is accessible throughout your program.
        /// </summary>
        public static Random Random { get; } = new Random();
        /// <summary>
        /// The game tree contains your game logic and is how your game is structured.
        /// If you have used Godot Engine, then this will seem very familiar.
        /// </summary>
        private GameTree GameTree { get; set; } = null;
        /// <summary>
        /// The result matrix of the ScreenSize and WorldSize values after technical methods
        /// at the bottom of this file are called to scale your sprites correctly.
        /// </summary>
        private Matrix ScaleMatrix { get; set; } = Matrix.Identity;

        public static bool CheckIsMouseInsideScreen()
        {
            return ViewportBounds.Contains(InputManager.MousePosition);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            AssetManager = new AssetManager(this.Content);

            // The code below is not placed in Initialize, because GameObject
            // instances in the tree might try to load content on initialization.
            this.GameTree = new GameTree(initialState: new SplashScreenState());
        }

        protected override void Update(GameTime gameTime)
        {
            // (1) Update "globals".
            InputManager.Update();

            // (2) Check for standard input.
            if (InputManager.CheckIsKeyPressed(Keys.Escape))
                // Close game.
                this.Exit();

            if (InputManager.CheckIsKeyJustPressed(Keys.F5))
            {
                // Toggle screen size.
                this.graphicsDeviceManager.IsFullScreen = !this.graphicsDeviceManager.IsFullScreen;
                this.CorrectScreenSize();
            }

            // (3) Update game tree.
            this.GameTree.Update(gameTime);

            // (4) Update miscellaneous.
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // (1) Clear at the start of every frame.
            GraphicsDevice.Clear(this.GameTree.BackgroundColor);

            // (2) Draw the current state.
            // These settings work well for crispy pixel art and
            // the draw layer system in GameObject.cs.
            this.spriteBatch.Begin(
                sortMode: SpriteSortMode.FrontToBack,
                blendState: BlendState.AlphaBlend,
                samplerState: SamplerState.PointClamp,
                // V We do not need these three V
                depthStencilState: null,
                rasterizerState: null,
                effect: null,
                transformMatrix: this.ScaleMatrix
            );
            this.GameTree.Draw(spriteBatch);
            this.spriteBatch.End();

            // (3) Draw miscellaneous.
            base.Draw(gameTime);
        }

        #region Technical
        /// <summary>
        /// Whenever the game is changed to full screen or not full screen, this
        /// method should be called. Basically, this code makes it so that your
        /// world coordinates are not mapped directly to screen coordinates.
        /// </summary>
        private void CorrectScreenSize()
        {
            Point fullScreenSize = new Point(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            Point screenSize = this.graphicsDeviceManager.IsFullScreen ? fullScreenSize : TrueScreenSize;
            this.graphicsDeviceManager.PreferredBackBufferWidth = screenSize.X;
            this.graphicsDeviceManager.PreferredBackBufferHeight = screenSize.Y;
            this.graphicsDeviceManager.ApplyChanges();
            this.CorrectGraphicsDeviceViewport(screenSize);
            this.CorrectSpriteScale();
        }

        private void CorrectGraphicsDeviceViewport(Point screenSize)
        {
            float worldAspectRatio = (float)ScreenSize.X / ScreenSize.Y;
            float screenAspectRatio = (float)screenSize.X / screenSize.Y;
            Viewport viewport = new Viewport();

            if (screenAspectRatio > worldAspectRatio)
            {
                viewport.Width = (int)(screenSize.Y * worldAspectRatio);
                viewport.Height = screenSize.Y;
            }
            else
            {
                viewport.Width = screenSize.X;
                viewport.Height = (int)(screenSize.X / worldAspectRatio);
            }

            viewport.X = (screenSize.X - viewport.Width) / 2;
            viewport.Y = (screenSize.Y - viewport.Height) / 2;
            ViewportBounds = viewport.Bounds;
            this.GraphicsDevice.Viewport = viewport;
        }

        private void CorrectSpriteScale()
        {
            this.ScaleMatrix = Matrix.CreateScale(
                xScale: (float)this.GraphicsDevice.Viewport.Width / ScreenSize.X,
                yScale: (float)this.GraphicsDevice.Viewport.Height / ScreenSize.Y,
                zScale: 1
                );
        }
        #endregion
    }
}