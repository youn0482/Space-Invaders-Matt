using System;
using System.Collections;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace spaceInvaders
{
    class Invaders
    {
        public ArrayList invaderGrid;

        private readonly Uri alien1AUri = new Uri("ms-appx:///Assets/sprites/alien-1-1.png");
        private readonly Uri alien1BUri = new Uri("ms-appx:///Assets/sprites/alien-1-2.png");
        private readonly Uri alienDeadUri = new Uri("ms-appx:///Assets/sprites/explosion.png");

        private BitmapImage alien1A;
        private BitmapImage alien1B;
        private BitmapImage alienDead;
        

        private bool invadersAreMovingLeft;

        double speed = 1;
        int invaderKillIndex;

        public Invaders(double sizeRatio)
        {
            invadersAreMovingLeft = false;

            alien1A = new BitmapImage(alien1AUri);
            alien1B = new BitmapImage(alien1BUri);
            alienDead = new BitmapImage(alienDeadUri);

            invaderGrid = new ArrayList();

            for (int r = 1; r < 12; r++)
            {
                for (int c = 1; c < 6; c++)
                {
                    Image invader = new Image();

                    alien1A.ImageOpened += (sender, e) =>
                    {
                        invader.Width = alien1A.PixelWidth * sizeRatio;
                        invader.Height = alien1A.PixelHeight * sizeRatio;
                    };

                    Canvas.SetLeft(invader, 40 * r);
                    Canvas.SetTop(invader, 40 * c);

                    invaderGrid.Add(invader);
                    invader.Source = alien1A;
                }
            }
        }

        public void move()
        {
            if (wallCollision())
            {
                if (invadersAreMovingLeft)
                {
                    invadersAreMovingLeft = false;
                    moveRight();
                }
                else if (!invadersAreMovingLeft)
                {
                    invadersAreMovingLeft = true;
                    moveLeft();
                }
                else
                {

                }
            }

            if (invadersAreMovingLeft) moveLeft();
            else moveRight();
        }

        public void moveLeft()
        {
            foreach (Image i in invaderGrid)
            {
                Canvas.SetLeft(i, Canvas.GetLeft(i) - speed);
            }
        }

        public void moveRight()
        {
            foreach (Image i in invaderGrid)
            {
                Canvas.SetLeft(i, Canvas.GetLeft(i) + speed);
            }
        }

        public void moveDown()
        {
            foreach (Image i in invaderGrid)
            {
                Canvas.SetTop(i, Canvas.GetTop(i) + (i.Height / 2));
                
            }
            speed += 0.20;
        }

        public void toggleSprite(double c)
        {
            if (c % (30 / Math.Round(speed)) == 1)
            {
                if (alien1A.UriSource == alien1AUri)
                {
                    alien1A.UriSource = alien1BUri;
                }
                else alien1A.UriSource = alien1AUri;
            }
        }

        public bool wallCollision()
        {
            foreach (Image i in invaderGrid)
            {
                if (Canvas.GetLeft(i) <= 0 || Canvas.GetLeft(i) >= Window.Current.Bounds.Width - i.Width)
                {
                    Debug.WriteLine("down");
                    moveDown();
                    return true;
                }
            }
            return false;
        }

        public bool killCheck(double bulletX, double bulletY)
        {
            var counter = 0;

            foreach (Image i in invaderGrid)
            {
                if ((Canvas.GetLeft(i) <= bulletX) && ((Canvas.GetLeft(i) + i.Width) >= bulletX))
                {
                    if ((Canvas.GetTop(i) + i.Height) >= bulletY)
                    {
                        i.Source = alienDead;
                        invaderKillIndex = counter;
                        return true;
                    }
                }

                counter++;
            }
            return false;
        }

       
        public int getKillIndex()
        {
          
            return invaderKillIndex;
        }


    }
}
