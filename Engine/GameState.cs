using chess.Board;
using chess.Board.Piece;

namespace chess.Engine;

using static Color;

// TODO(mlesniak) caching in game object 
public static class GameState
{
    public static bool IsGameOver(Board.Board board)
    {
        return IsMate(board, White) || IsMate(board, Black) || IsStaleMate(board, White) || IsStaleMate(board, Black);
    }

    public static bool IsStaleMate(Board.Board board, Color color)
    {
        // King is currently NOT in chess?
        if (IsChess(board, color))
        {
            return false;
        }

        // Under all available moves, the king would 
        // be in chess after performing a move.
        var moves = board.LegalMoves(color);
        return moves.All(move =>
            {
                var g = board.Move(move);
                return IsChess(g, color);
            }
        );
    }

    public static bool IsMate(Board.Board board, Color color)
    {
        // King is currently in chess?
        if (!IsChess(board, color))
        {
            return false;
        }

        // Under all available moves, the king is still
        // in chess after performing that move.
        var moves = board.LegalMoves(color);
        return moves.All(move =>
            {
                var g = board.Move(move);
                return IsChess(g, color);
            }
        );
    }

    public static bool IsChess(Board.Board board, Color color)
    {
        var kingPos = FindKing(board, color);
        var opponentMoves = board.LegalMoves(color.Next());

        var kingTaken = opponentMoves.Any(move => move.Dest == kingPos);
        return kingTaken;
    }

    static Position? FindKing(Board.Board board, Color color)
    {
        Position? kingPos = null;
        board.ForEach((x, y, piece) =>
        {
            if (piece.Color == color && piece.GetType() == typeof(King))
            {
                kingPos = new Position(x, y);
            }
        });

        return kingPos;
    }
}
