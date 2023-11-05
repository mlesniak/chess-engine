public class Score
{
    public static double Calculate(Game game)
    {
        // Positive score is good for white, 
        // negative one is good for black.
        double score = 0;

        game.Iterate((_, _, piece) =>
        {
            // Rooks are currently more rocks, 
            // blocking paths for both pieces.
            if (piece.GetType() == typeof(Rook))
            {
                return;
            }
                                                                    
            switch (piece.Color)
            {
                case Color.Empty:
                    break;
                case Color.White:
                    score += PieceScore(piece);
                    break;
                case Color.Black:
                    score -= PieceScore(piece);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });

        return score;
    }

    private static int PieceScore(Piece piece)
    {
        return piece switch
        {
            Rook => 5,
            Queen => 9,
            King => 100,
            _ => throw new ArgumentException()
        };
    }
}
