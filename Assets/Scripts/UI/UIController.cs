using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    VisualElement elementPause;
    VisualElement elementControls;
    static Label elementScore;

    void Start()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        elementPause = rootVisualElement.Q("ElementPause");
        elementControls = rootVisualElement.Q("ElementControls");
        elementScore = rootVisualElement.Q("ElementScore").Q<Label>("Score");
        var buttonResume = elementPause.Q<Button>("ButtonResume");
        var buttonControls = elementPause.Q<Button>("ButtonControls");
        var buttonExit = elementPause.Q<Button>("ButtonExit");
        var buttonBack = elementControls.Q<Button>("ButtonBack");

        buttonResume.RegisterCallback<ClickEvent>(ev => TogglePauseElement());
        buttonControls.RegisterCallback<ClickEvent>(ev => SwitchToControls());
        buttonBack.RegisterCallback<ClickEvent>(ev => SwitchToPause());
        buttonExit.RegisterCallback<ClickEvent>(ev => ExitGame());
    }

    bool pauseEnabled = false;

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePauseElement();
        }
    }


    void TogglePauseElement()
    {
        pauseEnabled = !pauseEnabled;

        Time.timeScale = pauseEnabled ? 0 : 1;
        elementPause.style.display = pauseEnabled ? DisplayStyle.Flex : DisplayStyle.None;
        elementControls.style.display = DisplayStyle.None;
        UnityEngine.Cursor.visible = pauseEnabled;
        UnityEngine.Cursor.lockState = pauseEnabled ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void SwitchToControls()
    {
        elementPause.style.display = DisplayStyle.None;
        elementControls.style.display = DisplayStyle.Flex;

    }
    void SwitchToPause()
    {
        elementPause.style.display = DisplayStyle.Flex;
        elementControls.style.display = DisplayStyle.None;
    }

    void ExitGame()
    {
        Application.Quit();
    }

    public static void SetScore(float score)
    {
        elementScore.text = score.ToString();
    }
}
