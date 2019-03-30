using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Conquest
{
    public partial class GamePage : PhoneApplicationPage
    {
        ContentManager contentManager;
        GameTimer timer;
        SpriteBatch spriteBatch;
        Texture2D _backgroundImage;
        SpriteFont _font;

        public GamePage()
        {
            InitializeComponent();

            // Get the content manager from the application
            contentManager = (Application.Current as App).Content;

            // Create a timer for this page
            timer = new GameTimer();
            timer.UpdateInterval = TimeSpan.FromTicks(333333);
            timer.Update += OnUpdate;
            timer.Draw += OnDraw;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Set the sharing mode of the graphics device to turn on XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(true);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(SharedGraphicsDeviceManager.Current.GraphicsDevice);

            this._backgroundImage = contentManager.Load<Texture2D>(@"Images\SimpleWorldMap");
            this._font = contentManager.Load<SpriteFont>(@"Fonts\HeaderFont");

            // Start the timer
            timer.Start();

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Stop the timer
            timer.Stop();

            // Set the sharing mode of the graphics device to turn off XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(false);

            base.OnNavigatedFrom(e);
        }

        /// <summary>
        /// Allows the page to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        private void OnUpdate(object sender, GameTimerEventArgs e)
        {
        }

        /// <summary>
        /// Allows the page to draw itself.
        /// </summary>
        private void OnDraw(object sender, GameTimerEventArgs e)
        {
            SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.Black);
            Viewport deviceViewport = SharedGraphicsDeviceManager.Current.GraphicsDevice.Viewport;
            this.spriteBatch.Begin();
            this.spriteBatch.Draw(this._backgroundImage, new Rectangle(0, 0, deviceViewport.Width, deviceViewport.Height), Color.White);
            this.spriteBatch.DrawString(this._font, "Conquest", new Vector2(300, 50), Color.White);
            this.spriteBatch.End();
        }
    }
}