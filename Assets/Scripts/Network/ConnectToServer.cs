// Written by Sage Mahmud

using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // Enable the "multiplayer" button
    }

    public override void OnJoinedLobby()
    {
        // Enable the "create/join room" buttons
    }
}
