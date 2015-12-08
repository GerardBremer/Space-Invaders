using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Space_Invaders
{
    class UFO
    {
        //Declarations 
        public Texture2D ufoT;
        public Vector2 position;
        private Vector2 velocity;
        private Boolean leftWall;
        private Boolean rightWall;

        //For Collision
        public Color[] ufoTextureData;
        public Rectangle ufoRectangle;

        public UFO (int x, int y)
            {
            position.X = x;
            position.Y = y;
            ufoT = Global.content.Load<Texture2D>("Textures\\ufo");
           
            //Defines starting point / starting velocity of the object
            rightWall = false;
            leftWall = true;
            velocity.X = 9f;

            //Defines texture parameters
            ufoTextureData =
            new Color[ufoT.Width * ufoT.Height];
            ufoT.GetData(ufoTextureData);
            
            // Get the bounding rectangle of the UFO  - OG in update / Robin
            Rectangle ufoRectangle =
               new Rectangle((int)position.X, (int)position.Y,
                   ufoT.Width, ufoT.Height);
        }

        public void UFOUpdate(GameTime gameTime)
        {
            //Not sure if works, ufo should bounce inbetween the sides now, it won't despawn yet. -Robin
            // If UFO hit right wall, move to left wall
            if (rightWall)
            {
                velocity.X = -1.5f;
                position.X += velocity.X;
            }

            // If UFO hit left wall, move to right wall 
            if (leftWall)
            {
                velocity.X = 1.5f;
                position.X -= velocity.X;
            }

            if (position.X >= Global.width - 40)
            {
                rightWall = false;
                leftWall = true;
                //position.Y += velocity.Y;
            }
            if (position.X <= Global.width - Global.width)
            {
                leftWall = false;
                rightWall = true;
                //position.Y += velocity.Y;
            }
        }

        public void UFODraw(GameTime gameTime)
        {
            Global.spriteBatch.Draw(ufoT, position, Color.White);
        }

    }
}
