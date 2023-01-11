using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Project1
{
    // Ce code est inspiré, pour la partie "Translation", d'un tutoriel sur YouTube : 
    // https://www.youtube.com/watch?v=ceBCDKU_mNw
    public class Camera
    {
        private float gaucheDroite, hautBas;
        public Matrix Transform { get; private set; }

        public void Follow (Player player, Game1 gameScreen)
        {
            // Verif de la pos de la du player
            if (player.Position.X <= Game1.WIDTH / 2 - player.Hitbox.Width / 2)
                gaucheDroite = -Game1.WIDTH / 2;
            else if (player.Position.X >= (gameScreen._tiledMap.Width * gameScreen._tiledMap.TileWidth) - Game1.WIDTH / 2 - player.Hitbox.Width / 2)
                gaucheDroite = -(gameScreen._tiledMap.Width * gameScreen._tiledMap.TileWidth) + Game1.WIDTH / 2;
            else
                gaucheDroite = -player.Position.X - (player.Hitbox.Width / 2);

            if (player.Position.Y <= Game1.HEIGHT / 2 - player.Hitbox.Height / 2)
                hautBas = -Game1.HEIGHT / 2;
            else if (player.Position.Y >= (gameScreen._tiledMap.Height * gameScreen._tiledMap.TileHeight) - Game1.HEIGHT / 2 - player.Hitbox.Height / 2)
                hautBas = -(gameScreen._tiledMap.Height * gameScreen._tiledMap.TileHeight) + Game1.HEIGHT / 2;
            else
                hautBas = -player.Position.Y - (player.Hitbox.Height / 2);

            // Creation vecteurs pur deplacer la camera
            var position = Matrix.CreateTranslation(gaucheDroite, hautBas, 0);

            var translation = Matrix.CreateTranslation(Game1.WIDTH / 2, Game1.HEIGHT / 2, 0);

            Transform = position * translation;

        }
    }
}
