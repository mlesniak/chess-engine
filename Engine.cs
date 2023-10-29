public class Engine
{
    // For every move, find the one with the highest score for 
    // the current side. Then choose this, switch sides, find 
    // the best response and switch again until our depth is
    // exhausted.
    public Move NextBestMove(Game game, Color currentColor)
    {
        // TODO(mlesniak) alternate between colors
        List<Move> allValidMoves = game
            .ValidMoves();
        allValidMoves.Sort((m1, m2) =>
        {
            var g1 = game.Move(m1);
            var g2 = game.Move(m2);

            return g2.Score() - g1.Score();
        });

        return allValidMoves[0];
    }
}
