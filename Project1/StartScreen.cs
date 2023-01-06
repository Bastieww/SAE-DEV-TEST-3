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
        


        public StartScreen(Game1 game) : base(game)
        {
            _myGame = game;
         
        }
        public override void LoadContent()
        {
        }
        public override void Update(GameTime gameTime)
        {
         
        }
        public override void Draw(GameTime gameTime)
        {
            _myGame._tiledMapRenderer.Draw();
            _myGame._spriteBatch.Begin();
          

            _myGame._spriteBatch.End();

        }
    }
}

