using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace Space_Invaders
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        static public SpriteBatch spriteBatch;
        static public ContentManager content;

        TileMap myMap = new TileMap();
        int squaresAcross = 80;
        int squaresDown = 80;

        Texture2D ship;
        Texture2D laser;

        // The color data for the images; used for per pixel collision
        Color[] shipTextureData; 
        Color[] laserTextureData;

        // Ship
        Vector2 shipPosition;
        const int shipMoveSpeed = 5;

        // Lasers
        List<Vector2> laserPositions = new List<Vector2>();
        float LaserSpawnProbability = 0.005f;
        const int LaserSpeed = 5;


        // Aliens
        //private List<Alien> aliens = new List<Alien>();
       // private const int nColInvaders = 5;
        //private int nInvaders = 3 * nColInvaders - 1;
        private Aliens aliens;

        private Rectangle alienrectangle = new Rectangle();

        // random for laser spawns
        Random random = new Random();

        // For when a collision is detected
        bool shipHit = false;
        //bool alienHit = false;

        // The sub-rectangle of the drawable area which should be visible
        Rectangle safeBounds;
        // Percentage of the screen on every side is the safe area
        const float SafeAreaPortion = 0.05f;

        public Game1()
        { 
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 624;
            graphics.PreferredBackBufferWidth = 848;

            //TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 100);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
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

            aliens = new Aliens(7, 4);

            // Add aliens to list
            //for (int i = 0; i <= nInvaders; i++)
            {
                //aliens.Add(new Alien((i % nColInvaders) * 80 + 112, (i / nColInvaders) * 64 + 49)); 
                // Description of numbers: 
                // 1. Space between aliens
                // 2. x coordinate
                // 3. y coordinate
                //aliens.Add(new Alien(i * 80 + 112, 49));  // Row 1
                //aliens.Add(new Alien(i * 80 + 112, 113)); // Row 2
                //aliens.Add(new Alien(i * 80 + 112, 177)); // Row 3
                //aliens.Add(new Alien(i * 80 + 112, 241)); // Row 4
            }

            // Calculate safe bounds based on current resolution
            Viewport viewport = graphics.GraphicsDevice.Viewport;
            safeBounds = new Rectangle(
                (int)(viewport.Width * SafeAreaPortion),
                (int)(viewport.Height * SafeAreaPortion),
                (int)(viewport.Width * (1 - 1 * SafeAreaPortion)),
                (int)(viewport.Height * (1 - 1 * SafeAreaPortion)));
            // Start the player in the center along the bottom of the screen
            shipPosition.X = (safeBounds.Width - ship.Width) / 2;
            shipPosition.Y = safeBounds.Height - ship.Height;
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

            //Load spaceship
            ship = Content.Load<Texture2D>("Images\\spaceship");

            // Load laser
            laser = Content.Load<Texture2D>("Images\\laser");

            // Extract collision data
            // laser
            laserTextureData =
                new Color[laser.Width * laser.Height];
            laser.GetData(laserTextureData);
            // ship
            shipTextureData =
                new Color[ship.Width * ship.Height];
            ship.GetData(shipTextureData);
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
            // Get input
            KeyboardState keyboard = Keyboard.GetState();
            // TODO: Add your update logic here

            // Move ship left and right with arrow keys
            if (keyboard.IsKeyDown(Keys.Left))
            {
                shipPosition.X -= shipMoveSpeed;
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                shipPosition.X += shipMoveSpeed;
            }

            // Update game objects
            aliens.Update(gameTime);
            //foreach (Alien a in aliens)
              //  a.Update();

            // Prevent the ship from moving off of the screen
            shipPosition.X = MathHelper.Clamp(shipPosition.X,
                safeBounds.Left, safeBounds.Right - ship.Width);

            // Spawn new lasers
            if (random.NextDouble() < LaserSpawnProbability)
            {
                float x = (float)random.NextDouble() *
                    (Window.ClientBounds.Width - laser.Width);
                laserPositions.Add(new Vector2(x, -laser.Height));
            }

            // Get the bounding rectangle of the ship
            Rectangle shipRectangle =
                new Rectangle((int)shipPosition.X, (int)shipPosition.Y,
                ship.Width, ship.Height);

            // Update each laser
            shipHit = false;
            for (int i = 0; i < laserPositions.Count; i++)
            {
                // Animate this laser
                laserPositions[i] =
                    new Vector2(laserPositions[i].X,
                                laserPositions[i].Y + LaserSpeed);

                // Get the bounding rectangle of this laser
                Rectangle blockRectangle =
                    new Rectangle((int)laserPositions[i].X, (int)laserPositions[i].Y,
                    laser.Width, laser.Height);

                // Check collision with ship
                if (CollisionDetection.IntersectPixels(shipRectangle, shipTextureData,
                                    blockRectangle, laserTextureData))
                {
                    shipHit = true;
                }

               

                // Remove this laser if it has gone off the screen
                if (laserPositions[i].Y > Window.ClientBounds.Height)
                {
                    laserPositions.RemoveAt(i);

                    // When removing a block, the next block will have the same index
                    // as the current block. Decrement i to prevent skipping a block.
                    i--;
                }
            }
                base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            // Change the background to red when the person was hit by a block
            if (shipHit)
            {
                GraphicsDevice.Clear(Color.Red);
            }
            else
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
            }

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

            // Draw spaceship
            spriteBatch.Draw(ship, shipPosition, Color.White);

            // Draw lasers  
            foreach (Vector2 laserPosition in laserPositions)
            {
                spriteBatch.Draw(laser, laserPosition, Color.White);
            }

            aliens.Draw(gameTime);
            //foreach (Alien a in aliens)
              //  a.Draw();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}
