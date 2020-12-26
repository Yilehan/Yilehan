using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TankWar
{
    class Bomb
    {
        protected int x, y;
        protected int speed;
        protected int dir;
        protected int swing;

        public Bomb(int x, int y)
        {
            this.x = x;
            this.y = y;
            speed = 10;
            Random rand = new Random();
            this.dir = (rand.Next(2) == 0) ? -1 : 1;
            this.swing = rand.Next(10);
        }

        public void Move()
        {
            y += speed;
            x += dir * swing;
            if (y >=600)
            {
                Scenario.bombs.Remove(this);
            }
        }
        public virtual void Draw(Graphics g)
        {
            Image img = Scenario.bombImage;
            g.DrawImage(img, x, y,20,25);
        }
        public virtual Rectangle GetRect()
        {
            return new Rectangle(x, y, 15, 20);
        }
        public virtual void Hit(Tank tank)
        {
            if(this.GetRect().IntersectsWith(tank.GetRect()))
            {
                tank.BeHit();
                Scenario.tankExplode=new Explode(tank.X - 20, tank.Y - 20, 2);
                Scenario.bombs.Remove(this);
            }
        }
    }
}
