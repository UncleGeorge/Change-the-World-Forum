// Use SignalR to establish a connection to a server for real-time communication
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

//Executed when a message named "ReceiveMessage" is received from the server
connection.on("ReceiveMessage", function (message) {
    const chatList = document.getElementById("chat-list");
    const listItem = document.createElement("li");
    listItem.textContent = message;
    chatList.appendChild(listItem);
});

// Start a signalR connection and print an error message to the console if the connection fails
connection.start().catch(function (err) {
    return console.error(err.toString());
});

//Add a click event to the button, get the input message, and send the message to the chat room by calling the "SendMessage" method
document.getElementById("send-button").addEventListener("click", function (event) {
    const messageInput = document.getElementById("message-input");
    const usernameInput = document.getElementById("username");
    const message = usernameInput.value + ': ' + messageInput.value;
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});