using chess.Board.Piece;

namespace chess.Engine;

/// <summary>
/// Compute the static score for a board by looking
/// at the current position. This is a very basic
/// scoring function that only looks at the value
/// of the pieces and priorities checkmates.
/// </summary>
public static class Score
{
    private static readonly Random Random = new(1);

    public static double Compute(Board.Board board, int depth)
    {
        double score = 0;

        // Sum up individual piece values.
        board.ForEach((_, _, piece) =>
        {
            var pieceValue = piece switch
            {
                Queen => 9.0,
                // Having a very high value prevents a
                // king from moving into chess.
                King => 20_000.0,
                _ => throw new ArgumentException($"No value for {piece.GetType()}")
            };
            if (piece.Color == Color.Black)
            {
                pieceValue = -pieceValue;
            }
            score += pieceValue;
        });

        // Checkmates are our ultimate goal and
        // should be prioritized over everything else.
        if (GameState.IsMate(board, Color.Black))
        {
            score += 10_000 + depth;
        }
        else if (GameState.IsMate(board, Color.White))
        {
            score -= 10_000 - depth;
        }

        // Add a small random number to avoid
        // choosing the same move every time.
        score += Random.NextDouble() / 10_000;

        return score;
    }
}
