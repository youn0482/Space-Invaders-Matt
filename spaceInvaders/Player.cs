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
    class Player
    {
        public Image turret;
        public BitmapImage turretBMS;

        private double xPos;
        private double yPos;
        
        public Player()
        {
            turretBMS = new BitmapImage(new Uri("ms-appx:///Assets/sprites/player.png"));
            turret = new Image();
            turretBMS.ImageOpened += (sender, e) =>
                {
                    turret.Width = turretBMS.PixelWidth;
                    turret.Height = turretBMS.PixelHeight;

                    xPos = Window.Current.Bounds.Width / 2;
                    yPos = Window.Current.Bounds.Height - (turret.Height + 20);

                    Canvas.SetLeft(turret, xPos);
                    Canvas.SetTop(turret, yPos);
                };

            
            turret.Source = turretBMS;
          
        }

        public void moveRight()
        {
            xPos += 4;
            Canvas.SetLeft(turret, xPos);
        }

        public void moveLeft()
        {
            xPos += -4;
            Canvas.SetLeft(turret, xPos);
        }

        public double getPlayerX()
        {
            return xPos;
        }
        public double getPlayerY()
        {
            return yPos;
        }
    }
}
