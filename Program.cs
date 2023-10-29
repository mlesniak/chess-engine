// Game root = new Game();

// Engine engine = new Engine();
// var nextMove = engine.NextBestMove(root, Piece.PieceColor.White);
// Console.WriteLine("nextMove = {0}", nextMove);
//
// var n = root.Move(nextMove);
// Console.WriteLine(n);
//

// TODO(mlesniak) move shall be two positions
// TODO(mlesniak) a piece is stateless and should not contain a position, hence 
//                the engine should pass the current position of a piece to 
//                the validMoves function.

Game root  = Loader.Load("game.txt");
Console.WriteLine("Original game");
Console.WriteLine(root.ToString());
Console.WriteLine("------------------");
