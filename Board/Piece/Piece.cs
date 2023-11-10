namespace chess.Board.Piece;

public abstract class Piece
{
    protected Piece(Color color)
    {
        Color = color;
    }

    public Color Color { get; init; }

    // Can also contain illegal ones, e.g. king moving into check.
    public abstract IEnumerable<Move> AvailableMoves(Board board, Position currentPiece);

    public abstract char DisplayCharacter();

    // TODO(mlesniak) why do we need this again?
    // Pieces are immutable. Do we even need this?
    public abstract Piece Copy();
}

public enum Color
{
    White,
    Black
}

public static class ColorTransition
{
    public static Color Next(this Color color)
    {
        return color switch
        {
            Color.Black => Color.White,
            Color.White => Color.Black,
            _ => throw new ArgumentException()
        };
    }
}
