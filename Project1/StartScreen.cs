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
    public class StartScreen : MonoGame.Extended.Screens.GameScreen
    {
        private Game1 _myGame;
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1

        private Texture2D _textButtons;
        private Rectangle[] buttons;
        private Vector2 _posbuttons;

        private Texture2D buttonplay;
        private Texture2D buttonplaypressed;
        private Texture2D buttonplayreleased;
        private Vector2 buttonplaypos;

        private Texture2D buttoncontrols;
        private Texture2D buttoncontrolspressed;
        private Texture2D buttoncontrolsreleased;
        private Vector2 buttoncontrolspos;

        private Texture2D buttonquit;
        private Texture2D buttonquitpressed;
        private Texture2D buttonquitreleased;
        private Vector2 buttonquitpos;

        public bool clickMenu;

        private Texture2D _fond;
        private Vector2 _posfond;




        public StartScreen(Game1 game) : base(game)
        {
            _myGame = game;


        }
        public override void LoadContent()
        {

            buttonplay = Content.Load<Texture2D>("buttonplay");
            buttonplaypressed = Content.Load<Texture2D>("buttonplaypressed");
            buttonplayreleased = buttonplay;
            buttonplaypos = new Vector2(719 + buttonplay.Width / 2, 275 / 2 + buttonplay.Height + 70);

            buttoncontrols = Content.Load<Texture2D>("buttoncontrols");
            buttoncontrolspressed = Content.Load<Texture2D>("buttoncontrolspressed");
            buttoncontrolsreleased = buttoncontrols;
            buttoncontrolspos = new Vector2(719 + buttoncontrols.Width / 2, 467 + buttoncontrols.Height / 2);

            buttonquit = Content.Load<Texture2D>("buttonquit");
            buttonquitpressed = Content.Load<Texture2D>("buttonquitpressed");
            buttonquitreleased = buttonquit;
            buttonquitpos = new Vector2(719 + buttonquit.Width / 2, 670 + buttonquit.Height / 2);

            _myGame._tiledMap = Content.Load<TiledMap>("map");
            _myGame._tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _myGame._tiledMap);
            _textButtons = Content.Load<Texture2D>("buttons");

            _fond = Content.Load<Texture2D>("fond");
            _posfond = new Vector2(0, 0);


            buttons = new Rectangle[3];
            buttons[0] = new Rectangle(719 + buttonplay.Width / 2, 275 / 2 + buttonplay.Height + 70, 231, 110);
            buttons[1] = new Rectangle(719 + buttoncontrols.Width / 2, 467 + buttoncontrols.Height / 2, 320, 135);
            buttons[2] = new Rectangle(719 + buttonquit.Width / 2, 670 + buttonquit.Height / 2, 320, 135);




        }



        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            Rectangle mouserect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);
            clickMenu = false;
            if (mouseState.LeftButton == ButtonState.Pressed)
            {

                for (int i = 0; i < buttons.Length; i++)
                {
                    // si le clic correspond à un des 3 boutons
                    if (buttons[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                    {
                        clickMenu = true;
                        // on change l'état défini dans Game1 en fonction du bouton cliqué
                        if (i == 0)
                            _myGame.Etat = Game1.Etats.GameScreen;
                        else if (i == 1)
                            _myGame.Etat = Game1.Etats.ControlsScreen;
                        else if (i == 2)
                            _myGame.Etat = Game1.Etats.EndScreen;


                    }

                }
            }

            if (buttons[0].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                buttonplayreleased = buttonplaypressed;
            else
                buttonplayreleased = buttonplay;

            if (buttons[1].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                buttoncontrolsreleased = buttoncontrolspressed;
            else
                buttoncontrolsreleased = buttoncontrols;

            if (buttons[2].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                buttonquitreleased = buttonquitpressed;
            else
                buttonquitreleased = buttonquit;

        }



        public override void Draw(GameTime gameTime)
        {
            //_myGame._tiledMapRenderer.Draw();

            _myGame._spriteBatch.Begin();
            _myGame._spriteBatch.Draw(_fond, _posfond, Color.White);
            _myGame._spriteBatch.Draw(buttonplayreleased, buttonplaypos, Color.Red);
            _myGame._spriteBatch.Draw(buttoncontrolsreleased, buttoncontrolspos, Color.Red);
            _myGame._spriteBatch.Draw(buttonquitreleased, buttonquitpos, Color.Red);
            //_myGame._spriteBatch.Draw(_textButtons, _posbuttons, Color.Red);


            _myGame._spriteBatch.End();

        }
    }
}


