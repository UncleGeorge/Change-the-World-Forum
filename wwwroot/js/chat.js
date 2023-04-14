const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

connection.on("ReceiveMessage", function (message) {
    const chatList = document.getElementById("chat-list");
    const listItem = document.createElement("li");
    listItem.textContent = message;
    chatList.appendChild(listItem);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("send-button").addEventListener("click", function (event) {
    const messageInput = document.getElementById("message-input");
    const usernameInput = document.getElementById("username");
    const message = usernameInput.value + ': ' + messageInput.value;
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});