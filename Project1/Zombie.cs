using System;
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
    public class Zombie : Game
    {
        public const int VIE_NORMAL = 25, VIE_GROS = 50, VIE_RAPIDE = 10;
        public const int VITESSE_NORMAL = 25, VITESSE_GROS = 10, VITESSE_RAPIDE = 50;
        private int vieZombie, vitesseZombie, XposZomb, YposZomb;
        private string typeZombie;
        private Random rd = new Random();
        private Vector2 _positionZombie;

        private AnimatedSprite textureZomb;

        public Zombie(GameScreen gamescreen, string typeZombie)
        {
            this.TypeZombie = typeZombie;
            this.TextureZomb = textureZomb;
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
        private void LoadContent(GameScreen gamescreen)
        {
            // Type du zombie
            switch (this.TypeZombie)
            {
                case "Normal":
                    vieZombie = Zombie.VIE_NORMAL;
                    vitesseZombie = Zombie.VITESSE_NORMAL;
                    SpriteSheet apparence = gamescreen.Content.Load<SpriteSheet>("zombieAnim.sf", new JsonContentLoader());
                    this.TextureZomb = new AnimatedSprite(apparence);
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

            // pt spawn zombie
            int XspawnGauche = rd.Next(50, 250);
            int XspawnDroite = rd.Next(0, 50);
            int YspawnHaut = rd.Next(0, 50);
            int YspawnBas = rd.Next(0, 250);
            if (XspawnGauche < XspawnDroite)
                XposZomb = XspawnDroite;
            else
                XposZomb = XspawnGauche;
            if (YspawnBas < YspawnHaut)
                YposZomb = YspawnHaut;
            else
                YposZomb = YspawnBas;

            _positionZombie = new Vector2(XposZomb, YposZomb);
        }
    }
}
