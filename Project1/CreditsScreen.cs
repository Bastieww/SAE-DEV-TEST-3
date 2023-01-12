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
    public class CreditsScreen : MonoGame.Extended.Screens.GameScreen
    {
        private Game1 _myGame;
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        private Texture2D fondCreditsScreen;
        private Vector2 _posFondControlsScreen;

        private Texture2D buttonMenu;
        private Texture2D buttonMenuPressed;
        private Texture2D buttonMenuReleased;
        private Vector2 _buttonMenuPos;
        private Rectangle button;
        public CreditsScreen(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void LoadContent()
        {
            fondCreditsScreen = Content.Load<Texture2D>("fondcredits");
            _posFondControlsScreen = new Vector2(0, 0);
            button = new Rectangle(10, 10, 390, 118);

            buttonMenu = Content.Load<Texture2D>("buttonmenuend");
            buttonMenuPressed = Content.Load<Texture2D>("buttonmenuendpressed");
            buttonMenuReleased = buttonMenu;
            _buttonMenuPos = new Vector2(10, 10);
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            Rectangle mouserect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (button.Contains(Mouse.GetState().X, Mouse.GetState().Y))
                    _myGame.Etat = Game1.Etats.StartScreen;
            }


            if (button.Contains(Mouse.GetState().X, Mouse.GetState().Y))
                buttonMenuReleased = buttonMenuPressed;
            else
                buttonMenuReleased = buttonMenu;
        }
            
            
        
        public override void Draw(GameTime gameTime)
        {
            _myGame._spriteBatch.Begin();
            
            _myGame._spriteBatch.Draw(fondCreditsScreen, _posFondControlsScreen, Color.White);
            _myGame._spriteBatch.Draw(buttonMenuReleased, _buttonMenuPos, Color.White);
            _myGame._spriteBatch.End();

        }
    }
}
