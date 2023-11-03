var root = Loader.Load("game.txt");
root.Turn = Color.White;

// TODO(mlesniak) seed randomizer for reproducability
// Once we've successfully enabled us to hunt the king,
// refactor this 🙈 ...

while (true)
{
    Console.WriteLine($"\nCurrent state ({root.Turn})");
    Console.WriteLine(root);
    var bestMove = Engine.NextBestMove(root, root.Turn, 5);
    Console.WriteLine($"{bestMove} -- state after the move");
    root = root.Move(bestMove.Item1);
    Console.WriteLine(root);
    if (bestMove.Item2 == Int32.MaxValue || bestMove.Item2 == Int32.MinValue)
    {
        // Mate.
        Console.WriteLine($"Found no move which will not loose. {root.Turn.Next()} won");
        // break;
    }

    Console.Write("Enter move: ");
    var line = Console.ReadLine();
    var move = Move.Parse(line!);
    Console.WriteLine("move = {0}", move);
    root = root.Move(move);
}
