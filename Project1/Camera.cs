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
    // Ce code est largement inspiré d'un tutoriel sur YouTube : 
    // https://www.youtube.com/watch?v=ceBCDKU_mNw
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow (Player joueur)
        {
            var position = Matrix.CreateTranslation(-joueur.Position.X - (joueur.Hitbox.Width / 2), -joueur.Position.Y - (joueur.Hitbox.Height / 2), 0);

            var translation = Matrix.CreateTranslation(Game1.WIDTH / 2, Game1.HEIGHT / 2, 0);

            Transform = position * translation;
        }
    }
}
