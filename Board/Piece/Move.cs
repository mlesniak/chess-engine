namespace chess.Board.Piece;

public record Position(int X, int Y);

public record Move(Position Src, Position Dest)
{
    /// <summary>
    /// Parses a move of format a1b2 into a Move object.
    /// </summary>
    public static Move Parse(string line)
    {
        var srcx = line[0] - 'a';
        var srcy = '8' - line[1];
        var dstx = line[2] - 'a';
        var dsty = '8' - line[3];
        return new Move(new Position(srcx, srcy), new Position(dstx, dsty));
    }

    public override string ToString()
    {
        var s1 = IndexUtils.ToCol(Src.X);
        var t1 = IndexUtils.ToRow(Src.Y);
        var s2 = IndexUtils.ToCol(Dest.X);
        var t2 = IndexUtils.ToRow(Dest.Y);
        return $"{s1}{t1}-{s2}{t2}";
    }
}
