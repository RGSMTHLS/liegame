using Unity.Netcode;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            NetworkManager.Singleton.StartHost();

        if (Input.GetKeyDown(KeyCode.C))
            NetworkManager.Singleton.StartClient();
    }
}
