using Unity.Collections;
using Unity.Netcode;

public class Player : NetworkBehaviour
{
    private NetworkVariable<FixedString128Bytes> networkPlayerName
        = new NetworkVariable<FixedString128Bytes>("",
            NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public string GetPlayerName() => networkPlayerName.Value.ToString();

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
            networkPlayerName.Value = "P-" + OwnerClientId;

        PlayerManager.Instance.AddPlayer(OwnerClientId, this);
    }

    public override void OnNetworkDespawn()
    {
        PlayerManager.Instance.RemovePlayer(OwnerClientId);
    }
}
