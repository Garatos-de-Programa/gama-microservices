import IncidentMessage from '@domain/messenger/IncidentMessage';

export default class OccurrenceEventMessage {
    public OccurrenceId: number = 0;
    public Latitude: number = 0;
    public Longitude: number = 0;
    public Location: string | null = null;
    public OccurrenceName: string | null = null;
    public StatusName: string | null = null;
    public OccurrenceUrgencyLevelName: string | null = null;
    public OccurrenceTypeName: string | null = null;
    public UserId: number = 0;
    public UserName: string | null = null;
    public Active: boolean = false;

    toIncidentMessage() : IncidentMessage {
        const messageString = JSON.stringify(this);
        return new IncidentMessage(messageString);
    }
}