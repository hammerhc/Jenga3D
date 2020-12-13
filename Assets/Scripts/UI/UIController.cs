using UnityEngine;
using UnityEngine.UIElements;


public class UIController : MonoBehaviour
{
    VisualElement elementPause;

    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        elementPause = rootVisualElement.Q("ElementPause");

        var resumeButton = elementPause.Q<Button>("ButtonResume");
        resumeButton.RegisterCallback<ClickEvent>(ev => DisablePauseElement());
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            EnablePauseElement();
        }
    }


    void EnablePauseElement()
    {
        elementPause.style.display = DisplayStyle.Flex;
        EnableMouse(true);
    }

    void DisablePauseElement()
    {
        elementPause.style.display = DisplayStyle.None;
        EnableMouse(false);
    }

    void EnableMouse(bool enabled)
    {
        UnityEngine.Cursor.visible = enabled;
        UnityEngine.Cursor.lockState = enabled ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
