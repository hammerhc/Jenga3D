using UnityEngine;
using UnityEngine.UIElements;


public class UIController : MonoBehaviour
{
    VisualElement elementPause;
    VisualElement elementControls;

    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        elementPause = rootVisualElement.Q("ElementPause");
        elementControls = rootVisualElement.Q("ElementControls");
        var buttonResume = elementPause.Q<Button>("ButtonResume");
        var buttonControls = elementPause.Q<Button>("ButtonControls");
        var buttonExit = elementPause.Q<Button>("ButtonExit");
        var buttonBack = elementControls.Q<Button>("ButtonBack");

        buttonResume.RegisterCallback<ClickEvent>(ev => EnablePauseElement(false));
        buttonControls.RegisterCallback<ClickEvent>(ev => SwitchToControls());
        buttonBack.RegisterCallback<ClickEvent>(ev => SwitchToPause());
        buttonExit.RegisterCallback<ClickEvent>(ev => ExitGame());

    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            EnablePauseElement(true);
        }
    }


    void EnablePauseElement(bool enabled)
    {
        elementPause.style.display = enabled ? DisplayStyle.Flex : DisplayStyle.None;
        UnityEngine.Cursor.visible = enabled;
        UnityEngine.Cursor.lockState = enabled ? CursorLockMode.None : CursorLockMode.Locked;
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
}
