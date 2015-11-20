using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace spaceInvaders
{
    class PlayerBullet
    {

        public Image bullet;
        public BitmapImage bulletBMS;

        public bool liveBullet;

        private double xPos;
        private double yPos;


        public PlayerBullet()
        {
            liveBullet = false;
            xPos = 0;
            yPos = 0;

            bullet = new Image();
            bulletBMS = new BitmapImage(new Uri("ms-appx:///Assets/sprites/player-bullet.png"));

            bulletBMS.ImageOpened += (sender, e) =>
            {
                bullet.Width = bulletBMS.PixelWidth;
                bullet.Height = bulletBMS.PixelHeight;

                Debug.Write("loaded da bullet");
            };

            bullet.Source = bulletBMS;
            Debug.Write("default");

        }


        

        public void shoot(double X, double Y)
        {


            Canvas.SetLeft(bullet, X);
            Canvas.SetTop(bullet, Y);

            yPos = Y;
            xPos = X;

            liveBullet = true;

        }

        public void moveUp()
        {

            yPos += -10;
            Canvas.SetTop(bullet, yPos);

            if (yPos <= 0)
            {
                setBulletState(false);
                xPos = 0;
                yPos = 0;
            }

        }

        public void setBulletState(bool state)
        {
            if (!state)
            {
                xPos = 0;
                yPos = 0;
            }

            liveBullet = state;
        }

        public bool getBulletState()
        {
            return liveBullet;
        }
        public double getBulletXpos()
        {

            return xPos;
        }
        public double getBulletYpos()
        {

            return yPos;
        }

        public Image getBullet()
        {
            return bullet;
        }


    }
}



