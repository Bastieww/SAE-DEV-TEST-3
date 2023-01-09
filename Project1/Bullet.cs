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
    internal class Bullet
    {
        private Vector2 position;
        private Vector2 coordClick;

        private Vector2 direction;

        public const int SPEED = 1;
        
        private Rectangle hitbox;
        private Texture2D apparence;



        public Bullet(GameScreen gamescreen, Player player, Vector2 coordClick)
        {
            this.Position = player.Position;
            this.CoordClick = coordClick;
            this.Hitbox = hitbox;
            this.Apparence = apparence;
            this.Direction = new Vector2(player.Position.X - CoordClick.X, player.Position.Y - CoordClick.Y);
            //Code non opti, il faudrait charger la texture qu'une seule fois par balle
            LoadContent(gamescreen);
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

        public Vector2 CoordClick
        {
            get
            {
                return this.coordClick;
            }

            set
            {
                this.coordClick = value;
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

        public Vector2 Direction
        {
            get
            {
                return this.direction;
            }

            set
            {
                this.direction = value;
            }
        }
        
        public void LoadContent(GameScreen gamescreen)
        {
            Texture2D apparence = gamescreen.Content.Load<Texture2D>("zombie");
            this.Apparence = apparence;
        }
    }
}
