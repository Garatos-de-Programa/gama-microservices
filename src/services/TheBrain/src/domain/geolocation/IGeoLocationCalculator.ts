export default interface IGeoLocationCalculator {
    isInsideRadius(
        centerLatitude : number,
        centerLongitude : number,
        targetLatitude : number,
        targetLongitude : number,
        radius : number
    ) : boolean;

    calculateDistance(
        firstLatitude : number,
        firstLongitude : number,
        secondLatitude : number,
        secondLongitude : number
        ) : number;

    degreesToRadians(degrees : number) : number;
    kilometerRadiusToDegreesRadius(kilometerRadius : number) : number;
}