var game = Loader.Load("game.txt");

while (true)
{
    Console.WriteLine($"\n{new string('-', 17)}");
    Console.WriteLine(game);
    
    if (Engine.NextMoveMate(game))
    {
        Console.WriteLine("MATE");
        break;
    }

    var bestMove = Engine.NextBestMove(game, game.Turn, 5);
    game = game.Move(bestMove.Item1);
    Console.WriteLine(game);

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
