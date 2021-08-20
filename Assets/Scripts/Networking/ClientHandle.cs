using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;

        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();
        float maxHealth = _packet.ReadFloat(),
            maxMana = _packet.ReadFloat(), 
            startHealth = _packet.ReadFloat(), 
            startMana = _packet.ReadFloat();

        GameManager.Instance.SpawnPlayer(_id, _username, _position, _rotation);
        GameManager.Players[_id].SetStartInfo(maxHealth,maxMana,startHealth,startMana);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 position = _packet.ReadVector3();

        if (GameManager.CheckPlayer(_id))GameManager.Players[_id].SetPosition(position);
    }

    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        if (GameManager.CheckPlayer(_id))GameManager.Players[_id].SetRotation(_rotation);
    }

    public static void PlayerDisconnected(Packet _packet)
    {
        int _id = _packet.ReadInt();

        Destroy(GameManager.Players[_id].gameObject);
        if (GameManager.CheckPlayer(_id))GameManager.Players.Remove(_id);
    }
    public static void PlayerAnimation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        AnimationModel _animationModel = _packet.ReadAnimationModel();
        if (GameManager.CheckPlayer(_id)) GameManager.Players[_id].SeAnimation(_animationModel);
    }
    public static void PlayerInfo(Packet _packet)
    {
        int _id = _packet.ReadInt();
        float health = _packet.ReadFloat(), mana = _packet.ReadFloat();
        if (GameManager.CheckPlayer(_id))GameManager.Players[_id].SetInfo(health, mana);
    }
    public static void PlayerDeath(Packet _packet)
    {
        int _id = _packet.ReadInt();
        if (GameManager.CheckPlayer(_id))GameManager.Players[_id].Death();
    }
}
