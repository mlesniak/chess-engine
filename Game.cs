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
        for (int y = 0; y < 8; y++)
        {
            Board[y] = new Piece[width];
            Array.Fill(Board[y], Piece.Empty, 0, width);
        }
    }

    // Copy constructor.
    private Game(Game src)
    {
        int width = 8;
        int height = 8;

        Board = new Piece[height][];
        for (int y = 0; y < height; y++)
        {
            Board[y] = new Piece[width];
            for (int x = 0; x < Board[y].Length; x++)
            {
                Board[y][x] = src.Board[y][x].Copy();
            }
        }
    }

    public Game Move(Move move)
    {
        // Create a copy with the move applied.
        var copy = new Game(this);
        copy.Board[move.Dest.Y][move.Dest.X] = Board[move.Src.Y][move.Src.X];
        copy.Board[move.Src.Y][move.Src.X] = Piece.Empty;
        return copy;
    }

    // Compute a basic score by counting number of pieces,
    // even ignoring their respective values and everything
    // else.
    // 
    // Since we have no king yet, obviously no mentioning of
    // checkmate or other winning conditions.
    public int Score()
    {
        // Positive score is good for white, 
        // negative one is good for black.
        var score = 0;

        Iterate((_, _, piece) =>
        {
            switch (piece.Color)
            {
                case Color.Empty:
                    break;
                case Color.White:
                    score++;
                    break;
                case Color.Black:
                    score--;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });

        return score;
    }

    private void Iterate(Action<int, int, Piece> action)
    {
        for (int y = Board.Length - 1; y >= 0; y--)
        {
            for (int x = 0; x < Board[y].Length; x++)
            {
                var piece = Board[y][x];
                action(x, y, piece);
            }
        }
    }

    // TODO(mlesniak) we do not distinguish yet between black and white.
    public List<Move> ValidMoves()
    {
        List<Move> moves = new();

        for (int y = Board.Length - 1; y >= 0; y--)
        {
            for (int x = 0; x < Board[y].Length; x++)
            {
                var piece = Board[y][x];
                if (piece.Color == Color.Black || piece.GetType() != typeof(Queen))
                {
                    continue;
                }
                moves.AddRange(piece.ValidMoves(this, Color.White, new Position(x, y)));
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
        for (int y = Board.Length - 1; y >= 0; y--)
        {
            sb.Append($"{ToRow(y)} ");
            for (int x = 0; x < Board[y].Length; x++)
            {
                var piece = Board[y][x];
                var c = withColor(piece.Color, piece.DisplayCharacter());
                sb.Append(c);
                sb.Append(" ");
            }
            sb.AppendLine();
        }
        sb.Append("  ");
        for (int x = 0; x < Board[0].Length; x++)
        {
            sb.Append($"{ToCol(x)} ");
        }
        var score = Score();
        sb.Append($"\tScore: {score}");

        return sb.ToString();
    }

    public static char ToRow(int y) => (char)('1' + y);

    public static char ToCol(int x) => (char)('a' + x);
}
