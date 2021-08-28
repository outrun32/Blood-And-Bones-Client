using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets

    public static void WelcomeReceived()
    {
        using(Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(Client.instance.Username);
            _packet.Write(Client.instance.PlayfabID);

            SendTCPData(_packet);
        }
    }
    //Todo:
    /*public static void PlayerMovement(float[] inputs, bool isJumping)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerInput))
        {
            _packet.Write(inputs.Length);
            foreach (float axis in inputs)
            {
                _packet.Write(axis);
            }
            _packet.Write(isJumping);
            _packet.Write(GameManager.Players[Client.instance.myId].transform.rotation);

            SendUDPData(_packet);
        }
    }*/
    public static void PlayerInput(InputModel inputModel)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerInput))
        {
            _packet.Write(inputModel);
            SendUDPData(_packet);
        }
    }
}

    #endregion
