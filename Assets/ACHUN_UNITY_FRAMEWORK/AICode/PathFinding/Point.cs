namespace Achun.AI
{
    public class PathPoint
    {
        public PathPoint parent;

        public float F;
        public float G;
        public float H;

        public int x;
        public int y;
        public bool isWall;

        public PathPoint(int x, int y, bool isWall, PathPoint parent = null)
        {
            this.x = x;
            this.y = y;
            this.parent = parent;
            this.isWall = isWall;
        }
    }
}
