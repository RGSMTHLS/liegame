using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [SerializeField] PlayerText playerTextPrefab;
    [SerializeField] GameObject panel;

    private NetworkVariable<List<FixedString128Bytes>> players =
        new NetworkVariable<List<FixedString128Bytes>>(
            new List<FixedString128Bytes>(),
             NetworkVariableReadPermission.Everyone,
              NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        players.OnValueChanged += Players_OnValueChanged;
        NetworkManager.Singleton.OnClientConnectedCallback += Singleton_OnClientConnectedCallback;
    }

    private void Players_OnValueChanged(List<FixedString128Bytes> previousValue, List<FixedString128Bytes> newValue)
    {
        UpdatePlayerList();
    }

    private void Singleton_OnClientConnectedCallback(ulong obj)
    {
        players.Value.Add("Player " + OwnerClientId);
    }

    public void UpdatePlayerList()
    {
        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var player in players.Value)
        {
            AddPlayerText("Player " + player);
        }
    }

    public void AddPlayerText(string text)
    {
        var playerTextGo = Instantiate(playerTextPrefab);
        playerTextGo.SetPlayerNameText(text);
        playerTextGo.transform.SetParent(panel.transform, false);
    }
}