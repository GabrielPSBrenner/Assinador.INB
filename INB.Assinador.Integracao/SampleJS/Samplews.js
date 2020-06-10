// e vira uma conexão web?? socket
//const webSocketClient = new WebSocket("ws://RES700850:27524/Assinador/");


const URLWS = "ws://RES700850:27524/Assinador/";

var Mensagem = "{ \"Codigo\":1,\"Versao\":1,\"URLWS\":\"http://localhost/PortalTesteAssinador/Servicos/ServicoTeste.svc\",\"UserID\":\"YYYY\",\"Senha\":\"XXXX\",\"UsuarioAutenticado\":\"1495\",\"HashArquivoOriginal\":\"f0ae681a1eedcfb121b5275bf8475fa50c53fe47449f2fe3f1d46971bbb1b9e2\"}";
var Mensagem1 = "{ \"Codigo\":2,\"Versao\":1,\"URLWS\":\"http://localhost/PortalTesteAssinador/Servicos/ServicoTeste.svc\",\"UserID\":\"YYYY\",\"Senha\":\"XXXX\",\"UsuarioAutenticado\":\"1495\",\"HashArquivoOriginal\":\"f0ae681a1eedcfb121b5275bf8475fa50c53fe47449f2fe3f1d46971bbb1b9e2\"}";
var Mensagem2 = "{ \"Codigo\":3,\"Versao\":1,\"URLWS\":\"http://localhost/PortalTesteAssinador/Servicos/ServicoTeste.svc\",\"UserID\":\"YYYY\",\"Senha\":\"XXXX\",\"UsuarioAutenticado\":\"1495\",\"HashArquivoOriginal\":\"f0ae681a1eedcfb121b5275bf8475fa50c53fe47449f2fe3f1d46971bbb1b9e2\"}";

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
        alert('Um erro aconteceu ao tentar se conectar no WebSocket. Provavelmente o Assinador não está sendo executado na máquina cliente. Erro:' + error.message);
    };
}