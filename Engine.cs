public class Engine
{
    private static readonly int maxDepth = 0;

    // Score: positive: better for white, negative: better for black.
    public (Move, int) NextBestMove(Game game, Color currentColor, int depth = 0)
    {
        List<Move> allMoves = game.ValidMoves();

        if (depth == maxDepth)
        {
            var bestMove = allMoves
                .Select(move =>
                {
                    var score = game.Score();
                    return (move, score);
                })
                .OrderBy<(Move, int), int>(t => t.Item2)
                .ToList();

            if (currentColor == Color.White)
            {
                // Choose biggest score.
                return bestMove.Last();
            }
            
            return bestMove.First();
        }

        throw new NotImplementedException();
    }
}
