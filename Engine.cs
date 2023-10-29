// Ignore caching, pruning, etc. for the time being.
// Come up with the simplest, even if inefficient,
// algorithm possible.
public class Engine
{
    private static readonly int maxDepth = 3;

    // For every move, find the one with the highest score for 
    // the current side. Then choose this, switch sides, find 
    // the best response and switch again until our depth is
    // exhausted.
    public (Move, int) NextBestMove(Game game, Color currentColor, int depth = 0)
    {
        List<Move> allMoves = game.ValidMoves();

        // Enrich this we the respective score, such that 
        // we have tuples of moves and their score effect.

        // TODO(mlesniak) enrich this...

        if (depth == maxDepth)
        {
            // Find best move and return it.
            allMoves.Sort((m1, m2) =>
            {
                var g1 = game.Move(m1);
                var g2 = game.Move(m2);

                return g2.Score() - g1.Score();
            });
            return (allMoves[0];
        }

        // We have not reached maximal depth for searching.
        // Branch for every single move. 



        // allValidMoves.Sort((m1, m2) =>
        // {
        //     var g1 = game.Move(m1);
        //     var g2 = game.Move(m2);
        //
        //     return g2.Score() - g1.Score();
        // });
        //
        // var bestMove = allValidMoves[0];
        // if (depth > maxDepth)
        // {
        //     return bestMove;
        // }

        // Apply move to game, switch sides, and find best move for
        // the other side based on this one.
    }
}
