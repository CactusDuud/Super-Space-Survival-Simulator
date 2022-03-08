//Contributor: Esmeralda Juarez
/*
 * This script saves players highest prosparity 
*/
using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highscore;

    private GameManager refGameManager;
    private int number;


    // Start is called before the first frame update
    void Start()
    {
        // fills in the highest prosparity at start
        number = PlayerPrefs.GetInt("Highscore", 0);
        highscore.text = $"Highest Prosperity: {PlayerPrefs.GetInt("Highscore", 0)}";
        refGameManager = GetComponent<GameManager>();
    }
    
    void FixedUpdate()
    {
        // if the current player prosperity is higher than saved highest prosperisty then updates it
        if (refGameManager.prosperity > number)
        {
            PlayerPrefs.SetInt("Highscore", refGameManager.prosperity);
            number = PlayerPrefs.GetInt("Highscore", 0);
            highscore.text = $"Highest Prosperity: {PlayerPrefs.GetInt("Highscore", 0)}";
        }
    }


}
