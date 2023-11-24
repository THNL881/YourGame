using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using YourGame.LevelLogic;
using YourEngine;
using YourGame.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace YourGame.States
{
    internal class PlayingState : State
    {
        //in PlayingState collision between player and tiles are handled

        Level level;
        Player player;
        public PlayingState() : base()
        {
            
            level = new Level();
            //player = new Player();
            this.AddChild(this.level);
           // this.AddChild(this.player);

            
        }

        protected override void EnterSelf()
        {
           
        }

        protected override void UpdateSelf(GameTime gameTime)
        {
            base.UpdateSelf(gameTime);
            if (YourGame.InputManager.CheckIsKeyJustPressed(Keys.Space))
                this.NextState = new DemoPlayerState();
        }

    }
}
