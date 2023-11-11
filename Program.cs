using chess.Board;
using chess.Board.Piece;
using chess.Engine;

var game = Loader.Load("game.txt");

// Console.WriteLine(game);
// game.Turn = Color.Black;
// var m = Engine.FindBestMove(game, 5);
// Console.WriteLine(m);
// Environment.Exit(1);

// TODO(mlesniak) add knights

DateTime start = DateTime.Now;

while (!GameState.IsGameOver(game))
{
    Console.WriteLine($"\n\n{game}");
    Console.WriteLine();

    var bestMove = Engine.FindBestMove(game, 5);
    game = game.Move(bestMove);

    // Console.WriteLine($"For {bestMove}\n{game}");
    //
    // if (GameState.IsGameOver(game))
    // {
    //     break;
    // }
    //
    // Console.Write("Move? ");
    // var input = Console.ReadLine();
    // if (input == "")
    // {
    //     // TODO(mlesniak) if removed, also check for illegal moves.
    //     var moves = game.LegalMoves(Color.Black);
    //     foreach (var move in moves)
    //     {
    //         var g = game.Move(move);
    //         if (!GameState.IsGameOver(g) && !GameState.IsChess(g, Color.Black))
    //         {
    //             game = g;
    //             break;
    //         }
    //     }
    // }
    // else
    // {
    //     var inputMove = Move.Parse(input!);
    //     game = game.Move(inputMove);
    // }
}
Console.WriteLine($"\n\n{game}");

var duration = DateTime.Now - start;
Console.WriteLine($"Duration: {duration}");