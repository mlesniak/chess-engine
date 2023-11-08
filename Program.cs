var game = Loader.Load("game.txt");

// TODO(mlesniak) Recognize mate and value max
// TODO(mlesniak) Recognize stalemate and value min
while (!Engine.IsGameOver(game))
{
    Console.WriteLine($"\n\n{game}");
    Console.WriteLine();

    var bestMove = Engine.BestMove(game, game.Turn, 3);

    game = game.Move(bestMove.move);
    Console.WriteLine($"For {bestMove}\n{game}");

    if (Engine.IsGameOver(game))
    {
        break;
    }

    Console.Write("Move? ");
    var input = Console.ReadLine();
    if (input == "")
    {
        game.Turn = game.Turn.Next();
        continue;
    }
    var inputMove = Move.Parse(input!);
    game = game.Move(inputMove);
}
