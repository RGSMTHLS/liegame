using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerText playerTextPrefab;
    [SerializeField] GameObject panel;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void AddPlayerText(Player player)
    {
        var playerTextGo = Instantiate(playerTextPrefab);
        playerTextGo.SetPlayerNameText(player.GetPlayerName());
        playerTextGo.SetPlayerScoreText(0);
        playerTextGo.transform.SetParent(panel.transform, false);
    }

    internal void UpdatePlayerText(Player player)
    {
        var existingTexts = panel.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var text in existingTexts)
        {
            if (text.text.Contains("Player " + player.OwnerClientId))
            {
                text.text = "Updated Player " + player.OwnerClientId;
                break;
            }
        }
    }

    internal void RemovePlayerText(ulong id)
    {
        var existingTexts = panel.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var text in existingTexts)
        {
            if (text.text.Contains("Player " + id))
            {
                Destroy(text.gameObject);
                break;
            }
        }
    }
}
