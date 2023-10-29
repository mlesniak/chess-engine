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
}
