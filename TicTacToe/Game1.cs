using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TicTacToe
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        int round = 1;
        char[,] board = new char[3, 3];
        Sprite[,] spriteBoard = {
                { new TicTac(new Vector2 (0,0)),
                new TicTac(new Vector2 (200,0)),
                new TicTac(new Vector2 (400,0)) },
                { new TicTac(new Vector2 (0,200)),
                new TicTac(new Vector2 (200,200)),
                new TicTac(new Vector2 (400,200)) },
                { new TicTac(new Vector2 (0,400)) ,
                 new TicTac(new Vector2 (200,400)),
                new TicTac(new Vector2 (400,400))} };
        bool end = false;
        bool draw = false;
        char winner;

        float alphaText = 0f;
        SpriteFont spriteFont;
        SpriteFont spriteFontSmall;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 600;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteFont = Content.Load<SpriteFont>("Arial");
            spriteFontSmall = Content.Load<SpriteFont>("ArialSmall");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            InputManager.Instance.Update();

            foreach (var sprite in spriteBoard)
            {
                if (!end)
                    sprite.Update(gameTime, Content, round);
                else if (sprite.WinningString)
                    sprite.Update(gameTime, Content, round);
            }

            int x = 0;
            if (!end)
            {


                if (round == 9)
                {
                    draw = true;
                    if (alphaText < 1f)
                        alphaText += 1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        Restart();
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            
                            if (spriteBoard[i, j].isActive)
                            {
                                board[i, j] = spriteBoard[i, j].x;
                                x++;
                            }
                        }

                    }

                    round = x;
                }
            }
            else
            {
                if (alphaText < 1f)
                alphaText += 1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    Restart();
            }
            winCheck();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            
            foreach(var sprite in spriteBoard)
            {
                sprite.Draw(_spriteBatch);
            }

            if (end)
            {
                _spriteBatch.DrawString(spriteFont, winner + " winns!", new Vector2(300 - (int)spriteFont.MeasureString(winner + " winns!").X / 2, 300 - (int)spriteFont.MeasureString(winner + " winns!").Y / 2), Color.White * alphaText);
                _spriteBatch.DrawString(spriteFontSmall, "Press SPACE to play agiain", new Vector2(300 - (int)spriteFontSmall.MeasureString("Press SPACE to play agiain").X / 2, 350 - (int)spriteFontSmall.MeasureString("Press SPACE to play agiain").Y / 2), Color.White * alphaText);
                
            }
            else if (draw)
            {
                _spriteBatch.DrawString(spriteFont, "DRAW!", new Vector2(300 - (int)spriteFont.MeasureString("DRAW!").X / 2, 300 - (int)spriteFont.MeasureString("DRAW!").Y / 2), Color.White * alphaText);
                _spriteBatch.DrawString(spriteFontSmall, "Press SPACE to play agiain", new Vector2(300 - (int)spriteFontSmall.MeasureString("Press SPACE to play agiain").X / 2, 350 - (int)spriteFontSmall.MeasureString("Press SPACE to play agiain").Y / 2), Color.White * alphaText);

            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }


        void winCheck()
        {
            for (int i=0;  i<3; i++)
            {
                bool win = true;
                char a = board[i, 0];
                for (int j=1; j<3; j++)
                {
                    if ((board[i, j] != 'x' && board[i, j] != 'o') || board[i, j] != a)
                    {
                        win = false;
                        break;
                    }
                }
                if (win)
                {
                    for (int j = 0; j < 3; j++)
                        spriteBoard[i, j].WinningString = true;
                    end = true;
                    winner = a;
                    return;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                bool win = true;
                char a = board[0, i];
                for (int j = 1; j < 3; j++)
                {
                    if ((board[j, i] != 'x' && board[j, i] != 'o') || board[j, i] != a)
                    {
                        win = false;
                        break;
                    }
                }
                if (win)
                {
                    for (int j = 0; j < 3; j++)
                        spriteBoard[j, i].WinningString = true;
                    end = true;
                    winner = a;
                    return;
                }
            }

            if ((board[1, 1] == 'x' || board[1, 1] == 'o'))
            {
                char d = board[1, 1];
                if ((board[0, 0] == d && board[2, 2] == d))
                {
                    spriteBoard[0, 0].WinningString = true;
                    spriteBoard[1, 1].WinningString = true;
                    spriteBoard[2, 2].WinningString = true;
                    winner = d;
                    end = true;
                    return;
                }
                else if ((board[0, 2] == d && board[2, 0] == d))
                {
                    spriteBoard[0, 2].WinningString = true;
                    spriteBoard[1, 1].WinningString = true;
                    spriteBoard[2, 0].WinningString = true;
                    winner = d;
                    end = true;
                    return;
                }
            }
        }

        void Restart()
        {
            foreach(var sprite in spriteBoard)
            {
                sprite.isActive = false;
                sprite.WinningString = false;
            }
            for (int i=0; i<3; i++)
            {
                for (int j=0; j<3; j++)
                {
                    board[i, j] = '.';
                }
            }
            end = false;
            draw = false;
            round = 1;
            alphaText = 0f;
        }
    }

    
}
