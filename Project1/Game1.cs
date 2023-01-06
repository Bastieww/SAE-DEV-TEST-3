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
        public GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;
        public SpriteBatch _spriteBatch { get; set; }
        public const int WIDTH = 1920, HEIGHT = 1080;
        

        
        public enum Etats { StartScreen, GameScreen, EndScreen };


        private Etats etat;
        
            public Etats Etat
        {
            get
            {
                return this.etat;
            }

            set
            {
                this.etat = value;
            }
        }

        StartScreen startscreen;
        GameScreen gamescreen;
        EndScreen endscreen;


        public TiledMap _tiledMap;
        public TiledMapRenderer _tiledMapRenderer;
        public TiledMapTileLayer mapLayer;

        //a deplacer + tard
        

       

        
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

            _graphics.PreferredBackBufferWidth = WIDTH;
            _graphics.PreferredBackBufferHeight = HEIGHT;

            _screenManager = new ScreenManager();
            Components.Add(_screenManager);

            // Par défaut, le 1er état flèche l'écran de menu
            Etat = Etats.StartScreen;

            // on charge les 3 écrans 
            startscreen = new StartScreen(this);
            gamescreen = new GameScreen(this);
            endscreen = new EndScreen(this);


        }

        protected override void Initialize()
        {

            // TODO: Add your initialization logic here
            
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            // Zombie
            //Zombie zombie1 = new Zombie("Normal");
            //zombie1.Initialize();
            

            //Joueur
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _screenManager.LoadScreen(gamescreen, new FadeTransition(GraphicsDevice, Color.Black));

            // TODO: use this.Content to load your game content here
            //a deplacer + tard
            






            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KeyboardState keyboardState = Keyboard.GetState();
            

            

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                _screenManager.LoadScreen(startscreen, new FadeTransition(GraphicsDevice,
                Color.White));
            }
            else if (keyboardState.IsKeyDown(Keys.Right) && this.Etat == Etats.StartScreen)
            {
                _screenManager.LoadScreen(gamescreen, new FadeTransition(GraphicsDevice,
                Color.Black));
            }
            

            // TODO: Add your update logic here
            /*
            _player.Play(playerSide);
            _player.Update(deltaSeconds);
            */

            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        { 
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