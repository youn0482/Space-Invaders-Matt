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
        private Image turret;
        private BitmapImage turretBitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/sprites/player.png"));

        private double xPos;
        private double yPos;

        public Player(double sizeRatio)
        {
            turret = new Image();
            turretBitmapImage.ImageOpened += (sender, e) =>
            {
                turret.Width = turretBitmapImage.PixelWidth * sizeRatio;
                turret.Height = turretBitmapImage.PixelHeight * sizeRatio;

                xPos = Window.Current.Bounds.Width / 2;
                yPos = Window.Current.Bounds.Height - (turret.Height * 2);

                Canvas.SetLeft(turret, xPos);
                Canvas.SetTop(turret, yPos);
            };

            turret.Source = turretBitmapImage;
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

        public Image getPlayer()
        {
            return turret;
        }
    }
}
