// We ignore game states such as en passant, rochade, etc. 
// for the time being, since it's ignored by the engine 
// anyway...
public class Loader
{
    public static Game Load(string filename)
    {
        var game = new Game();
        string[] lines = File.ReadAllLines(filename);

        for (int y = 0; y < 8; y++)
        {
            var pieces = lines[8 - y - 1].Split(" ");
            for (int x = 1; x < pieces.Length - 1; x++)
            {
                char pieceChar = pieces[x][0];
                if (pieceChar == '.')
                {
                    game.Board[y][x - 1] = Piece.Empty;
                    continue;
                }

                var color = pieceChar is >= 'a' and <= 'z'
                    ? Color.Black
                    : Color.White;
                Piece boardPiece = Char.ToLower(pieceChar) switch
                {
                    'q' => new Queen(color),
                    'r' => new Rook(color),
                    'k' => new King(color),
                    _ => throw new ArgumentOutOfRangeException($"Unknown char {pieceChar}")
                };
                game.Board[y][x - 1] = boardPiece;
            }
        }

        return game;
    }
}
