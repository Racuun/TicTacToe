using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TicTacToe
{
    public class Sprite
    {
        public bool isActive = false;
        public bool WinningString = false;
        public char x;
        public virtual void LoadContent()
        {

        }

        public virtual void Update(GameTime gameTime, ContentManager contentManager, int round)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
