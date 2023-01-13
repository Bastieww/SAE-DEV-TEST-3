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
                            zombie.VieZombie -= player.Damage;
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



   

        public void EscapingObstacles(Zombie zombie, List<Walls> walls, Vector2 direction, Vector2 move)
        {
            foreach (Walls wall in walls)
            {
                if (zombie.Hitbox.Intersects(wall.Hitbox))
                {
                    if (zombie.DernierMurTouche != wall)
                    {
                        zombie.DernierMurTouche = wall;
                        zombie.Memorisation(Point.Zero, "");
                    }
                    
                        
                    zombie.Position -= move;
                    zombie.UpdateHitbox();


                    Point hautGaucheZombie = zombie.Hitbox.Location;
                    Point hautDroiteZombie = zombie.Hitbox.Location + new Point(zombie.Hitbox.Width, 0);
                    Point basGaucheZombie = zombie.Hitbox.Location + new Point(0, zombie.Hitbox.Height);
                    Point basDroiteZombie = zombie.Hitbox.Location + new Point(zombie.Hitbox.Width, zombie.Hitbox.Height);


                    Point hautGaucheWall = wall.Hitbox.Location;
                    Point hautDroiteWall = wall.Hitbox.Location + new Point(wall.Hitbox.Width, 0);
                    Point basGaucheWall = wall.Hitbox.Location + new Point(0, wall.Hitbox.Height);
                    Point basDroiteWall = wall.Hitbox.Location + new Point(wall.Hitbox.Width, wall.Hitbox.Height);
                    /*
                    Console.WriteLine(hautGaucheZombie.ToString()+ " : "+ hautGaucheWall.ToString()+ "\n"+ hautDroiteZombie.ToString()+ " : "+ hautDroiteWall.ToString()+ "\n"+ basGaucheZombie.ToString()+ " : "+ basGaucheWall.ToString()+ "\n"+ basDroiteZombie.ToString()+ " : "+ basDroiteWall.ToString() + "\n" + (hautDroiteWall.X + zombie.Hitbox.Width).ToString() + "\n" +(hautGaucheWall.X - zombie.Hitbox.Width).ToString() + "\n" + (wall.Hitbox.Contains(basGaucheZombie) && (wall.Hitbox.Contains(basDroiteZombie))) +"\n" + (wall.Hitbox.Contains(hautGaucheZombie) && (wall.Hitbox.Contains(hautDroiteZombie))));
                    Console.WriteLine(" ");
                    Console.WriteLine((basGaucheZombie.X <= hautDroiteWall.X));
                    Console.WriteLine(basDroiteZombie.X < hautDroiteWall.X + zombie.Hitbox.Width);
                    Console.WriteLine(basGaucheZombie.X > hautGaucheWall.X - zombie.Hitbox.Width);
                    Console.WriteLine((wall.Hitbox.Contains(basGaucheZombie + new Point(0, 5)) && (wall.Hitbox.Contains(basDroiteZombie + new Point(0, 5)))));
                    Console.WriteLine((wall.Hitbox.Contains(hautGaucheZombie + new Point(0, -5)) && (wall.Hitbox.Contains(hautDroiteZombie + new Point(0, -5)))));
                    if ((wall.Hitbox.Contains(hautGaucheZombie + new Point(0, -5)) && (wall.Hitbox.Contains(hautDroiteZombie + new Point(0, -5)))))
                        Console.Read();

                    Console.WriteLine((basGaucheZombie.X <= hautDroiteWall.X && basDroiteZombie.X < hautDroiteWall.X + zombie.Hitbox.Width && basGaucheZombie.X > hautGaucheWall.X - zombie.Hitbox.Width && ((wall.Hitbox.Contains(basGaucheZombie + new Point(0, -5)) && (wall.Hitbox.Contains(basDroiteZombie + new Point(0, -5)))) || (wall.Hitbox.Contains(hautGaucheZombie + new Point(0, 5)) && (wall.Hitbox.Contains(hautDroiteZombie + new Point(0, 5)))))));
                    if (basGaucheZombie.X <= hautDroiteWall.X && basDroiteZombie.X < hautDroiteWall.X + zombie.Hitbox.Width && basGaucheZombie.X > hautGaucheWall.X - zombie.Hitbox.Width && ((wall.Hitbox.Contains(basGaucheZombie+ new Point(0,-5)) && (wall.Hitbox.Contains(basDroiteZombie + new Point(0, -5)))) || (wall.Hitbox.Contains(hautGaucheZombie + new Point(0, 5)) && (wall.Hitbox.Contains(hautDroiteZombie + new Point(0, 5)))))) 


                    {
                        
                        if(zombie.Cible.X - zombie.Hitbox.Center.X <= 0)
                            deplacement = new Vector2(-1, 0);
                        else
                            deplacement = new Vector2(1, 0);
                    }
                    else 
                    {
                        if (zombie.Cible.Y - zombie.Hitbox.Center.Y <= 0)
                            deplacement = new Vector2(0, -1);
                        else
                            deplacement = new Vector2(0, 1);
                    }
                    */
                    
                    
                        if (zombie.MemoireDirection == "")
                        {

                            if (direction.X > 0)
                            {
                                zombie.Position += new Vector2(1, 0);
                                zombie.UpdateHitbox();
                                if (zombie.Hitbox.Intersects(wall.Hitbox))
                                {
                                    zombie.Position -= new Vector2(1, 0);
                                    zombie.UpdateHitbox();
                                }
                                else
                                {
                                    zombie.Memorisation(hautDroiteWall, "droite");
                                }

                            }
                            else if (direction.X > 0)
                            {
                                zombie.Position += new Vector2(-1, 0);
                                zombie.UpdateHitbox();
                                if (zombie.Hitbox.Intersects(wall.Hitbox))
                                {
                                    zombie.Position -= new Vector2(-1, 0);
                                    zombie.UpdateHitbox();
                                }
                                else
                                {
                                    zombie.Memorisation(hautGaucheWall, "gauche");
                                }
                            }
                            if (zombie.MemoireDirection == "")
                            {
                                if (direction.Y > 0)
                                {
                                    zombie.Position += new Vector2(0, 1);
                                    zombie.UpdateHitbox();
                                    if (zombie.Hitbox.Intersects(wall.Hitbox))
                                    {
                                        zombie.Position -= new Vector2(0, 1);
                                        zombie.UpdateHitbox();
                                    }
                                    else
                                    {
                                        zombie.Memorisation(basDroiteWall, "bas");
                                    }

                                }
                                else
                                {
                                    zombie.Position += new Vector2(0, -1);
                                    zombie.UpdateHitbox();
                                    if (zombie.Hitbox.Intersects(wall.Hitbox))
                                    {
                                        zombie.Position -= new Vector2(0, -1);
                                        zombie.UpdateHitbox();
                                    }
                                    else
                                    {
                                        zombie.Memorisation(hautGaucheWall - new Point(0, zombie.Hitbox.Height), "haut");
                                    }
                                }
                            }
                        }

                        switch (zombie.MemoireDirection)
                        {
                            case "droite":
                                zombie.Position += new Vector2(1, 0);
                                zombie.UpdateHitbox();

                                if (hautGaucheZombie.X > zombie.MemoireCoinADepasser.X)
                                {
                                    zombie.MemoireDirection = "";
                                }
                                break;

                            case "gauche":
                                zombie.Position += new Vector2(-1, 0);
                                zombie.UpdateHitbox();

                                if (hautDroiteZombie.X < zombie.MemoireCoinADepasser.X)
                                {
                                    zombie.MemoireDirection = "";
                                }
                                break;

                            case "bas":
                                zombie.Position += new Vector2(0, 1);
                                zombie.UpdateHitbox();

                                if (hautDroiteZombie.Y > zombie.MemoireCoinADepasser.Y)
                                {
                                    zombie.MemoireDirection = "";
                                }
                                break;

                            case "haut":
                                zombie.Position += new Vector2(0, -1);
                                zombie.UpdateHitbox();

                                if (hautDroiteZombie.Y < zombie.MemoireCoinADepasser.Y)
                                {
                                    zombie.MemoireDirection = "";
                                }
                                break;
                        }
                        Console.WriteLine(zombie.MemoireCoinADepasser.ToString() + " ; " + zombie.MemoireDirection);
                    }

                }
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
