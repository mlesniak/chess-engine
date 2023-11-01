Game root  = Loader.Load("game.txt");
Console.WriteLine("Original game");
Console.WriteLine(root.ToString());
Console.WriteLine("------------------");


// TODO(mlesniak) implement basic min-maxing with at least a depth of 3 showing that white always captures the king
//                we might need to add some immovable objects for testing.
root.Turn = Color.White;
foreach (var move in root.ValidMoves())
{
    Console.Write($"{move}\t");
}
Console.WriteLine();

Engine engine = new Engine();
var bestMove = engine.NextBestMove(root, Color.White);
Console.WriteLine($"{bestMove}");
