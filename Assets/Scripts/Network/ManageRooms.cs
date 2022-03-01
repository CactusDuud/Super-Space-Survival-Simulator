// Written by Sage Mahmud

using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ManageRooms : MonoBehaviourPunCallbacks
{
    public InputField newRoom;
    public InputField joinRoom;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(newRoom.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoom.text);
    }

    public override void OnJoinedRoom()
    {
        // Load the proper scene
        //PhotonNetwork.LoadLevel("");
    }
}
