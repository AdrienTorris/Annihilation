using System;
using System.Collections.Generic;

namespace TundraEngine
{
    public enum GameStateTransition : byte
    {
        /// <summary>
        /// Continue as normal.
        /// </summary>
        None,
        /// <summary>
        /// Remove the active state and resume the next state on the stack or stop if there are none.
        /// </summary>
        Pop,
        /// <summary>
        /// Pause the active state and push the new state onto the stack.
        /// </summary>
        Push,
        /// <summary>
        /// Remove the current state on the stack and insert a different one.
        /// </summary>
        Switch,
        /// <summary>
        /// Stop, remove all states and shut down the game.
        /// </summary>
        Quit
    }

    public abstract class GameState
    {
        
    }
}