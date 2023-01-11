using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Sprites;
using System;
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
        

        
        public void CollisionBalleZombie(ref List<Bullet> bullets, ref List<Zombie> zombies)
        {
            toucheBalleZombie = false;
            foreach (Bullet bullet in bullets)
            {
                if (toucheBalleZombie == false)
                {
                    foreach (Zombie zombie in zombies)
                    {
                        if (zombie.Hitbox.Intersects(bullet.Hitbox))
                        {
                            bullets.Remove(bullet);
                            zombies.Remove(zombie);
                            toucheBalleZombie = true;
                            break;
                        }
                    }
                }
                break;
            }
        }
        public void CollisionZombiePlayer(ref List<Zombie> zombies, ref Player player)
        {
            foreach (Zombie zombie in zombies)
            {
                if (zombie.Hitbox.Intersects(player.Hitbox))
                {
                    player.Life -= 1;
                }
            }
        }
        public void CollisionZombieCore(ref List<Zombie> zombies, ref Core core)
        {
            foreach (Zombie zombie in zombies)
            {
                if (zombie.Hitbox.Intersects(core.Hitbox))
                {
                    core.Life -= 1;
                }
            }
        }

        public bool CollisionZombieWall( Zombie zombie,  List<Walls> walls)
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

        public bool CollisionPlayerWall( Player player, List<Walls> walls)
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

        public bool CollisionBulletWall( Bullet bullet,  List<Walls> walls)
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
