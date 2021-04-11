using com.tvd12.ezyfoxserver.client.config;
using com.tvd12.ezyfoxserver.client;
using com.tvd12.ezyfoxserver.client.constant;
using com.tvd12.ezyfoxserver.client.entity;
using com.tvd12.ezyfoxserver.client.factory;
using com.tvd12.ezyfoxserver.client.handler;
using com.tvd12.ezyfoxserver.client.evt;
using com.tvd12.ezyfoxserver.client.request;

public delegate void Callback0();
public delegate void ObjectCallback(EzyObject obj);

class DisconnectionHandler : EzyDisconnectionHandler
{

    protected override void postHandle(EzyDisconnectionEvent evt)
    {
        SocketClientProxy.getInstance().emitDisconnected();
    }
}

class HandshakeHandler : EzyHandshakeHandler
{
    protected override EzyRequest getLoginRequest()
    {
        return new EzyLoginRequest(
            SocketClientProxy.ZONE_NAME,
            SocketClientProxy.getInstance().username,
            SocketClientProxy.getInstance().password,
            EzyEntityFactory.newArrayBuilder()
                .append("gameName", SocketClientProxy.GAME_NAME)
                .build()
            );
    }
}

class LoginSuccessHandler : EzyLoginSuccessHandler
{
    protected override void handleLoginSuccess(EzyData responseData)
    {
        client.udpConnect(2611);
        if (SocketClientProxy.getInstance().firstLogin)
        {
            SocketClientProxy.getInstance().firstLogin = false;
        }
    }
}

class UdpHandshakeHandler : EzyUdpHandshakeHandler
{
    protected override void onAuthenticated(EzyArray data)
    {
        var request = new EzyAppAccessRequest(SocketClientProxy.APP_NAME);
        client.send(request);
    }
}

class AppAccessHandler : EzyAppAccessHandler
{
    protected override void postHandle(EzyApp app, EzyArray data)
    {
        var request = EzyEntityFactory.newObject();
        request.put("gameName", SocketClientProxy.GAME_NAME);
        app.send("reconnect", request);
    }
}

class ReconnectResponseHandler : EzyAbstractAppDataHandler<EzyObject>
{
    protected override void process(EzyApp app, EzyObject data)
    {
        SocketClientProxy.getInstance().emitReconnected(data);
    }
}

class GetGameIdResponseHandler : EzyAbstractAppDataHandler<EzyObject>
{
    protected override void process(EzyApp app, EzyObject data)
    {
        SocketClientProxy.getInstance().emitGameIdReceived(data);
    }
}

class StartGameResponseHandler : EzyAbstractAppDataHandler<EzyObject>
{
    protected override void process(EzyApp app, EzyObject data)
    {
        SocketClientProxy.getInstance().emitStartGame(data);
    }
}
public class SocketClientProxy
{
    public const string GAME_NAME = "space-shooter";
    public const string ZONE_NAME = "space-shooter";
    public const string APP_NAME = "space-shooter";

    public string username;
    public string password;
    public bool firstLogin;
    public bool appAccessed;
    private EzyUTClient socketClient;

    private Callback0 disconnectedCallback;
    private ObjectCallback reconnectedCallback;
    private ObjectCallback gameIdReceivedCallback;
    private ObjectCallback startGameCallback;


    private static readonly SocketClientProxy INSTANCE = new SocketClientProxy();

