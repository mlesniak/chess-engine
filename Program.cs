Game root = Loader.Load("game.txt");
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


Color turn = Color.White;

while (true)
{
    Console.WriteLine();
    Console.WriteLine(root);
    var bestMove = Engine.NextBestMove(root, turn, 2);
    Console.WriteLine($"{bestMove}");
    root = root.Move(bestMove.Item1);
    turn = turn.Next();
    Console.WriteLine("<Press any key>");
    Console.ReadKey();
}
