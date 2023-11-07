using System.Text;

public class Game
{
    public Color Turn = Color.White;

    public Game()
    {
        var width = 8;
        var height = 8;

        Board = new Piece[height][];
        for (var y = 0; y < 8; y++)
        {
            Board[y] = new Piece[width];
            Array.Fill(Board[y], Piece.Empty, 0, width);
        }
    }

    // Copy constructor.
    private Game(Game src)
    {
        var width = 8;
        var height = 8;

        Board = new Piece[height][];
        for (var y = 0; y < height; y++)
        {
            Board[y] = new Piece[width];
            for (var x = 0; x < Board[y].Length; x++)
            {
                Board[y][x] = src.Board[y][x].Copy();
            }
        }
    }

    // 0/0 is in the lower left corner.
    public Piece[][] Board { get; }

    public Game Move(Move move)
    {
        // Create a copy with the move applied.
        var copy = new Game(this);
        copy.Board[move.Dest.Y][move.Dest.X] = Board[move.Src.Y][move.Src.X];
        copy.Board[move.Src.Y][move.Src.X] = Piece.Empty;
        copy.Turn = Turn.Next();
        return copy;
    }

    public void Iterate(Action<int, int, Piece> action)
    {
        for (var y = Board.Length - 1; y >= 0; y--)
        {
            for (var x = 0; x < Board[y].Length; x++)
            {
                var piece = Board[y][x];
                if (piece.GetType() == typeof(Block))
                {
                    continue;
                }
                action(x, y, piece);
            }
        }
    }
    
    public List<(Piece, Position)> Find(Func<int, int, Piece, bool> predicate)
    {
        List<(Piece, Position)> pieces = new();

        for (var y = 0; y < 8; y++)
        {
            for (var x = 0; x < Board[y].Length; x++)
            {
                var piece = Board[y][x];
                if (predicate(x, y, piece))
                {
                    // Subtract since the lower left corner is 0/0.
                    pieces.Add((piece, new Position(x, y)));
                }
            }
        }

        return pieces;
    }

    public List<Move> LegalMoves(Color color)
    {
        List<Move> moves = new();

        for (var y = Board.Length - 1; y >= 0; y--)
        {
            for (var x = 0; x < Board[y].Length; x++)
            {
                var piece = Board[y][x];
                if (piece.Color != color)
                {
                    continue;
                }
                var validMoves = piece.ValidMoves(this, Turn, new Position(x, y));
                moves.AddRange(validMoves);
            }
        }

        return moves;
    }

    private char withColor(Color color, char display)
    {
        return color switch
        {
            Color.White => Char.ToUpper(display),
            Color.Black => Char.ToLower(display),
            Color.Empty => display,
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, "Unknown value")
        };
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var y = 0; y < 8; y++)
        {
            sb.Append($"{ToRow(y)} ");
            for (var x = 0; x < Board[y].Length; x++)
            {
                var piece = Board[y][x];
                var c = withColor(piece.Color, piece.DisplayCharacter());
                sb.Append(c);
                sb.Append(" ");
            }
            sb.AppendLine();
        }
        sb.Append("  ");
        for (var x = 0; x < Board[0].Length; x++)
        {
            sb.Append($"{ToCol(x)} ");
        }
        sb.Append($"\n  - {Turn} to move");
        sb.Append($"\n  - Score: {Score.Calculate(this)}");

        return sb.ToString();
    }

    public static char ToRow(int y) => (char)('8' - y);

    public static char ToCol(int x) => (char)('a' + x);
}
