using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TicTacToe
{
    public class InputManager
    {
        private static InputManager instance;

        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new InputManager();
                return instance;
            }
        }


        public Vector2 MousePosition
        {
            get
            {
                return Mouse.GetState().Position.ToVector2();
            }
        }

        MouseState mouseState;
        MouseState prevMouseState;

        public void Update()
        {
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
        }


        public bool LeftPressed()
        {
            if (prevMouseState.LeftButton == ButtonState.Released &&
                mouseState.LeftButton == ButtonState.Pressed)
                return true;
            return false;
        }
    }
}
