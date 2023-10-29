public record Move(int x1, int y1, int x2, int y2)
{
    public override string ToString()
    {
        var s1 = Game.ToCol(x1);
        var t1 = Game.ToRow(y1);
        var s2 = Game.ToCol(x2);
        var t2 = Game.ToRow(y2);

        return $"{s1}{t1}-{s2}{t2}";
    }
}
