namespace NationalGeographicMessager.Domain.GeolocationAggregated
{
    public struct Point
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Radius { get; set; }

        public Point()
        {
        }

        public Point(double latitude, double longitude, double radius)
        {
            Latitude = latitude;
            Longitude = longitude;
            Radius = radius;
        }
    }
}
