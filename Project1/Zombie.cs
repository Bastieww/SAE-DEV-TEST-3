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
    public class Zombie
    {
        public const int VIE_NORMAL = 25, VIE_GROS = 50, VIE_RAPIDE = 10;
        public const int VITESSE_NORMAL = 100, VITESSE_GROS = 2, VITESSE_RAPIDE = 8;
        public const int PUISSANCE_NORMAl = 3, PUISSANCE_RAPIDE = 8, PUISSANCE_GROS = 11;

        private int vieZombie, vitesseZombie, XposZomb, YposZomb;
        private int width = 91;
        private int height = 177;

        private string typeZombie;

        private AnimatedSprite textureZomb;
        
        private Random rd = new Random();

        private Rectangle hitbox;

        private Vector2 position;

        private Vector2 cible;

        private string memoireDirection;
        private  Point memoireCoinADepasser;
        private Walls dernierMurTouche;


        public Zombie(GameScreen gamescreen, string typeZombie, TiledMap map)
        {
            this.TypeZombie = typeZombie;
            LoadContent(gamescreen);
            
            int Xspawn = rd.Next(0, map.WidthInPixels);
            int Yspawn = rd.Next(0, map.WidthInPixels);
            int coteSpawn = rd.Next(4);
            if (coteSpawn == 0)
            {   
                XposZomb = Xspawn;
                YposZomb = - 180;
            }
            else if (coteSpawn == 1)
            {
                XposZomb = Xspawn;
                YposZomb = map.HeightInPixels;
            }
            else if (coteSpawn == 2)
            {
                XposZomb = -90;
                YposZomb = Yspawn;
            }
            else
            {
                XposZomb = map.WidthInPixels;
                YposZomb = Yspawn;
            }

            this.Position = new Vector2(XposZomb,YposZomb);
            

            this.Cible = new Vector2(0, 0);

            this.MemoireDirection = "";
            this.memoireCoinADepasser = Point.Zero;

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
        public int VieZombie
        {
            get
            {
                return this.vieZombie;
            }

            set
            {
                this.vieZombie = value;
            }
        }
        public int VitesseZombie
        {
            get
            {
                return this.vitesseZombie;
            }

            set
            {
                this.vitesseZombie = value;
            }
        }

        public string MemoireDirection
        {
            get
            {
                return this.memoireDirection;
            }

            set
            {
                this.memoireDirection = value;
            }
        }

        public Point MemoireCoinADepasser
        {
            get
            {
                return this.memoireCoinADepasser;
            }

            set
            {
                this.memoireCoinADepasser = value;
            }
        }

        internal Walls DernierMurTouche
        {
            get
            {
                return this.dernierMurTouche;
            }

            set
            {
                this.dernierMurTouche = value;
            }
        }

        private void LoadContent(GameScreen gamescreen)
        {
            // Type du zombie
            switch (this.TypeZombie)
            {
                case "Normal":
                    this.VieZombie = Zombie.VIE_NORMAL;
                    this.VitesseZombie = Zombie.VITESSE_NORMAL;
                    SpriteSheet apparence = gamescreen.Content.Load<SpriteSheet>("zombie-NormalAnim.sf", new JsonContentLoader());
                    this.TextureZomb = new AnimatedSprite(apparence);
                    break;
                case "Rapide":
                    this.VieZombie = Zombie.VIE_RAPIDE;
                    this.VitesseZombie = Zombie.VITESSE_RAPIDE;
                    apparence = gamescreen.Content.Load<SpriteSheet>("zombie-NinjaAnim.sf", new JsonContentLoader());
                    this.TextureZomb = new AnimatedSprite(apparence);
                    break;
                case "Gros":
                    this.VieZombie = Zombie.VIE_GROS;
                    this.VitesseZombie = Zombie.VITESSE_GROS;
                    apparence = gamescreen.Content.Load<SpriteSheet>("zombie-CasquetteAnim.sf", new JsonContentLoader());
                    this.TextureZomb = new AnimatedSprite(apparence);
                    break;
            }
        }
        public void UpdateHitbox()
        {
            this.Hitbox = new Rectangle((int)Position.X - width / 2, (int)Position.Y - height / 2, this.width, this.height);
        }

        public void Memorisation(Point coin, string direction)
        {
            this.MemoireCoinADepasser = coin;
            this.MemoireDirection = direction;
        }
    }
}
