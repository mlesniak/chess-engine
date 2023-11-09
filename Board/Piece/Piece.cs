namespace chess.Board.Piece;

public abstract class Piece
{
    public static readonly Piece Empty = new Empty();

    protected Piece(Color color)
    {
        Color = color;
    }

    public Color Color { get; init; }

    public abstract IEnumerable<Move> ValidMoves(Board board, Color turn, Position currentPiece);

    public abstract char DisplayCharacter();

    // TODO(mlesniak) why do we need this again?
    public abstract Piece Copy();
}

public enum Color
{
    // I'm not happy that we have this color, but we 
    // inherit from piece and have an empty tile. 
    // Alternatively, we can have a null value in 
    // the board, but this can lead to ugly errors.
    Empty,

    White,
    Black
}

public static class ColorSwitcher
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
