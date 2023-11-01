Game root = Loader.Load("game.txt");
Console.WriteLine("Original game");
Console.WriteLine(root.ToString());
Console.WriteLine("------------------");


// TODO(mlesniak) implement basic min-maxing with at least a depth of 3 showing that white always captures the king
//                we might need to add some immovable objects for testing.
// foreach (var move in root.ValidMoves())
// {
//     Console.Write($"{move}\t");
// }
// Console.WriteLine();


// Color turn = Color.Black;
// Color turn = Color.White;
// root.Turn = 

root.Turn = Color.Black;

while (true)
{
    Console.WriteLine($"\nCurrent state ({root.Turn})");
    Console.WriteLine(root);
    var bestMove = Engine.NextBestMove(root, root.Turn, 1);
    Console.WriteLine($"{bestMove} -- state after the move");
    root = root.Move(bestMove.Item1);
    Console.WriteLine(root);
    Console.WriteLine("<Press any key>");
    Console.ReadKey();
    
}
