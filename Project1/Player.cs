using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace Project1
{
    internal class Player 
    {
        private int life = 3;
        private int gold = 0;
        private int speed = 100;
        private int damage = 1;

        private AnimatedSprite apparence;

        public Player(GameScreen gamescreen)
        {
            LoadContent(gamescreen);

            this.Life = life;
            this.Gold = gold;
            this.Speed = speed;
            this.Damage = damage;
            this.Apparence = apparence;

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
        public void LoadContent(GameScreen gamescreen)
        {
            SpriteSheet apparence = gamescreen.Content.Load<SpriteSheet>("animation.sf", new JsonContentLoader());
            this.Apparence = new AnimatedSprite(apparence);
        }
    }
}
