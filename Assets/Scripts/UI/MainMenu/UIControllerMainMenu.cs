using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIControllerMainMenu : MonoBehaviour
{
    VisualElement elementMenu;
    VisualElement elementOptions;
    VisualElement elementControls;

    void Start()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        elementMenu = rootVisualElement.Q("ElementMenu");
        elementOptions = rootVisualElement.Q("ElementOptions");
        elementControls = rootVisualElement.Q("ElementControls");

        var buttonPlay = elementMenu.Q<Button>("ButtonPlay");
        var buttonOptions = elementMenu.Q<Button>("ButtonOptions");
        var buttonExit = elementMenu.Q<Button>("ButtonExit");

        var buttonBackOpt = elementOptions.Q<Button>("ButtonBack");
        var buttonControls = elementOptions.Q<Button>("ButtonControls");
        var sliderMusic = elementOptions.Q<Slider>("SliderMusic");
        var sliderSound = elementOptions.Q<Slider>("SliderSound");

        var buttonBackCont = elementControls.Q<Button>("ButtonBack");

        buttonPlay.RegisterCallback<ClickEvent>(ev => StartGame());
        buttonOptions.RegisterCallback<ClickEvent>(ev => Options());
        buttonExit.RegisterCallback<ClickEvent>(ev => ExitGame());

        buttonBackOpt.RegisterCallback<ClickEvent>(ev => BackOptions());
        buttonControls.RegisterCallback<ClickEvent>(ev => Controls());
        sliderMusic.RegisterValueChangedCallback(ev => MusicChange(ev));
        sliderSound.RegisterValueChangedCallback(ev => SoundChange(ev));

        buttonBackCont.RegisterCallback<ClickEvent>(ev => BackControls());

        sliderMusic.value = FindObjectOfType<AudioManager>().GetVolume("backgroundMusic");
        sliderSound.value = FindObjectOfType<AudioManager>().GetVolume("laserShot");
    }

    void StartGame()
    {
        FindObjectOfType<GameManager>().ClearList();
        SceneManager.LoadScene(1);
    }

    void Options()
    {
        elementMenu.style.display = DisplayStyle.None;
        elementOptions.style.display = DisplayStyle.Flex;
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void BackOptions()
    {
        elementOptions.style.display = DisplayStyle.None;
        elementMenu.style.display = DisplayStyle.Flex;
    }

    void Controls()
    {
        elementOptions.style.display = DisplayStyle.None;
        elementControls.style.display = DisplayStyle.Flex;
    }

    void MusicChange(ChangeEvent<float> ev)
    {
        FindObjectOfType<AudioManager>().ChangeVolume("backgroundMusic", ev.newValue / 100);
    }

    void SoundChange(ChangeEvent<float> ev)
    {
        FindObjectOfType<AudioManager>().ChangeVolume("laserShot", ev.newValue / 100);
    }

    void BackControls()
    {
        elementControls.style.display = DisplayStyle.None;
        elementOptions.style.display = DisplayStyle.Flex;
    }
}
