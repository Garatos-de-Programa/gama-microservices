import IGeoLocationCalculator from "@domain/geolocation/IGeoLocationCalculator";
import Point from "@domain/geolocation/Point";
import IOccurrenceRepository from "@domain/occurrences/IOccurrenceRepository";
import OccurrenceEventMessage from "@domain/occurrences/OccurrenceEventMessage";
import ISocketConnection from "@infrastructure/Socket/ISocketConnection";
import net, { Server, Socket } from "net";

export default class SubscriberNetSocketConnection
  implements ISocketConnection
{
  private readonly _serverAddress: string = "localhost";
  private readonly _serverPort: number = 8080;
  private readonly _server: Server;
  private _clientSocketsRadius: Map<Socket, Point | null> = new Map();
  private readonly _occurrenceRepository : IOccurrenceRepository;
  private readonly _geolocationCalculator : IGeoLocationCalculator;

  constructor(
    occurrenceRepository : IOccurrenceRepository,
    geolocationCalculator : IGeoLocationCalculator
    ) {
    this._server = net.createServer((serverSocket) => {
      console.log("Connected to server");
    });

    this._server.on("close", () => {
      console.log("Connection closed");
    });

    this._server.on("connection", (socket) => {
      console.log(
        `Client connected from ${socket.remoteAddress}:${socket.remotePort}`
      );
      
      this._clientSocketsRadius.set(socket, null);
      socket.on('data', (data) => this.onMessageReceivedHandler(data.toString(), socket));

      socket.on("close", () => {
        console.log("Client disconnected");

        this._clientSocketsRadius.delete(socket);
      });
    });
    this._occurrenceRepository = occurrenceRepository;
    this._geolocationCalculator = geolocationCalculator;
    this._server.listen(this._serverPort, this._serverAddress);
  }

  write(data: Uint8Array): void {
    const decoder = new TextDecoder();
    const occurrenceString : string = decoder.decode(data);
    const occurrence : OccurrenceEventMessage = JSON.parse(occurrenceString);
    this._clientSocketsRadius.forEach((point, client) => {
      
      if(!point) {
        return;
      }

      const isInsideRadius = this._geolocationCalculator.isInsideRadius(
        point.Latitude,
        point.Longitude,
        occurrence.Latitude,
        occurrence.Longitude,
        point?.Radius
      );

      if (isInsideRadius) {
        client.write(data);
      }
    });
  }

  onMessageReceivedHandler(message: string, socket: Socket) {
    const data = JSON.parse(message);
    if (data.type === "subscribe") {
      const { latitude, longitude, radius } = data;
      let socketPoint = this._clientSocketsRadius.get(socket);
      this._occurrenceRepository.getAsync( {latitude, longitude}, radius)
        .then((occurrences: OccurrenceEventMessage[]) => {
          if (!socketPoint) {
            const json = JSON.stringify(occurrences);
            socket.write(json);
            const point = {
              Latitude: latitude,
              Longitude: longitude,
              Radius: radius
            } as Point
            this._clientSocketsRadius.set(socket, point);
            return
          }
          
          console.log(`Client subscribed to occurrence`);
      });
      
      return;
    } 
    
    if (data.type === "unsubscribe") {
      this._clientSocketsRadius.delete(socket);
      return;
    }
  }
}
