﻿using Microsoft.Xna.Framework;
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
        public SpriteFont font;
        public bool changementMusic;
        public Random rd = new Random();






        public enum Etats { StartScreen, GameScreen, EndScreen, ControlsScreen, CreditsScreen };


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

                Console.WriteLine(value + " SET");
                Console.WriteLine(controlsscreen.clickControls);

                if (value == Etats.EndScreen && startscreen.clickQuit == false)
                    _screenManager.LoadScreen(endscreen, new FadeTransition(GraphicsDevice, Color.Black));
                else if (value == Etats.EndScreen && startscreen.clickQuit==true)
                    Exit();
                else if (value == Etats.GameScreen && startscreen.clickMenu == true || endscreen.clickRestart==true)
                    _screenManager.LoadScreen(gamescreen, new FadeTransition(GraphicsDevice, Color.Black));
                else if (value == Etats.ControlsScreen && startscreen.clickMenu == true)
                    _screenManager.LoadScreen(controlsscreen, new FadeTransition(GraphicsDevice, Color.Black));
                else if (value == Etats.StartScreen)
                {
                    Console.WriteLine("TEST START");
                    _screenManager.LoadScreen(startscreen, new FadeTransition(GraphicsDevice, Color.Black));
                }
                else if(value == Etats.CreditsScreen)
                    _screenManager.LoadScreen(creditsscreen, new FadeTransition(GraphicsDevice, Color.Black));
            }
        }

        public object SpriteBatch { get; internal set; }

        StartScreen startscreen;
        GameScreen gamescreen;
        EndScreen endscreen;
        ControlsScreen controlsscreen;
        CreditsScreen creditsscreen;


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

            // on charge les 3 écrans 
            startscreen = new StartScreen(this);
            gamescreen = new GameScreen(this);
            endscreen = new EndScreen(this);
            controlsscreen = new ControlsScreen(this);
            changementMusic = true;
            creditsscreen = new CreditsScreen(this);
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
            // Par défaut, le 1er état flèche l'écran de menu
            Etat = Etats.StartScreen;

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("font");
         

        }

        protected override void Update(GameTime gameTime)
        {   
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();



            if(keyboardState.IsKeyDown(Keys.S))
                {
                gamescreen.shopoui = true;
                gamescreen.screenpause=true;
            }

            


            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                if (this.Etat == Etats.GameScreen)
                {
                    {
                        gamescreen.screenpause = true;
                       
                    }
                }
            }

           



            base.Update(gameTime);
            }
       
            
        

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}