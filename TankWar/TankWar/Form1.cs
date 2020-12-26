using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TankWar
{
    public partial class FrmMain : Form
    {
        private int waitCount;
        private bool pause = false;

        public FrmMain()
        {
            InitializeComponent();
            waitCount = 0;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (pause)
            {
                int level = Scenario.level;
                e.Graphics.DrawString("Pause.....", new Font("Tohoma", 30), Brushes.Red, 310, 200);
                return;
            }

            if (Scenario.planeGroup.IsEmpty)
            {
                int level = Scenario.level;
                e.Graphics.DrawString("Level "+level, new Font("Tohoma", 30), Brushes.Red, 310, 200);
                return;
            }
            if (Scenario.myTank == null)
            {
                e.Graphics.DrawString("Game Over!", new Font("Tohoma", 30), Brushes.Red, 260, 200);
                return;
            }
            Scenario.myTank.Draw(e.Graphics);
            e.Graphics.DrawRectangle(Pens.YellowGreen, 5, 560, 100, 10);
            e.Graphics.FillRectangle(Brushes.Red, 6, 561, Scenario.myTank.Life*10, 8);
            Scenario.planeGroup.Act(e.Graphics);
            for(int i=0;i<Scenario.missiles.Count;i++)
            {
                Missile m = Scenario.missiles[i];
                m.Draw(e.Graphics);
                m.Move();
                m.Hit(Scenario.planeGroup);
            }
            for (int i = 0; i < Scenario.bombs.Count; i++)
            {
                Bomb b = Scenario.bombs[i];
                b.Draw(e.Graphics);
                b.Move();
                if (Scenario.myTank != null) b.Hit(Scenario.myTank);
            }
            for (int i = 0; i < Scenario.explodes.Count; i++)
            {
                Explode ex = Scenario.explodes[i];
                ex.Draw(e.Graphics);
                ex.Expand();
            }
            if (Scenario.tankExplode != null)
            {
                Scenario.tankExplode.Draw(e.Graphics);
                Scenario.tankExplode.Expand();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Scenario.myTank = new Tank();
            Scenario.planeGroup = new PlaneGroup(1, 2,1,1);
            //Cursor.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Scenario.planeGroup.IsEmpty )
            {
                if (waitCount == 0) Scenario.level++;
                if (waitCount > 50)
                {

                    if (Scenario.level < 4)
                    {
                        Scenario.planeGroup = new PlaneGroup(1, 1 + Scenario.level, 3,1);
                    }
                    else if (Scenario.level < 8)
                    {
                        Scenario.planeGroup = new PlaneGroup(2, 1 + Scenario.level - 4, 6,1);
                    }
                    else if (Scenario.level < 16)
                    {
                        Scenario.planeGroup = new PlaneGroup(2, 5, Scenario.level - 4,1);
                    }
                    else
                    {
                        Scenario.planeGroup = new PlaneGroup(2, 5, 3, Scenario.level-14);
                    }
                    Scenario.explodes.Clear();
                    Scenario.missiles.Clear();
                    Scenario.bombs.Clear();
                    waitCount = 0;
                }
                else
                {
                    waitCount++;
                }
                
            }
            this.Refresh();
        }

        
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Scenario.myTank != null) Scenario.myTank.X = e.X - 25;
        }


        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (Scenario.myTank != null) Scenario.myTank.fire();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void FrmMain_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.P:
                    pause = !pause;
                    timer1.Enabled = !timer1.Enabled;
                    break;
                case Keys.A:
                    Scenario.myTank.Life = 10;
                    break;
                case Keys.X:
                    Scenario.myTank.FirePower=100;
                    break;
            }
        }


    }
}