using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Dictionary<ulong, Player> Players = new Dictionary<ulong, Player>();

    public static PlayerManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void AddPlayer(ulong id, Player player)
    {
        if (!Players.ContainsKey(id))
        {
            Players.Add(id, player);
            UIManager.Instance.AddPlayerText(player);
        }
        else
        {
            Debug.LogError("Player already exists: " + id);
        }
    }

    public void UpdatePlayer(ulong id, Player player)
    {
        if (Players.ContainsKey(id))
        {
            Players[id] = player;
            UIManager.Instance.UpdatePlayerText(player);
        }
        else
        {
            Debug.LogError("Player not found: " + id);
        }
    }

    public void RemovePlayer(ulong id)
    {
        if (Players.ContainsKey(id))
        {
            Players.Remove(id);
            UIManager.Instance.RemovePlayerText(id);
        }
        else
        {
            Debug.LogError("Player not found: " + id);
        }
    }
}
