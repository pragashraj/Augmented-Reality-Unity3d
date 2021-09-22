using UnityEngine;
using UnityEngine.SceneManagement;

public class StarterManager : MonoBehaviour
{
    [Header("Responsive")]
    [SerializeField] private RectTransform newGameButton;
    [SerializeField] private RectTransform continueButton;
    [SerializeField] private RectTransform quitButton;

    void Update()
    {
        HandleUISizing();
    }

    private void HandleUISizing()
    {
        newGameButton.sizeDelta = getUIResolutionVector(150.0f, 40.0f);
        continueButton.sizeDelta = getUIResolutionVector(150.0f, 40.0f);
        quitButton.sizeDelta = getUIResolutionVector(100.0f, 40.0f);
    }

    private Vector2 getUIResolutionVector(float wv, float hv)
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float width = wv / 293 * screenWidth;
        float height = hv / 522 * screenHeight;

        return new Vector2(width, height);
    }

    private void PlayAudio(string name)
    {
        FindObjectOfType<AudioManager>().Play(name);
    }

    public void NewGame()
    {
        PlayAudio("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ContinueGame()
    {
        PlayAudio("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        PlayAudio("Click");
        Application.Quit();
    }
}
