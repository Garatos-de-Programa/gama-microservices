import ISocketConnection from "./ISocketConnection";
import net, { Server, Socket } from 'net';

export default class NetSocketConnection
  implements ISocketConnection {

    private readonly _serverAddress : string = 'localhost';
    private readonly _serverPort : number = 8080;
    private readonly _server : Server;
    private _clientSockets : Socket[]  = [];

    constructor() {
      this._server = net.createServer();
      
      this._server.listen(
        this._serverPort, 
        this._serverAddress, 
        () => {
          console.log('Connected to server');
      });

      this._server.on('close', () => {
          console.log('Connection closed');
      });

      this._server.on('connection', (socket) => {
        console.log(`Client connected from ${socket.remoteAddress}:${socket.remotePort}`);
        
        this._clientSockets.push(socket);

        socket.on('close', () => {
          console.log('Client disconnected');
      
          // Remove the disconnected client's socket from the array
          this._clientSockets = this._clientSockets.filter((clientSocket) => clientSocket !== socket);
        });
      });
    }

    write(data: Uint8Array): void {
      this._clientSockets.forEach((clientSocket) => {
        clientSocket.write(data);
      });
    }
  }