    private SocketClientProxy()
    {
        this.username = "dungtv";
        this.password = "123456";
        firstLogin = true;
        var config = EzyClientConfig.builder()
            .clientName(ZONE_NAME)
            .build();
        socketClient = new EzyUTClient(config);
        EzyClients.getInstance().addClient(socketClient);
        var setup = socketClient.setup();
        setup.addEventHandler(EzyEventType.CONNECTION_SUCCESS, new EzyConnectionSuccessHandler());
        setup.addEventHandler(EzyEventType.CONNECTION_FAILURE, new EzyConnectionFailureHandler());
        setup.addEventHandler(EzyEventType.DISCONNECTION, new DisconnectionHandler());
        setup.addDataHandler(EzyCommand.HANDSHAKE, new HandshakeHandler());
        setup.addDataHandler(EzyCommand.LOGIN, new LoginSuccessHandler());
        setup.addDataHandler(EzyCommand.LOGIN_ERROR, new EzyLoginErrorHandler());
        setup.addDataHandler(EzyCommand.APP_ACCESS, new AppAccessHandler());
        setup.addDataHandler(EzyCommand.UDP_HANDSHAKE, new UdpHandshakeHandler());
        var appSetup = setup.setupApp(APP_NAME);
        appSetup.addDataHandler("reconnect", new ReconnectResponseHandler());
        appSetup.addDataHandler("getGameId", new GetGameIdResponseHandler());
        appSetup.addDataHandler("startGame", new StartGameResponseHandler());
        appAccessed = false;
    }

    public static SocketClientProxy getInstance()
    {
        return INSTANCE;
    }

    public void setCredential(string username, string password)
    {
        this.username = username;
        this.password = password;
    }

    public void connectToServer()
    {
        if (!isConnected())
        {
            socketClient.connect("127.0.0.1", 3005);
        }
    }

    public void processEvents()
    {
        socketClient.processEvents();
    }

    public bool isConnected()
    {
        return socketClient.isConnected() &&
            socketClient.isUdpConnected() &&
            appAccessed;
    }

    public void emitReconnected(EzyObject data)
    {
        appAccessed = true;
        reconnectedCallback(data);
    }

    public void onReconnected(ObjectCallback callback)
    {
        reconnectedCallback = callback;
    }

    public void emitGameIdReceived(EzyObject data)
    {
        gameIdReceivedCallback(data);
    }

    public void onGameIdReceived(ObjectCallback callback)
    {
        gameIdReceivedCallback = callback;
    }

    public void emitStartGame(EzyObject data)
    {
        startGameCallback(data);
    }

    public void onStartGame(ObjectCallback callback)
    {
        startGameCallback = callback;
    }

    public void emitDisconnected()
    {
        appAccessed = false;
        disconnectedCallback();
    }

    public void onDisconnected(Callback0 callback)
    {
        disconnectedCallback = callback;
    }

    public void getGameId()
    {
        var request = EzyEntityFactory.newObject();
        request.put("gameName", GAME_NAME);
        socketClient.getApp().send("getGameId", request);
    }

    public void startGame(long gameId)
    {
        var request = EzyEntityFactory.newObject();
        request.put("gameName", GAME_NAME);
        request.put("gameId", gameId);
        socketClient.getApp().send("startGame", request);
    }

    public void finishGame(long gameId)
    {
        var request = EzyEntityFactory.newObject();
        request.put("gameName", GAME_NAME);
        request.put("gameId", gameId);
        socketClient.getApp().send("finishGame", request);
    }

    public void syncScore(long gameId, long score)
    {
        var request = EzyEntityFactory.newObject();
        request.put("gameName", GAME_NAME);
        request.put("gameId", gameId);
        request.put("score", score);
        socketClient.getApp().send("updateScore", request);
    }

    public void deleteGameObject(long gameId, int objectId)
    {
        var request = EzyEntityFactory.newObject();
        request.put("gameName", GAME_NAME);
        request.put("gameId", gameId);
        request.put("objectId", objectId);
        socketClient.getApp().send("deleteGameObject", request);
    }

    public void syncPosition(long gameId,
                    string objectName,
                    int objectType,
                    int objectId,
                    bool visible,
                    double x,
                    double y,
                    double z
    )
    {
        if (!appAccessed)
        {
            return;
        }
        var position = EzyEntityFactory.newObject();
        position.put("x", x);
        position.put("y", y);
        position.put("z", z);
        var request = EzyEntityFactory.newObject();
        request.put("gameName", GAME_NAME);
        request.put("gameId", gameId);
        request.put("objectId", objectId);
        request.put("objectName", objectName);
        request.put("objectType", objectType);
        request.put("visible", visible);
        request.put("position", position);
        var app = socketClient.getApp();
        if (app != null)
        {
            app.udpSend("syncPosition", request);
        }
    }
}
