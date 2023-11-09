namespace chess.Engine;

using static Color;

// TODO(mlesniak) caching in game object 
public static class GameState
{
    public static bool IsGameOver(Game game)
    {
        return IsMate(game, White) || IsMate(game, Black) || IsStaleMate(game, White) || IsStaleMate(game, Black);
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
}
