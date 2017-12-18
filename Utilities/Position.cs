using System;

public class Position
{
    public int Row { get; set; }
    public int Col { get; set; }

    public Position(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public static Position Mod(Position a, Position b, int mod)
    {
        if (mod <= 0)
        {
            throw new ArgumentException("modulus must be a positive integer.");
        }
        int r = (a.Row + b.Row) % mod;
        r = r < 0 ? r + mod : r;
        int c = (a.Col + b.Col) % mod;
        c = c < 0 ? c + mod : c;
        return new Position(r, c);
    }

    public static Position FromOrientation(Orientation o)
    {
        switch (o)
        {
            case Orientation.Up: return new Position(-1, 0);
            case Orientation.Down: return new Position(1, 0);
            case Orientation.Left: return new Position(0, -1);
            case Orientation.Right: return new Position(0, 1);
            case Orientation.None: return new Position(0, 0);
            default:
                throw new ArgumentException("orientation was null.");
        }
    }

    public static Position FromReverseOrientation(Orientation o)
    {
        switch (o)
        {
            case Orientation.Up: return new Position(1, 0);
            case Orientation.Down: return new Position(-1, 0);
            case Orientation.Left: return new Position(0, 1);
            case Orientation.Right: return new Position(0, -1);
            case Orientation.None: return new Position(0, 0);
            default:
                throw new ArgumentException("orientation was null.");
        }
    }
}