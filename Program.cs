using chess.Board;
using chess.Board.Piece;
using chess.Engine;

var game = Loader.Load("game.txt");

while (!GameState.IsGameOver(game))
{
    Console.WriteLine($"\n\n{game}");
    Console.WriteLine();

    var bestMove = Engine.FindBestMove(game, 5);
    game = game.Move(bestMove);
    Console.WriteLine($"For {bestMove}\n{game}");

    if (GameState.IsGameOver(game))
    {
        break;
    }

    Console.Write("Move? ");
    var input = Console.ReadLine();
    if (input == "")
    {
        // TODO(mlesniak) if removed, also check for illegal moves.
        game.Turn = game.Turn.Next();
        continue;
    }
    var inputMove = Move.Parse(input!);
    game = game.Move(inputMove);
}
