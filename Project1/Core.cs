using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.VectorDraw;

namespace Project1
{
    internal class Core
    {
        public int life;

        private Vector2 position;

        private Rectangle hitbox;
        private Texture2D apparence;

        public Core(GameScreen gamescreen)
        {
            LoadContent(gamescreen);
            this.Life = 20;
            this.Position = new Vector2(500,500);// A REMPLACER PAR LA MOITIE DE LA MAP
            this.Hitbox = hitbox;
            

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

        public Texture2D Apparence
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
            Texture2D apparence = gamescreen.Content.Load<Texture2D>("zombie");
            this.Apparence = apparence;
        }
    }
}
