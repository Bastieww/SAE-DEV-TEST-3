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
using System.Collections.Generic;
using MonoGame.Extended.TextureAtlases;
using System.Diagnostics;

namespace Project1
{
    public class GameScreen : MonoGame.Extended.Screens.GameScreen
    {
        private Game1 _myGame;
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1;

        private Player player;
        private Camera camera;
        private Core core;

        private Vector2 relativeCursor;
        private bool click;

        private Texture2D pause;
        private Vector2 _pausepos;
        private bool screenpause;
        private double pausetemps=0;

        
        List<Bullet> listeBalles;

        List<Zombie> listeZomb;
        private int chrono = 0;

        public GameScreen(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void LoadContent()
        {
            _myGame._tiledMap = Content.Load<TiledMap>("map");
            _myGame._tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _myGame._tiledMap);
            _myGame.mapLayer = _myGame._tiledMap.GetLayer<TiledMapTileLayer>("Cailloux");

            pause = Content.Load<Texture2D>("pause");
            _pausepos = new Vector2(0, 0);
            

            player = new Player(this);
            camera = new Camera();
            core = new Core(this);

            click = false;
            screenpause = false;
            
            
            
            

            listeBalles = new List<Bullet>();


            listeZomb = new List<Zombie>();


        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            //pausetemps =+ 0.1;

            //Debug.WriteLine(pausetemps);
            if (keyboardState.IsKeyUp(Keys.P))
                pausetemps = +1;
            else if (keyboardState.IsKeyDown(Keys.P) && pausetemps == 1)
                screenpause = false;
                    

            if (screenpause == false)
            {
                float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
                float walkSpeed = deltaSeconds * player.Speed; // Vitesse de déplacement du joueur
                float flySpeed = deltaSeconds * Bullet.SPEED; // Vitesse de déplacement de la balle
          
            float zombSpeed = deltaSeconds * Zombie.VITESSE_NORMAL; //Vitesse de déplacement du zomb
                
                

                relativeCursor = Vector2.Transform(new Vector2(mouseState.X, mouseState.Y), Matrix.Invert(camera.Transform));

                string animation = "walkWest";

                //ALL TESTS ///////////////////////////////////////////////////////////////////////////////////

                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    ushort tx = (ushort)(player.Position.X / _myGame._tiledMap.TileWidth);
                    ushort ty = (ushort)(player.Position.Y / _myGame._tiledMap.TileHeight - 1);
                    animation = "walkNorth";
                    if (!IsCollision(tx, ty))
                        player.Position -= new Vector2(0, walkSpeed);
                }

                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    ushort tx = (ushort)(player.Position.X / _myGame._tiledMap.TileWidth);
                    ushort ty = (ushort)(player.Position.Y / _myGame._tiledMap.TileHeight + 1);
                    animation = "walkSouth";
                    if (!IsCollision(tx, ty))
                        player.Position += new Vector2(0, walkSpeed);
                }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                ushort tx = (ushort)(player.Position.X / _myGame._tiledMap.TileWidth - 1);
                ushort ty = (ushort)(player.Position.Y / _myGame._tiledMap.TileHeight);
                animation = "walkWest";
                if (!IsCollision(tx, ty))
                    player.Position -= new Vector2(walkSpeed, 0);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                ushort tx = (ushort)(player.Position.X / _myGame._tiledMap.TileWidth + 1);
                ushort ty = (ushort)(player.Position.Y / _myGame._tiledMap.TileHeight);
                animation = "walkEast";
                if (!IsCollision(tx, ty))
                    player.Position += new Vector2(walkSpeed, 0);
            }
            
           
            if (listeBalles != null)
            {
                foreach (Bullet balle in listeBalles)
                {
                    balle.Position += new Vector2(flySpeed * balle.Direction.X, flySpeed * balle.Direction.Y);
                }
            }
            
            if (mouseState.LeftButton == ButtonState.Pressed && click == false)
            {
                Bullet balle = new Bullet(this, player, new Vector2(relativeCursor.X, relativeCursor.Y));
                listeBalles.Add(balle);
                click = true;
            }
            else if (mouseState.LeftButton == ButtonState.Released && click == true)
            {
                click = false;
            }


            if (listeZomb != null)
            {
                foreach (Zombie zombie in listeZomb)
                {
                    zombie.Position += Vector2.Normalize(player.Position - zombie.Position)*5;
                }
            }
            chrono += 1;
            
            if (chrono == 1)
            {
                chrono = 0;
                Zombie zombie = new Zombie(this, "Normal");
                listeZomb.Add(zombie);
               
                
            }

           
         

                if(keyboardState.IsKeyDown(Keys.P))
                { 
                    
                    screenpause = true;
                   
                }
                
          
                player.Apparence.Play(animation);







                _myGame._tiledMapRenderer.Update(gameTime);

                player.Apparence.Update(deltaSeconds); // time écoulé

                camera.Follow(player);

                //player.Position += new Vector2 (6,1);
                //Console.WriteLine(player.Position);
            }
           
           

        }
        public override void Draw(GameTime gameTime)
        {
            _myGame._tiledMapRenderer.Draw(viewMatrix: camera.Transform);
            
            _myGame._spriteBatch.Begin(transformMatrix : camera.Transform);
            _myGame._spriteBatch.Draw(player.Apparence, player.Position);

            if (listeBalles != null)
            {
                foreach (Bullet balle in listeBalles)
                {
                    _myGame._spriteBatch.Draw(balle.Apparence, balle.Position, Color.White);
                    
                }
            }
            _myGame._spriteBatch.Draw(core.Apparence, core.Position, Color.White);
         
            if (listeZomb != null)
            {
                foreach (Zombie zombie in listeZomb)
                {
                    _myGame._spriteBatch.Draw(zombie.TextureZomb, zombie.Position);
                 

                }
            }

            _myGame._spriteBatch.End();
        }
        private bool IsCollision(ushort x, ushort y)
        {
            // définition de tile qui peut être null (?)

            TiledMapTile? tile;
            if (_myGame.mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
            {
                Console.WriteLine(_myGame.mapLayer.GetTile(x, y).GlobalIdentifier);
                return true;
            }
            return false;
        }
    }
}

