using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Space_Invaders
{
    public class Laser : Microsoft.Xna.Framework.Game
    {
       public Texture2D laser;
       public Color[] laserTextureData;

        List<Vector2> laserPositions = new List<Vector2>();
        float LaserSpawnProbability = 0.0005f;
        const int LaserSpeed = 5;
        public Boolean shipHit;
        public bool LaseralienHit;

        // Random for laser spawns
        Random random = new Random();
        private Rectangle shipRectangle;
         Color[] shipTextureData;

        private Alien alien;

        public Laser(int x, int y)
        {                         
            
            laser = Global.content.Load<Texture2D>("Textures\\laser");

            // Extract collision data
            laserTextureData =
               new Color[laser.Width * laser.Height];
            laser.GetData(laserTextureData);


        }

        public void LaserUpdate(GameTime gameTime)
        {
            //Spawn new lasers
            /*
            if (random.NextDouble() < LaserSpawnProbability)
            {
                float x = (float)random.NextDouble() *
                    (Window.ClientBounds.Width - laser.Width);
                laserPositions.Add(new Vector2(x, -laser.Height));
            }
            */
              
             //Get the bounding rectangle of the ship
             //Rectangle shipRectangle =
             //    new Rectangle((int)shipPosition.X, (int)shipPosition.Y,
              //       ship.Width, ship.Height);

            // Update each laser
               shipHit = false;
               LaseralienHit = false;

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


                if (CollisionDetection.IntersectPixels(blockRectangle, laserTextureData,
                    alien.alienRectangle, laserTextureData))
                {
                    LaseralienHit = true;
                    Console.WriteLine("HAAA SEVENYAAAAA");
                }



                // Remove this laser if it has gone off the screen
                if (laserPositions[i].Y > Window.ClientBounds.Height)
                {
                    laserPositions.RemoveAt(i);

                    // When removing a laser, the next block will have the same index
                    // as the current laser. Decrement i to prevent skipping a block.
                    i--;
                }

            }
        }

        public void LaserDraw(GameTime gameTime)
        {
            //Global.spriteBatch.Draw(laser, position, Color.White);
            // Draw lasers  
            foreach (Vector2 laserPosition in laserPositions)
            {
                 Global.spriteBatch.Draw(laser, laserPosition, Color.White);
            }
        }   
    }
}
