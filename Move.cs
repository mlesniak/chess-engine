public record Move(Position Src, Position Dest)
{
    public override string ToString()
    {
        var s1 = Game.ToCol(Src.X);
        var t1 = Game.ToRow(Src.Y);
        var s2 = Game.ToCol(Dest.X);
        var t2 = Game.ToRow(Dest.Y);

        return $"{s1}{t1}-{s2}{t2}";
    }

    // a2b5
    public static Move Parse(string line)
    {
        var srcx = line[0] - 'a';
        var srcy = line[1] - '1';
        var dstx = line[2] - 'a';
        var dsty = line[3] - '1';
        return new Move(new Position(srcx, srcy), new Position(dstx, dsty));
    }
}
