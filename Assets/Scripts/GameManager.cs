using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
public class GameManager : NetworkBehaviour
{
    [SerializeField] PlayerText playerTextPrefab;
    [SerializeField] GameObject panel;
    private List<PlayerData> allPlayerData = new List<PlayerData>();
    public static GameManager Instance { get; private set; }
    void Awake() => Instance = this;
    public void UpdatePlayerList(PlayerDataList playerDataList)
    {
        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var player in playerDataList.Players)
        {
            AddPlayer(player);
        }
    }
    public void AddPlayer(PlayerData data)
    {
        var playerTextGo = Instantiate(playerTextPrefab);
        playerTextGo.SetPlayerNameText(data.playerName.ToString());
        playerTextGo.SetPlayerScoreText(data.playerScore);
        playerTextGo.transform.SetParent(panel.transform, false);
    }

    private string SerializePlayerDataToJson(List<PlayerData> players)
    {
        PlayerDataList playerDataList = new PlayerDataList { Players = players };
        return JsonUtility.ToJson(playerDataList);
    }

    private List<PlayerData> DeserializePlayerDataFromJson(string json)
    {
        PlayerDataList playerDataList = JsonUtility.FromJson<PlayerDataList>(json);
        return playerDataList.Players;
    }

    [Rpc(SendTo.Server)]
    public void SendPlayerDataToServerRpc(PlayerData data)
    {
        if (allPlayerData.Contains(data))
            return;

        allPlayerData.Add(data);
        string jsonData = SerializePlayerDataToJson(allPlayerData);

        List<PlayerData> allPlayerData_ = DeserializePlayerDataFromJson(jsonData);
        PlayerDataList playerDataList = new PlayerDataList { Players = allPlayerData_ };
        UpdateAllClientsUIRpc(playerDataList);
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void UpdateAllClientsUIRpc(PlayerDataList playerDataList)
    {
        UpdatePlayerList(playerDataList);
    }
}