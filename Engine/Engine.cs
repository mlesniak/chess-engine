using Color = chess.Board.Piece.Color;

namespace chess.Engine;

using Board.Piece;

using Color = Color;

using static Color;

/// <summary>
/// A chess engine that uses the minimax algorithm to
/// find the best move. We currently do not use alpha-beta
/// pruning.
/// </summary>
public static class Engine
{
    /// <summary>
    /// Find the best move for the current player, using a depth
    /// of <paramref name="depth"/> half-moves.
    /// </summary>
    public static Move FindBestMove(Board.Board board, int depth)
    {
        var bestScore = board.Turn == White
            ? Double.MinValue
            : Double.MaxValue;
        Move? bestMove = null;

        var legalMoves = board.LegalMoves(board.Turn);
        foreach (var move in legalMoves)
        {
            var nextGameState = board.Move(move);
            double score = ComputeScore(nextGameState, board.Turn.Next(), depth - 1, Double.MinValue, Double.MaxValue);
            switch (board.Turn)
            {
                case White when bestScore < score:
                    bestMove = move;
                    bestScore = score;
                    break;
                case Black when bestScore > score:
                    bestMove = move;
                    bestScore = score;
                    break;
            }
        }

        if (bestMove == null)
        {
            // This should never happen, since we would be in a
            // GameOver state before this point.
            throw new InvalidOperationException("No valid move found");
        }
        return bestMove;
    }

    // While we use alpha-beta pruning, our scoring function is
    // not very advanced. Therefore we do not see a significant
    // performance improvement from using alpha-beta pruning.
    private static double ComputeScore(Board.Board board, Color color, int depth, double alpha, double beta)
    {
        if (depth == 0 || GameState.IsGameOver(board))
        {
            return Score.Compute(board, depth);
        }

        var legalMoves = board.LegalMoves(color);
        if (color == White)
        {
            double max = Double.MinValue;
            foreach (var move in legalMoves)
            {
                var g = board.Move(move);
                var b = ComputeScore(g, color.Next(), depth - 1, alpha, beta);
                max = Double.Max(max, b);
                alpha = Double.Max(alpha, b);
                if (alpha >= beta)
                {
                    break;
                }
            }
            return max;
        }

        double min = Double.MaxValue;
        foreach (var move in legalMoves)
        {
            var g = board.Move(move);
            var b = ComputeScore(g, color.Next(), depth - 1, alpha, beta);
            min = Double.Min(min, b);
            beta = Double.Min(beta, b);
            if (beta <= alpha)
            {
                break;
            }
        }
        return min;
    }
}
