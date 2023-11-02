using System.Security.AccessControl;

Game root = Loader.Load("game.txt");
root.Turn = Color.Black;

while (true)
{
    Console.WriteLine($"\nCurrent state ({root.Turn})");
    Console.WriteLine(root);
    var bestMove = Engine.NextBestMove(root, root.Turn, 4);
    Console.WriteLine($"{bestMove} -- state after the move");
    root = root.Move(bestMove.Item1);
    Console.WriteLine(root);
    if (bestMove.Item2 == Int32.MaxValue || bestMove.Item2 == Int32.MinValue)
    {
        // Mate.
        Console.WriteLine($"Mate. {root.Turn.Next()} won");
        break;
    }

    Console.WriteLine("<Press any key>");
    Console.ReadKey();
    
}
