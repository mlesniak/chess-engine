using chess.Board.Piece;

namespace chess.Board;

public abstract class Loader
{
    public static Board Load(string filename)
    {
        var game = new Board();
        var lines = File.ReadAllLines(filename);

        for (var y = 0; y < 8; y++)
        {
            var pieces = lines[y].Split(" ");
            for (var x = 1; x < pieces.Length - 1; x++)
            {
                var pieceChar = pieces[x][0];
                if (pieceChar == '.')
                {
                    game.Pieces[y][x - 1] = null;
                    continue;
                }

                var color = pieceChar is >= 'a' and <= 'z'
                    ? Color.Black
                    : Color.White;
                Piece.Piece boardPiece = Char.ToLower(pieceChar) switch
                {
                    // We could use reflection to prevent duplicating 
                    // the display character of a piece, but given that
                    // we only have a very limited number of pieces to
                    // support, this reduces current complexity.
                    'q' => new Queen(color),
                    'x' => new Block(color),
                    'k' => new King(color),
                    'n' => new Knight(color),
                    _ => throw new ArgumentOutOfRangeException($"Unknown char {pieceChar}")
                };
                game.Pieces[y][x - 1] = boardPiece;
            }
        }

        return game;
    }
}
