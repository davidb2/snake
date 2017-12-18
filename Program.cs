using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    private static readonly int SIZE = 20;
    private static readonly int PADDING = 5;
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;
        Console.SetWindowSize((SIZE + PADDING) << 1, SIZE + PADDING);
        Console.SetBufferSize((SIZE + PADDING) << 1, SIZE + PADDING);
        Game game = new Game(SIZE);
        game.Play();
        Console.Write(typeof(string).Assembly.ImageRuntimeVersion);
        while (true)
        {
            game.HandleKeyPress(Console.ReadKey());
            if (game.IsOver)
            {
                break;
            }
        }
        Console.ReadLine();
    }
}
