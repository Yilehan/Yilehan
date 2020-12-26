using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TankWar
{
    class Gift:Bomb
    {
        private int giftType = 1;

        public Gift(int x,int y,int type):base(x,y)
        {
            this.speed = 5;
            if (type >= 1 && type <= 4) this.giftType = type;
        }

        public override void Draw(Graphics g)
        {
            Image img =null;
            switch(this.giftType)
            {
                case 1:
                    img = Scenario.giftImage1;
                    break;
                case 2:
                    img = Scenario.giftImage2;
                    break;
                case 3:
                    img = Scenario.giftImage3;
                    break;
                case 4:
                    img = Scenario.giftImage4;
                    break;
            }
            g.DrawImage(img, x, y, 32, 32);
        }

        public override Rectangle GetRect()
        {
            return new Rectangle(x, y, 32, 32);
        }

        public override void Hit(Tank tank)
        {
            if (this.GetRect().IntersectsWith(tank.GetRect()))
            {
                tank.GetGift(this.giftType);
                Scenario.bombs.Remove(this);
            }

        }
    }
}
