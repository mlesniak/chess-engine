var game = Loader.Load("game.txt");

// TODO(mlesniak) continue to refactor and simplify code
// TODO(mlesniak) cache
while (!Engine.IsGameOver(game))
{
    Console.WriteLine($"\n\n{game}");
    Console.WriteLine();

    var bestMove = Engine.FindBestMove(game, 5);
    game = game.Move(bestMove);
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
