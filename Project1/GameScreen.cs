using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System;
using System.Collections.Generic;
using MonoGame.Extended.TextureAtlases;
using System.Diagnostics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using System.Threading;
using MonoGame.Extended;

namespace Project1
{
    public class GameScreen : MonoGame.Extended.Screens.GameScreen
    {
        private Game1 _myGame;
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1;

        private Player player;
        public Camera camera;
        private Core core;
        private Collisions collisions;




        private Vector2 relativeCursor;
        private bool click;

        private Texture2D pause;
        private Vector2 _pausepos;
        public bool screenpause;


        private bool testpause = false;
        private bool toucheBalleZombie;


        private int speedsup;

        
        private AnimatedSprite barredevie;
        private Vector2 barredeviepos;

        private Collisions collisions;

        List<Bullet> listeBalles;
        List<Zombie> listeZomb;
        private int nbZombie = 0, numVague = 1, zombMaxVague = 10;
        private bool vagueFinie = false;


        private Texture2D shop;
        private Vector2 _shopPos;
        private Rectangle[] buttons;
        public bool shopoui;


        Walls wallReference;
        List<Walls> listeWalls;
       

        public GameScreen(Game1 game) : base(game)
        {
            _myGame = game;

        }

        public override void LoadContent()
        {

            _myGame._tiledMap = Content.Load<TiledMap>("map");
            _myGame._tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _myGame._tiledMap);
            _myGame.mapLayer = _myGame._tiledMap.GetLayer<TiledMapTileLayer>("Cailloux");

            
            barredeviepos = new Vector2(250, 1000);



            pause = Content.Load<Texture2D>("pause");
            _pausepos = new Vector2(700, 400);


            SpriteSheet sprite = Content.Load<SpriteSheet>("barredevie.sf", new JsonContentLoader());
            barredevie = new AnimatedSprite(sprite);


            player = new Player(this);
            camera = new Camera();
            core = new Core(this, _myGame._tiledMap);
            



            click = false;
            screenpause = false;

            listeBalles = new List<Bullet>();
            listeZomb = new List<Zombie>();

            listeBalles = new List<Bullet>();
            listeZomb = new List<Zombie>();

            shopoui = false;
            shop = Content.Load<Texture2D>("fondshop");
            _shopPos = new Vector2(0, 0);
            buttons = new Rectangle[5];
            buttons[0] = new Rectangle(329, 196, 564, 325);
            buttons[1] = new Rectangle(962, 203, 564, 325);
            buttons[2] = new Rectangle(329, 571, 564, 325);
            buttons[3] = new Rectangle(962, 571, 564, 325);
            buttons[4] = new Rectangle(56, 927, 438, 132);


            wallReference = new Walls(_myGame, new Rectangle(0, 0, 0, 0));
            

            listeWalls = new List<Walls>();
            listeWalls = wallReference.ChargementMap();


            collisions = new Collisions();

            speedsup = 0;

        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();



            if (keyboardState.IsKeyDown(Keys.P))
            {
                screenpause = false;
                Console.WriteLine("play");
            }




