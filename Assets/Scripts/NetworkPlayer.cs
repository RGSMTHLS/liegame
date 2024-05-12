using Unity.Netcode;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePlayerReadyRpc(OwnerClientId);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            IncreaseScoreRpc(OwnerClientId);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DecreaseScoreRpc(OwnerClientId);
        }
    }

    [Rpc(SendTo.Server)]
    private void TogglePlayerReadyRpc(ulong clientId)
    {
        var playerData = GameManager.Instance.GetPlayerData(clientId);
        GameManager.Instance.UpdatePlayerData(clientId,
            new PlayerData
            {
                clientId = (ulong)playerData?.clientId,
                score = (int)playerData?.score,
                ready = !playerData?.ready ?? false
            });
        if (GameManager.Instance.IsAllPlayersReady())
        {
            StartGameRpc();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void StartGameRpc()
    {
        GameManager.Instance.StartGame();
    }

    [Rpc(SendTo.Server)]
    private void IncreaseScoreRpc(ulong clientId)
    {
        var playerData = GameManager.Instance.GetPlayerData(clientId);
        GameManager.Instance.UpdatePlayerData(clientId,
        new PlayerData
        {
            clientId = (ulong)playerData?.clientId,
            score = playerData?.score + 1 ?? 0,
            ready = playerData?.ready ?? false
        });
    }

    [Rpc(SendTo.Server)]
    private void DecreaseScoreRpc(ulong clientId)
    {
        var playerData = GameManager.Instance.GetPlayerData(clientId);
        GameManager.Instance.UpdatePlayerData(clientId,
        new PlayerData
        {
            clientId = (ulong)playerData?.clientId,
            score = playerData?.score - 1 ?? 0,
            ready = playerData?.ready ?? false
        });
    }
}
