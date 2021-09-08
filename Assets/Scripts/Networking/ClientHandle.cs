using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using All.Modes;
using Networking;
using PlayFab.Internal;

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

    public static void SetCounterTimer(Packet _packet)
    {
        int id = _packet.ReadInt();
        int count = _packet.ReadInt();
        GameManager.Instance.SetCountDownTimer(id, count);
    }

    public static void StartSession(Packet _packet)
    {
        GameManager.Instance.StartSession();
    }

    public static void SetTeam(Packet packet)
    {
        int id = packet.ReadInt();
        bool isRed = packet.ReadBool();
        if (GameManager.CheckPlayer(id))GameManager.Players[id].SetTeam(isRed);
    }
    public static void EndSession(Packet _packet)
    {
        Dictionary<string, PlayerDataModel> blue = new Dictionary<string, PlayerDataModel>(),
                                            red = new Dictionary<string, PlayerDataModel>();
        int count = _packet.ReadInt();
        Debug.Log("Blue");
        for (int i = 0; i < count; i++)
        {
            string name = _packet.ReadString();
            int killCount = _packet.ReadInt();
            int deathCount = _packet.ReadInt();
            float score = _packet.ReadInt();
            blue.Add(name, new PlayerDataModel(score,killCount,deathCount));
            Debug.Log($"{name}, kills = {killCount}, death = {deathCount}, score = {score}");
        }
        count = _packet.ReadInt();
        Debug.Log("Red");
        for (int i = 0; i < count; i++)
        {
            string name = _packet.ReadString();
            int killCount = _packet.ReadInt();
            int deathCount = _packet.ReadInt();
            float score = _packet.ReadInt();
            red.Add(name, new PlayerDataModel(score,killCount,deathCount));
            
            Debug.Log($"{name}, kills = {killCount}, death = {deathCount}, score = {score}");
        }
        GameManager.Instance.EndSession(red,blue);
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
