using System.Resources;

using static Color;

public record BestMove(Move Move, double Score);

public static class Engine
{
    public static bool IsGameOver(Game game)
    {
        if (IsMate(game, White))
        {
            return true;
        }
        
        if (IsMate(game, Black))
        {
            return true;
        }

        if (IsStaleMate(game, game.Turn))
        {
            return true;
        }
        
        return false;
    }

    public static bool IsStaleMate(Game game, Color color)
    {
        // King is currently NOT in chess?
        if (IsChess(game, color))
        {
            return false;
        }

        // Under all available moves, the king would 
        // be in chess after performing a move.
        var moves = game.LegalMoves(color);
        return moves.All(move =>
            {
                var g = game.Move(move);
                return IsChess(g, color);
            }
        );
    }

    public static bool IsMate(Game game, Color color)
    {
        // King is currently in chess?
        if (!IsChess(game, color))
        {
            return false;
        }

        // Under all available moves, the king is still
        // in chess after performing that move.
        var moves = game.LegalMoves(color);
        return moves.All(move =>
            {
                var g = game.Move(move);
                return IsChess(g, color);
            }
        );
    }

    public static bool IsChess(Game game, Color color)
    {
        var kingPos = FindKing(game, color);
        var opponentMoves = game.LegalMoves(color.Next());

        var kingTaken = opponentMoves.Any(move => move.Dest == kingPos);
        return kingTaken;
    }

    static Position? FindKing(Game game, Color color)
    {
        Position? kingPos = null;
        game.ForEach((x, y, piece) =>
        {
            if (piece.Color == color && piece.GetType() == typeof(King))
            {
                kingPos = new Position(x, y);
            }
        });

        return kingPos;
    }

    public static Move FindBestMove(Game game, int depth)
    {
        var color = game.Turn;
        var bestScore = color == White
            ? Double.MinValue
            : Double.MaxValue;
        Move? bestMove = null;

        game.LegalMoves(color).ForEach(move =>
        {
            var nextGameState = game.Move(move);
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

    private static double ComputeScore(Game game, Color color, int depth)
    {
        if (depth == 0 || IsGameOver(game))
        {
            return Score.Compute(game);
        }

        if (color == White)
        {
            var legalMoves = game.LegalMoves(White);
            double max = Double.MinValue;
            legalMoves.ForEach(move =>
            {
                var g = game.Move(move);
                var b = ComputeScore(g, color.Next(), depth - 1);
                max = Double.Max(max, b);
            });
            return max;
        }
        else
        {
            var legalMoves = game.LegalMoves(Black);
            double min = Double.MaxValue;
            legalMoves.ForEach(move =>
            {
                var g = game.Move(move);
                var b = ComputeScore(g, color.Next(), depth - 1);
                min = Double.Min(min, b);
            });
            return min;
        }
    }
}
