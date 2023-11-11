namespace chess.Board;

public static class IndexUtils
{
    public static char ToRow(int y) => (char)('8' - y);

    public static char ToCol(int x) => (char)('a' + x);
}
