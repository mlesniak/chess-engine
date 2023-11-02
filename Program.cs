Game root = Loader.Load("game.txt");
Console.WriteLine("Original game");
Console.WriteLine(root.ToString());
Console.WriteLine("------------------");

root.Turn = Color.Black;

while (true)
{
    Console.WriteLine($"\nCurrent state ({root.Turn})");
    Console.WriteLine(root);
    var bestMove = Engine.NextBestMove(root, root.Turn, 3);
    Console.WriteLine($"{bestMove} -- state after the move");
    root = root.Move(bestMove.Item1);
    Console.WriteLine(root);
    Console.WriteLine("<Press any key>");
    Console.ReadKey();
    
}
