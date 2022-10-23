class PathNode
{

    public int X;
    public int Y;
    public int F;
    public int G;
    public int H;
    public PathNode Parent;

    public PathNode()
    {
    }

    public PathNode(int x, int y)
    {
        X = x;
        Y = y;
    }
}

