using System;
using Unity.Netcode;
using UnityEngine;
public class NetworkPlayer : NetworkBehaviour
{
    public NetworkVariable<int> PlayerScore = new NetworkVariable<int>(0
    , NetworkVariableReadPermission.Everyone
    , NetworkVariableWritePermission.Owner);
    public PlayerData Data;
    public override void OnNetworkSpawn()
    {
        Data = new PlayerData()
        {
            Id = OwnerClientId,
            playerName = "Player " + OwnerClientId,
            playerScore = 1
        };
        GameManager.Instance.AddPlayer(Data);
        PlayerScore.OnValueChanged += UpdateScore;
    }
    void UpdateScore(int oldScore, int newScore)
    {
        Data.playerScore = newScore;
        GameManager.Instance.UpdatePlayer(Data);
    }
    void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerScore.Value++;
            Debug.Log("Player score " + PlayerScore.Value);
        }
    }
}
