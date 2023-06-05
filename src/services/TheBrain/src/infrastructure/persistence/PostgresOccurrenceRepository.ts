import IOccurrenceRepository from "@domain/occurrences/IOccurrenceRepository";
import IGeoLocationCalculator from "@domain/geolocation/IGeoLocationCalculator";
import OccurrenceEventMessage from "@domain/occurrences/OccurrenceEventMessage";
import IDatabaseConnection from "./IDatabaseConnection";

export default class PostgresOccurrenceRepository implements IOccurrenceRepository {
    private readonly _rowDatabaseConnection : IDatabaseConnection
    private readonly _geolocationCalculator : IGeoLocationCalculator;

    constructor(
        databaseConnection : IDatabaseConnection,
        geolocationCalculator : IGeoLocationCalculator
        ) {
        this._rowDatabaseConnection = databaseConnection;
        this._geolocationCalculator = geolocationCalculator;
    }

    async getAsync(currentLocation: { latitude: number, longitude: number }, radius: number): Promise<OccurrenceEventMessage[]>{
        const connection = await this._rowDatabaseConnection.getPostgressConnectionAsync();
        const radiusInDegrees = this._geolocationCalculator.kilometerRadiusToDegreesRadius(radius);

        const query = `
        SELECT   
                occurrence_id
            ,   geolocation
            ,   location
            ,   occurrence_name
            ,   status_name
            ,   occurrence_type_name
            ,   occurrence_urgency_level_name
            ,   user_id
            ,   user_name
            ,   active
            ,   ST_X(geolocation::geometry) AS latitude
            ,   ST_Y(geolocation::geometry) AS longitude
        FROM public.occurrences
        WHERE
        ST_DWithin(
            geolocation::geography,
            ST_SetSRID(ST_MakePoint($1, $2), 4326)::geography,
            $3
          )
        `;
        
        const params = [currentLocation.latitude, currentLocation.longitude, radiusInDegrees];
        try {
            const result = await connection.query(query, params);
            return result.rows.map((row : any) => {
                return {
                    OccurrenceId: row.occurrence_id,
                    Latitude: row.latitude,
                    Longitude: row.longitude,
                    Location: row.location,
                    OccurrenceName: row.occurrence_name,
                    StatusName: row.status_name,
                    OccurrenceUrgencyLevelName: row.occurrence_urgency_level_name,
                    OccurrenceTypeName: row.occurrence_type_name,
                    UserId: row.user_id,
                    UserName: row.user_name,
                    Active: row.active
                } as OccurrenceEventMessage
            });
        } finally {
            connection.release();
          }
    }

}