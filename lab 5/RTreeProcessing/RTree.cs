using System;
using System.Linq;

namespace RTreeProcessing
{
    public class RTree
    {
        private bool IsParent = false;
        public int Size { get; private set; }
        
        private int _maxSize = 10;
        public double xMax  { get; private set; }
        public double xMin { get; private set; }
        public double yMax { get; private set; }
        public double yMin { get; private set; }
        
        public GeographicalPoint[] Points;
        
        private RTree Child1, Child2;

        public void Add(GeographicalPoint geographicalPoint)
        {
            Size++;
            if (Size == 1)
            {
                Points = new GeographicalPoint[_maxSize];
                xMax = xMin = geographicalPoint.Latitude;
                yMax = yMin = geographicalPoint.Longitude;
                Points[Size-1] = geographicalPoint;
            }
            else
            {
                xMax = Math.Max(geographicalPoint.Latitude, xMax);
                yMax = Math.Max(geographicalPoint.Longitude, yMax);
                xMin = Math.Min(geographicalPoint.Latitude, xMin);
                yMin = Math.Min(geographicalPoint.Longitude, yMin);
                if (IsParent)
                {
                    if (IsMoreOptimalToIncludeInFirstChild(Child1, Child2, geographicalPoint)) Child1.Add(geographicalPoint);
                    else Child2.Add(geographicalPoint);
                }
                else
                {
                    Points[Size-1] = geographicalPoint;
                    if (Size == _maxSize) Divide();
                }
            }
        }
        private static int CompareFirstlyByCoordinateX(GeographicalPoint e1, GeographicalPoint e2)
        {
            if (e1.Latitude > e2.Latitude) return 1;
            if (e1.Latitude < e2.Latitude) return -1;
            if (e1.Longitude > e2.Longitude) return 1; // if e1.X == e2.X
            if (e1.Longitude < e2.Longitude) return -1;
            return String.Compare(e1.Name, e2.Name, StringComparison.Ordinal); // if e1.X == e2.X and e1.Y == e2.Y
        }

        private static int CompareFirstlyByCoordinateY(GeographicalPoint e1, GeographicalPoint e2)
        {
            if (e1.Longitude > e2.Longitude) return 1;
            if (e1.Longitude < e2.Longitude) return -1;
            if (e1.Latitude > e2.Latitude) return 1; // if e1.Y == e2.Y
            if (e1.Latitude < e2.Latitude) return -1;
            return String.Compare(e1.Name, e2.Name, StringComparison.Ordinal); // if e1.X == e2.X and e1.Y == e2.Y
        }

        private void Divide()
        {
            IsParent = true;
            Child1 = new RTree();
            Child2 = new RTree();
            if (xMax-xMin>yMax-yMin) Array.Sort(Points, CompareFirstlyByCoordinateX);
            else Array.Sort(Points, CompareFirstlyByCoordinateY);
            Child1.Add(Points[0]);
            Child2.Add(Points[Size-1]);
            foreach (GeographicalPoint entity in Points[1..^1])
            {
                if (IsMoreOptimalToIncludeInFirstChild(Child1, Child2, entity)) Child1.Add(entity);
                else Child2.Add(entity);
            }

            Points = null;
        }
        private static bool IsMoreOptimalToIncludeInFirstChild(RTree firstChild, RTree secondChild, GeographicalPoint point)
        {
            //imagine that we add a point to the first node
            double newXMax1 = Math.Max(firstChild.xMax, point.Latitude);
            double newXMin1 = Math.Min(firstChild.xMin, point.Latitude);
            double newYMax1 = Math.Max(firstChild.yMax, point.Longitude);
            double newYMin1 = Math.Min(firstChild.yMin, point.Longitude);
            double delta_S1 = (newXMax1 - newXMin1) * (newYMax1 - newYMin1) - (firstChild.xMax - firstChild.xMin) * (firstChild.yMax - firstChild.yMin); 
            //imagine that we add a point to the second node
            double newXMax2 = Math.Max(secondChild.xMax, point.Latitude);
            double newXMin2 = Math.Min(secondChild.xMin, point.Latitude);
            double newYMax2 = Math.Max(secondChild.yMax, point.Longitude);
            double newYMin2 = Math.Min(secondChild.yMin, point.Longitude);
            double delta_S2 = (newXMax2 - newXMin2) * (newYMax2 - newYMin2) - (secondChild.xMax - secondChild.xMin) * (secondChild.yMax - secondChild.yMin);
            //in which case the area will be smaller?
            return delta_S1 != delta_S2 ? delta_S1 < delta_S2 : firstChild.Size < secondChild.Size;
        }

        private bool HasIntersectionWithArea((double, double) point, int radius)
        {
            if (xMax > point.Item1 && point.Item1 > xMin && yMax > point.Item2 && point.Item2 > yMin) return true;
            double nearestVerticalEdge = Math.Abs(xMax - point.Item1) < Math.Abs(xMin - point.Item1) ? xMax : xMin;
            double nearestHorizontalEdge = Math.Abs(yMax - point.Item2) < Math.Abs(yMin - point.Item2) ? yMax : yMin;
            if (Math.Abs(nearestVerticalEdge - point.Item1) < Math.Abs(nearestHorizontalEdge - point.Item2))
            {
                return new GeographicalPoint(nearestVerticalEdge, point.Item2, "", "", "",
                    "") - point <= radius && (point.Item1 < xMax && point.Item1 > xMin || 
                    new GeographicalPoint(nearestVerticalEdge, nearestHorizontalEdge, "", "",
                        "", "") - point <= radius);
            }
            return new GeographicalPoint(point.Item1, nearestHorizontalEdge, "", "", "",
              "") - point <= radius && (point.Item2 < yMax && point.Item2 > yMin ||
              new GeographicalPoint(nearestVerticalEdge, nearestHorizontalEdge, "",
              "", "", "") - point <= radius);

        }
        
        public GeographicalPoint[] FindNearest(double targetX, double targetY, int radius)
        {
            if (!HasIntersectionWithArea((targetX, targetY), radius)) return Array.Empty<GeographicalPoint>();
            if (IsParent)
            {
                return Child1.FindNearest(targetX, targetY, radius)
                    .Concat(Child2.FindNearest(targetX, targetY, radius)).ToArray();
            }
            
            return Points.Where(p1 => p1 - (targetX, targetY) <= radius).ToArray();
        }
    }
}