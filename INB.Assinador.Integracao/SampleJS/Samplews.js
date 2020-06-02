// e vira uma conexão web?? socket
//const webSocketClient = new WebSocket("ws://RES700850:27524/Assinador/");

//RES700850 = ESTAÇÃO EM QUE FOI LEVANTADO O SOCKET
const URLWS = "ws://RES700850:27524/Assinador/";


function SendMessageSocket(MyMessage) {
    let socket = new WebSocket(URLWS);

    socket.onopen = function (e) {
        socket.send(MyMessage);
        socket.close();
    };

    socket.onmessage = function (event) {
        //alert("[message] Data received from server: ${event.data}");
    };

    socket.onclose = function (event) {
        if (event.wasClean) {
            //alert("[close] Connection closed cleanly, code=${event.code} reason=${event.reason}");
        } else {
            // e.g. server process killed or network down
            // event.code is usually 1006 in this case
            //alert("[close] Connection died");
        }
    };

    socket.onerror = function (error) {
        alert(error.message);
    };


}