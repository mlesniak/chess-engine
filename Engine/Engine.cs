using chess.Board;
using chess.Board.Piece;

namespace chess.Engine;

using static Color;

public record BestMove(Move Move, double Score);

public static class Engine
{
    public static Move FindBestMove(Board.Board board, int depth)
    {
        var color = board.Turn;
        var bestScore = color == White
            ? Double.MinValue
            : Double.MaxValue;
        Move? bestMove = null;

        board.LegalMoves(color).ForEach(move =>
        {
            var nextGameState = board.Move(move);
            double score = ComputeScore(nextGameState, color.Next(), depth - 1);
            switch (color)
            {
                case White when bestScore < score:
                    bestMove = move;
                    bestScore = score;
                    Console.WriteLine("bestScore = {0}", bestScore);
                    break;
                case Black when score < bestScore:
                    bestMove = move;
                    bestScore = score;
                    break;
            }
        });

        if (bestMove == null)
        {
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

        if (color == White)
        {
            var legalMoves = board.LegalMoves(White);
            double max = Double.MinValue;
            legalMoves.ForEach(move =>
            {
                var g = board.Move(move);
                var b = ComputeScore(g, color.Next(), depth - 1);
                max = Double.Max(max, b);
            });
            return max;
        }
        else
        {
            var legalMoves = board.LegalMoves(Black);
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
}
