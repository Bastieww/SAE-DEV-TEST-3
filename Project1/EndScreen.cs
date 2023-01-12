using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System;

namespace Project1
{
    public class EndScreen : MonoGame.Extended.Screens.GameScreen
    {
        private Game1 _myGame;
        
        //FOND
        private Texture2D fondEndscreen;
        private Vector2 _posFondEndscreen;

        //BUTTONS
        private Rectangle[] buttons;
        private Texture2D buttonRestart;
        private Texture2D buttonRestartPressed;
        private Texture2D buttonRestartReleased;
        private Vector2 _buttonRestartPos;

        private Texture2D buttonMenu;
        private Texture2D buttonMenuPressed;
        private Texture2D buttonMenuReleased;
        private Vector2 _buttonMenuPos;
        public bool clickRestart;




        public EndScreen(Game1 game) : base(game)
        {
            _myGame = game;
        }

        

        public override void LoadContent()
        {
            base.LoadContent();
            fondEndscreen = Content.Load<Texture2D>("fondEndscreen");
            _posFondEndscreen = new Vector2(0, 0);
            buttons = new Rectangle[2];
            buttons[0] = new Rectangle(494, 754, 390, 118);
            buttons[1] = new Rectangle(999, 754, 390, 118);

            buttonRestart = Content.Load<Texture2D>("buttonrestart");
            buttonRestartPressed = Content.Load<Texture2D>("buttonrestartpressed");
            buttonRestartReleased = buttonRestart;
            _buttonRestartPos = new Vector2(494, 754);

            buttonMenu = Content.Load<Texture2D>("buttonmenuend");
            buttonMenuPressed = Content.Load<Texture2D>("buttonmenuendpressed");
            buttonMenuReleased = buttonMenu;
            _buttonMenuPos = new Vector2(999, 754);



        }
        public override void Update(GameTime gameTime)
        {
            clickRestart = false;
            MouseState mouseState = Mouse.GetState();
            Rectangle mouserect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                for(int i = 0; i < buttons.Length; i++)
                {
                    if(buttons[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                    {
                        if (i == 0)
                        {
                            clickRestart = true;
                            _myGame.Etat = Game1.Etats.GameScreen;
                        }
                            
                        else if (i == 1)
                            _myGame.Etat = Game1.Etats.StartScreen;
                    }
                }
                
            }

            if (buttons[0].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                buttonRestartReleased = buttonRestartPressed;
            else
                buttonRestartReleased = buttonRestart;

            if (buttons[1].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                buttonMenuReleased = buttonMenuPressed;
            else
                buttonMenuReleased = buttonMenu;

        }

        public override void Draw(GameTime gameTime)
        {
            _myGame._spriteBatch.Begin();
            _myGame._spriteBatch.Draw(fondEndscreen, _posFondEndscreen, Color.White);
            _myGame._spriteBatch.Draw(buttonRestartReleased, _buttonRestartPos, Color.White);
            _myGame._spriteBatch.Draw(buttonMenuReleased, _buttonMenuPos, Color.White);

            _myGame._spriteBatch.End();

        }






    }


}