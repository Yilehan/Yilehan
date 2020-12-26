using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TankWar
{
    class SuperShip:Plane
    {

        public SuperShip(int x, int y, int l,int fp):base(x,y,l,fp)
        {
            //this.life = 20;
        }
        public override void Draw(Graphics g)
        {
            Image img = null;
            if (this.firePower <= 1)
            {
               img = Scenario.superShipImage1;
            }
            else if (this.firePower <= 2)
            {
                img = Scenario.superShipImage2;
            }
            else
            {
                img = Scenario.superShipImage3;
            }
            
            g.DrawImage(img, x, y, 128, 128);
        }
        public override Rectangle GetRect()
        {
            return new Rectangle(x+20, y+20, 80, 80);
        }
        public override void Move(int speed)
        {
            base.Move(speed);
            x += dir;
            if (x > 450) dir = -1;
            if (x < 250) dir = 1;
            //Random rand = new Random((int)DateTime.Now.Ticks);
            //if (rand.Next(200) > 199 - this.firePower)
            //{
            //    Scenario.bombs.Add(new Bomb(this.x + 64, this.y + 128));
            //}
        }
    }
}
