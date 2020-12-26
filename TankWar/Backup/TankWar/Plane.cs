using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TankWar
{
    class Plane
    {
        protected int x, y;
        protected int firePower;
        protected int life;
        public int Y
        {
            get { return y; }
        }

        public int X
        {
            get { return x; }
        }
        protected int dir;

        public Plane(int x,int y,int life,int fp)
        {
            this.x = x;
            this.y = y;
            this.dir = 1;
            this.firePower = fp;
            this.life = life;
        }
        public void BeHit()
        {
            this.life--;
            if (this.life == 0)
            {
                Scenario.planeGroup.Planes.Remove(this);
            }
        }

        public virtual void Draw(Graphics g)
        {
            Image img = Scenario.planeImage;
            g.DrawImage(img, x, y, 40, 40);
        }
        public virtual Rectangle GetRect()
        {
            return new Rectangle(x, y, 40, 40);
        }
        public virtual void Move(int speed)
        {
            x += dir * speed;
            if (x <= 0 || x >= 750) dir = -dir;
            Random rand = new Random();
            int r = (this.GetHashCode() + rand.Next(200)) % 200;
            if (r > 196-this.firePower)
            {
                Scenario.bombs.Add(new Bomb(this.x + 15, this.y + 30));
            }
        }


    }
}
