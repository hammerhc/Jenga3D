using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIControllerMainMenu : MonoBehaviour
{
    VisualElement elementPlay;
    VisualElement elementControls;

    void Start()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        elementPlay = rootVisualElement.Q("ElementPlay");
        elementControls = rootVisualElement.Q("ElementControls");
        var buttonPlay = elementPlay.Q<Button>("ButtonPlay");
        var buttonControls = elementPlay.Q<Button>("ButtonControls");
        var buttonExit = elementPlay.Q<Button>("ButtonExit");
        var buttonBack = elementControls.Q<Button>("ButtonBack");

        buttonPlay.RegisterCallback<ClickEvent>(ev => StartGame());
        buttonControls.RegisterCallback<ClickEvent>(ev => SwitchToControls());
        buttonBack.RegisterCallback<ClickEvent>(ev => SwitchToPause());
        buttonExit.RegisterCallback<ClickEvent>(ev => ExitGame());
    }

    void StartGame()
    {
        GameManager.ClearList();
        SceneManager.LoadScene(1);
    }

    void SwitchToControls()
    {
        elementPlay.style.display = DisplayStyle.None;
        elementControls.style.display = DisplayStyle.Flex;

    }
    void SwitchToPause()
    {
        elementPlay.style.display = DisplayStyle.Flex;
        elementControls.style.display = DisplayStyle.None;
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
