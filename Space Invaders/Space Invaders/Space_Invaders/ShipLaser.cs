using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Space_Invaders
{
    public class ShipLaser : Microsoft.Xna.Framework.Game
    {
        public Texture2D laser;
        Color[] laserTextureData;

        public List<Vector2> laserPositions = new List<Vector2>();
        const int LaserSpeed = -5;
        public int nLasers;
        public bool alienHit;
        private Alien alien;

        public ShipLaser(int x, int y)
        {                         
            // Load laser texture
            laser = Global.content.Load<Texture2D>("Textures\\laser");

            // Extract collision data
            laserTextureData =
               new Color[laser.Width * laser.Height];
            laser.GetData(laserTextureData);
        }

        public void ShiplaserUpdate(GameTime gameTime)
        {
            // Update each laser
            alienHit = false;

            for (int i = 0; i < laserPositions.Count; i++)
            {
                // Animate this laser
                laserPositions[i] =
                    new Vector2(laserPositions[i].X,
                                laserPositions[i].Y + LaserSpeed);

                // Get the bounding rectangle of this laser
                Rectangle laserRectangle =
                    new Rectangle((int)laserPositions[i].X, (int)laserPositions[i].Y,
                    laser.Width, laser.Height);

              //   Check collision with ship
                //if (CollisionDetection.IntersectPixels(laserRectangle, laserTextureData,
                //                    alien.alienRectangle, laserTextureData))
                //{
               //     alienHit = true;
               // } 

                // Give nLasers the same value as number of lasers in the list.
                nLasers = laserPositions.Count;

                // Remove this laser if it has gone off the screen
                if (laserPositions[i].Y < Window.ClientBounds.Top - laser.Height - 160)
                {
                    laserPositions.RemoveAt(i);

                    // When removing a laser, the next laser will have the same index
                    // as the current laser. Decrement i to prevent skipping a laser.
                    i--;

                    // Remove 1 from nLasers
                    nLasers--;
                }
            }
        }

        public void ShiplaserDraw(GameTime gameTime)
        {
            // Draw lasers  
                foreach (Vector2 laserPosition in laserPositions)
                {
                    Global.spriteBatch.Draw(laser, laserPosition, Color.White);
                }
        }
    }
}
