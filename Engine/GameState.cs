using Color = chess.Board.Piece.Color;

namespace chess.Engine;

using Board.Piece;

using static Color;

public static class GameState
{
    public static bool IsGameOver(Board.Board board)
    {
        return IsMate(board, White) || IsMate(board, Black) || IsStaleMate(board) || OneKingLeft(board);
    }

    /// <summary>
    /// Check if the player with the given color is in checkmate.
    /// </summary>
    public static bool IsMate(Board.Board board, Color color)
    {
        // Opponent king is currently in chess?
        if (!IsCheck(board, color))
        {
            return false;
        }

        // Own king is NOT in chess. This prevents
        // moves which would move the own king near
        // the enemy to perform a checkmate.
        if (IsCheck(board, color.Next()))
        {
            return false;
        }

        // Under all available moves, the king is still
        // in chess after performing that move.
        var moves = board.LegalMoves(color);
        return moves.All(move =>
            {
                var g = board.Move(move);
                return IsCheck(g, color);
            }
        );
    }

    /// <summary>
    /// Check if the current player is in stalemate.
    /// </summary>
    private static bool IsStaleMate(Board.Board board)
    {
        // King is currently NOT in chess?
        if (IsCheck(board, board.Turn))
        {
            return false;
        }

        // Under all available moves, the king would 
        // be in chess after performing the move.
        return board.LegalMoves(board.Turn).All(move =>
            {
                var g = board.Move(move);
                return IsCheck(g, board.Turn);
            }
        );
    }

    private static bool IsCheck(Board.Board board, Color color)
    {
        var kingPos = FindKing(board, color);
        var opponentMoves = board.LegalMoves(color.Next());
        var moveToTakeKingExists = opponentMoves.Any(move => move.Dest == kingPos);
        return moveToTakeKingExists;
    }

    private static bool OneKingLeft(Board.Board board)
    {
        var count = 0;
        board.ForEach((_, _, piece) =>
        {
            if (piece.GetType() == typeof(King))
            {
                count++;
            }
        });
        return count == 1;
    }

    private static Position? FindKing(Board.Board board, Color color)
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
