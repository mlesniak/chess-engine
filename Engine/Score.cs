public class Score
{
    public static int Calculate(Game game)
    {
        // Positive score is good for white, 
        // negative one is good for black.
        var score = 0;

        // If the king is caught, we "won".
        var whiteKing = false;
        var blackKing = false;
        game.Iterate((_, _, piece) =>
        {
            if (piece.GetType() == typeof(King))
            {
                switch (piece.Color)
                {
                    case Color.Empty:
                        break;
                    case Color.White:
                        whiteKing = true;
                        break;
                    case Color.Black:
                        blackKing = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        });
        if (!whiteKing)
        {
            return Int32.MinValue;
        }
        if (!blackKing)
        {
            return Int32.MaxValue;
        }


        game.Iterate((_, _, piece) =>
        {
            if (piece.GetType() == typeof(Rook))
            {
                return;
            }
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
}
