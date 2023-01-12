using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Sprites;
using System;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Project1
{
    internal class Collisions
    {
        private bool toucheBalleZombie;

        public Collisions()
        {
        }

        public void CollisionBalleZombie(List<Bullet> bullets, List<Zombie> zombies, Player player)
        {
            bool touche = false;
            List<Bullet> bulletsToRemove = new List<Bullet>();

            foreach (Bullet bullet in bullets)
            {
                List<Zombie> zombiesToRemove = new List<Zombie>();
                if (!touche)
                {
                    foreach (Zombie zombie in zombies)
                    {
                        if (zombie.Hitbox.Intersects(bullet.Hitbox))
                        {
                            Console.WriteLine("Intersect");

                            bulletsToRemove.Add(bullet);
                            zombie.VieZombie -= 5;
                            if (zombie.VieZombie <= 0)
                            {
                                zombiesToRemove.Add(zombie);
                                player.Gold += 3;
                            }
                            touche = true;
                            break;
                        }
                    }
                }
                zombiesToRemove.ForEach(zombie => zombies.Remove(zombie));
            }

            bulletsToRemove.ForEach(bullet=> bullets.Remove(bullet));
        }

        public bool CollisionBalleOutside(Bullet bullet,TiledMap map, Player player)
        {
            if(bullet.Position.X < player.Position.X - Game1.WIDTH / 2 || 
                bullet.Position.X > player.Position.X + Game1.WIDTH/2 ||
                bullet.Position.Y < player.Position.Y - Game1.WIDTH / 2 ||
                bullet.Position.Y > player.Position.Y + Game1.WIDTH / 2)
            {
                return true;
            }
            return false;
        }
        public void CollisionZombiePlayer(List<Zombie> zombies, Player player)
        {
            foreach (Zombie zombie in zombies)
            {
                if (zombie.Hitbox.Intersects(player.Hitbox))
                {
                    player.Life -= 1;
                }
            }
        }
        public bool CollisionZombieCore(List<Zombie> zombies, Core core)
        {
            foreach (Zombie zombie in zombies)
            {
                if (zombie.Hitbox.Intersects(core.Hitbox))
                {
                    core.Life -= 1;
                    return true;
                }
            }
            return false;
        }

        public bool CollisionZombieWall(Zombie zombie,  List<Walls> walls)
        {
            Vector2 deplacement = Vector2.Zero;
            Point hautGaucheZombie = zombie.Hitbox.Location;
            Point hautDroiteZombie = zombie.Hitbox.Location + new Point(zombie.Hitbox.Width,0);
            Point basGaucheZombie = zombie.Hitbox.Location + new Point(0,zombie.Hitbox.Height);
            Point basDroiteZombie = zombie.Hitbox.Location + new Point(zombie.Hitbox.Width, zombie.Hitbox.Height);
            



            foreach (Walls wall in walls)
            {
                //PAS LE CHOIX FAUT FAIRE LES 3 checks par truc, la j'ai fais que les premiers

               if(zombie.Hitbox.Intersects(wall.Hitbox))
               {
                    Point hautGaucheWall = wall.Hitbox.Location;
                    Point hautDroiteWall = wall.Hitbox.Location + new Point(wall.Hitbox.Width, 0);
                    Point basGaucheWall = wall.Hitbox.Location + new Point(0, wall.Hitbox.Height);
                    Point basDroiteWall = wall.Hitbox.Location + new Point(wall.Hitbox.Width, wall.Hitbox.Height);
                    if ((basGaucheZombie.X <= hautDroiteWall.X) && (basDroiteZombie.X < hautDroiteWall.X))
                    {
                        deplacement = new Vector2(-1, 0);
                    }
                    else if ((hautDroiteZombie.X >= basGaucheWall.X) && (hautGaucheZombie.X < basDroiteWall.X))
                    {
                        deplacement = new Vector2(1, 0);
                    }
               }
                    
                
            }
            //a enlever
            return false;
            

        }

        public bool CollisionPlayerWall(Player player, List<Walls> walls)
        {
            
            foreach (Walls wall in walls)
            {
               
                if (player.Hitbox.Intersects(wall.Hitbox))
                {
                    
                    return true;
                }
            }
            return false;
        }

        public bool CollisionBulletWall(Bullet bullet, List<Walls> walls)
        {
            foreach (Walls wall in walls)
            {
                if (bullet.Hitbox.Intersects(wall.Hitbox))
                {
                    return true;
                }
            }
            return false;

        }



    }
}
