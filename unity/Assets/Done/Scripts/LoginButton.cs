using UnityEngine;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour
{
    public InputField usernameInput;

    private void Start()
    {
        usernameInput.text = "dungtv";   
    }

    public void OnClick()
    {
        SocketClientProxy socketClientProxy = SocketClientProxy.getInstance();
        socketClientProxy.setCredential(usernameInput.text, "123456");
        socketClientProxy.connectToServer();
    }
}