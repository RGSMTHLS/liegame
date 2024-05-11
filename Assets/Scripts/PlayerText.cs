using TMPro;
using UnityEngine;

public class PlayerText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI playerScore;

    public void SetPlayerNameText(string value)
    {
        Debug.Log("setplayername called " + value);
        playerName.text = value;
    }

    public void SetPlayerScoreText(int value)
    {
        playerScore.text = value.ToString();
    }
}
