using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpMenu : MonoBehaviour

{
    [Header("Singleton Insurance")]
    private static PauseMenu _instance;
    public static PauseMenu GetInstance { get { return _instance; } }

    public GameObject HelpUI;

    void Start()
    {
        //PauseMenu.TogglePause();
    }

    void Awake()
    {
        //if (_instance != null && _instance != this) Destroy(this.gameObject);
        //else _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
