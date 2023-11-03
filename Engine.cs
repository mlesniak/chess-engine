using static Color;

public class Engine
{
    // Depth is in half-moves.

    // TODO(mlesniak) check for mate by setting a depth of 1, maybe even a dedicated method?

    public static (Move, int) NextBestMove(Game game, Color currentColor, int depth = 1)
    {
        List<Move> allMoves = game.ValidMoves();

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
                return (new Move(new Position(0, 0), new Position(0, 0)), -1000 + depth*2);
            }
            
            return (new Move(new Position(0, 0), new Position(0, 0)), +1000 - depth*2);
        }

        List<(Move, int)> evaluatedMoves;
        if (depth == 1)
        {
            evaluatedMoves = allMoves
                .Select(move =>
                {
                    var newGame = game.Move(move);
                    var score = newGame.Score();
                    return (move, score);
                })
                .OrderBy<(Move, int), int>(t => t.Item2)
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
            return rnd[Random.Shared.Next() % rnd.Count];
            // return evaluatedMoves.Last();
        }

        var score2 = evaluatedMoves.First().Item2;
        var rnd2 = evaluatedMoves.TakeWhile(move => move.Item2 == score2).ToList();
        return rnd2[Random.Shared.Next() % rnd2.Count];
        // return evaluatedMoves.First();
    }
}
