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

namespace SAE
{
    internal class Player
    {
        //private sprite 
        private SpriteBatch spriteBatch;
        private string pseudo;
        private int points_de_vie;
        private int vitesse;
        private int coins;

        public Player(string pseudo, int points_de_vie, int vitesse, int coins)
        {
            this.Pseudo = pseudo;
            this.Points_de_vie = points_de_vie;
            this.Vitesse = vitesse;
            this.Coins = coins;
        }

        public string Pseudo
        {
            get
            {
                return this.pseudo;
            }

            set
            {
                this.pseudo = value;
            }
        }

        public int Points_de_vie
        {
            get
            {
                return this.points_de_vie;
            }

            set
            {
               
                this.points_de_vie = value;
            }
        }

        public int Vitesse
        {
            get
            {
                return this.vitesse;
            }

            set
            {
                this.vitesse = value;
            }
        }

        public int Coins
        {
            get
            {
                return this.coins;
            }

            set
            {
                if (value != 0)
                    throw new ArgumentException("Le Joueur ne peut pas avoir des coins au debut de la partie");
                this.coins = value;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Player player &&
                   this.Pseudo == player.Pseudo &&
                   this.Points_de_vie == player.Points_de_vie &&
                   this.Vitesse == player.Vitesse &&
                   this.Coins == player.Coins;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Pseudo, this.Points_de_vie, this.Vitesse, this.Coins);
        }

        public override string ToString()
        {
            return base.ToString();
        }


         

    }
}
