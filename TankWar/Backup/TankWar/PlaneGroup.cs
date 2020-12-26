using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TankWar
{
    class PlaneGroup
    {
        private List<Plane> planes;

        public List<Plane> Planes
        {
            get { return planes; }
        }
        private int speed;
        private int max;
        private int num;

        public bool IsEmpty
        {
            get { return planes.Count == 0; }
        }
        public PlaneGroup(int layer,int count,int life,int fp)
        {
            Random rand=new Random();
            this.speed = rand.Next(5);
            this.planes = new List<Plane>();
            for (int i = 0; i < layer; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    planes.Add(new Plane(80 * j+30*i, 60 * i+10,life,fp));
                }
            }
            planes.Add(new SuperShip(300, 120,20,fp));
            this.num = 0;
            this.max = rand.Next(10) + 1;
        }

        public void Act(Graphics g)
        {
            if (num < max)
            {
                num++;
            }
            else
            {
                Random rand = new Random();
                this.speed = rand.Next(5);
                this.max = rand.Next(10) + 1;
                num = 0;
            }
            for (int i = 0; i <planes.Count; i++)
            {
                Plane p = planes[i];
                p.Draw(g);
                p.Move(this.speed);
            }
        }
    }
}

