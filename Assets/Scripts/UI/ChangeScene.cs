using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
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
}