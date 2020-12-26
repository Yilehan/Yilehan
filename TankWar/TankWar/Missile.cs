using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TankWar
{
    class Missile
    {
        private int x, y;
        private int speed;
        private int vSpeed;

        public Missile(int x,int y,int v)
        {
            this.x = x;
            this.y = y;
            speed = 30;
            vSpeed = v;
        }

        public void Move()
        {
            y -= speed;
            x += vSpeed;
            if (y < 0)
            {
                Scenario.missiles.Remove(this);
            }
        }
        public void Draw(Graphics g)
        {
            Image img =Scenario.missileImage;
            g.DrawImage(img, x, y,15,20);
        }
        public Rectangle GetRect()
        {
            return new Rectangle(x, y, 15, 20);
        }
        public void Hit(PlaneGroup group)
        {
            for (int i = 0; i < group.Planes.Count; i++)
            {
                if(this.GetRect().IntersectsWith(group.Planes[i].GetRect()))
                {
                    int eX = group.Planes[i].X;
                    int eY = group.Planes[i].Y;
                    if(group.Planes[i].GetType()==typeof(SuperShip))
                    {
                        eX += 60;
                        eY += 60;
                    }
                    Scenario.explodes.Add(new Explode(eX,eY,1));
                    Scenario.missiles.Remove(this);
                    group.Planes[i].BeHit();
                    Random rand = new Random();
                    int t = rand.Next(1000);
                    if (t > 800)
                    {
                        int type = 1;
                        if (t > 800 && t < 840) type = 3;
                        if (t == 840 || t == 841) type = 4;
                        if (t >841 && t<930) type = 1;
                        if (t >= 930) type = 2;
                        Scenario.bombs.Add(new Gift(eX, eY, type));
                    }
                    return;
                }
            }
        }
    }
}