            if (screenpause == false)
            {
                float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
                float walkSpeed = deltaSeconds * player.Speed; // Vitesse de déplacement du joueur

                float zombSpeed = deltaSeconds * Zombie.VITESSE_NORMAL; //Vitesse de déplacement du zomb



                relativeCursor = Vector2.Transform(new Vector2(mouseState.X, mouseState.Y), Matrix.Invert(camera.Transform));

                string animation = "idle";
                string animationcore = "idle";
                string animationbarredevie = "100%";

                //ALL TESTS ///////////////////////////////////////////////////////////////////////////////////


                if (keyboardState.IsKeyDown(Keys.Up) && player.Position.Y > player.Hitbox.Height / 2)
                {


                    animation = "walkNorth";

                    player.Position -= new Vector2(0, walkSpeed);
                    player.UpdateHitbox();
                    if (collisions.CollisionPlayerWall(player, listeWalls))
                    {
                        player.Position += new Vector2(0, walkSpeed);
                        player.UpdateHitbox();
                    }

                }

                if (keyboardState.IsKeyDown(Keys.Down) && player.Position.Y < _myGame._tiledMap.HeightInPixels - player.Hitbox.Height / 2)
                {

                    animation = "walkSouth";

                    player.Position += new Vector2(0, walkSpeed);
                    player.UpdateHitbox();
                    if (collisions.CollisionPlayerWall(player, listeWalls))
                    {
                        player.Position -= new Vector2(0, walkSpeed);
                        player.UpdateHitbox();
                    }
                }

                if (keyboardState.IsKeyDown(Keys.Left) && player.Position.X > player.Hitbox.Width / 2)
                {

                    animation = "walkEast";
                        player.Position -= new Vector2(walkSpeed, 0);
                    player.UpdateHitbox();
                    if (collisions.CollisionPlayerWall(player, listeWalls))
                    {
                        player.Position += new Vector2(walkSpeed, 0);
                        player.UpdateHitbox();
                    }
                }
                if (keyboardState.IsKeyDown(Keys.Right) && player.Position.X < _myGame._tiledMap.WidthInPixels - player.Hitbox.Width / 2)
                {

                    animation = "walkWest";

                    player.Position += new Vector2(walkSpeed, 0);
                    player.UpdateHitbox();
                    if (collisions.CollisionPlayerWall(player, listeWalls))
                    {
                        player.Position -= new Vector2(walkSpeed, 0);
                        player.UpdateHitbox();
                    }
                }

                //if (keyboardState.IsKeyDown(Keys.R))
                //{

                //    screenpause = true;
                //    Console.WriteLine("pause");
                //}


                foreach (Bullet balle in listeBalles)
                {
                    flySpeed = deltaSeconds * balle.speed; // Vitesse de déplacement de la balle

                    balle.Position += new Vector2(flySpeed * balle.Direction.X, flySpeed * balle.Direction.Y);
                    balle.UpdateHitbox();
                    
                    if (collisions.CollisionBulletWall(balle, listeWalls) || collisions.CollisionBalleOutside(balle,_myGame._tiledMap, player))
                    {
                        listeBalles.Remove(balle);
                        break;
                    }

                }


                if (mouseState.LeftButton == ButtonState.Pressed && click == false)
                {
                    Bullet balle = new Bullet(this, player, new Vector2(relativeCursor.X, relativeCursor.Y));
                    balle.Speed += speedsup;
                    listeBalles.Add(balle);

                    click = true;
                    //A EFFACER
                    core.Life -= 5;
                }
                else if (mouseState.LeftButton == ButtonState.Released && click == true)
                {
                    click = false;
                }



                foreach (Zombie zombie in listeZomb)
                {
                    zombie.Position += Vector2.Normalize((player.Position - zombie.Position) * 9);
                    zombie.UpdateHitbox();
                    if (collisions.CollisionZombieWall(zombie, listeWalls))
                    {
                        zombie.Position -= Vector2.Normalize((player.Position - zombie.Position) * 9);
                        zombie.UpdateHitbox();
                    }
                    
                }

              /*  if (vagueFinie == true)
                {
                    nbZombie = 0;
                    while (nbZombie < zombMaxVague)
                    {
                        nbZombie += 1;
                        Zombie zombie = new Zombie(this, "Normal", _myGame._tiledMap);
                        listeZomb.Add(zombie);
                    }
                    vagueFinie = false;
                }
                if (listeZomb.Count == 0)
                    vagueFinie = true;
              */


                Console.WriteLine(listeZomb.Count);
                // Verif collision Zombie/Joueur , Zombie/Coeur , Zombie/Balle
                if (listeZomb.Count >= 1)
                {
                    collisions.CollisionZombiePlayer(ref listeZomb, ref player);
                    collisions.CollisionZombieCore(ref listeZomb, ref core);
                    if (listeBalles.Count >= 1)
                    {
                        collisions.CollisionBalleZombie(ref listeBalles, ref listeZomb);
                    }
                }
                    }
                }

                
                // Systeme de vague
                if public override void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        (listeZomb.Count == 0)
                    chrono += deltaSeconds;
                if (chrono >= 5)
                {
                    chrono = 0;
                    nbZombie = 0;
                    while (nbZombie < zombMaxVague)
                    {
                        nbZombie += 1;
                        Zombie zombie = new Zombie(this, "Normal", _myGame._tiledMap);
                        listeZomb.Add(zombie);
                    }
                    numVague += 1;
                    zombMaxVague += 25;
                }


