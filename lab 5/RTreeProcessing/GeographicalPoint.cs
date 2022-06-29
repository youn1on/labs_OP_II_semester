using System;

namespace RTreeProcessing
{
    public struct GeographicalPoint
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        
        public string Type { get; private set; }
        public string Subtype { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        
        public GeographicalPoint(double latitude, double longitude, string type, string subtype, string name, string address)
        {
            Longitude = latitude;
            Latitude = longitude;
            Type = type;
            Subtype = subtype;
            Name = name != "" ? name : subtype;
            Address = address;
        }
        
        public static double operator -(GeographicalPoint point1, (double, double) point2)
        {
            // using haversine formula:
            const double earthRadius = 6371.0087714;
            double f1 = point1.Latitude * Math.PI/180;
            double f2 = point2.Item1 * Math.PI/180;
            double a = Math.Pow(Math.Sin((f1 - f2) / 2), 2) + Math.Cos(f1) * Math.Cos(f2) *
                Math.Pow(Math.Sin(((point1.Longitude - point2.Item2) * Math.PI / 180) / 2), 2);
            return earthRadius * 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
        }
    }
}