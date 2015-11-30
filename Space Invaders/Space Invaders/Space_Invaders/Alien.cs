using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Space_Invaders
{
    public class Alien : Microsoft.Xna.Framework.Game
    {
        private Texture2D texture;
        public Texture2D lasertexture;
        public Vector2 position;
        private Vector2 velocity;
        private Boolean leftWall;
        private Boolean rightWall;
        Random random = new Random();
        private Laser laser;

        public Alien(int x, int y)
        {
            position.X = x;
            position.Y = y;

            texture = Global.content.Load<Texture2D>("Textures\\alien");
            //Defines starting point / starting velocity
            rightWall = true;
            leftWall = false;
            velocity.X = 3f;
            velocity.Y = 48f;

            // Laser
            laser = new Laser(x, y);
        }

        public void Update(GameTime gameTime)
        {
            // Get the bounding rectangle of the alien
            Rectangle alienRectangle =
               new Rectangle((int)position.X, (int)position.Y,
                   texture.Width, texture.Height);

            // If aliens hit right wall, move down and move towards left wall
            if (rightWall)
            {
                velocity.X = 1.5f;
                position.X += velocity.X;
            }
            // If aliens hit left wall, move down and move towards right wall 
            if (leftWall)
            {
                velocity.X = 1.5f;
                position.X -= velocity.X;
            }
            if (position.X >= Global.width - 40)
            {
                rightWall = false;
                leftWall = true;
                position.Y += velocity.Y;
            }
            if (position.X <= Global.width - Global.width)
            {
                leftWall = false;
                rightWall = true;
                position.Y += velocity.Y;
            }

            //Spawn new lasers
            //if (random.NextDouble() < LaserSpawnProbability)
            //{

            ///}
            ///
            laser.Update(gameTime);
        }

        /*public void Shoot()
        {
            laser = new AlienLaser(lasertexture);
            lasertexture.velocity.X = velocity.X - 3f;
            laser.position = new Vector2(position.X + laser);
        }*/

        public void Draw(GameTime gameTime)
        {
            Global.spriteBatch.Draw(texture, position, Color.White);

            // Spawn new lasers
            //if (random.NextDouble() < LaserSpawnProbability)
            //{
            laser.Draw(gameTime);
            //}
        }

    }
}
