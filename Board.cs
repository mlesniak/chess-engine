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

    // Copy constructor.
    public Game(Game src)
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
        copy.Board[move.y2][move.x2] = Board[move.y1][move.x1];
        copy.Board[move.y1][move.x1] = Piece.Empty;
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
                case Piece.PieceColor.Empty:
                    break;
                case Piece.PieceColor.White:
                    score++;
                    break;
                case Piece.PieceColor.Black:
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

    private char withColor(Piece.PieceColor color, char display)
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
            sb.Append($"{y} ");
            for (int x = 0; x < Board[y].Length; x++)
            {
                var piece = Board[y][x];
                var c = withColor(piece.Color, piece.Display());
                sb.Append(c);
                sb.Append(" ");
            }
            sb.AppendLine();
        }
        sb.Append("  ");
        for (int x = 0; x < Board[0].Length; x++)
        {
            sb.Append($"{x} ");
        }
        var score = Score();
        sb.Append($"\tScore: {score}");

        return sb.ToString();
    }
}


