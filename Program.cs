var game = Loader.Load("game.txt");

// TODO(mlesniak) Check for chess.
// TODO(mlesniak) Move blocking rock to a new pice BLOCK 🟥
// TODO(mlesniak) Check for mate.
// TODO(mlesniak) check for stalemate and prevent this.

while (true)
{
    Console.WriteLine($"\n{new string('-', 17)}");
    Console.WriteLine(game);

    var bestMove = Engine.NextBestMove(game, game.Turn, 5);
    game = game.Move(bestMove.Item1);
    Console.WriteLine("bestMove = {0}", bestMove);
    Console.WriteLine(game);
    
    if (Engine.IsCurrentColorInChess(game))
    {
        Console.WriteLine("Chess");
    }

    Console.Write("? ");
    var line = Console.ReadLine();
    // Add passing/not doing anything for testing purposes
    // until we implemented stalemate prevention.
    if (line == "")
    {
        game.Turn = game.Turn.Next();
        continue;
    }
    var move = Move.Parse(line!);
    game = game.Move(move);
}
