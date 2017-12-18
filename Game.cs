using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

class Game
{
    private static readonly int SLEEP = 50;
    public int Size { get; }
    public bool IsOver { get; private set; }
    public int SnakeLength { get; private set; }
    private Piece[,] board;
    private SnakePiece head, tail;
    private Food food;
    private Random random;


    public Game(int size)
    {
        if (size < 2)
        {
            throw new NotImplementedException("Game board must be at least 2 x 2.");
        }

        Size = size;
        SnakeLength = 1;
        IsOver = false;
        board = new Piece[size, size];
        random = new Random();

        int foodRow = random.Next(size);
        int foodCol = random.Next(size);

        food = new Food(new Position(foodRow, foodCol));

        int snakeRow, snakeCol;
        do
        {
            snakeRow = random.Next(size);
            snakeCol = random.Next(size);
        } while (foodRow == snakeRow && foodCol == snakeCol);

        Orientation[] vals =
            Enum.GetValues(typeof(Orientation))
                .OfType<Orientation>()
                .Where(o => o != Orientation.None)
                .ToArray();
        Orientation direction = vals[random.Next(vals.Length)];
        head = new SnakePiece { Direction = direction, Location = new Position(snakeRow, snakeCol) };
        tail = head;

        ResetBoard();
    }

    private void ResetBoard()
    {
        for (int row = 0; row < board.GetLength(0); row++)
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
                board[row, col] = new Blank();
            }
        }
    }

    private void PrintBoard()
    {
        StringBuilder sb = new StringBuilder();
        for (int row = 0; row < board.GetLength(0); row++)
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
                sb.AppendFormat("{0} ", board[row, col]);
            }
            sb.AppendLine();
        }
        Console.Clear();
        Console.Write(sb);
    }

    public void Play()
    {
        Thread thread = new Thread(new ThreadStart(UpdateGame));
        thread.Start();
    }

    private void UpdateGame()
    {
        while (true)
        {
            lock (board)
            {
                Position directionPosition = Position.FromOrientation(head.Direction);
                Position newSnakePosition = Position.Mod(head.Location, directionPosition, Size);
                Piece piece = board[newSnakePosition.Row, newSnakePosition.Col];

                if (piece is SnakePiece)
                {
                    // the snake has crashed !!!
                    IsOver = true;
                }
                else if (piece is Food)
                {
                    SnakePiece newTail = new SnakePiece(
                        tail, 
                        Position.Mod(tail.Location, Position.FromReverseOrientation(tail.Direction), Size)
                    );
                    tail.Child = newTail;
                    tail = newTail;
                    SnakeLength++;
                }
            case Orientation.Up: return Orientation.Down;

                ResetBoard();
                board[food.Location.Row, food.Location.Col] = food;

                SnakePiece curr = head;
                Position oldPosition = null;
                Orientation oldDirection = Orientation.None;

                while (curr != null)
                {
                    if (curr.Parent == null)
                    {
                        oldPosition = curr.Location;
                        oldDirection = curr.Direction;
                        curr.Location = newSnakePosition;
                    }
                    else 
                    {
                        Position tempPosition = curr.Location;
                        Orientation tempDirection = curr.Direction;

                        curr.Direction = oldDirection;
                        curr.Location = oldPosition;
                        oldPosition = tempPosition;
                        oldDirection = tempDirection;

                    }
                    board[curr.Location.Row, curr.Location.Col] = curr;
                    curr = curr.Child;
                }

                if (piece is Food)
                {
                    int row, col;
                    do
                    {
                        row = random.Next(Size);
                        col = random.Next(Size);
                    } while (!(board[row, col] is Blank));
                    food.Location = new Position(row, col);
                    board[row, col] = food;
                }
                PrintBoard();
            }
            if (IsOver) break;
            Thread.Sleep(SLEEP);
        }
    }

    private void Move(Orientation direction)
    {
        lock (board)
        {
            if (!OppositeDirection(direction).Equals(head.Direction))
            {
                head.Direction = direction;
            }
        }
    }

    public void HandleKeyPress(ConsoleKeyInfo keyInfo)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                Move(Orientation.Up);
                break;
            case ConsoleKey.DownArrow:
                Move(Orientation.Down);
                break;
            case ConsoleKey.LeftArrow:
                Move(Orientation.Left);
                break;
            case ConsoleKey.RightArrow:
                Move(Orientation.Right);
                break;
            case ConsoleKey.Q:
                IsOver = true;
                break;
            default:
                break;
        }
    }

    private Orientation OppositeDirection(Orientation o)
    {
        switch (o)
        {
            case Orientation.Up: return Orientation.Down;
            case Orientation.Down: return Orientation.Up;
            case Orientation.Left: return Orientation.Right;
            case Orientation.Right: return Orientation.Left;
            case Orientation.None: return Orientation.None;
            default: throw new ArgumentNullException();
        }
    }
}

