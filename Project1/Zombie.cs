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
    internal class Zombie : Game1
    {
        private AnimatedSprite _textureZombNormal, _textureZombGros, _textureZombRapide;
        private Random rd = new Random();
        private Vector2[] _positionZombie;
        private int vieZombie, vitesseZombie, XposZomb, YposZomb;
        private int nbZombies = 0, nbZombMax = 5;
        private string typeZombie;
        public const int VIE_NORMAL = 25, VIE_GROS = 50, VIE_RAPIDE = 10;
        public const int VITESSE_NORMAL = 25, VITESSE_GROS = 10, VITESSE_RAPIDE = 50;


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
        protected override void Initialize()
        {
            // pt spawn chaque zombie
            for (int i = 0; i < nbZombMax; i++)
            {
                int XspawnGauche = rd.Next(-250, -50);
                int XspawnDroite = rd.Next(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Width + 150);
                int YspawnHaut = rd.Next(-250, -50);
                int YspawnBas = rd.Next(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Width + 150);
                if (XspawnGauche < XspawnDroite)
                    XposZomb = XspawnDroite;
                else
                    YposZomb = XspawnDroite;
                if (YspawnBas < YspawnHaut)
                    YposZomb = YspawnHaut;
                else
                    XposZomb = XspawnDroite;
                _positionZombie[i] = new Vector2(XposZomb, YposZomb);

            }
        }
        
        protected override void LoadContent()
        {
            for (int i = 0; i < nbZombMax; i++)
            {
                switch (this.TypeZombie)
                {
                    case "Normal":
                        vieZombie = Zombie.VIE_NORMAL;
                        vitesseZombie = Zombie.VITESSE_NORMAL;
                        SpriteSheet spriteSheet = Content.Load<SpriteSheet>("zombie.sf", new JsonContentLoader());
                        _textureZombNormal = new AnimatedSprite(spriteSheet);
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
        protected override void Update(GameTime gameTime)
        {
        }
    }
}
