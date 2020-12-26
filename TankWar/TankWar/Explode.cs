using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TankWar
{
    class Explode
    {
        private int x, y;
        private int count;
        private int type;

        public Explode(int x, int y,int type)
        {
            this.x = x-5;
            this.y = y-5;
            this.type = type;
            this.count = 0;
        }
        public void Draw(Graphics g)
        {
            if (this.type == 1)
            {
                //string imgFileName = "..\\..\\images\\explosion2.png";
                Image img = Scenario.explodeImage1;
                Rectangle destRect = new Rectangle(x, y, 60, 60);
                int i = count / 4;
                int j = count % 4;
                Rectangle srcRect = new Rectangle(j * 40, i * 40, 40, 40);
                GraphicsUnit units = GraphicsUnit.Pixel;
                g.DrawImage(img, destRect, srcRect, units);

            }
            else
            {
                Image img = Scenario.explodeImage2;
                Rectangle destRect = new Rectangle(x, y, 96, 96);
                int i = count / 4;
                int j = count % 4;
                Rectangle srcRect = new Rectangle(j * 96, i * 96, 96, 96);
                GraphicsUnit units = GraphicsUnit.Pixel;
                g.DrawImage(img, destRect, srcRect, units);

            }

        }

        public void Expand()
        {
            count++;
            if (this.type == 1)
            {
                if (count > 8)
                {
                    Scenario.explodes.Remove(this);
                }
            }
            else
            {
                if (count > 16)
                {
                    Scenario.tankExplode=null;
                }

            }
        }
    }
}
