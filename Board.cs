using System.Drawing;
using System.Text;


public class Game
{
    // 0/0 is in the lower left corner.
    public Piece[][] Board { get; }

    public Game()
    {
        int width = 8;
        int height = 8;

        Board = new Piece[height][];
        for (int y = 0; y < height; y++)
        {
            Board[y] = new Piece[width];
            Array.Fill(Board[y], Piece.Empty, 0, width);
        }

        var queen = new Queen();
        // this is not very good...
        queen.X = 3;
        queen.Y = 0;
        queen.Color = Piece.PieceColor.White;
        Board[0][3] = queen;

        var otherQueen = new Queen();
        // this is not very good...
        otherQueen.X = 3;
        otherQueen.Y = 4;
        otherQueen.Color = Piece.PieceColor.Black;
        Board[4][3] = otherQueen;

        var rook = new Rook();
        // this is not very good...
        rook.X = 2;
        rook.Y = 0;
        rook.Color = Piece.PieceColor.White;
        Board[0][2] = rook; // pieces already now their position?
    }

    public Game Move(Move move)
    {
        return this;
    }

    public IEnumerable<Move> ValidMoves()
    {
        List<Move> moves = new();

        for (int y = Board.Length - 1; y >= 0; y--)
        {
            for (int x = 0; x < Board[y].Length; x++)
            {
                var piece = Board[y][x];
                if (piece.Color == Piece.PieceColor.Black || piece.GetType() != typeof(Queen))
                {
                    continue;
                }
                moves.AddRange(piece.ValidMoves(this));
            }
        }


        return moves;
    }
    
    protected char withColor(Piece.PieceColor color, char display)
    {
        switch (color)
        {
            case Piece.PieceColor.White:
                return Char.ToUpper(display);
            case Piece.PieceColor.Black:
                return Char.ToLower(display);
            case Piece.PieceColor.Empty:
                return display;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (int y = Board.Length - 1; y >= 0; y--)
        {
            for (int x = 0; x < Board[y].Length; x++)
            {
                var piece = Board[y][x];
                var c = withColor(piece.Color, piece.Display());
                sb.Append(c);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}


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

}

public class Empty : Piece
{
    public override IEnumerable<Move> ValidMoves(Game game) => Enumerable.Empty<Move>();
    public override char Display() => '.';
}

public record Move(int x1, int y1, int x2, int y2);
