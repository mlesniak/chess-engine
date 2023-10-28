public abstract class Piece
{
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;

    public enum PieceColor
    {
        Empty,
        White,
        Black
    }

    // Ideally, set in constructor and then not changable.
    public PieceColor Color { get; set; } = PieceColor.Empty;

    public static readonly Piece Empty = new Empty();

    public abstract IEnumerable<Move> ValidMoves(Game game);

    public abstract char Display();

    public abstract Piece Copy();
}

