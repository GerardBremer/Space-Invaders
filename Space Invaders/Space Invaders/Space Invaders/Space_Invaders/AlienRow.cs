using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Space_Invaders
{
    /// <summary>
    /// Summary description for InvaderRow.
    /// </summary>
    public class AlienRow
    {
        public Alien[] Aliens = new Alien[11];
        public Point LastPosition = new Point(0, 0);

        public AlienRow(string gif1, string gif2, int rowNum)
        {
            //
            // TODO: Add constructor logic here
            //
            for (int i = 0; i < Aliens.Length; i++)
            {
                Aliens[i] = new Alien(gif1, gif2);
                Aliens[i].Position.X = i * Aliens[i].GetBounds().Width + 5;
                Aliens[i].Position.Y = rowNum * Aliens[i].GetBounds().Height + 10;
            }

            LastPosition = Aliens[Aliens.Length - 1].Position;
        }

        public Alien this[int index]   // indexer declaration
        {
            get
            {
                return Aliens[index];
            }
        }


        public void Draw(Graphics g)
        {
            for (int i = 0; i < Aliens.Length; i++)
            {
                Aliens[i].Draw(g);
            }
        }

        public Alien GetFirstInvader()
        {
            int count = 0;
            Aliens TheInvader = Aliens[count];
            while ((TheInvader.BeenHit == true) && (count < Aliens.Length - 1))
            {
                count++;
                TheInvader = Aliens[count];
            }

            return TheInvader;
        }

        public Alien GetLastInvader()
        {
            int count = Aliens.Length - 1;
            Alien TheInvader = Aliens[count];
            while ((TheInvader.BeenHit == true) && (count > 0))
            {
                count--;
                TheInvader = Aliens[count];
            }

            return TheInvader;
        }

        public int NumberOfLiveInvaders()
        {
            int count = 0;
            for (int i = 0; i < Aliens.Length; i++)
            {
                if (Aliens[i].Died == false)
                    count++;
            }

            return count;
        }
    }
}
