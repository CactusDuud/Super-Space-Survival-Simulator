// Written by Sage Mahmud

using UnityEngine;
using TMPro;
using Photon.Pun;

public class ManageRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField _roomName;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(_roomName.text);
        Debug.Log($"Created room \"{_roomName.text}\"");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(_roomName.text);
        Debug.Log($"Joined room \"{_roomName.text}\"");
    }

    public override void OnJoinedRoom()
    {
        // Load the proper scene
        PhotonNetwork.LoadLevel("SuperSpaceSurvivalSimulator");
    }
}
