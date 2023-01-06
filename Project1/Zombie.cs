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
        private static int nbZombies = 0;
        private int vieZombie, vitesseZombie, XposZomb, YposZomb;
        private string typeZombie, _textureZomb;
        private Random rd = new Random();
        private Vector2 _positionZombie;

        public Zombie(string typeZombie)
        {
            this.TypeZombie = typeZombie;
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
        public void CreationZombie()
        {
            // pt spawn chaque zombie
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

            //_spriteBatch = new SpriteBatch(GraphicsDevice);
            switch (this.TypeZombie)
            {
                case "Normal":
                    vieZombie = Zombie.VIE_NORMAL;
                    vitesseZombie = Zombie.VITESSE_NORMAL;
                    _textureZomb = "Normal";
                    break;
                case "Rapide":
                    vieZombie = Zombie.VIE_RAPIDE;
                    vitesseZombie = Zombie.VITESSE_RAPIDE;
                    _textureZomb = "Rapide";
                    break;
                case "Gros":
                    vieZombie = Zombie.VIE_GROS;
                    vitesseZombie = Zombie.VITESSE_GROS;
                    _textureZomb = "Gros";
                    break;
            }
        }
        public string StatZombie()
        {
            return _textureZomb;
        }
    }
}
