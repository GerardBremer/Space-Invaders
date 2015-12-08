using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    /// <summary>
    /// This is the main type for your game
    /// Rienk Werkt aan het menu
    /// Gerard werkt aan van alles en nog wat
    /// Robin werkt aan de UFO + van alles en nog wat
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        static public SpriteBatch spriteBatch;
        static public ContentManager content;

        // Tilemap
        TileMap myMap = new TileMap();
        int squaresAcross = 80;
        int squaresDown = 80;

        // Aliens
        private Aliens aliens;

        // Player
        private Player player;
        private ShipLaser plaser;

        // UFO - Werk Robin aan
        private UFO ufo;

        // For when a collision is detected
        bool shipHit = false;
        //bool alienHit = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Size of application window
            graphics.PreferredBackBufferHeight = 624;
            graphics.PreferredBackBufferWidth = 848;

            //TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 100);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.f
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;

            base.Initialize();

            // Pass often referenced variables to Global
            Global.GraphicsDevice = GraphicsDevice;
            Global.content = Content;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Global.spriteBatch = spriteBatch;

            // Aliens x amount of object on y amount of roads
            aliens = new Aliens(8, 4); 
            // Player
            player = new Player(1, 1);
        }
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here

            //Load tileset
            Tile.TileSetTexture = Content.Load<Texture2D>("Textures\\TileSets\\part1_tileset3");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Update game objects
            aliens.Update(gameTime);
            player.PlayerUpdate(gameTime);
      
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            // Change the background to red when the player was hit by a laser
            GraphicsDevice.Clear(Color.White);

            if (shipHit == true)
            {GraphicsDevice.Clear(Color.Red);}
            else
            {GraphicsDevice.Clear(Color.CornflowerBlue);}

            spriteBatch.Begin();

                // Draw tileset
                Vector2 firstSquare = new Vector2(Camera.Location.X / 32, Camera.Location.Y / 32);
                int firstX = (int)firstSquare.X;
                int firstY = (int)firstSquare.Y;

                Vector2 squareOffset = new Vector2(Camera.Location.X % 32, Camera.Location.Y % 32);
                int offsetX = (int)squareOffset.X;
                int offsetY = (int)squareOffset.Y;

                for (int y = 0; y < squaresDown; y++)
                {
                    for (int x = 0; x < squaresAcross; x++)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,  
                            new Rectangle
                                ((x * 16) - offsetX, (y * 16) // Space between tiles
                                - offsetY, 16, 16),           // Size of tiles
                            Tile.GetSourceRectangle(myMap.Rows[y + firstY].Columns[x + firstX].TileID),
                            Color.White);
                    }
                }

                aliens.AliensDraw(gameTime);
                player.PlayerDraw(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}