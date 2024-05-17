using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : NetworkBehaviour
{
    [SerializeField] PlayerText playerTextPrefab;
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI gameText;
    [SerializeField] Button joinButton;
    [SerializeField] Button hostButton;
    [SerializeField] GameObject menuPanel;
    NetworkList<PlayerData> playerDataList = new NetworkList<PlayerData>(
        null,
    NetworkVariableReadPermission.Everyone,
    NetworkVariableWritePermission.Owner);
    public event EventHandler OnPlayerListChanged;
    public static GameManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        playerDataList = new NetworkList<PlayerData>();
        playerDataList.OnListChanged += PlayerDataList_OnListChanged;
    }

    void Start()
    {
        hostButton.onClick.AddListener(() =>
        {
            var success = StartHost();
            if (success)
                menuPanel.SetActive(false);
        });
        joinButton.onClick.AddListener(async () =>
        {
            var success = NetworkManager.Singleton.StartClient();
            if (success == true)
                menuPanel.SetActive(false);
        });
    }
    private void PlayerDataList_OnListChanged(NetworkListEvent<PlayerData> changeEvent)
    {
        UpdatePlayerList();
    }
    public bool StartHost()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
        return NetworkManager.Singleton.StartHost();
    }
    private void NetworkManager_OnClientConnectedCallback(ulong clientId)
    {
        playerDataList.Add(new PlayerData { clientId = clientId });
    }
    public PlayerData? GetPlayerData(ulong clientId)
    {
        for (int i = 0; i < playerDataList.Count; i++)
        {
            if (playerDataList[i].clientId == clientId)
                return playerDataList[i];
        }
        return null;
    }
    public void UpdatePlayerData(ulong clientId, PlayerData newData)
    {
        for (int i = 0; i < playerDataList.Count; i++)
        {
            if (playerDataList[i].clientId == clientId)
            {
                playerDataList.RemoveAt(i);
                playerDataList.Insert(i, newData);
                break;
            }
        }
    }
    public bool IsAllPlayersReady()
    {
        if (playerDataList.Count < 2)
            return false;

        foreach (var player in playerDataList)
        {
            if (!player.ready)
                return false;
        }
        return true;
    }
    public void StartGame()
    {
        gameText.text = "Game Started!";
    }

    public void UpdatePlayerList()
    {
        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var player in playerDataList)
        {
            AddPlayer(player);
        }
    }
    public void AddPlayer(PlayerData data)
    {
        var playerTextGo = Instantiate(playerTextPrefab);
        playerTextGo.SetPlayerNameText(data.clientId.ToString());
        //playerTextGo.SetPlayerReadyText(data.ready);
        playerTextGo.SetPlayerScoreText(data.score);
        playerTextGo.transform.SetParent(panel.transform, false);
    }
}