import IMessage from "./IMessage";

export default class IncidentMessage 
  implements IMessage {

    private readonly _databaseMessage : string; 

    constructor(databaseMessage : string) {
      this._databaseMessage = databaseMessage;
    }
      
    getBytes(): Uint8Array {
      const encoder = new TextEncoder();
      return encoder.encode(this._databaseMessage);
    }
  }