using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

abstract class Piece
{
    public String Character { get; protected set; }

    public override string ToString()
    {
        return Character;
    }
}

