using static Color;

public record BestMove(Move move, double score);

public static class Engine
{
    private static Random random = new(1);

    // If we return null, there is no legal next best move for the color currently in turn.
    // Since we are not in checkmate, this is a stalemate.
    public static BestMove? NextBestMove(Game game, Color currentColor, int depth = 1)
    {
        var allMoves = game.LegalMoves(currentColor);

        List<BestMove> evaluatedMoves;
        if (depth == 1)
        {
            evaluatedMoves = allMoves
                .Select(move =>
                {
                    var newGame = game.Move(move);
                    var score = Score.Calculate(newGame);
                    return new BestMove(move, score);
                })
                .OrderBy(t => t.score)
                .ToList();
        }
        else
        {
            evaluatedMoves = allMoves.Select(move =>
                {
                    var updatedGame = game.Move(move);
                    var bestOpponentMove = NextBestMove(updatedGame, currentColor.Next(), depth - 1);
                    double score;
                    if (bestOpponentMove == null)
                    {
                        // Stalemate. Due to our move, the
                        // opponent has no valid moves left.
                        score = currentColor == White
                            ? -Score.StaleMate
                            : Score.StaleMate;
                    }
                    else
                    {
                        score = bestOpponentMove.score;
                    }
                    return new BestMove(move, score);
                })
                .OrderBy(t => t.score)
                .ToList();
        }

        if (!evaluatedMoves.Any())
        {
            return null;
        }

        if (currentColor == White)
        {
            var s = evaluatedMoves.Last().score;
            var bestMoves = evaluatedMoves.SkipWhile(move => move.score < s).ToList();
            return bestMoves[random.Next(bestMoves.Count)];
        }
        else
        {
            var s = evaluatedMoves.First().score;
            var bestMoves = evaluatedMoves.TakeWhile(move => move.score <= s).ToList();
            return bestMoves[random.Next(bestMoves.Count)];
        }
    }
}
