using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public class TicTac : Sprite
    {
        public Texture2D texture;
        public Vector2 Position;
        Color color = Color.Black;
        bool alpha;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, 200, 200);
            }
        }

        public TicTac(Vector2 vector2)
        {
            Position = vector2;
        }

        public override void Update(GameTime gameTime, ContentManager contentManager, int round)
        {
            if (!isActive)
            {
                if (Rectangle.Contains(InputManager.Instance.MousePosition))
                {
                    if (InputManager.Instance.LeftPressed())
                    {
                        isActive = true;
                        if (round % 2 == 1)
                        {
                            texture = contentManager.Load<Texture2D>("x");
                            x = 'x';
                        }
                        else
                        {
                            texture = contentManager.Load<Texture2D>("0");
                            x = 'o';
                        }
                    }
                }
            }
            else
            {
                if (WinningString)
                { 
                    color.A -= (byte)(255/1 * gameTime.ElapsedGameTime.TotalSeconds);    
                }
                // else
                    //color.R = 0;
            }
    
            base.Update(gameTime, contentManager, round);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                spriteBatch.Draw(texture, Rectangle, color);
            }
            base.Draw(spriteBatch);


        }
    }
}
