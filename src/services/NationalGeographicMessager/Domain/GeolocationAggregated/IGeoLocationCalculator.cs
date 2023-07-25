using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NationalGeographicMessager.Domain.GeolocationAggregated
{
    public interface IGeoLocationCalculator
    {
        bool IsInsideRadius(
            double centerLatitude,
            double centerLongitude,
            double targetLatitude,
            double targetLongitude,
            double radius
            );

        double KilometerRadiusToDegreesRadius(double kilometerRadius);
    }
}
