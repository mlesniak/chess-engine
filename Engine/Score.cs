using chess.Board;
using chess.Board.Piece;

namespace chess.Engine;

public static class Score
{
    public static double Compute(Board.Board board)
    {
        if (GameState.IsMate(board, Color.Black))
        {
            return Double.MaxValue;
        }
        if (GameState.IsMate(board, Color.White))
        {
            return Double.MinValue;
        }

        double score = 0;
        board.ForEach((_, _, piece) =>
        {
            score += ValueFor(piece).ForColor(piece.Color);
        });
        return score;
    }

    private static double ForColor(this double number, Color color)
    {
        return color == Color.Black
            ? -number
            : number;
    }

    private static double ValueFor(Piece piece)
    {
        return piece switch
        {
            Empty => 0.0,
            Queen => 9.0,
            King => 100.0,
            _ => throw new ArgumentException($"No value for {piece.GetType()}")
        };
    }
}
