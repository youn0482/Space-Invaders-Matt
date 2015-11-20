using System;
using System.Collections;
using System.Diagnostics;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace spaceInvaders
{
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer dispatcherTimer;

        private Player player;
        private PlayerBullet playerBullet;
        private Invaders invaders;

        private ArrayList invaderGrid;

        private bool playerIsMovingLeft;
        private bool playerIsMovingRight;        

        private int count = 0;
        private readonly double sizeRatio = 0.70;

        public MainPage()
        {
            InitializeComponent();

            player = new Player(sizeRatio);
            playerBullet = new PlayerBullet();

            canvas.Children.Add(player.getPlayer());
            playerIsMovingLeft = playerIsMovingRight = false;

            invaders = new Invaders(sizeRatio);
            invaderGrid = invaders.invaderGrid;

            foreach (Image i in invaderGrid)
            {
                canvas.Children.Add(i);
            }

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += Game;
            dispatcherTimer.Interval = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60);
            dispatcherTimer.Start();

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
        }

        private void Game(object sender, object e)
        {
            if (playerIsMovingLeft) player.moveLeft();
            if (playerIsMovingRight) player.moveRight();

            if (playerBullet.getBulletState()) playerBullet.moveUp();
            else canvas.Children.Remove(playerBullet.bullet);

            invaders.toggleSprite(count);
            invaders.move();

            if (invaders.killCheck(playerBullet.getBulletXpos(), playerBullet.getBulletYpos()))
            {
                //canvas.Children.Remove(Convert.ChangeType(invaders.invaderGrid[0], typeof(Image)));
               
                playerBullet.setBulletState(false);
            }

            count++;
        }

        void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs e)
        {
            if (e.VirtualKey == Windows.System.VirtualKey.Left)
            {
                playerIsMovingRight = false;
                playerIsMovingLeft = true;
            }
            else if (e.VirtualKey == Windows.System.VirtualKey.Right)
            {
                playerIsMovingLeft = false;
                playerIsMovingRight = true;
            }
            else if (e.VirtualKey == Windows.System.VirtualKey.Up)
            {
                if (!playerBullet.getBulletState() && !canvas.Children.Contains(playerBullet.bullet))
                {
                    canvas.Children.Add(playerBullet.getBullet());

                    Debug.WriteLine(playerBullet.getBullet().Width);
                    playerBullet.shoot((Canvas.GetLeft(player.getPlayer())) + ((player.getPlayer().Width / 2) - 2), Canvas.GetTop(player.getPlayer()));
                    playerBullet.setBulletState(true);
                } 
            }
        }
        

        void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs e)
        {
            if (e.VirtualKey == Windows.System.VirtualKey.Left)
            {
                playerIsMovingLeft = false;
            }
            else if (e.VirtualKey == Windows.System.VirtualKey.Right)
            {
                playerIsMovingRight = false;
            }
        }
    }
}
