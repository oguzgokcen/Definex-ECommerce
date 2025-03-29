import * as signalR from "@microsoft/signalr";

export default ({ store }, inject) => {
  const signalRService = {
    connection: null,
    messageReceivedCallbacks: [],
    messageSentCallbacks: [],
    errorReceivedCallbacks: [],
    hubUrl: "https://localhost:5054/chat", 

    async startConnection() {
      if (process.client) { // client side connection için
        this.connection = new signalR.HubConnectionBuilder()
          .withUrl(this.hubUrl, {
            accessTokenFactory: () => {
              const token = store.state.auth.token;
              return token;
            },
          })
          .withAutomaticReconnect()
          .build();

        this.connection.on("ReceiveMessage", (message) => {
          this.messageReceivedCallbacks.forEach((callback) => callback(message));
        });

        this.connection.on("MessageSent", (message) => {
          this.messageSentCallbacks.forEach((callback) => callback(message));
        });

        this.connection.on("ReceiveError", (errorMessage) => {
          this.errorReceivedCallbacks.forEach((callback) => callback(errorMessage));
        });

        try {
          await this.connection.start();
          console.log("SignalR Connected");
        } catch (error) {
          console.error("SignalR Connection Error:", error);
        }
      }
    },

    async stopConnection() {
      if (process.client && this.connection) {
        try {
          await this.connection.stop();
          console.log("SignalR Disconnected");
        } catch (error) {
          console.error("SignalR Disconnection Error:", error);
        }
      }
    },

    async sendMessageToUser(receiverId, message) {
      if (process.client && this.connection) {
        try {
          await this.connection.invoke("SendMessageToUser", receiverId, message);
        } catch (error) {
          console.error("Error sending message:", error);
        }
      }
    },

    async sendMessageToAdmin(message) {
      if (process.client && this.connection) {
        try {
          await this.connection.invoke("SendMessageToAdmin", message);
        } catch (error) {
          console.error("Error sending message:", error);
        }
      }
    },

    onMessageReceived(callback) {
      this.messageReceivedCallbacks.push(callback);
    },

    onMessageSent(callback) {
      this.messageSentCallbacks.push(callback);
    },

    onErrorReceived(callback) {
      this.errorReceivedCallbacks.push(callback);
    },
  };

  inject("signalR", signalRService);
};