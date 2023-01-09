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

        public object SpriteBatch { get; internal set; }

        StartScreen startscreen;
        GameScreen gamescreen;
        EndScreen endscreen;


        public TiledMap _tiledMap;
        public TiledMapRenderer _tiledMapRenderer;
        public TiledMapTileLayer mapLayer;

       

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

            _screenManager.LoadScreen(startscreen, new FadeTransition(GraphicsDevice, Color.Black));

            _screenManager.LoadScreen(startscreen, new FadeTransition(GraphicsDevice, Color.Black));
         

            _screenManager.LoadScreen(gamescreen, new FadeTransition(GraphicsDevice, Color.Black));



        }

        protected override void Update(GameTime gameTime)
        {   
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState _mouseState = Mouse.GetState();



            

                // TODO: Add your update logic here



                if (_mouseState.LeftButton == ButtonState.Pressed)
                {
                // Attention, l'état a été mis à jour directement par l'écran en question
                if (this.Etat == Etats.EndScreen)
                    Exit();

                else if (this.Etat == Etats.GameScreen && startscreen.clickMenu == true)
                        _screenManager.LoadScreen(gamescreen, new FadeTransition(GraphicsDevice, Color.Black));

                }


            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                if (this.Etat == Etats.GameScreen)
                {
                    {
                        this.Etat = Etats.StartScreen;
                       
                        _screenManager.LoadScreen(startscreen, new FadeTransition(GraphicsDevice, Color.Black));
                        

                    }



                }



            }

                // TODO: Add your update logic here



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