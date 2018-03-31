//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AStar : MonoBehaviour
//{
//    private Point[,] map;
//    private List<Point> openList;
//    private List<Point> closeList;

//    private int mapWidth = 8;
//    private int mapHeight = 6;

//    private void Start()
//    {
//        openList = new List<Point>();
//        closeList = new List<Point>();
//        InitMap(8, 6);
//        FindPath(map[2, 3], map[6, 3]);
//    }


//    private void InitMap(int x, int y)
//    {
//        map = new Point[x, y];
//        for (int i = 0; i < x; i++)
//            for (int j = 0; j < y; j++)
//            {
//                map[i, j] = new Point(i, j, false);
//            }

//        map[4, 2].isWall = true;
//        map[4, 3].isWall = true;
//        map[4, 4].isWall = true;
//    }


//    private void FindPath(Point startPoint, Point EndPoint)
//    {
//        openList.Add(startPoint);
//        while (openList.Count > 0)
//        {
//            if (openList.IndexOf(EndPoint) > -1)
//                break;

//            Point point = FindMinFOfPoint(openList);
//            openList.Remove(point);
//            closeList.Add(point);
//            List<Point> surroundPoints = GetSurroundPoints(point);
//            PointsFilter(surroundPoints, closeList);

//            foreach (Point surroundPoint in surroundPoints)
//                if (openList.IndexOf(surroundPoint) > -1)
//                {
//                    float nowG = CalculateG(surroundPoint, point);
//                    if (nowG < surroundPoint.G)
//                    {
//                        surroundPoint.parent = point;
//                        surroundPoint.G = nowG;
//                        surroundPoint.F = surroundPoint.H + nowG;
//                    }
//                }
//                else
//                {
//                    surroundPoint.parent = point;
//                    CalculateF(surroundPoint,EndPoint);
//                    openList.Add(surroundPoint);
//                }
//        }
//    }


//    private List<Point> GetSurroundPoints(Point point)
//    {
//        Point up = null;
//        Point down = null;
//        Point left = null;
//        Point right = null;
//        Point up_left = null;
//        Point up_right = null;
//        Point down_left = null;
//        Point down_right = null;

//        if (point.y >= mapWidth - 1)
//            up = null;
//        else
//            up = map[point.x, point.y + 1];

//        if (point.y <= 0)
//            down = null;
//        else
//            down = map[point.x, point.y - 1];

//        if (point.x >= mapHeight - 1)
//            right = null;
//        else
//            right = map[point.x + 1, point.y];

//        if (point.x <= 0)
//            left = null;
//        else
//            left = map[point.x - 1, point.y];

//        if (up != null && left != null)
//            up_left = map[point.x - 1, point.y + 1];
//        if (up != null && right != null)
//            up_right = map[point.x + 1, point.y + 1];

//        if (down != null && left != null)
//            down_left = map[point.x - 1, point.y - 1];
//        if (down != null && right != null)
//            down_right = map[point.x + 1, point.y - 1];

//        List<Point> list = new List<Point>();
//        if (down != null && !down.isWall)
//            list.Add(down);

//        if (up_left != null && !up_left.isWall && !left.isWall && !up.isWall)
//            list.Add(up_left);

//        return list;
//    }


//    private void PointsFilter(List<Point> list, List<Point> closeList)
//    {
//        for (int i = list.Count - 1; i >= 0; i--)
//            if (closeList.Contains(list[i]))
//                list.RemoveAt(i);
//    }


//    private Point FindMinFOfPoint(List<Point> openList)
//    {
//        float f = float.MaxValue;
//        Point tmp = null;
//        foreach (Point p in openList)
//            if (p.F < f)
//            {
//                tmp = p;
//                f = p.F;
//            }

//        return tmp;
//    }


//    private void CalculateF(Point nowPoint, Point endPoint)
//    {
//        //F = G + H
//        float h = Mathf.Abs(endPoint.x - nowPoint.x) + Mathf.Abs(endPoint.y - nowPoint.y);
//        float g = 0;
//        if (nowPoint.parent != null)
//            g = Vector2.Distance(new Vector2(nowPoint.x, nowPoint.y), new Vector2(nowPoint.parent.x, nowPoint.parent.y)) + nowPoint.parent.G;
//        float f = g + h;
//        nowPoint.F = f;
//        nowPoint.G = g;
//        nowPoint.F = f;
//    }


//    private float CalculateG(Point nowPoint, Point parentPoint)
//    {
//        return Vector2.Distance(new Vector2(nowPoint.x, nowPoint.y), new Vector2(nowPoint.parent.x, nowPoint.parent.y)) + nowPoint.parent.G;
//    }
//}

using System.Collections.Generic;
using UnityEngine;

namespace Achun.AI
{
    public class PathFinding
    {
        private List<PathPoint> openList;
        private List<PathPoint> surroundPoints;
        public Queue<PathPoint> path;

        public delegate float CalculateG(PathPoint nowPoint, PathPoint parentPoint);
        public event CalculateG CalG;

        public delegate void CalculateF(PathPoint nowPoint, PathPoint tagPoint);
        public event CalculateF CalF;

        public delegate float CalculateH();
        public event CalculateH CalH;

        public PathFinding()
        {
            openList = new List<PathPoint>();
            surroundPoints = new List<PathPoint>();
            path = new Queue<PathPoint>();
        }


        public void FindPath(PathPoint startPoint,PathPoint endPoint)
        {
            openList.Clear();
            path.Clear();
            openList.Add(startPoint);

            while (openList.Count > 0)
            {
                PathPoint point = FindMinFOfList(openList);
                openList.Remove(point);
                path.Enqueue(point);

                surroundPoints.Clear();
                GetSurroundPoints(point,surroundPoints);
                foreach (PathPoint p in surroundPoints)
                {
                    if (openList.Contains(p))
                    {
                        float nowG = float.MaxValue;
                        if (CalG != null)
                            nowG = CalG(p,point);
                        if (nowG < p.G)
                        {
                            p.parent = point;
                            p.G = nowG;
                            p.F = p.G + p.H;
                        }
                    }
                    else
                    {
                        p.parent = point;
                        if (CalF != null)
                            CalF(p,point);
                        openList.Add(p);
                    }
                    if (openList.Contains(endPoint))
                        break;
                }
            }
        }


        private PathPoint FindMinFOfList(ICollection<PathPoint> points)
        {
            float f = float.MaxValue;
            PathPoint ret = null;
            foreach (PathPoint p in points)
            {
                if (p.F < f && !p.isWall)
                {
                    f = p.F;
                    ret = p;
                }
            }
            return ret;
        }


        private void GetSurroundPoints(PathPoint point,ICollection<PathPoint> ret)
        {

        }
    }
}
