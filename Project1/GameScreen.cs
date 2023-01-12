using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
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

        //PAUSE
        private Texture2D pause;
        private Vector2 _pausepos;
        private Rectangle[] buttonsPause;
        public bool screenpause;

        private Texture2D buttonresume;
        private Texture2D buttonresumepressed;
        private Texture2D buttonresumereleased;
        private Vector2 buttonresumepos;

        private Texture2D buttonmenu;
        private Texture2D buttonmenupressed;
        private Texture2D buttonmenureleased;
        private Vector2 buttonmenupos;
        // Barre de vie du coeur
        private SpriteBatch barredeviestatiqueCore;
        private AnimatedSprite barredevieCore;
        private Vector2 barredevieposCore;

        // Barre de vie du joueur
        private SpriteBatch barredeviestatiquePlayer;
        private AnimatedSprite barredeviePlayer;
        private Vector2 barredevieposPlayer;

        private int speedsup;

        // Texte
        Vector2 positionText;


        private AnimatedSprite barredevie;
        private Vector2 barredeviepos;


        // Zombies
        List<Bullet> listeBalles;
        List<Zombie> listeZomb;
        private int nbZombie = 0, numVague = 0, zombMaxVague = 10;
        private float chrono = 0;

        // Shop
        private Texture2D shop;
        private Vector2 _shopPos;
        private Rectangle[] buttons;
        public bool shopoui;
        public const int PRICESHOP1 = 10;
        public const int PRICESHOP2 = 20;
        public const int PRICESHOP3 = 30;
        public const int PRICESHOP4 = 40;

        private Texture2D shop1;
        private Texture2D shop1pressed;
        private Texture2D shop1released;
        private Vector2 shop1pos;

        private Texture2D shop2;
        private Texture2D shop2pressed;
        private Texture2D shop2released;
        private Vector2 shop2pos;

        private Texture2D shop3;
        private Texture2D shop3pressed;
        private Texture2D shop3released;
        private Vector2 shop3pos;

        private Texture2D shop4;
        private Texture2D shop4pressed;
        private Texture2D shop4released;
        private Vector2 shop4pos;


        // Murs
        Walls wallReference;
        List<Walls> listeWalls;

        //Musique
        Song gameScreenMusic;
        SoundEffect shootSound;
        
       

        public GameScreen(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void LoadContent()
        {
            _myGame._tiledMap = Content.Load<TiledMap>("map");
            _myGame._tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _myGame._tiledMap);
            _myGame.mapLayer = _myGame._tiledMap.GetLayer<TiledMapTileLayer>("Cailloux");

            // Boutons
            pause = Content.Load<Texture2D>("fondpause");
            _pausepos = new Vector2(0, 0);

            buttonresume = Content.Load<Texture2D>("buttonresume");
            buttonresumepressed = Content.Load<Texture2D>("buttonresumepressed");
            buttonresumereleased = buttonresume;
            buttonresumepos = new Vector2(690, 314);

            buttonmenu = Content.Load<Texture2D>("buttonmenu");
            buttonmenupressed = Content.Load<Texture2D>("buttonmenupressed");
            buttonmenureleased = buttonmenu;
            buttonmenupos = new Vector2(690, 625);

            buttonsPause = new Rectangle[2];
            buttonsPause[0] = new Rectangle(690, 314, 500, 150);
            buttonsPause[1] = new Rectangle(690, 625, 500, 150);


            // Barre de vie Coeur
            barredevieposCore = new Vector2(250, Game1.HEIGHT - 80);
            SpriteSheet spriteCore = Content.Load<SpriteSheet>("barredevie.sf", new JsonContentLoader());
            barredevieCore = new AnimatedSprite(spriteCore);

            // Barre de vie du joueur
            barredevieposPlayer = new Vector2(Game1.WIDTH - 400, Game1.HEIGHT - 80);
            SpriteSheet spriteVieJoueur = Content.Load<SpriteSheet>("barredevie.sf", new JsonContentLoader());
            barredeviePlayer = new AnimatedSprite(spriteVieJoueur);

            player = new Player(this);
            camera = new Camera();
            core = new Core(this, _myGame._tiledMap);


            click = false;
            screenpause = false;

            wallReference = new Walls(_myGame, new Rectangle(0, 0, 0, 0));
            
            // Listes
            listeBalles = new List<Bullet>();
            listeZomb = new List<Zombie>();
            listeWalls = new List<Walls>();
            listeWalls = wallReference.ChargementMap();

            // SHOP
            shopoui = false;
            shop = Content.Load<Texture2D>("fondshop");
            _shopPos = new Vector2(0, 0);

            shop1 = Content.Load<Texture2D>("shop1");
            shop1pressed = Content.Load<Texture2D>("shop1pressed");
            shop1released = shop1;
            shop1pos = new Vector2(336, 185);

            shop2 = Content.Load<Texture2D>("shop2");
            shop2pressed = Content.Load<Texture2D>("shop2pressed");
            shop2released = shop2;
            shop2pos = new Vector2(961, 185);

            shop3 = Content.Load<Texture2D>("shop3");
            shop3pressed = Content.Load<Texture2D>("shop3pressed");
            shop3released = shop3;
            shop3pos = new Vector2(336, 545);

            shop4 = Content.Load<Texture2D>("shop4");
            shop4pressed = Content.Load<Texture2D>("shop4pressed");
            shop4released = shop4;
            shop4pos = new Vector2(961, 545);

            buttons = new Rectangle[5];
            buttons[0] = new Rectangle(336, 185, 608, 353);
            buttons[1] = new Rectangle(961, 185, 608, 353);
            buttons[2] = new Rectangle(336, 545, 608, 353);
            buttons[3] = new Rectangle(961, 545, 608, 353);
            buttons[4] = new Rectangle(36, 937, 438, 132);

            
            collisions = new Collisions();

            gameScreenMusic = Content.Load<Song>("Demon-Slayer");
            MediaPlayer.Play(gameScreenMusic);
            shootSound = Content.Load<SoundEffect>("ShootSound");
            
            speedsup = 0;

            

        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();


            // Si screen de pause
            if (screenpause == false)
            {
                float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
                float walkSpeed = deltaSeconds * player.Speed; // Vitesse de déplacement du joueur
                float zombSpeed = deltaSeconds * Zombie.VITESSE_NORMAL; //Vitesse de déplacement du zomb


                relativeCursor = Vector2.Transform(new Vector2(mouseState.X, mouseState.Y), Matrix.Invert(camera.Transform));

                string animation = "idle";
                string animationcore = "idle";
                string animationbarredevieCore = "100%";
                string animationbarredeviePlayer = "100%";


                //ALL TESTS ///////////////////////////////////////////////////////////////////////////////////

                // Deplacement du Joueur
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


                // Disparition du bullet
                foreach (Bullet balle in listeBalles)
                {
                    float flySpeed = deltaSeconds * balle.speed; // Vitesse de déplacement de la balle

                    balle.Position += new Vector2(flySpeed * balle.Direction.X, flySpeed * balle.Direction.Y);
                    balle.UpdateHitbox();

                    if (collisions.CollisionBulletWall(balle, listeWalls) || collisions.CollisionBalleOutside(balle, _myGame._tiledMap, player))
                    {
                        listeBalles.Remove(balle);
                        break;
                    }

                }

                // Verif si clique -> ajoute une balle dans liste
                if (mouseState.LeftButton == ButtonState.Pressed && click == false)
                {
                    Bullet balle = new Bullet(this, player, new Vector2(relativeCursor.X, relativeCursor.Y));
                    balle.Speed += speedsup;
                    listeBalles.Add(balle);
                    shootSound.Play();

                    click = true;

                    //A EFFACER
                    core.Life -= 10;
                }
                else if (mouseState.LeftButton == ButtonState.Released && click == true)
                {
                    click = false;
                }


                // Verif collision Bullet/Mur
                foreach (Bullet balle in listeBalles)
                {
                    float flySpeed = deltaSeconds * balle.speed; // Vitesse de déplacement de la balle

                    balle.Position += new Vector2(flySpeed * balle.Direction.X, flySpeed * balle.Direction.Y);
                    balle.UpdateHitbox();
                    if (collisions.CollisionBulletWall(balle, listeWalls))
                    {
                        listeBalles.Remove(balle);
                        break;
                    }
                }

                // Verif collision Zombie/Mur
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


                // Verif collision Zombie/Joueur , Zombie/Coeur , Zombie/Balle
                if (listeZomb.Count >= 1)
                {
                    collisions.CollisionZombiePlayer(listeZomb, player);
                    collisions.CollisionZombieCore(listeZomb, core);
                    if (listeBalles.Count >= 1)
                    {
                        collisions.CollisionBalleZombie(listeBalles, listeZomb);
                    }
                }


                // Systeme de vague
                if (listeZomb.Count == 0)
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


                // Affichage de la vie du Coeur
                /*
                for (int i = 10; i >= 0; i++)
                    if (core.Life < i * 10 && core.Life > (i - 1) * 10)
                        animationbarredevie = $"{i}0%"; */

                if (core.Life < 90 && core.Life > 80)
                    animationbarredevieCore = "90%";
                else if (core.Life < 80 && core.Life > 70)
                    animationbarredevieCore = "80%";
                else if (core.Life < 70 && core.Life > 60)
                    animationbarredevieCore = "70%";
                else if (core.Life < 60 && core.Life > 50)
                    animationbarredevieCore = "60%";
                else if (core.Life < 50 && core.Life > 40)
                    animationbarredevieCore = "50%";
                else if (core.Life < 40 && core.Life > 30)
                    animationbarredevieCore = "40%";
                else if (core.Life < 30 && core.Life > 20)
                    animationbarredevieCore = "30%";
                else if (core.Life < 20 && core.Life > 10)
                    animationbarredevieCore = "20%";
                else if (core.Life < 10 && core.Life > 0)
                    animationbarredevieCore = "10%";
                else if (core.Life == 0)
                    animationbarredevieCore = "0%";







                player.Apparence.Play(animation);
                player.Apparence.Update(deltaSeconds);
                
                barredevieCore.Play(animationbarredevieCore);
                barredevieCore.Update(deltaSeconds);

                barredeviePlayer.Play(animationbarredeviePlayer);
                barredeviePlayer.Update(deltaSeconds);

                core.Apparence.Play(animationcore);
                core.Apparence.Update(deltaSeconds);
               
                _myGame._tiledMapRenderer.Update(gameTime);
                
                camera.Follow(player, _myGame);
            }

            //PAUSE
            if (screenpause == true)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    for (int i = 0; i < buttonsPause.Length; i++)
                    {
                        if (buttonsPause[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                        {
                            if (i == 0)
                            {
                                screenpause = false;
                            }
                            else if (i == 1)
                            {
                                _myGame.Etat = Game1.Etats.StartScreen;
                                _myGame.changementMusic = true;
                            }
                        }
                    }
                }
                if (buttonsPause[0].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                    buttonresumereleased = buttonresumepressed;
                else
                    buttonresumereleased = buttonresume;

                if (buttonsPause[1].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                    buttonmenureleased = buttonmenupressed;
                else
                    buttonmenureleased = buttonmenu;
            }

            // SHOP
            if (shopoui == true)
            {

                shop1released = shop1;
                shop2released = shop2;
                shop3released = shop3;
                shop4released = shop4;
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        if (buttons[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                        {
                            if (i == 0)
                            {
                                if (player.Gold >= PRICESHOP1)
                                {
                                    player.Gold -= PRICESHOP1;
                                    player.Life += 10;
                                    shop1released = shop1pressed;

                                }


                            }
                            else if (i == 1)
                            {
                                if (player.Gold >= PRICESHOP2)
                                {
                                    speedsup += 100;
                                    player.Gold -= PRICESHOP2;
                                    shop2released = shop2pressed;
                                }


                            }

                            else if (i == 2)
                            {
                                if (player.Gold >= PRICESHOP3)
                                {
                                    player.Speed += 10;
                                    player.Gold -= PRICESHOP3;
                                    shop3released = shop3pressed;
                                }

                            }

                            else if (i == 3)
                            {
                                if (player.Gold >= PRICESHOP4)
                                {
                                    player.Speed += 100;
                                    player.Gold -= PRICESHOP4;
                                    shop4released = shop4pressed;
                                }

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
        }
        public override void Draw(GameTime gameTime)
        {

            if (shopoui == true)
            {
                _myGame._spriteBatch.Begin();
                _myGame._spriteBatch.Draw(shop, _shopPos, Color.White);
                _myGame._spriteBatch.Draw(shop1released, shop1pos, Color.White);
                _myGame._spriteBatch.Draw(shop2released, shop2pos, Color.White);
                _myGame._spriteBatch.Draw(shop3released, shop3pos, Color.White);
                _myGame._spriteBatch.Draw(shop4released, shop4pos, Color.White);

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
                    _myGame._spriteBatch.Draw(pause, balle.Hitbox, Color.White);
                }

                foreach (Zombie zombie in listeZomb)
                {
                    _myGame._spriteBatch.Draw(rect, zombie.Hitbox, Color.White);
                    _myGame._spriteBatch.Draw(zombie.TextureZomb, zombie.Position);
                    _myGame._spriteBatch.Draw(pause, zombie.Hitbox, Color.White);
                }

                foreach (Walls wall in listeWalls)
                {
                    _myGame._spriteBatch.Draw(rect, wall.Hitbox, Color.White);
                }
                _myGame._spriteBatch.End();

                _myGame._spriteBatch.Begin();
                _myGame._spriteBatch.Draw(barredevieCore, barredevieposCore);
                _myGame._spriteBatch.Draw(barredeviePlayer, barredevieposPlayer);


                // Texte
                positionText = new Vector2(Game1.WIDTH - 600, 10);
                _myGame._spriteBatch.DrawString(_myGame.font, "Zombies Restants : " + listeZomb.Count, positionText, new Color(74, 110, 40));
                positionText = new Vector2(Game1.WIDTH - 270, 250);
                _myGame._spriteBatch.DrawString(_myGame.font, "Argent " + player.Gold, positionText, new Color(74, 110, 40));
                positionText = new Vector2(Game1.WIDTH - 250, 120);
                _myGame._spriteBatch.DrawString(_myGame.font, "Vague " + numVague, positionText, new Color(74, 110, 40));

                _myGame._spriteBatch.End();

            }

            if (screenpause == true && shopoui == false)
            {
                Console.WriteLine("test");

                _myGame._spriteBatch.Begin();
                _myGame._spriteBatch.Draw(pause, _pausepos, Color.White);
                _myGame._spriteBatch.Draw(buttonresumereleased, buttonresumepos, Color.White);
                _myGame._spriteBatch.Draw(buttonmenureleased, buttonmenupos, Color.White);
                _myGame._spriteBatch.End();
            }
        }


    }
}