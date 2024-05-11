using TMPro;
using Unity.Netcode;
using UnityEngine;

public class TurnManager : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI turnText;
    NetworkVariable<int> currentPlayerIndex = new NetworkVariable<int>(0,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        currentPlayerIndex.OnValueChanged += OnCurrentPlayerIndexChanged;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("atleast space got pressed");
            currentPlayerIndex.Value = (currentPlayerIndex.Value + 1) % NetworkManager.Singleton.ConnectedClientsList.Count;
            Debug.Log("Value is now " + currentPlayerIndex.Value);
        }
    }
    private void OnCurrentPlayerIndexChanged(int previousValue, int newValue)
    {
        Debug.Log("Value changed got called");
        turnText.text = $"Player {newValue + 1}'s turn";
    }
}
