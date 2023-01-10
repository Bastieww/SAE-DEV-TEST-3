﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace Project1
{
    public class Zombie
    {
        public const int VIE_NORMAL = 25, VIE_GROS = 50, VIE_RAPIDE = 10;
        public const int VITESSE_NORMAL = 25, VITESSE_GROS = 10, VITESSE_RAPIDE = 50;

        private int vieZombie, vitesseZombie, XposZomb, YposZomb, nbzombie = 0;

        private string typeZombie;

        private Random rd = new Random();

        private AnimatedSprite textureZomb;

        private Rectangle hitbox;

        private Vector2 position;

        private Vector2 cible;


        public Zombie(GameScreen gamescreen, string typeZombie)
        {
            this.TypeZombie = typeZombie;
            this.TextureZomb = textureZomb;
            
            int XspawnGauche = rd.Next(-250, -10);
            int XspawnDroite = rd.Next(Game1.WIDTH, Game1.WIDTH + 250);
            int YspawnHaut = rd.Next(-250, -30);
            int YspawnBas = rd.Next(Game1.HEIGHT, Game1.HEIGHT + 250);
            if (XspawnGauche < XspawnDroite)
                XposZomb = XspawnDroite;
            else
                XposZomb = XspawnGauche;
            if (YspawnBas < YspawnHaut)
                YposZomb = YspawnHaut;
            else
                YposZomb = YspawnBas;
           
            this.Position = new Vector2(XposZomb,YposZomb);

            this.Cible = new Vector2(0, 0);

            
            
            LoadContent(gamescreen);
        }


        public string TypeZombie
        {
            get
            {
                return this.typeZombie;
            }

            set
            {
                this.typeZombie = value;
                switch (this.TypeZombie)
                {
                    case "Normal":
                        vieZombie = Zombie.VIE_NORMAL;
                        vitesseZombie = Zombie.VITESSE_NORMAL;
                        break;
                    case "Rapide":
                        vieZombie = Zombie.VIE_RAPIDE;
                        vitesseZombie = Zombie.VITESSE_RAPIDE;
                        break;
                    case "Gros":
                        vieZombie = Zombie.VIE_GROS;
                        vitesseZombie = Zombie.VITESSE_GROS;
                        break;
                }
            }
        }
        public AnimatedSprite TextureZomb
        {
            get
            {
                return this.textureZomb;
            }

            set
            {
                this.textureZomb = value;
            }
        }
        public Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
            }
        }

        public Vector2 Cible
        {
            get
            {
                return this.cible;
            }

            set
            {
                this.cible = value;
            }
        }

        public Rectangle Hitbox
        {
            get
            {
                return this.hitbox;
            }

            set
            {
                this.hitbox = value;
            }
        }

        private void LoadContent(GameScreen gamescreen)
        {
            // Type du zombie
            switch (this.TypeZombie)
            {
                case "Normal":
                    SpriteSheet apparence = gamescreen.Content.Load<SpriteSheet>("zombieAnim.sf", new JsonContentLoader());
                    this.TextureZomb = new AnimatedSprite(apparence);
                    break;
            }
        }
        public void UpdateHitbox()
        {
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, 200, 200);
        }
    }
}
