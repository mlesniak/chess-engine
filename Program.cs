// Game root = new Game();

// Engine engine = new Engine();
// var nextMove = engine.NextBestMove(root, Piece.PieceColor.White);
// Console.WriteLine("nextMove = {0}", nextMove);
//
// var n = root.Move(nextMove);
// Console.WriteLine(n);
//

// TODO(mlesniak) add a king for black, so we can hunt him

Game root  = Loader.Load("game.txt");
Console.WriteLine("Original game");
Console.WriteLine(root.ToString());
Console.WriteLine("------------------");
