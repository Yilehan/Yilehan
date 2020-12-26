using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TankWar
{
    static class Scenario
    {
        public static int level = 1;
        public static Tank myTank;
        public static List<Missile> missiles = new List<Missile>();
        public static List<Explode> explodes = new List<Explode>();
        public static Explode tankExplode = null;
        public static List<Bomb> bombs = new List<Bomb>();
        public static PlaneGroup planeGroup;
        
        public static Image tankImage = Image.FromFile(@"..\..\images\tank6.gif");
        public static Image planeImage = Image.FromFile(@"..\..\images\spaceship.png");
        public static Image missileImage = Image.FromFile(@"..\..\images\plasmashot.png");
        public static Image bombImage = Image.FromFile(@"..\..\images\powerup_gun.png");
        public static Image explodeImage1 = Image.FromFile(@"..\..\images\explosion2.png");
        public static Image explodeImage2 = Image.FromFile(@"..\..\images\explosion.png");
        public static Image superShipImage1 = Image.FromFile(@"..\..\images\ship5.png");
        public static Image superShipImage2 = Image.FromFile(@"..\..\images\ship4.png");
        public static Image superShipImage3 = Image.FromFile(@"..\..\images\ship1.png");
        public static Image giftImage1 = Image.FromFile(@"..\..\images\gift1.png");
        public static Image giftImage2 = Image.FromFile(@"..\..\images\gift2.png");
        public static Image giftImage3 = Image.FromFile(@"..\..\images\gift3.png");
        public static Image giftImage4 = Image.FromFile(@"..\..\images\gift4.png");

        static Scenario()
        {
            Scenario.planeImage.RotateFlip(RotateFlipType.Rotate180FlipX);
            Scenario.missileImage.RotateFlip(RotateFlipType.Rotate180FlipX);
            Scenario.superShipImage3.RotateFlip(RotateFlipType.Rotate180FlipX);
        }
    }

}
