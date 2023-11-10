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
            // if (!move.ToString().Equals("b3-b2"))
            // {
            //     continue;
            // }

            var nextGameState = board.Move(move);
            double score = ComputeScore(nextGameState, board.Turn.Next(), depth - 1);
            Console.WriteLine($"{move} -> {score}");
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
        Console.WriteLine($"Best move: {bestMove}");

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
            return Score.Compute(board, depth);
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
