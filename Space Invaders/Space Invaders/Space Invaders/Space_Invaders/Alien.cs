using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Space_Invaders
{
    public class Alien
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 velocity;
        private Boolean leftWall;
        private Boolean rightWall;
        //private EnemyBullet theEnemyBullet;
        //public int shootChance;

        public Alien(int x, int y)
        {
            position.X = x;
            position.Y = y;
            
            //shootChance = 10;
            texture = Global.content.Load<Texture2D>("Images\\alien");
            rightWall = true;
            leftWall = false;
            velocity.X = 3f;
            velocity.Y = 49f;
        }


        public void Update(bool shoot)
        {
            // If aliens hit right wall, move down and move towards left wall
            if (rightWall)
            {
                velocity.X = 2f;
                position.X += velocity.X;
            }
            // If aliens hit left wall, move down and move towards right wall 
            if (leftWall)
            {
                velocity.X = 2f;
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
            /*if (((float)Random(1, 1000) / 10 <= .2f && Game1.enemybullets.Count <= 1))
            {
                EnemyBullet newBullet = new EnemyBullet(position + new Vector2(0f, 14f));
                newBullet.Init();
                Game1.enemybullets.Add(newBullet);
            }*/

        }
        public void Draw()
        {
            Global.spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
