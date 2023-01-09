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




        public StartScreen(Game1 game) : base(game)
        {
            _myGame = game;
            

        }
        public override void LoadContent()
        {
            _myGame._tiledMap = Content.Load<TiledMap>("map");
            _myGame._tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _myGame._tiledMap);
            _textButtons = Content.Load<Texture2D>("buttons");
            _posbuttons = new Vector2(Game1.WIDTH / 2 - _textButtons.Width / 2, Game1.HEIGHT / 2 - _textButtons.Height / 2);

            buttons = new Rectangle[2];
            buttons[0] = new Rectangle(719,315, 461, 150);
            buttons[1] = new Rectangle(719, 670, 461, 150);
           
        }



        public override void Update(GameTime gameTime)
        {
           MouseState mouseState = Mouse.GetState();
           Rectangle mouserect = new Rectangle(mouseState.X, mouseState.Y, 1,1);
            
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    // si le clic correspond à un des 3 boutons
                    if (buttons[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                    {
                        // on change l'état défini dans Game1 en fonction du bouton cliqué
                        if (i == 0)
                            _myGame.Etat = Game1.Etats.GameScreen;
                        else if(i == 1)
                            _myGame.Etat = Game1.Etats.EndScreen;
                        
                    }

                }

            }

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                if (_myGame.Etat == Game1.Etats.GameScreen)
                    _myGame.Etat = Game1.Etats.StartScreen;
            }
           

        }



        public override void Draw(GameTime gameTime)
        {
            //_myGame._tiledMapRenderer.Draw();

            _myGame._spriteBatch.Begin();
            
            _myGame._spriteBatch.Draw(_textButtons, _posbuttons, Color.Red);
          

            _myGame._spriteBatch.End();

        }
    }
}

