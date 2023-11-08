public static class Score
{
    public static double Calculate(Game game)
    {
        // TODO(mlesniak) this is kind of ugly and should not be necessary.
        if (Engine.IsMate(game, Color.Black))
        {
            return 10000;
        }
        if (Engine.IsMate(game, Color.White))
        {
            return -10000;
        }

        double score = 0;
        game.Iterate((_, _, piece) =>
        {
            score += PieceValue(piece).WithColor(piece.Color);
        });

        return score;
    }

    private static double WithColor(this double number, Color color)
    {
        if (color == Color.Black)
        {
            return -number;
        }

        return number;
    }

    private static double PieceValue(Piece piece)
    {
        return piece switch
        {
            Empty => 0.0,
            Queen => 9.0,
            King => 1000.0,
            _ => throw new ArgumentException($"No value for {piece.GetType()}")
        };
    }
}
