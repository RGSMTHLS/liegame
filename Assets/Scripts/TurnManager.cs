using TMPro;
using Unity.Netcode;
using UnityEngine;

public class TurnManager : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI turnText;

    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Next players turn");
        }
    }
}
