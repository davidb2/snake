using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Food : Piece
{
    private static readonly String FOOD_EMOJI = "+";
    public Position Location { get; set; }

    public Food(Position position)
    {
        Location = position;
        Character = FOOD_EMOJI;
    }

}