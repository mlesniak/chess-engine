using static Color;

public class Engine
{
    // We use a static seed for some determinism. Later on, this
    // can be replaced with Random.Shared.
    private static Random random = new(11031981);

    // TODO(mlesniak) check for mate by setting a depth of 1, maybe even a dedicated method?

    // TODO(mlesniak) this is actually a move which is not allowed to be made since the
    //                king would be captured.
    public static bool NextMoveMate(Game game)
    {
        // Check if the next move would move to the King's position for the side
        // that's currently not in turn.
        var move = NextBestMove(game, game.Turn, 1);

        var opponent = game.Turn.Next();
        var (_, kingPos) = game.Find((x, y, piece) => piece.GetType() == typeof(King) && piece.Color == opponent)[0];
        
        Console.WriteLine("move = {0}", move);
        Console.WriteLine("kingPos = {0}", kingPos);

        return move.Item1.Dest == kingPos;
    }

    // TODO(mlesniak) count number of occupied squares?
    // Depth is in half-moves.
    public static (Move, double) NextBestMove(Game game, Color currentColor, int depth = 1)
    {

        // TODO(mlesniak) Moves which will mate shall have the highest score.

        var allMoves = game.ValidMoves();

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
