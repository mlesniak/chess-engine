using chess.Board;
using chess.Board.Piece;

namespace chess.Engine;

public static class Score
{
    public static double Compute(Board.Board board)
    {
        double score = 0;
        board.ForEach((_, _, piece) =>
        {
            score += ValueFor(piece).ForColor(piece.Color);
        });
        
        if (GameState.IsMate(board, Color.Black))
        {
            score += 10_000;
        } else if (GameState.IsMate(board, Color.White))
        {
            score -= 10_000;
        }

        // TODO(mlesniak) add more explanation.
        // Force pieces to move closer to the king.
        var blackKingPos = GameState.FindKing(board, Color.Black);
        var whiteKingPos = GameState.FindKing(board, Color.White);
        board.ForEach((x, y, piece) =>
        {
            if (whiteKingPos != null && piece.Color == Color.Black)
            {
                // Distance to white king
                var d = Math.Abs(x - whiteKingPos.X) + Math.Abs(y - whiteKingPos.Y);
                score -= 1.0 / d;
            }
            else if (blackKingPos != null)
            {
                // Distance to black king
                var d = Math.Abs(x - blackKingPos.X) + Math.Abs(y - blackKingPos.Y);
                score += 1.0 / d;
            }
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
