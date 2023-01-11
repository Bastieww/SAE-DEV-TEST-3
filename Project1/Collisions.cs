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
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Collisions
    {
        private bool toucheBalleZombie;

        public Collisions()
        {
        }
        

        
        public void CollisionBalleZombie(List<Bullet> bullets, List<Zombie> zombies)
        {
            bool touche = false;
            foreach (Bullet bullet in bullets)
            {
                if (touche == false)
                {

                    foreach (Zombie zombie in zombies)
                    {
                        if (zombie.Hitbox.Intersects(bullet.Hitbox))
                        {
                            Console.WriteLine("Intersect");

                            bullets.Remove(bullet);
                            zombies.Remove(zombie);

                            touche = true;
                            break;

                        }
                    }
                }
                break;
            }
           
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
        public void CollisionZombieCore(List<Zombie> zombies, Core core)
        {
            foreach (Zombie zombie in zombies)
            {
                if (zombie.Hitbox.Intersects(core.Hitbox))
                {
                    core.Life -= 1;
                }
            }
        }

        public bool CollisionZombieWall(Zombie zombie,  List<Walls> walls)
        {
            foreach (Walls wall in walls)
            {
               
                if(zombie.Hitbox.Intersects(wall.Hitbox))
                {
                    return true;
                }
            }
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
