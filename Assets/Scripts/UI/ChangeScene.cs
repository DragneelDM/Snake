using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    
    private bool _infiniteVersion = false;
    public void Change(string Levels)
    {
        switch (Levels)
        {
            case "Lobby":
                print("You are already in Lobby");
                break;

            case "Settings":
                Debug.LogWarning("Yet to be Constructed");
                break;

            case "Game":
                SceneManager.LoadScene(Levels);
                break;

            case "Exit":
                Application.Quit();
                break;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(LevelManager.Instance != null) 
        {
            LevelManager.Instance.SetVersion(_infiniteVersion);
            Destroy(gameObject);
        }
    }

    public void SetVersion()
    {
        _infiniteVersion = !_infiniteVersion;
    }
}