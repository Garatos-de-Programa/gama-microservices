namespace NationalGeographicMessager.Domain.GeolocationAggregated
{
    public class GeoLocationCalculator : IGeoLocationCalculator
    {
        private const double EarthRadiusInMeters = 6371000;
        private const int EarthRadiusInKilometers = 6371;

        public bool IsInsideRadius(
            double centerLatitude, 
            double centerLongitude, 
            double targetLatitude, 
            double targetLongitude, 
            double radius
            )
        {
            var distance = CalculateDistance(
                centerLatitude,
                centerLongitude,
                targetLatitude,
                targetLongitude
            );

            return distance <= radius;
        }

        public double KilometerRadiusToDegreesRadius(double kilometerRadius)
        {
            return kilometerRadius / EarthRadiusInMeters * (180 / Math.PI);
        }

        internal static double CalculateDistance(
            double firstLatitude,
            double firstLongitude,
            double secondLatitude,
            double secondLongitude
            )
        {
            var latitudeDifference = DegreesToRadians(secondLatitude - firstLatitude);
            var longitudeDifference = DegreesToRadians(secondLongitude - firstLongitude);

            var haversineCentralAngle =
                Math.Sin(latitudeDifference / 2) * Math.Sin(latitudeDifference / 2) +
                Math.Cos(DegreesToRadians(firstLatitude)) *
                Math.Cos(DegreesToRadians(secondLatitude)) *
                Math.Sin(longitudeDifference / 2) *
                Math.Sin(longitudeDifference / 2);

            var radiansCentralAngle = 2 * Math.Atan2(Math.Sqrt(haversineCentralAngle), Math.Sqrt(1 - haversineCentralAngle));
            var distance = EarthRadiusInKilometers * radiansCentralAngle;
            return distance;
        }

        internal static double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

    }
}
