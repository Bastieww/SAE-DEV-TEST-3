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
    public class ControlsScreen : MonoGame.Extended.Screens.GameScreen
    {
        private Game1 _mygame;
        private Texture2D _fondcontrols;
        private Vector2 _fondpos;
        private Rectangle goback;
        

        public ControlsScreen(Game1 game) : base(game)
        {
            _mygame = game;
        }


        public override void LoadContent()
        {
            base.LoadContent();
            _fondcontrols = Content.Load<Texture2D>("fondcontrols");
            _fondpos = new Vector2(0,0);
            goback = new Rectangle(87,919,419,101);
        }



        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            Rectangle mouserect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (goback.Contains(Mouse.GetState().X, Mouse.GetState().Y))
                {
                    _mygame.Etat = Game1.Etats.StartScreen;
                }
            }
        
        }
        public override void Draw(GameTime gameTime)
        {
            _mygame._spriteBatch.Begin();
            _mygame._spriteBatch.Draw(_fondcontrols, _fondpos, Color.White);
            _mygame._spriteBatch.End();

        }
    }
}