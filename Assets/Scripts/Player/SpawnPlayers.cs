//  written by Sage Mahmud and Elizabeth Castreje

using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayers : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject _pf_player1;
    [SerializeField] GameObject _pf_player2;
    
    

    void Awake()
    {
        // Spawn player 1
        var p1 = PlayerInput.Instantiate(_pf_player1, controlScheme: "Player1", pairWithDevice: Keyboard.current);

        // Spawn player 2
        var p2 = PlayerInput.Instantiate(_pf_player2, controlScheme: "Player2", pairWithDevice: Keyboard.current);
    }
}
