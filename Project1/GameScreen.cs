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
    public class GameScreen : MonoGame.Extended.Screens.GameScreen
    {
        private Game1 _myGame;
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1;

        private Player player;
        private Zombie zombie;

        private float deltaTime;

        public GameScreen(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void LoadContent()
        {
            _myGame._tiledMap = Content.Load<TiledMap>("map");
            _myGame._tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _myGame._tiledMap);
            _myGame.mapLayer = _myGame._tiledMap.GetLayer<TiledMapTileLayer>("Cailloux");

            player = new Player(this);
            //zombie = new Zombie(this, "Normal");

        }
        public override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _myGame._tiledMapRenderer.Update(gameTime);
            

        }
        public override void Draw(GameTime gameTime)
        {
            _myGame._tiledMapRenderer.Draw();
            
            _myGame._spriteBatch.Begin();
            _myGame._spriteBatch.Draw(player.Apparence, new Vector2(644, 566));
            //_myGame._spriteBatch.Draw(zombie.TextureZomb, new Vector2(544, 874));
            _myGame._spriteBatch.End();

        }
    }
}

