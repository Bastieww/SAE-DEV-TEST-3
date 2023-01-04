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

namespace SAE
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;
        public SpriteBatch _spriteBatch { get; set; }
        
        

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
            //_positionPlayer = new Vector2(_graphics.PreferredBackBufferWidth / 2 , _graphics.PreferredBackBufferHeight / 2);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            

            startscreen = new StartScreen(this); 
            gamescreen = new GameScreen(this); 
            endscreen = new EndScreen(this); 

            

            //Joueur
            //SpriteSheet spriteSheet = Content.Load<SpriteSheet>("playerSide.sf", new JsonContentLoader());
         


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

           

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
            //float walkSpeed = deltaSeconds * playerDeBase.Vitesse; 

            KeyboardState keyboardState = Keyboard.GetState();


            //player bouge
            /*
            string playerSide = "idle";
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                _positionPlayer.Y += 1;
                playerSide = "walkNorth";
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                _positionPlayer.Y -= 1;
                playerSide = "walkSouth";
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                _positionPlayer.X -= 1;
                playerSide = "walkEast";

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _positionPlayer.X += 1;
                playerSide = "walkWest";
            }
            */
           

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
            else
            {

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
    }
}