import OccurrenceEventMessage from "./OccurrenceEventMessage";

export default interface IOccurrenceRepository {
    getAsync(currentLocation: { latitude: number, longitude: number }, radius: number) : Promise<Array<OccurrenceEventMessage>>
}