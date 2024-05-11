using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
public class GameManager : NetworkBehaviour
{
    [SerializeField] PlayerText playerTextPrefab;
    [SerializeField] GameObject panel;
    private NetworkVariable<List<PlayerData>> players =
        new NetworkVariable<List<PlayerData>>(
            new List<PlayerData>(),
             NetworkVariableReadPermission.Everyone,
              NetworkVariableWritePermission.Owner);
    public static GameManager Instance { get; private set; }
    void Awake() => Instance = this;
    public override void OnNetworkSpawn()
    {
        players.OnValueChanged += Players_OnValueChanged;
    }
    private void Players_OnValueChanged(
        List<PlayerData> previousValue,
        List<PlayerData> newValue)
    {
        UpdatePlayerList();
    }
    public void UpdatePlayerList()
    {
        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var player in players.Value)
        {
            AddPlayer(player);
        }
    }
    public void UpdatePlayer(PlayerData updatedPlayer)
    {
        Debug.Log("updated plauer " + updatedPlayer.playerName + " score " + updatedPlayer.playerScore + " id " + updatedPlayer.Id);
        int index = players.Value.FindIndex(player => player.Id == updatedPlayer.Id);
        if (index != -1)
        {
            players.Value[index] = updatedPlayer;
        }
        UpdatePlayerList();
    }
    public void AddPlayer(PlayerData data)
    {
        var playerTextGo = Instantiate(playerTextPrefab);
        playerTextGo.SetPlayerNameText(data.playerName.ToString());
        playerTextGo.SetPlayerScoreText(data.playerScore);
        playerTextGo.transform.SetParent(panel.transform, false);
    }
}