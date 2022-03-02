// Written by Sage Mahmud

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text _loadingText;
    [SerializeField] Button _multiplayerButton;
    [SerializeField] Button _createRoomButton;
    [SerializeField] Button _joinRoomButton;

    void Start()
    {
        // Disable multiplayer buttons and load text
        _loadingText.enabled = true;
        _multiplayerButton.interactable = false;

        Debug.Log("Connecting to server...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        _loadingText.enabled = false;
        _multiplayerButton.interactable = true;
        _createRoomButton.interactable = false;
        _joinRoomButton.interactable = false;
        Debug.Log("Connected to Photon PUN Server.");
    }

    public void MenuJoinLobby()
    {
        _loadingText.enabled = true;
        Debug.Log("Joining multiplayer lobby...");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        _loadingText.enabled = false;
        _createRoomButton.interactable = true;
        _joinRoomButton.interactable = true;
        Debug.Log("Joined multiplayer lobby.");
    }
}
