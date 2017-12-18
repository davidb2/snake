using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SnakePiece : Piece
{
    private static readonly String SNAKE_EMOJI = "o";
    public SnakePiece Parent { get; }
    public SnakePiece Child { get; set; }
    public Orientation Direction { get; set; }
    public Position Location { get; set; }

    public SnakePiece()
    {
        Character = SNAKE_EMOJI;
        new SnakePiece(null, null);
    }

    public SnakePiece(SnakePiece parent, Position position)
    {
        Parent = parent;
        Direction = parent == null ? Orientation.None : parent.Direction;
        Child = null;
        Location = position == null ? new Position(0, 0) : position;
        Character = SNAKE_EMOJI;
    }
}
