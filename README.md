# ChatService API

ChatService is an ASP.NET Web API (.NET 8) providing a real‑time one‑to‑one chat service for users. It leverages SignalR for real‑time communication, Entity Framework Core with LINQ for data storage and manipulation, and currently supports sending text messages and emojis. The API is hosted on **Monster ASP**, making it accessible over the internet.

---

## 🚀 Features

- **Real‑time messaging**: Built on ASP.NET Core SignalR
- **Data persistence**: Uses Entity Framework Core (Code‑First) with LINQ queries
- **One‑to‑one chat**: Direct messaging between two users
- **Text & emojis**: Supports plain text content and Unicode emojis

> 🔜 **Future enhancements**:
> - Send/receive files (images, documents)
> - Group chats & channels
> - Message delivery/read receipts
> - Typing indicators

---

## 📡 Deployment & Hosting

The ChatService API is hosted on **Monster ASP**. You can access the live endpoints at:

```
https://<your-app>.monsterasp.net/
```

Replace `<your-app>` with your actual Monster ASP application name.

---

## 📦 Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) or another EF‑Core-compatible database
- Visual Studio 2022+ or VS Code

---

## 🔗 API Endpoints

All endpoints are relative to the base URL of the deployed API.

| Route                           | Method | Description                        |
|---------------------------------|--------|------------------------------------|
| `/api/ChatUsers`                | GET    | Get all registered users           |
| `/api/ChatUsers/{id}`           | GET    | Get a specific user by ID          |
| `/api/Conversations`            | GET    | List all conversations             |
| `/api/Conversations/{id}`       | GET    | Get messages for a conversation    |
| `/api/Conversations`            | POST   | Create a new conversation          |

> For full details, refer to the [Swagger UI](/swagger/index.html) when running locally or deployed on Monster ASP.

---

## 💬 SignalR Hub

- **Hub URL**: `/hubs/chat`
- **Client methods**:
  - `ReceiveMessage(string conversationId, ChatMessageDto message)`
- **Server methods**:
  - `SendMessage(string conversationId, string senderId, string text)`

Example JavaScript client:
```js
const connection = new signalR.HubConnectionBuilder()
  .withUrl("https://<your-app>.monsterasp.net/hubs/chat")
  .build();

connection.on("ReceiveMessage", (convId, message) => {
  console.log(`[${message.senderName}]: ${message.text}`);
});

await connection.start();
await connection.invoke("SendMessage", conversationId, userId, "Hello! 👋");
```

---

 🗃️ Data Model

- **ChatUser**: Represents a registered user
- **Conversation**: A chat session between two users
- **ChatMessage**: Stores individual messages


---

 🔧 Technologies

- ASP.NET Core Web API (.NET 8)
- SignalR Core
- Entity Framework Core
- LINQ
- SQL Server / LocalDB
- Swashbuckle (Swagger)

---

## 🎯 Next Steps

- **Files & images**: Add support for file uploads and media
- **Groups & channels**: Enable multi‑user chat rooms
- **Security**: Integrate JWT‑based authentication
- **UI client**: Build a React or Angular front‑end

