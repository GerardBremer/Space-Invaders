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
        Texture2D lasertexture;
        Color[] shipTextureData;
        Vector2 shipPosition;
        const int shipMoveSpeed = 5;
        private ShipLaser laser;

        // The sub-rectangle of the drawable area which should be visible
        Rectangle safeBounds;
        // Percentage of the screen on every side is the safe area
        const float SafeAreaPortion = 0.05f;

        public List<Vector2> lasers = new List<Vector2>();

        public static bool Shoot = false;

        Random random = new Random();

        public Player(int x, int y) 
        {
            //Load spaceship
            ship = Global.content.Load<Texture2D>("Textures\\spaceship");
            lasertexture = Global.content.Load<Texture2D>("Textures\\laser");

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

            laser = new ShipLaser(x, y);
        }

        public void Update(GameTime gameTime)
        {
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
            if (keyboard.IsKeyDown(Keys.Space))
            {
                laser.laserPositions.Add(new Vector2(shipPosition.X + 15, shipPosition.Y));
                
            }
            laser.Update(gameTime);

                // Prevent the ship from moving off of the screen
                shipPosition.X = MathHelper.Clamp(shipPosition.X,
                    safeBounds.Left, safeBounds.Right - ship.Width);
        }

        public void Draw(GameTime gameTime)
        {
            // Draw spaceship
            Global.spriteBatch.Draw(ship, shipPosition, Color.White);

            laser.Draw(gameTime);
        }
    }
}
