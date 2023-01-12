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
        private Rectangle[] buttonsPause;
        public bool screenpause;

        private bool testpause = false;
        private bool toucheBalleZombie;

        // Barre de vie du coeur
        private AnimatedSprite barredevieCore;
        private Vector2 barredevieposCore;

        // Barre de vie du joueur
        private AnimatedSprite barredeviePlayer;
        private Vector2 barredevieposPlayer;

        private int speedsup;

        // Texte
        Vector2 positionText;

        // Zombies
        List<Bullet> listeBalles;
        List<Zombie> listeZomb;
        private int nbZombie = 0, numVague = 0, zombMaxVague = 10;
        private float chrono = 0, chronoVagueSuivante = 60;
        private string textureZomb;

        // Shop
        private Texture2D shop;
        private Vector2 _shopPos;
        private Rectangle[] buttons;
        public bool shopoui;

        // Murs
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

            // Boutons
            pause = Content.Load<Texture2D>("fondpause");
            _pausepos = new Vector2(0, 0);
            buttonsPause = new Rectangle[2];
            buttonsPause[0] = new Rectangle(618, 314, 621, 154);
            buttonsPause[1] = new Rectangle(618, 625, 621, 154);


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


            // Shop
            shopoui = false;
            shop = Content.Load<Texture2D>("fondshop");
            _shopPos = new Vector2(0, 0);
            buttons = new Rectangle[5];
            buttons[0] = new Rectangle(329, 196, 564, 325);
            buttons[1] = new Rectangle(962, 203, 564, 325);
            buttons[2] = new Rectangle(329, 571, 564, 325);
            buttons[3] = new Rectangle(962, 571, 564, 325);
            buttons[4] = new Rectangle(56, 927, 438, 132);

            
            collisions = new Collisions();

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
                    
                    if (collisions.CollisionBulletWall(balle, listeWalls) || collisions.CollisionBalleOutside(balle,_myGame._tiledMap, player))
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
                    collisions.CollisionZombiePlayer( listeZomb,  player);
                    collisions.CollisionZombieCore( listeZomb,  core);
                    if (listeBalles.Count >= 1)
                    {
                        collisions.CollisionBalleZombie( listeBalles,  listeZomb);
                    }
                }

                // Systeme de vague
                if (listeZomb.Count == 0)
                    chrono += deltaSeconds;
                else
                    chronoVagueSuivante -= deltaSeconds;
                if (chrono >= 5 || chronoVagueSuivante <= 0)
                {
                    chronoVagueSuivante = 60;
                    chrono = 0;
                    nbZombie = 0;
                    while (nbZombie < zombMaxVague)
                    {
                        nbZombie += 1;
                        int puissanceZomb = 1;
                        if (puissanceZomb == 1)
                        {
                            textureZomb = "Normal";
                        }
                        else if (puissanceZomb == 2)
                        {
                            textureZomb = "Rapide";
                        }
                        else
                        {
                            textureZomb = "Gros";
                        }
                        Zombie zombie = new Zombie(this, textureZomb, _myGame._tiledMap);
                        listeZomb.Add(zombie);
                    }
                    numVague += 1;
                    zombMaxVague += 25;
                }

                // Affichage de la vie du Coeur
                switch(core.Life)
                {
                    case 90:
                        animationbarredevieCore = "90%";
                        break;
                    case 80:
                        animationbarredevieCore = "80%";
                        break;
                    case 70:
                        animationbarredevieCore = "70%";
                        break;
                    case 60:
                        animationbarredevieCore = "60%";
                        break;
                    case 50:
                        animationbarredevieCore = "50%";
                        break;
                    case 40:
                        animationbarredevieCore = "40%";
                        break;
                    case 30:
                        animationbarredevieCore = "30%";
                        break;
                    case 20:
                        animationbarredevieCore = "20%";
                        break;
                    case 10:
                        animationbarredevieCore = "10%";
                        break;
                    case 0:
                        {
                            animationbarredevieCore = "0%";
                            //_myGame.Etat = Game1.Etats.EndScreen;
                        }
                        break;
                }

                // Affichage de la vie du joueur
                //Console.WriteLine(player.Life);
                switch (player.Life)
                {
                    case 900:
                        animationbarredeviePlayer = "90%";
                        break;
                    case 800:
                        animationbarredeviePlayer = "80%";
                        break;
                    case 700:
                        animationbarredeviePlayer = "70%";
                        break;
                    case 600:
                        animationbarredeviePlayer = "60%";
                        break;
                    case 500:
                        animationbarredeviePlayer = "50%";
                        break;
                    case 400:
                        animationbarredeviePlayer = "40%";
                        break;
                    case 300:
                        animationbarredeviePlayer = "30%";
                        break;
                    case 200:
                        animationbarredeviePlayer = "20%";
                        break;
                    case 100:
                        animationbarredeviePlayer = "10%";
                        break;
                    case 0:
                        {
                            animationbarredeviePlayer = "0%";
                            //_myGame.Etat = Game1.Etats.EndScreen;
                        }
                        break;
                }

                // Anime zombie
                player.Apparence.Play(animation);
                player.Apparence.Update(deltaSeconds);

                // Anime barre de vie
                barredevieCore.Play(animationbarredevieCore);
                barredevieCore.Update(deltaSeconds);

                // Anime player
                barredeviePlayer.Play(animationbarredeviePlayer);
                barredeviePlayer.Update(deltaSeconds);

                // Anime core
                core.Apparence.Play(animationcore);
                core.Apparence.Update(deltaSeconds);
               
                _myGame._tiledMapRenderer.Update(gameTime);
                
                camera.Follow(player, _myGame);
            }
             
            // PAUSE
            if(screenpause == true)
            {
                if(mouseState.LeftButton== ButtonState.Pressed)
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
                            }
                        }
                    }
                }
            }

            // SHOP
            if (shopoui == true)
            {
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
                    _myGame._spriteBatch.Draw(pause, balle.Hitbox, Color.White);
                    //_myGame._spriteBatch.DrawRectangle(balle.Hitbox, Color.Cyan, 7);
                    _myGame._spriteBatch.Draw(balle.Apparence, balle.Position, Color.White);
                }

                foreach (Zombie zombie in listeZomb)
                {
                    _myGame._spriteBatch.Draw(pause, zombie.Hitbox, Color.White);
                    _myGame._spriteBatch.Draw(rect, zombie.Hitbox, Color.White);
                    _myGame._spriteBatch.Draw(zombie.TextureZomb, zombie.Position);
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
                positionText = new Vector2(20, 10);
                _myGame._spriteBatch.DrawString(_myGame.font, "Temps avant vague suivante: " + Math.Round(chronoVagueSuivante), positionText, Color.YellowGreen);
                positionText = new Vector2(Game1.WIDTH - 500, 10);
                _myGame._spriteBatch.DrawString(_myGame.font, "Zombies Restants : " + listeZomb.Count, positionText, Color.YellowGreen);
                positionText = new Vector2(20, 100);
                _myGame._spriteBatch.DrawString(_myGame.font, "Vague " + numVague, positionText, Color.YellowGreen);
                positionText = new Vector2(20, 200);
                _myGame._spriteBatch.DrawString(_myGame.font, "Argent : " + player.Gold, positionText, Color.YellowGreen);

                _myGame._spriteBatch.End();
                
            }

            if (screenpause == true && shopoui== false)
            {
                Console.WriteLine("test");

                _myGame._spriteBatch.Begin();
                _myGame._spriteBatch.Draw(pause, _pausepos, Color.White);
                _myGame._spriteBatch.End();
            }
        }


    }
}