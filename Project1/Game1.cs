using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;
        public SpriteBatch _spriteBatch { get; set; }


        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer mapLayer;

        //a deplacer + tard
        private Vector2 _positionPerso;
        private MonoGame.Extended.Sprites.AnimatedSprite _perso;
        private int vitesse = 100;

        // Zombie
        private Zombie zombie1;
        private Vector2 _positionZombie;
        private MonoGame.Extended.Sprites.AnimatedSprite _zombie;

        StartScreen startscreen;
        GameScreen gamescreen;
        EndScreen endscreen;

        //Joueur
        /*private Vector2 _positionPlayer;
        private AnimatedSprite _player;
        Player playerDeBase = new Player("Player1", 100, 10, 0);
        */



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }

        protected override void Initialize()
        {

            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();


            //Joueur
            _positionPerso = new Vector2(_graphics.PreferredBackBufferWidth / 3, _graphics.PreferredBackBufferHeight / 2);
            _positionZombie = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            startscreen = new StartScreen(this);
            gamescreen = new GameScreen(this);
            endscreen = new EndScreen(this);

            // TODO: use this.Content to load your game content here
            //a deplacer + tard
            _tiledMap = Content.Load<TiledMap>("map");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Cailloux");

            // Zombie
            zombie1 = new Zombie("Normal");
            zombie1.CreationZombie();

            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("animation.sf", new JsonContentLoader());
            _perso = new MonoGame.Extended.Sprites.AnimatedSprite(spriteSheet);

            SpriteSheet spriteSheetZomb = Content.Load<SpriteSheet>("zombieAnim.sf", new JsonContentLoader());
            _zombie = new MonoGame.Extended.Sprites.AnimatedSprite(spriteSheetZomb);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
            float walkSpeed = deltaSeconds * vitesse; // Vitesse de déplacement du sprite
            KeyboardState keyboardState = Keyboard.GetState();


            // Joueur
            if (keyboardState.IsKeyDown(Keys.Z))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 1);

                if (!IsCollision(tx, ty))
                    _positionPerso.Y -= walkSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 1);

                if (!IsCollision(tx, ty))
                    _positionPerso.Y += walkSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.Q))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 1);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);

                if (!IsCollision(tx, ty))
                    _positionPerso.X -= walkSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 1);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);

                if (!IsCollision(tx, ty))
                    _positionPerso.X += walkSpeed;
            }


            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                _screenManager.LoadScreen(startscreen, new FadeTransition(GraphicsDevice,
                Color.White));
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                _screenManager.LoadScreen(endscreen, new FadeTransition(GraphicsDevice,
                Color.Black));
            }


            // TODO: Add your update logic here

            _tiledMapRenderer.Update(gameTime);

            _perso.Update(deltaTime); // time écoulé

            // Zombie
            Vector2 deplacementZombie = new Vector2((_positionPerso.X - _positionZombie.X), (_positionPerso.Y - _positionZombie.Y));
            _positionZombie += deplacementZombie / 100;

            _zombie.Play("idle");
            _zombie.Update(deltaSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            _tiledMapRenderer.Draw();
            _spriteBatch.Begin();
            _spriteBatch.Draw(_perso, _positionPerso);
            _spriteBatch.Draw(_zombie, _positionZombie);
            _spriteBatch.End();
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }

        private bool IsCollision(ushort x, ushort y)
        {
            // définition de tile qui peut être null (?)

            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
            {
                Console.WriteLine(mapLayer.GetTile(x, y).GlobalIdentifier);
                return true;
            }

            return false;
        }
    }
}