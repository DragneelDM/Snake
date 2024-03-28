using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _snake1UI;
    [SerializeField] private TextMeshProUGUI _snake2UI;
    [SerializeField] private TextMeshProUGUI _wonMessage;
    [SerializeField] private AudioClip audioClip;
    public static LevelManager Instance { get; private set; }

    private int _snake1Points = 0;
    private int _snake2Points = 0;
    private bool _infiniteVersion = false;

    private static bool _pause = false;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UpdateUI(int newValue, bool isFirst)
    {
        AudioSource.PlayClipAtPoint(audioClip, Vector3.zero);
        if (isFirst)
        {
            _snake1Points += newValue;
            _snake1UI.text = _snake1Points.ToString();
        }
        else
        {
            _snake2Points += newValue;
            _snake2UI.text = _snake2Points.ToString();
        }
    }

    public void EndScene(Reason reason, bool isFirst)
    {
        if (!_infiniteVersion)
        {
            string sender = isFirst ? StringConsts.SNAKE1 : StringConsts.SNAKE2;
            string opposite = isFirst ? StringConsts.SNAKE2 : StringConsts.SNAKE1;

            switch (reason)
            {
                case Reason.AteEnemy:
                    _wonMessage.text = $"{sender} lost by getting hit. Congrats {opposite}";
                break;

                case Reason.AteFood:
                    _wonMessage.text = $"{sender} Won !! by eating a lot";
                break;

                case Reason.NoMoreBody:
                    _wonMessage.text = $"{sender} lost all body parts. Congrats {opposite}";
                break;

            }
            SceneManager.LoadScene(2);
        }
    }

    public void SetVersion(bool value)
    {
        _infiniteVersion = value;
    }

    public void Pause()
    {
        _pause = !_pause;
        
        Time.timeScale = _pause ? 0 : 1;
    }

}


public enum Reason
{
    AteEnemy,
    AteFood,
    NoMoreBody
}