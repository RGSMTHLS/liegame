using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject playerTextmesh;

    Dictionary<ulong, string> Players = new Dictionary<ulong, string>();

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += Singleton_OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback += Singleton_OnClientDisconnectCallback;
    }

    private void Singleton_OnClientDisconnectCallback(ulong obj)
    {
        Debug.Log("Player disconnected");
    }

    private void Singleton_OnClientConnectedCallback(ulong obj)
    {
        Debug.Log("Player connected");
        Players.Add(obj, "Player " + Players.Count);

        var go = Instantiate(playerTextmesh, transform.position, Quaternion.identity);
        go.transform.SetParent(panel.transform);
    }
}
