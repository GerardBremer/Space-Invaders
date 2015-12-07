using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    class Player : Microsoft.Xna.Framework.Game
    {
        Texture2D ship;
        Color[] shipTextureData;
        Vector2 shipPosition;
        const int shipMoveSpeed = 5;
        private ShipLaser laser;

        // The sub-rectangle of the drawable area which should be visible
        Rectangle safeBounds;
        // Percentage of the screen on every side is the safe area
        const float SafeAreaPortion = 0.05f;

        public Player(int x, int y) 
        {
            //Load spaceship texture
            ship = Global.content.Load<Texture2D>("Textures\\spaceship");

            // Texture data
            shipTextureData =
                new Color[ship.Width * ship.Height];
            ship.GetData(shipTextureData);

            // Calculate safe bounds based on current resolution
            Viewport viewport = Global.GraphicsDevice.Viewport;
            safeBounds = new Rectangle(
                (int)(viewport.Width * SafeAreaPortion),
                (int)(viewport.Height * SafeAreaPortion),
                (int)(viewport.Width * (1 - 1 * SafeAreaPortion)),
                (int)(viewport.Height * (1 - 1 * SafeAreaPortion)));
            // Start the player in the center along the bottom of the screen
            shipPosition.X = (safeBounds.Width - ship.Width) / 2;
            shipPosition.Y = safeBounds.Height - ship.Height;

            // Initialize ship laser
            laser = new ShipLaser(x, y);
        }

        public void PlayerUpdate(GameTime gameTime)
        {
            // Get the bounding rectangle of the ship
            Rectangle shipRectangle =
               new Rectangle((int)shipPosition.X, (int)shipPosition.Y,
                   ship.Width, ship.Height);

            // Get input from keyboard
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
            // Shoot with space
            if (keyboard.IsKeyDown(Keys.Space))
            {
                // Shoot new laser when last laser has been removed. 
                if (laser.nLasers < 1)
                {
                    laser.laserPositions.Add(new Vector2(shipPosition.X + 15, shipPosition.Y -10));
                }
            }
            // Update laser
            laser.ShiplaserUpdate(gameTime);

                // Prevent the ship from moving off of the screen
                shipPosition.X = MathHelper.Clamp(shipPosition.X,
                    safeBounds.Left, safeBounds.Right - ship.Width);
        }

        public void PlayerDraw(GameTime gameTime)
        {
            // Draw laser
            laser.ShiplaserDraw(gameTime);
            
            // Draw spaceship
            Global.spriteBatch.Draw(ship, shipPosition, Color.White);
        }
    }
}
