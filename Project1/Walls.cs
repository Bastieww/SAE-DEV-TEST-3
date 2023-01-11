using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Walls
    {
        private Game1 _myGame;
        private Stream descriptionMap;
        private StreamReader lecteurMap;
        private string line;
        private bool lineChecker;
        public List<string> acceptedCharacters;
        public List<char> mapDescripteur;
        private const string CHEMINACCESMAP = "../../../Content/map.tmx";

        
        private Rectangle hitbox;

       

        public Walls(Game1 game, Rectangle rectangle)
        {
            _myGame = game;
            
            this.Hitbox = rectangle;
            
            
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

     

        public List<Walls> ChargementMap()
        {
            
            List<Walls> listeWalls = new List<Walls>();

            descriptionMap = TitleContainer.OpenStream(CHEMINACCESMAP);
            lecteurMap = new StreamReader(descriptionMap);
            mapDescripteur = new List<char>();
            

            //HERE FOR TILES ID FOR LAYERS OBSTACLES, MUST BE CHANGED AT THE END OF ChargementMap too
            acceptedCharacters = new List<string>() { "0", "5", "," };

            while (lecteurMap.EndOfStream == false)
            {
                lineChecker = true;
                line = lecteurMap.ReadLine();


                foreach (char c in line)
                {
                    if (!acceptedCharacters.Contains(c.ToString()))
                    {
                        lineChecker = false;

                        break;
                    }
                }
                if (lineChecker)
                {
                    foreach (char c in line)
                    {
                        if (c.ToString() != ",")
                        {
                            mapDescripteur.Add(c);
                        }
                    }

                }
            }
            
            int idTile = 0;
            foreach (char c in mapDescripteur)
            {
                if (c.ToString() == "5")
                {
                    
                    Rectangle rectangle = new Rectangle(
                        _myGame._tiledMap.TileWidth * (idTile % _myGame._tiledMap.Width),
                        _myGame._tiledMap.TileHeight * (idTile / _myGame._tiledMap.Width),
                        _myGame._tiledMap.TileWidth,
                        _myGame._tiledMap.TileHeight);
      
                    Walls wall = new Walls(_myGame, rectangle);
                    
                    listeWalls.Add(wall);
                    
                    

                   
                }
                idTile++;
            }
            return listeWalls;
        }
        

    }
}
