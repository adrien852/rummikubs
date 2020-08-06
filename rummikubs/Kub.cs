using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rummikubs
{
    class Kub
    {
        //VARIABLES
        public int score { get; set; }
        public string color { get; set; }
        public bool toBePlayed { get; set; }
        public bool isInHands { get; set; }

        //CONSTRUCTOR
        public Kub(int score, string color, bool isInHands)
        {
            this.score = score;
            this.color = color;
            toBePlayed = false;
            this.isInHands = isInHands;
        }
    }
}
