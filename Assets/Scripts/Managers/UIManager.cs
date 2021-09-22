using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Responsive")]
    [SerializeField] private RectTransform alertPopUp;
    [SerializeField] private RectTransform noButton;
    [SerializeField] private RectTransform yesButton;
    [SerializeField] private RectTransform closeButton;
    [SerializeField] private RectTransform nextButton;
    [SerializeField] private GameObject info;
    [SerializeField] private RectTransform answers;
    [SerializeField] private RectTransform[] answerSlots;
    [SerializeField] private RectTransform nextBtn;
    [SerializeField] private RectTransform success;
    [SerializeField] private RectTransform failure;
    [SerializeField] private RectTransform end;


    void Update()
    {
        HandleUISizing();
        handleTextSizing();
    }

    private void HandleUISizing()
    {
        alertPopUp.sizeDelta = getUIResolutionVector(240.0f, 220.0f);
        closeButton.sizeDelta = getUIResolutionVector(30.0f, 30.0f);
        yesButton.sizeDelta = getUIResolutionVector(30.0f, 20.0f);
        noButton.sizeDelta = getUIResolutionVector(30.0f, 20.0f);
        nextButton.sizeDelta = getUIResolutionVector(100.0f, 50.0f);
        info.GetComponent<RectTransform>().sizeDelta = getUIResolutionVector(160.0f, 30.0f);
        answers.sizeDelta = getUIResolutionVector(280.0f, 170.0f);
        nextBtn.sizeDelta = getUIResolutionVector(100.0f, 50.0f);
        success.sizeDelta = getUIResolutionVector(100.0f, 100.0f);
        failure.sizeDelta = getUIResolutionVector(100.0f, 100.0f);
        end.sizeDelta = getUIResolutionVector(240.0f, 220.0f);

        for (int i = 0; i < answerSlots.Length; i++)
        {
            answerSlots[i].sizeDelta = getUIResolutionVector(120.0f, 30.0f);
            //answerSlots[i].GetChild(0).GetComponent<RectTransform>().sizeDelta = getUIResolutionVector(120.0f, 30.0f);
        }
    }

    private void handleTextSizing()
    {
        info.GetComponent<Text>().fontSize = (int)getTextSize(14.0f);
        /*for (int i = 0; i < answerSlots.Length; i++)
        {
            answerSlots[i].GetChild(0).GetComponent<Text>().fontSize = (int)getTextSize(12.0f);
        }*/
    }

    private Vector2 getUIResolutionVector(float wv, float hv)
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float width = wv / 293 * screenWidth;
        float height = hv / 522 * screenHeight;

        return new Vector2(width, height);
    }

    private float getTextSize(float size)
    {
        float screenWidth = Screen.width;
        return size / 293 * screenWidth;
    }
}
