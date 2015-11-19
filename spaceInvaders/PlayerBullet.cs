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
        
        private double xPos;
        private double yPos;


        public PlayerBullet()
        {
            bullet = new Image();
            bulletBMS = new BitmapImage(new Uri("ms-appx:///Assets/sprites/player-bullet.png"));

            bulletBMS.ImageOpened += (sender, e) =>
            {
                bullet.Width = bulletBMS.PixelWidth;
                bullet.Height = bulletBMS.PixelHeight;

                Debug.WriteLine("Width: {0}, Height: {1}",
        bullet.Width, bullet.Height);

                
            };

            bullet.Source = bulletBMS;
            
        }

        public void shoot(double X, double Y)
        {
           
            
            Canvas.SetLeft(bullet, X);
            Canvas.SetTop(bullet, Y);

            yPos = Y;

        }

        public void moveUp()
        {

            yPos += -10;
            Canvas.SetTop(bullet, yPos);

        }

        
    }
}
