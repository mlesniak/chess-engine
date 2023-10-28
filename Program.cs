Game root = new Game();
Console.WriteLine(root.ToString());

var moves = root.ValidMoves();
int i = 0;
foreach (var m in moves)
{
    Console.WriteLine(m);
    i++;
}
Console.WriteLine("i = {0}", i);
