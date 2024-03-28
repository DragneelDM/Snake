using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _snake1UI;
    [SerializeField] private TextMeshProUGUI _snake2UI;
    [SerializeField] private AudioClip audioClip;
    public static LevelManager Instance { get; private set; }

    private int _snake1Points = 0;
    private int _snake2Points = 0;
    private bool _infiniteVersion = false;


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
        if(isFirst)
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
        if(_infiniteVersion)
        {

        }
        else
        {
            if (reason == Reason.Ate)
            {
                string name = isFirst ? StringConsts.SNAKE1 : StringConsts.SNAKE2;
                print($"{name} died");
            }
        }
    }

    public void SetVersion(bool value)
    {
        _infiniteVersion = value;
    }

}


public enum Reason
{
    Ate
}