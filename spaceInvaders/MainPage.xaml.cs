using System;
using System.Diagnostics;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace spaceInvaders
{
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer dispatcherTimer;
        private Player player;
        private PlayerBullet bullet;
        private bool playerIsMovingLeft;
        private bool playerIsMovingRight;
        private bool liveBullet;

        public MainPage()
        {
            InitializeComponent();            

            player = new Player();
            bullet = new PlayerBullet();

            canvas.Children.Add(player.turret);
            playerIsMovingLeft = playerIsMovingRight = liveBullet = false;

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += Game;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(15);
            dispatcherTimer.Start();

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;

            //Draws alien grid
            //Will eventually be moved inside a method or class
            for (int y = 0; y < 10; y++)
            {
                for(int x = 0; x < 5; x++)
                {
                    Image invader = new Image();
                    invader.Width = 32;
                    invader.Height = 32;
                    invader.Source = new BitmapImage(new Uri("ms-appx:///Assets/sprites/alien-1-1.png")); ;
                    canvas.Children.Add(invader);
                    Canvas.SetLeft(invader, 60 * y);
                    Canvas.SetTop(invader, 60 * x);
                }
            }
        }

        private void Game(object sender, object e)
        {
            if (playerIsMovingLeft) player.moveLeft();
            if (playerIsMovingRight) player.moveRight();
            if (liveBullet) bullet.moveUp();
        }

        void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs e)
        {
            if(e.VirtualKey == Windows.System.VirtualKey.Left)
            {
                playerIsMovingRight = false;
                playerIsMovingLeft = true;

            } else if(e.VirtualKey == Windows.System.VirtualKey.Right)
            {
                playerIsMovingLeft = false;
                playerIsMovingRight = true;
            }else if (e.VirtualKey == Windows.System.VirtualKey.Up)
            {
                canvas.Children.Add(bullet.bullet);

               
                bullet.shoot((player.getPlayerX()) + ((player.turret.Width / 2) - 2), player.getPlayerY());
                liveBullet = true;
                
            }
        }

        void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs e)
        {
            if (e.VirtualKey == Windows.System.VirtualKey.Left)
            {
                playerIsMovingLeft = false;
            } else if (e.VirtualKey == Windows.System.VirtualKey.Right)
            {
                playerIsMovingRight = false;
            }
        }

    }
}
