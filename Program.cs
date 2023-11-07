var game = Loader.Load("game.txt");

while (true)
{
    Console.WriteLine($"\n\n{game}");

    var bestMove = Engine.NextBestMove(game, game.Turn, 5);
    if (bestMove == null)
    {
        break;
    }

    game = game.Move(bestMove.move);
    Console.WriteLine($"Using {bestMove} for\n{game}");
    
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
