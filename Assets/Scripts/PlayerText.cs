using TMPro;
using UnityEngine;

public class PlayerText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI playerScore;
    [SerializeField] TextMeshProUGUI playerReady;

    public void SetPlayerNameText(string value)
    {
        playerName.text = value;
    }

    public void SetPlayerScoreText(int value)
    {
        playerScore.text = value.ToString();
    }

    public void SetPlayerReadyText(bool ready)
    {
        playerReady.text = ready ? "Ready" : "Not ready";
    }
}
