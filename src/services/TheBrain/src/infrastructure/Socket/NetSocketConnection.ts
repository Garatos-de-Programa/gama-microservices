import ISocketConnection from "./ISocketConnection";
import net, { Socket } from 'net';

export default class NetSocketConnection
  implements ISocketConnection {

    private readonly _serverAddress : string = 'localhost';
    private readonly _serverPort : number = 8080;
    private readonly _socket : Socket;

    constructor() {
      this._socket = new net.Socket();
      
      this._socket.connect(
        this._serverPort, 
        this._serverAddress, 
        () => {
          console.log('Connected to server');
      });

      this._socket.on('close', () => {
          console.log('Connection closed');
      });
    }

    write(data: Uint8Array): void {
      this._socket.write(data);
    }
  }