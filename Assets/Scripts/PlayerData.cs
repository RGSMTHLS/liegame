using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;

[Serializable]
public struct PlayerData : INetworkSerializable
{
    public ulong id;
    public FixedString128Bytes playerName;
    public int playerScore;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref id);
        serializer.SerializeValue(ref playerName);
        serializer.SerializeValue(ref playerScore);
    }
}

public class PlayerDataList : INetworkSerializable
{
    public List<PlayerData> Players = new List<PlayerData>();

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        int count = Players.Count;
        serializer.SerializeValue(ref count);
        for (int i = 0; i < count; i++)
        {
            PlayerData player = Players[i];
            serializer.SerializeValue(ref player);
        }
    }

    public void NetworkDeserialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        int count = 0;
        serializer.SerializeValue(ref count);
        Players.Clear();
        for (int i = 0; i < count; i++)
        {
            PlayerData player = new PlayerData();
            serializer.SerializeValue(ref player);
            Players.Add(player);
        }
    }
}
