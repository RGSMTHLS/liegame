using Unity.Netcode;

public class NetworkPlayer : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        GameManager.Instance.AddPlayerText("Player " + OwnerClientId);
    }
}
