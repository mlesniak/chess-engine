public class Engine
{
    // private readonly int depth = 2;

    public Move NextBestMove(Game game, Piece.PieceColor currentTurn)
    {
        var allValidMoves = game.ValidMoves();

        // For every move, find the one with the highest score for 
        // the current side. Then choose this, switch sides, find 
        // the best response and switch again until our depth is
        // exhausted.
        var gamesWithNextMovesExecuted = allValidMoves.ToList().Select(game.Move);

        foreach (var g in gamesWithNextMovesExecuted)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine(g);
        }

        return new Move(0,0,0,0);
    }
}
