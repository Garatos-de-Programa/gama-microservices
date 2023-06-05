import IGeoLocationCalculator from "./IGeoLocationCalculator";

export default class GeoLocationCalculator implements IGeoLocationCalculator {
    private readonly EarthRadiusInMeters : number = 6371000;
    private readonly EarthRadiusInKilometers : number = 6371;

    isInsideRadius(
        centerLatitude: number, 
        centerLongitude: number, 
        targetLatitude: number, 
        targetLongitude: number,
        radius : number
        ): boolean {
        const distance = this.calculateDistance(
            centerLatitude, 
            centerLongitude, 
            targetLatitude, 
            targetLongitude
            );
        return distance <= radius;
    }

    calculateDistance(
        firstLatitude: number,
        firstLongitude: number, 
        secondLatitude: number, 
        secondLongitude: number
        ): number {
        const latitudeDifference = this.degreesToRadians(secondLatitude - firstLatitude);
        const longitudeDifference = this.degreesToRadians(secondLongitude - firstLongitude);
        
        const haversineCentralAngle =
            Math.sin(latitudeDifference / 2) * Math.sin(latitudeDifference / 2) +
            Math.cos(this.degreesToRadians(firstLatitude)) *
            Math.cos(this.degreesToRadians(secondLatitude)) *
            Math.sin(longitudeDifference / 2) *
            Math.sin(longitudeDifference / 2);
        
        const radiansCentralAngle = 2 * Math.atan2(Math.sqrt(haversineCentralAngle), Math.sqrt(1 - haversineCentralAngle));
        const distance = this.EarthRadiusInKilometers * radiansCentralAngle;
        return distance;
    }

    degreesToRadians(degrees: number): number {
        return degrees * (Math.PI / 180);
    }

    kilometerRadiusToDegreesRadius(kilometerRadius: number): number {
        return kilometerRadius / this.EarthRadiusInMeters * (180 / Math.PI);
    }
    
}