public static class Score
{
    public static readonly double StaleMate = 1000;

    public static double Calculate(Game game)
    {
        // Positive score is good for white, 
        // negative one is good for black.
        double score = 0;

        game.Iterate((_, _, piece) =>
        {
            switch (piece.Color)
            {
                // TODO(mlesniak) Remove this empty notion.
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
            Queen => 9,
            King => 100,
            _ => throw new ArgumentException()
        };
    }
}
