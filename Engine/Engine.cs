using static Color;

public record BestMove(Move move, double score);

public static class Engine
{
    private static Random random = new(1);

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

    private static Position? FindKing(Game game, Color color)
    {
        Position? kingPos = null;
        game.Iterate((x, y, piece) =>
        {
            if (piece.Color == color && piece.GetType() == typeof(King))
            {
                kingPos = new Position(x, y);
            }
        });

        return kingPos;
    }

    public static double BestScore(Game game, Color color, int depth)
    {
        if (depth == 0 || IsGameOver(game))
        {
            return Score.Calculate(game);
        }

        if (color == White)
        {
            var legalMoves = game.LegalMoves(White);
            double max = Double.MinValue;
            legalMoves.ForEach(move =>
            {
                var g = game.Move(move);
                var b = BestScore(g, color.Next(), depth - 1);
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
                var b = BestScore(g, color.Next(), depth - 1);
                min = Double.Min(min, b);
            });
            return min;
        }
    }

    public static BestMove BestMove(Game game, Color color, int depth)
    {
        var bestScore = color == White
            ? Double.MinValue
            : Double.MaxValue;
        Move? bestMove = null;

        var legalMoves = game.LegalMoves(color);
        legalMoves.ForEach(move =>
        {
            var g = game.Move(move);
            double score = BestScore(g, color.Next(), depth - 1);
            if (color == White && bestScore < score)
            {
                bestScore = score;
                bestMove = move;
            }
            else if (color == Black && score < bestScore) 
            {
                bestScore = score;
                bestMove = move;
            }
        });

        return new BestMove(bestMove!, bestScore);
    }
}
