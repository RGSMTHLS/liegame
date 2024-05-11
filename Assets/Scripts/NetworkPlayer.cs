using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
public class NetworkPlayer : NetworkBehaviour
{
    NetworkVariable<FixedString32Bytes> PlayerName = new NetworkVariable<FixedString32Bytes>(
   default,
   NetworkVariableReadPermission.Everyone,
   NetworkVariableWritePermission.Owner
);

    NetworkVariable<int> PlayerScore = new NetworkVariable<int>(0
   , NetworkVariableReadPermission.Everyone
   , NetworkVariableWritePermission.Owner);
    public PlayerData Data;
    public override void OnNetworkSpawn()
    {
        Data = new PlayerData()
        {
            id = OwnerClientId,
            playerName = "Player " + OwnerClientId,
            playerScore = 1
        };
        GameManager.Instance.AddPlayer(Data);
    }
    void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerScore.Value++;
            Data.playerScore = PlayerScore.Value;
            GameManager.Instance.SendPlayerDataToServerRpc(Data);
        }
    }
}
