using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using YourEngine;
using YourGame.States;

namespace YourGame
{
    /// <summary>
    /// Updates game objects occurs recursively in "tree" order. This is the "root" of your tree.
    /// Having no root practically means having no game. A tree is a "data structure" that you will
    /// encounter in other courses as well.
    /// Anyway, this design is very similar to how Godot Engine works.
    /// Read: https://www.geeksforgeeks.org/generic-treesn-array-trees/
    /// Technically, you can add an instance of this class as a child of another GameObject, but
    /// let us stick to the basics and not get too crazy with it ;-).
    /// </summary>
    public sealed class GameTree : GameObject
    {
        public GameTree(State initialState) : base()
        {
            // Feel free to remove debug statements if you get how this works.
            this.AddChild(initialState);
            this.CurrentState = initialState;
            Debug.WriteLine($"Entering State");
        }

        /// <summary>
        /// The color used to clean at the start of every Draw call.
        /// </summary>
        public Color BackgroundColor => this.CurrentState.DrawClearColor;
        /// <summary>
        /// The current state of your game, which can represent anything you would like.
        /// Examples: splash screen, menu state, play state, dungeon, house...
        /// Remember "polymorphism"? It is applied here.
        /// </summary>
        private State CurrentState { get; set; }

        protected override void EnterSelf()
        {
            // Trigger a warning to the programmer if something unintentional happens ("defensive programming")
            // If you know what you are doing, then you can remove this ;-).
            throw new Exception("Are you sure you want to add a GameTree instance as a child of another GameObject instance?");
        }

        protected override void UpdateSelf(GameTime gameTime)
        {
            // States themselves decide if they want to switch to a next state.
            if (!this.CurrentState.TrySwitch(out State nextState))
                return;

            Debug.WriteLine("Exiting State");
            this.RemoveChild(this.CurrentState);
            this.AddChild(nextState);
            Debug.WriteLine("Entering State");
            this.CurrentState = nextState;
        }
    }
}
