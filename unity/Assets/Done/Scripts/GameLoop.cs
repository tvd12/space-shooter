using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private void Update()
    {
        SocketClientProxy.getInstance().processEvents();
    }
}