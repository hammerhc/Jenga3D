using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIControllerGame : MonoBehaviour
{
    VisualElement elementPause;
    VisualElement elementControls;
    static Label elementScore;
    bool pauseEnabled = false;

    void Start()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        elementPause = rootVisualElement.Q("ElementPause");
        elementControls = rootVisualElement.Q("ElementControls");
        elementScore = rootVisualElement.Q("ElementScore").Q<Label>("Score");
        var buttonResume = elementPause.Q<Button>("ButtonResume");
        var buttonControls = elementPause.Q<Button>("ButtonControls");
        var buttonMenu = elementPause.Q<Button>("ButtonMenu");
        var buttonBack = elementControls.Q<Button>("ButtonBack");

        buttonResume.RegisterCallback<ClickEvent>(ev => TogglePauseElement());
        buttonControls.RegisterCallback<ClickEvent>(ev => SwitchToControls());
        buttonBack.RegisterCallback<ClickEvent>(ev => SwitchToPause());
        buttonMenu.RegisterCallback<ClickEvent>(ev => MainMenu());
    }

    

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

    void MainMenu()
    {
        GameManager.ClearList();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public static void SetScore(float score)
    {
        elementScore.text = score.ToString();
    }
}
