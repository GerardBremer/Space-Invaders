using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space_Invaders
{
    class Aliens
    {
        private List<Alien> aAliens = new List<Alien>();
        private int nColInvaders;
        private int nInvaders;
        private Random rnd = new Random(1000);

        public Aliens(int nCols, int nRows)
        {
            this.nColInvaders = nCols;
            this.nInvaders = nRows * nCols - 1;
            for (int i = 0; i <= nInvaders; i++)
                aAliens.Add(new Alien((i % nColInvaders) * 80 + 112, (i / nColInvaders) * 64 + 49)); 
        }

        public void Update(GameTime gameTime)
        {
            // Update game objects
            foreach (Alien a in aAliens)
                a.Update(gameTime);
                //a.Update(rnd.Next()==3);
        }

        public  void Draw(GameTime gameTime)
        {
            foreach (Alien a in aAliens)
                a.Draw(gameTime);
        }
    }
}
