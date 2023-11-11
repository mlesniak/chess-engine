namespace chess.Board;

using Piece;

using System.Text;

using Engine;

public class Board
{
    private const int Width = 8;
    private const int Height = 8;
    public Color Turn = Color.White;

    public Board()
    {
        Pieces = new Piece.Piece[Height][];
        for (var y = 0; y < 8; y++)
        {
            Pieces[y] = new Piece.Piece[Width];
            Array.Fill(Pieces[y], null, 0, Width);
        }
    }

    // Copy constructor.
    private Board(Board src)
    {
        Pieces = new Piece.Piece[Height][];
        for (var y = 0; y < Height; y++)
        {
            Pieces[y] = new Piece.Piece[Width];
            for (var x = 0; x < Pieces[y].Length; x++)
            {
                Pieces[y][x] = src.Pieces[y][x];
                // Pieces[y][x] = src.Pieces[y][x].Copy();
            }
        }
    }

    // 0/0 is in the lower left corner.
    public Piece.Piece?[][] Pieces { get; }

    public Board Move(Move move)
    {
        // Create a copy with the move applied.
        var copy = new Board(this);
        copy.Pieces[move.Dest.Y][move.Dest.X] = Pieces[move.Src.Y][move.Src.X];
        copy.Pieces[move.Src.Y][move.Src.X] = null;
        copy.Turn = Turn.Next();
        return copy;
    }

    public void ForEach(Action<int, int, Piece.Piece> action)
    {
        for (var y = Pieces.Length - 1; y >= 0; y--)
        {
            for (var x = 0; x < Pieces[y].Length; x++)
            {
                var piece = Pieces[y][x];
                if (piece == null)
                {
                    continue;
                }
                // BLOCK 
                if (piece.GetType() == typeof(Block))
                {
                    continue;
                }
                action(x, y, piece);
            }
        }
    }

    public List<Move> LegalMoves(Color color)
    {
        List<Move> moves = new();

        for (var y = Pieces.Length - 1; y >= 0; y--)
        {
            for (var x = 0; x < Pieces[y].Length; x++)
            {
                var piece = Pieces[y][x];
                if (piece == null || piece.Color != color)
                {
                    continue;
                }
                var validMoves = piece.AvailableMoves(this, new Position(x, y));
                moves.AddRange(validMoves);
            }
        }

        return moves;
    }

    private char WithColor(Color color, char display)
    {
        return color switch
        {
            Color.White => Char.ToUpper(display),
            Color.Black => Char.ToLower(display),
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, "Unknown value")
        };
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var y = 0; y < 8; y++)
        {
            sb.Append($"{IndexUtils.ToRow(y)} ");
            for (var x = 0; x < Pieces[y].Length; x++)
            {
                var piece = Pieces[y][x];
                if (piece == null)
                {
                    sb.Append(". ");
                    continue;   
                }
                var c = WithColor(piece.Color, piece.Character);
                sb.Append(c);
                sb.Append(" ");
            }
            sb.AppendLine();
        }
        sb.Append("  ");
        for (var x = 0; x < Pieces[0].Length; x++)
        {
            sb.Append($"{IndexUtils.ToCol(x)} ");
        }
        
        return sb.ToString();
    }
}
