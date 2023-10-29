// Game root = new Game();

// Engine engine = new Engine();
// var nextMove = engine.NextBestMove(root, Piece.PieceColor.White);
// Console.WriteLine("nextMove = {0}", nextMove);
//
// var n = root.Move(nextMove);
// Console.WriteLine(n);
//

using static Color;

Game root  = Loader.Load("game.txt");
Console.WriteLine("Original game");
Console.WriteLine(root.ToString());
Console.WriteLine("------------------");


// TODO(mlesniak) implement basic min-maxing with at least a depth of 3 showing that white always captures the king
//                we might need to add some immovable objects for testing.
root.Turn = Black;
foreach (var move in root.ValidMoves())
{
    Console.WriteLine($"{move}");
}