                switch(core.Life)
                {
                    case 90:
                        animationbarredevie = "90%";
                        break;
                    case 80:
                        animationbarredevie = "80%";
                        break;
                    case 70:
                        animationbarredevie = "70%";
                        break;
                    case 60:
                        animationbarredevie = "60%";
                        break;
                    case 50:
                        animationbarredevie = "50%";
                        break;
                    case 40:
                        animationbarredevie = "40%";
                        break;
                    case 30:
                        animationbarredevie = "30%";
                        break;
                    case 20:
                        animationbarredevie = "20%";
                        break;
                    case 10:
                        animationbarredevie = "10%";
                        break;
                    case 0:
                        animationbarredevie = "0%";
                        break;
                }






                player.Apparence.Play(animation);
                core.Apparence.Play(animationcore);
                barredevie.Play(animationbarredevie);


                core.Apparence.Update(deltaSeconds);
                barredevie.Update(deltaSeconds);

                _myGame._tiledMapRenderer.Update(gameTime);

                player.Apparence.Update(deltaSeconds);


                camera.Follow(player, _myGame);


            }


            //SHOP
            
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (buttons[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                    {
                        if (i == 0)
                        {
                            if (player.Gold >= 0)
                                player.Life += 10;
                        }
                        else if (i == 1)
                        {
                            if (player.Gold >= 0)
                                speedsup += 100;

                        }

                        else if (i == 2)
                        {
                            if (player.Gold >= 0)
                                player.Speed += 10;
                        }

                        else if (i == 3)
                        {
                            if (player.Gold >= 0)
                                player.Speed += 100;
                        }
                        else if (i == 4)
                        {
                            shopoui = false;
                            screenpause = false;
                        
                        }
                            

                    }
                }
            }





        }
        public override void Draw(GameTime gameTime)
        {


            


            if (shopoui == true)
            {
                _myGame._spriteBatch.Begin();
                _myGame._spriteBatch.Draw(shop, _shopPos, Color.White);
                _myGame._spriteBatch.End();
            }
            else
            {
                Texture2D rect = new Texture2D(GraphicsDevice, 1, 1);
                rect.SetData(new Color[] { Color.Blue });


                _myGame._spriteBatch.Begin(transformMatrix: camera.Transform);
                _myGame._tiledMapRenderer.Draw(viewMatrix: camera.Transform);

                _myGame._spriteBatch.Draw(rect, player.Hitbox, Color.White);

                _myGame._spriteBatch.Draw(core.Apparence, core.Position);
                _myGame._spriteBatch.Draw(player.Apparence, player.Position);
            
                
                
   


                foreach (Bullet balle in listeBalles)
                {
                    _myGame._spriteBatch.DrawRectangle(balle.Hitbox, Color.Cyan, 7);
                    _myGame._spriteBatch.Draw(balle.Apparence, balle.Position, Color.White);
                }


                foreach (Zombie zombie in listeZomb)
                {
                    _myGame._spriteBatch.Draw(rect, zombie.Hitbox, Color.White);
                    _myGame._spriteBatch.Draw(zombie.TextureZomb, zombie.Position);
                }

                foreach (Walls wall in listeWalls)
                {
                    _myGame._spriteBatch.Draw(rect, wall.Hitbox, Color.White);
                }
   

                _myGame._spriteBatch.End();
             
                _myGame._spriteBatch.Begin();
                _myGame._spriteBatch.Draw(barredevie, barredeviepos);
                _myGame._spriteBatch.End();
                
            }

        }
    }
}

