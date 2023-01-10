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
    public class Player 
    {
        private int life = 3;
        private int gold = 0;
        private int speed = 400;
        private int damage = 1;

        //ATTENTION : JE NE SAVAIS PAS COMMENT RECUPERER LA TAILLE D'UNE SEULE TUILE SUR UNE SPRITE SHEET
        //DONC JE L'AI MESURE A LA MAIN; IL FAUT DONC MODIFIER CES VALEURS EN CAS DE CHANGEMENT DE SPRITE
        private int width = 91;
        private int height = 177;

      
        private AnimatedSprite apparence;

        private Vector2 position;
        
        private Rectangle hitbox;

        
        public Player(GameScreen gamescreen)
        {
            LoadContent(gamescreen);

            this.Life = life;
            this.Gold = gold;
            this.Speed = speed;
            this.Damage = damage;
            
            this.Position = new Vector2(Game1.WIDTH / 3, Game1.HEIGHT / 2);
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, this.width, this.height);
        }


        public int Life
        {
            get
            {
                return this.life;
            }

            set
            {
                this.life = value;
            }
        }

        public int Gold
        {
            get
            {
                return this.gold;
            }

            set
            {
                this.gold = value;
            }
        }

        public int Speed
        {
            get
            {
                return this.speed;
            }

            set
            {
                this.speed = value;
            }
        }

        public int Damage
        {
            get
            {
                return this.damage;
            }

            set
            {
                this.damage = value;
            }
        }

        public AnimatedSprite Apparence
        {
            get
            {
                return this.apparence;
            }

            set
            {
                this.apparence = value;
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

       

        public void LoadContent(GameScreen gamescreen)
        {
            SpriteSheet apparence = gamescreen.Content.Load<SpriteSheet>("playerbase.sf", new JsonContentLoader());
            this.Apparence = new AnimatedSprite(apparence);
        }
        
        
    }
}
