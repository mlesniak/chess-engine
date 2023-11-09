using chess.Board.Piece;

namespace chess.Engine;

using static Color;

public static class Engine
{
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
            double score = ComputeScore(nextGameState, board.Turn.Next(), depth - 1);
            switch (board.Turn)
            {
                case White when bestScore < score:
                    bestMove = move;
                    bestScore = score;
                    Console.WriteLine("bestScore = {0} for {1}", bestScore, bestMove);
                    break;
                case Black when score < bestScore:
                    bestMove = move;
                    bestScore = score;
                    break;
            }

            // If we have found a single mate path, we can
            // abort (in our current scoring evaluation),
            // since we will never find something with a
            // better score.
            if (Math.Abs(bestScore) > 10_000)
            {
                break;
            }
        }

        if (bestMove == null)
        {
            // This should never happen.
            throw new InvalidOperationException("No move found");
        }
        return bestMove;
    }

    private static double ComputeScore(Board.Board board, Color color, int depth)
    {
        if (depth == 0 || GameState.IsGameOver(board))
        {
            return Score.Compute(board);
        }

        var legalMoves = board.LegalMoves(color);
        if (color == White)
        {
            double max = Double.MinValue;
            legalMoves.ForEach(move =>
            {
                var g = board.Move(move);
                var b = ComputeScore(g, color.Next(), depth - 1);
                max = Double.Max(max, b);
            });
            return max;
        }
        
        double min = Double.MaxValue;
        legalMoves.ForEach(move =>
        {
            var g = board.Move(move);
            var b = ComputeScore(g, color.Next(), depth - 1);
            min = Double.Min(min, b);
        });
        return min;
    }
}
