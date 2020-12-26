using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TankWar
{
    class Tank
    {
        private int x, y;

        public int Y
        {
            get { return y; }
        }
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        private int life;

        public int Life
        {
            get { return life; }
            set { life = value; }
        }
        private int firePower;

        public int FirePower
        {
            get { return firePower; }
            set { firePower = value; }
        }
        private bool hasShield;
        private int defenseCount;
        public Tank()
        {
            x = 380;
            y = 530;
            this.life = 10;
            this.firePower = 0;
            this.hasShield = false;
            this.defenseCount = 0;
        }

        public void Draw(Graphics g)
        {
            if (this.hasShield)
            {
                this.defenseCount++;
                if (this.defenseCount > 500)
                {
                    this.hasShield = false;
                    this.defenseCount = 0;
                }
                Pen p = new Pen(Brushes.Yellow, 2);
                g.DrawRectangle(p, x + 5, y + 5, 40, 40);
            }
            Image img = Scenario.tankImage;
            g.DrawImage(img, x, y);
        }
        public Rectangle GetRect()
        {
            return new Rectangle(x, y, 40, 40);
        }
        public void fire()
        {
            Missile m2 = new Missile(x + 20, y, 0);
            Scenario.missiles.Add(m2);
            if (this.firePower > 0)
            {
                Missile m1 = new Missile(x + 10, y, -5);
                Scenario.missiles.Add(m1);
                Missile m3 = new Missile(x + 30, y, 5);
                Scenario.missiles.Add(m3);
            }
        }

        public  void BeHit()
        {
            if (this.hasShield) return;
            this.life--;
            if(this.firePower>0) this.firePower--;
            if (this.life == 0)
            {
                Scenario.myTank = null;
                Scenario.explodes.Clear();
            }
        }

        public void GetGift(int p)
        {
            switch (p)
            {
                case 1:
                    if (this.life < 10) this.life++;
                    break;
                case 2:
                    if (this.life < 9) this.life+=2;
                    break;
                case 3:
                    if (this.firePower < 1) this.firePower = 3;
                    break;
                case 4:
                    this.hasShield = true;
                    break;
            }
        }
    }
}
