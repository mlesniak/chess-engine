Engine engine = new Engine();
Game root = new Game();
Console.WriteLine("Original game");
Console.WriteLine(root.ToString());
Console.WriteLine("------------------");

var nextMove = engine.NextBestMove(root, Piece.PieceColor.White);
Console.WriteLine("nextMove = {0}", nextMove);
