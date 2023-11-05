using System.Security.AccessControl;

using static Color;

public class Engine
{
    // We use a static seed for some determinism. Later on, this
    // can be replaced with Random.Shared.
    private static Random random = new(11031981);

    public static bool IsCurrentColorMate(Game game)
    {
        // Check if the next BEST move would still lead
        // to a position in which the king would be in
        // chess. In this case, we have a mate (I guess).
        var move = NextBestMove(game, game.Turn, 1);
        var (_, kingPos) = game.Find((_, _, piece) => piece.GetType() == typeof(King) && piece.Color == game.Turn)[0];

        Console.WriteLine("move = {0}", move);
        Console.WriteLine("kingPos = {0}", kingPos);

        return move.Item1.Dest == kingPos;
    }

    public static bool IsCurrentColorInChess(Game game)
    {
        // Check if the next move would move to the King's position for the side
        // that's currently not in turn.
        var move = NextBestMove(game, game.Turn.Next(), 1);
        var (_, kingPos) = game.Find((_, _, piece) => piece.GetType() == typeof(King) && piece.Color == game.Turn)[0];
        return move.Item1.Dest == kingPos;
    }

    public static (Move, double) NextBestMove(Game game, Color currentColor, int depth = 1)
    {
        // TODO(mlesniak) currently, we simply ignore the current color.

        var allMoves = game.ValidMoves(currentColor);

        // No moves means that there are no pieces on the
        // board anymore, i.e. even the king has been
        // captured. 
        //
        // Maybe I'll find a better design choice, this
        // is ugly and lead to bugs in the past.
        // 
        // Should we abort before?
        if (allMoves.Count == 0)
        {
            if (currentColor == White)
            {
                return (new Move(new Position(0, 0), new Position(0, 0)), -1000 + depth * 2);
            }

            return (new Move(new Position(0, 0), new Position(0, 0)), +1000 - depth * 2);
        }

        List<(Move, double)> evaluatedMoves;
        if (depth == 1)
        {
            evaluatedMoves = allMoves
                .Select(move =>
                {
                    var newGame = game.Move(move);
                    var score = Score.Calculate(newGame);
                    return (move, score);
                })
                .OrderBy<(Move, double), double>(t => t.Item2)
                .ToList();
        }
        else
        {
            evaluatedMoves = allMoves.Select(myMove =>
                {
                    var newGameState = game.Move(myMove);
                    var bestMoveInNewGameForOpponent = NextBestMove(newGameState, currentColor.Next(), depth - 1);
                    return (myMove, bestMoveInNewGameForOpponent.Item2);
                })
                .OrderBy(t => t.Item2)
                .ToList();
        }

        if (depth == 5)
        {
            Console.WriteLine($"--- (Turn {currentColor})");
            foreach (var choice in evaluatedMoves)
            {
                Console.Write("{0}\t", choice);
            }
            Console.WriteLine();
        }

        if (currentColor == White)
        {
            // Pick a random move.
            var score = evaluatedMoves.Last().Item2;
            var rnd = evaluatedMoves.SkipWhile(move => move.Item2 != score).ToList();
            return rnd[random.Next() % rnd.Count];
            // return evaluatedMoves.Last();
        }

        var score2 = evaluatedMoves.First().Item2;
        var rnd2 = evaluatedMoves.TakeWhile(move => move.Item2 == score2).ToList();
        return rnd2[random.Next() % rnd2.Count];
        // return evaluatedMoves.First();
    }
}
