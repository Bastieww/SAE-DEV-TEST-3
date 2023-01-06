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
        // défini dans Game1


        private Vector2 _positionPerso;
        private MonoGame.Extended.Sprites.AnimatedSprite _perso;
        private int vitesse = 100;

        private Vector2 _positionZombie;
        private MonoGame.Extended.Sprites.AnimatedSprite _zombie;



        private float deltaTime;

        public GameScreen(Game1 game) : base(game)
        {
            _myGame = game;
            _positionPerso = new Vector2(Game1.WIDTH / 2, Game1.HEIGHT / 3);
            _positionZombie = new Vector2(Game1.WIDTH / 3, Game1.HEIGHT / 2);

        }
        public override void LoadContent()
        {
            _myGame._tiledMap = Content.Load<TiledMap>("map");
            _myGame._tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _myGame._tiledMap);
            _myGame.mapLayer = _myGame._tiledMap.GetLayer<TiledMapTileLayer>("Cailloux");


            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("animation.sf", new JsonContentLoader());
            _perso = new MonoGame.Extended.Sprites.AnimatedSprite(spriteSheet);

            SpriteSheet spriteSheetZombie = Content.Load<SpriteSheet>("zombieAnim.sf", new JsonContentLoader());
            _zombie = new MonoGame.Extended.Sprites.AnimatedSprite(spriteSheetZombie);

        }
        public override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _myGame._tiledMapRenderer.Update(gameTime);

            _perso.Update(deltaTime); // time écoulé

            _zombie.Play("idle");
            _zombie.Update(deltaTime);

        }
        public override void Draw(GameTime gameTime)
        {
            _myGame._tiledMapRenderer.Draw();
            _myGame._spriteBatch.Begin();
            _myGame._spriteBatch.Draw(_perso, _positionPerso);
            _myGame._spriteBatch.Draw(_zombie, _positionZombie);
            _myGame._spriteBatch.End();

        }
    }
}

