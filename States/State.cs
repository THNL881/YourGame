using Microsoft.Xna.Framework;
using YourEngine;

namespace YourGame.States
{
    /// <summary>
    /// The base state. Inherit this class to create your own state ;-).
    /// This class is inspired by the state "design pattern:
    /// https://refactoring.guru/design-patterns/state
    /// The idea is to prevent if-statements and switches to become gigantic
    /// when the amount of game states grows in your game. This way, we can
    /// "encapsulate" each game state in isolated classes.
    /// Behavior that is common to every state should be added to this class.
    /// </summary>
    public abstract class State : GameObject
    {
        public State() : base()
        {
            this.NextState = this;
        }

        public Color DrawClearColor { get; protected set; } = Color.White;
        protected State NextState { get; set; }

        /// <summary>
        /// Checks whether a next state is available to switch to.
        /// </summary>
        /// <param name="nextState">The next state that should be switched to.</param>
        /// <returns>Whether switching is possible.</returns>
        public bool TrySwitch(out State nextState)
        {
            // This method works similarly to int.TryParse:
            // https://www.geeksforgeeks.org/out-parameter-with-examples-in-c-sharp/
            // You do not need an out keyword most of the time, but
            // it can be helpful when a method needs to:
            // (1) Check whether an operation is safe.
            // (2) Return an appropriate value if (1) is satisfied.
            nextState = null;

            // Do not initiate a state switch if no new value is provided.
            if (this == this.NextState)
                return false;

            nextState = this.NextState;
            return true;
        }
    }
}
