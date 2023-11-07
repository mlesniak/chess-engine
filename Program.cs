var game = Loader.Load("game.txt");

// TODO(mlesniak) Recognize mate and value max
// TODO(mlesniak) Recognize stalemate and value min
while (true)
{
    Console.WriteLine($"\n\n{game}");

    var bestMove = Engine.NextBestMove(game, game.Turn, 3);
    if (bestMove == null)
    {
        break;
    }

    game = game.Move(bestMove.move);
    Console.WriteLine($"For {bestMove}\n{game}");

    var bestBlack = Engine.NextBestMove(game, Color.Black, 3);
    Console.WriteLine("bestBlack = {0}", bestBlack);
    
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
