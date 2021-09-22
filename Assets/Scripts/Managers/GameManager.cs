using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject alertPopup;

    private AudioManager audioManager;
    private static int score = 0;

    public int Score { get { return score; } set { score = value; } }

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        PlayAudio("Theme");
    }

    private void PlayAudio(string name)
    {
        audioManager.Play(name);
    }

    private void HandleAlertPopupActive(bool active)
    {
        alertPopup.SetActive(active);
    }

    public void Close()
    {
        PlayAudio("Click");
        HandleAlertPopupActive(true);
    }

    public void HandleNoButtonOnClick()
    {
        HandleAlertPopupActive(false);
        PlayAudio("Swipe");
    }

    public void HandleYesButtonOnClick()
    {
        HandleAlertPopupActive(false);
        PlayAudio("Swipe");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
