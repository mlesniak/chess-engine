using chess.Board;
using chess.Board.Piece;
using chess.Engine;

var game = Loader.Load("game.txt");

DateTime start = DateTime.Now;

while (!GameState.IsGameOver(game))
{
    Console.WriteLine($"\n\n{game}");
    Console.WriteLine();

    var bestMove = Engine.FindBestMove(game, 5);
    game = game.Move(bestMove);

    // Comment this out to play against the engine.
    //
    // Console.WriteLine($"For {bestMove}\n{game}");
    //
    // if (GameState.IsGameOver(game))
    // {
    //     break;
    // }
    //
    // Console.Write("Move? ");
    // var input = Console.ReadLine();
    // // If we do not provide any input, move to the
    // // first legal move that is found. This is very
    // // useful for testing.
    // if (input == "")
    // {
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