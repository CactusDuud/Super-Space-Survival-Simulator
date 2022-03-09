//  written by Sage Mahmud and Elizabeth Castreje

using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [Header("Singleton Insurance")]
    private static SpawnPlayers _instance;
    public static SpawnPlayers GetInstance { get { return _instance; } }

    [Header("References")]
    [SerializeField] GameObject _pf_player1;
    [SerializeField] GameObject _pf_player2;
    [SerializeField] GameObject _pf_friend;
    [SerializeField] Transform _player1Spawn;
    [SerializeField] Transform _player2Spawn;

    [Header("Attributes")]
    /*
    Okay this is gross but
    0 - Singleplayer
    1 - Multiplayer
    2 - Multiplayer Online
    */
    int _gameMode;
    bool _isLocalMultiplayer;
    bool _p1Exists;

    

    void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;

        _gameMode = PlayerPrefs.GetInt("gameType", 0);

        if (_gameMode == 2)
        {
            if (!_p1Exists)
            {
                // Spawn player 1
                var p1 = PhotonNetwork.Instantiate(_pf_player1.name, _player1Spawn.position, Quaternion.identity);
                _p1Exists = true;
            }
            else
            {
                // Spawn player 2
                var p2 = PhotonNetwork.Instantiate(_pf_player2.name, _player2Spawn.position, Quaternion.identity);
            }
        }
        else
        {
            // Spawn player 1
            var p1 = PlayerInput.Instantiate(_pf_player1, controlScheme: "Player1", pairWithDevice: Keyboard.current);
            p1.transform.position = _player1Spawn.position;

            if (_gameMode == 1)
            {
                // Spawn player 2
                var p2 = PlayerInput.Instantiate(_pf_player2, controlScheme: "Player2", pairWithDevice: Keyboard.current);
                p2.transform.position = _player2Spawn.position;
            }

            // Spawn an awesome friend
            GameObject.Instantiate(_pf_friend, transform.position, Quaternion.identity);
        }
    }
}
