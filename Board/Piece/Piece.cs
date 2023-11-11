namespace chess.Board.Piece;

public abstract class Piece
{
    protected Piece(char character, Color color)
    {
        Color = color;
        Character = character;
    }

    public Color Color { get; init; }
    public char Character { get; init; }

    // Can also contain illegal ones, e.g. king moving into check.
    public abstract IEnumerable<Move> AvailableMoves(Board board, Position currentPiece);
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
