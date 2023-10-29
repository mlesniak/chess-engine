public abstract class Piece
{
    protected Piece(Color color)
    {
        Color = color;
    }

    public Color Color { get; init; }

    public static readonly Piece Empty = new Empty();

    public abstract IEnumerable<Move> ValidMoves(Game game, Color turn, Position currentPiece);

    public abstract char DisplayCharacter();

    // TODO(mlesniak) why do we need this again?
    public abstract Piece Copy();
}

public record Position(int X, int Y);

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