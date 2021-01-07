using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIControllerGame : MonoBehaviour
{
    VisualElement elementPlay;
    VisualElement elementPause;
    VisualElement elementOptions;
    VisualElement elementControls;
    VisualElement elementGameOver;
    
    Label score;
    Label highScore;
    Label scoreGameOver;
    Label highScoreGameOver;

    bool pauseEnabled = false;
    bool gameOverActive = false;

    void Start()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        elementPlay = rootVisualElement.Q("ElementPlay");
        elementPause = rootVisualElement.Q("ElementPause");
        elementOptions = rootVisualElement.Q("ElementOptions");
        elementControls = rootVisualElement.Q("ElementControls");
        elementGameOver = rootVisualElement.Q("ElementGameOver");

        score = elementPlay.Q<Label>("LabelScore");
        highScore = elementPlay.Q<Label>("LabelHighScore");

        var buttonBackPau = elementPause.Q<Button>("ButtonBack");
        var buttonOptions = elementPause.Q<Button>("ButtonOptions");
        var buttonMenu = elementPause.Q<Button>("ButtonMainMenu");

        var buttonBackOpt = elementOptions.Q<Button>("ButtonBack");
        var buttonControls = elementOptions.Q<Button>("ButtonControls");
        var sliderMusic = elementOptions.Q<Slider>("SliderMusic");
        var sliderSound = elementOptions.Q<Slider>("SliderSound");

        var buttonBackCont = elementControls.Q<Button>("ButtonBack");

        var buttonRetry = elementGameOver.Q<Button>("ButtonRetry");
        var buttonMainMenu = elementGameOver.Q<Button>("ButtonMainMenu");
        scoreGameOver = elementGameOver.Q<Label>("LabelScore");
        highScoreGameOver = elementGameOver.Q<Label>("LabelHighScore");

        buttonBackPau.RegisterCallback<ClickEvent>(ev => Pause());
        buttonOptions.RegisterCallback<ClickEvent>(ev => Options());
        buttonMenu.RegisterCallback<ClickEvent>(ev => Menu());

        buttonBackOpt.RegisterCallback<ClickEvent>(ev => BackOptions());
        buttonControls.RegisterCallback<ClickEvent>(ev => Controls());
        sliderMusic.RegisterValueChangedCallback(ev => MusicChange(ev));
        sliderSound.RegisterValueChangedCallback(ev => SoundChange(ev));

        buttonBackCont.RegisterCallback<ClickEvent>(ev => BackControls());

        buttonRetry.RegisterCallback<ClickEvent>(ev => Retry());
        buttonMainMenu.RegisterCallback<ClickEvent>(ev => Menu());

        FindObjectOfType<GameManager>().Load();
        SetHighScore(FindObjectOfType<GameManager>().GetHighScore());

        sliderMusic.value = FindObjectOfType<AudioManager>().GetVolume("backgroundMusic");
        sliderSound.value = FindObjectOfType<AudioManager>().GetVolume("laserShot");
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }

        if (FindObjectOfType<GameManager>().gameOver && !gameOverActive)
        {
            GameOver();
        }
    }

    void Pause()
    {
        if (!gameOverActive)
        {
            pauseEnabled = !pauseEnabled;

            Time.timeScale = pauseEnabled ? 0 : 1;
            elementPlay.style.display = pauseEnabled ? DisplayStyle.None : DisplayStyle.Flex;
            elementPause.style.display = pauseEnabled ? DisplayStyle.Flex : DisplayStyle.None;
            UnityEngine.Cursor.visible = pauseEnabled;
            UnityEngine.Cursor.lockState = pauseEnabled ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    void Options()
    {
        elementPause.style.display = DisplayStyle.None;
        elementOptions.style.display = DisplayStyle.Flex;
    }

    void BackOptions()
    {
        elementOptions.style.display = DisplayStyle.None;
        elementPause.style.display = DisplayStyle.Flex;
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

    void GameOver()
    {
        gameOverActive = !gameOverActive;
        FindObjectOfType<GameManager>().SetHighScore();
        FindObjectOfType<GameManager>().Save();
        FindObjectOfType<GameManager>().gameOver = !gameOverActive;
        Time.timeScale = gameOverActive ? 0 : 1;
        elementPlay.style.display = gameOverActive ? DisplayStyle.None : DisplayStyle.Flex;
        elementGameOver.style.display = gameOverActive ? DisplayStyle.Flex : DisplayStyle.None;
        UnityEngine.Cursor.visible = gameOverActive;
        UnityEngine.Cursor.lockState = gameOverActive ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void Retry()
    {
        LoadScene(1);
    }

    void Menu()
    {
        LoadScene(0);
    }

    void LoadScene(int scene)
    {
        FindObjectOfType<GameManager>().ClearList();
        Time.timeScale = 1;
        gameOverActive = false;
        FindObjectOfType<GameManager>().gameOver = false;
        SceneManager.LoadScene(scene);
    }

    public void SetScore(float scoreValue)
    {
        score.text = scoreValue.ToString();
        scoreGameOver.text = scoreValue.ToString();
    }

    public void SetHighScore(float highScoreValue)
    {
        highScore.text = highScoreValue.ToString();
        highScoreGameOver.text = highScoreValue.ToString();
    }
}
