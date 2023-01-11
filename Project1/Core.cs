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
       

        private AnimatedSprite apparence;    


        public Core(GameScreen gamescreen, TiledMap map)
        {
            LoadContent(gamescreen);
            this.Life = 100;
            this.Position = new Vector2((map.Width*map.TileWidth)/2+100,(map.Height* map.TileHeight)/2);
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y,200,200);
            

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
            SpriteSheet apparence = gamescreen.Content.Load<SpriteSheet>("heart.sf", new JsonContentLoader());
            this.Apparence = new AnimatedSprite(apparence);
        }
    }
}
