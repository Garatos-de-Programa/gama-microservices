import OccurrenceEventMessage from "@domain/occurrences/Occurrence";
import ISocketConnection from "@infrastructure/Socket/ISocketConnection";
import net, { Server, Socket } from "net";

export default class SubscriberNetSocketConnection
  implements ISocketConnection
{
  private readonly _serverAddress: string = "localhost";
  private readonly _serverPort: number = 8080;
  private readonly _server: Server;
  private _clientSocketsOccurrences: Map<Socket, Set<number>> = new Map();

  constructor() {
    this._server = net.createServer();

    this._server.listen(this._serverPort, this._serverAddress, () => {
      console.log("Connected to server");
    });

    this._server.on("close", () => {
      console.log("Connection closed");
    });

    this._server.on("connection", (socket) => {
      console.log(
        `Client connected from ${socket.remoteAddress}:${socket.remotePort}`
      );

      this._server.on("message", (message: string) => this.onMessageHandler(message, socket));

      socket.on("close", () => {
        console.log("Client disconnected");

        this._clientSocketsOccurrences.delete(socket);
      });
    });
  }

  write(data: Uint8Array): void {
    const decoder = new TextDecoder();
    const occurrenceString : string = decoder.decode(data);
    const occurrence : OccurrenceEventMessage = JSON.parse(occurrenceString);
    this._clientSocketsOccurrences.forEach((occurrences, client) => {
      if (occurrences.has(occurrence.OccurrenceId)) {
        client.write(data);
      }
    });
  }

  onMessageHandler(message: string, socket: Socket) {
    const data = JSON.parse(message);
    if (data.type === "subscribe") {
      const occurrenceId = data.occurrenceId;

      let occurrences = this._clientSocketsOccurrences.get(socket);
      if (!occurrences) {
        occurrences = new Set();
        this._clientSocketsOccurrences.set(socket, occurrences);
        return
      }
      
      occurrences.add(occurrenceId);

      console.log(`Client subscribed to occurrence ${occurrenceId}`);
      return;
    } 
    
    if (data.type === "unsubscribe") {
      const occurrenceId = data.occurrenceId;

      const occurrences = this._clientSocketsOccurrences.get(socket);
      if (occurrences) {
        occurrences.delete(occurrenceId);

        console.log(`Client unsubscribed from occurrence ${occurrenceId}`);
        return;
      }
      
      return;
    }
  }
}
