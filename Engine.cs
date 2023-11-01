using static Color;

public class Engine
{
    private const int MaxDepth = 0;

    // Score: positive: better for white, negative: better for black.
    // depth is actually depth+1, this is confusing...
    public static (Move, int) NextBestMove(Game game, Color currentColor, int depth = 0)
    {
        List<Move> allMoves = game.ValidMoves();

        // No moves means that there are no pieces on the
        // board anymore, i.e. even the king has been
        // captured. 
        if (allMoves.Count == 0)
        {
            if (currentColor == White)
            {
                return (new Move(new Position(0,0), new Position(0,0)), Int32.MinValue);
            }

            return (new Move(new Position(0,0), new Position(0,0)), Int32.MaxValue);
        }


        if (depth == MaxDepth)
        {
            var bestMove = allMoves
                .Select(move =>
                {
                    var newGame = game.Move(move);
                    var score = newGame.Score();
                    return (move, score);
                })
                .OrderBy<(Move, int), int>(t => t.Item2)
                .ToList();

            if (currentColor == White)
            {
                // Sorted in ascending order, 
                // biggest score is at the last
                // position.
                return bestMove.Last();
            }

            return bestMove.First();
        }

        // For every single move, call this recursively. Choose the best for the 
        // opposite side. Then determine based on that.
        var bestOpponentMoves = allMoves.Select(myMove =>
        {
            var newGameState = game.Move(myMove);
            var bestMoveInNewGameForOpponent = NextBestMove(newGameState, currentColor.Next(), depth - 1);
            return (myMove, bestMoveInNewGameForOpponent.Item2);
        })
            .OrderBy(t => t.Item2)
            .ToList();

        if (currentColor == White)
        {
            // Worst black move?
            var nextWhite = bestOpponentMoves.Last();
            return (nextWhite.Item1, nextWhite.Item2);
        }

        // Worst white move?
        var nextBlack = bestOpponentMoves.First();
        return (nextBlack.Item1, nextBlack.Item2);
    }
}
