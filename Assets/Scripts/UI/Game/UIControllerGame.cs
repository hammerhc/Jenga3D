using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIControllerGame : MonoBehaviour
{
    VisualElement elementPause;
    VisualElement elementControls;
    VisualElement elementGameOver;
    static Label elementScore;
    static Label elementScoreGameOver;
    bool pauseEnabled = false;
    bool gameOverActive = false;

    void Start()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        elementPause = rootVisualElement.Q("ElementPause");
        elementControls = rootVisualElement.Q("ElementControls");
        elementGameOver = rootVisualElement.Q("ElementGameOver");
        elementScore = rootVisualElement.Q("ElementScore").Q<Label>("Score");
        elementScoreGameOver = elementGameOver.Q<Label>("Score");
        var buttonResume = elementPause.Q<Button>("ButtonResume");
        var buttonControls = elementPause.Q<Button>("ButtonControls");
        var buttonMenu = elementPause.Q<Button>("ButtonMenu");
        var buttonBack = elementControls.Q<Button>("ButtonBack");
        var buttonRetry = elementGameOver.Q<Button>("ButtonRetry");
        var buttonMenuGameOver = elementGameOver.Q<Button>("ButtonMenu");
        
        buttonResume.RegisterCallback<ClickEvent>(ev => TogglePauseElement());
        buttonControls.RegisterCallback<ClickEvent>(ev => SwitchToControls());
        buttonBack.RegisterCallback<ClickEvent>(ev => SwitchToPause());
        buttonMenu.RegisterCallback<ClickEvent>(ev => MainMenu());
        buttonRetry.RegisterCallback<ClickEvent>(ev => Retry());
        buttonMenuGameOver.RegisterCallback<ClickEvent>(ev => MainMenu());
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause") && !gameOverActive)
        {
            TogglePauseElement();
        }

        if (GameManager.gameOver && !gameOverActive)
        {
            GameOver();
        }
    }


    void TogglePauseElement()
    {
        pauseEnabled = !pauseEnabled;

        Time.timeScale = pauseEnabled ? 0 : 1;
        elementPause.style.display = pauseEnabled ? DisplayStyle.Flex : DisplayStyle.None;
        elementControls.style.display = DisplayStyle.None;
        elementGameOver.style.display = DisplayStyle.None;
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

    void GameOver()
    {
        gameOverActive = !gameOverActive;
        GameManager.gameOver = !gameOverActive;
        Time.timeScale = gameOverActive ? 0 : 1;
        elementGameOver.style.display = gameOverActive ? DisplayStyle.Flex : DisplayStyle.None;
        elementControls.style.display = DisplayStyle.None;
        elementPause.style.display = DisplayStyle.None;
        UnityEngine.Cursor.visible = gameOverActive;
        UnityEngine.Cursor.lockState = gameOverActive ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void Retry()
    {
        GameManager.ClearList();
        Time.timeScale = 1;
        gameOverActive = false;
        GameManager.gameOver = false;
        SceneManager.LoadScene(1);
    }

    void MainMenu()
    {
        GameManager.ClearList();
        Time.timeScale = 1;
        gameOverActive = false;
        GameManager.gameOver = false;
        SceneManager.LoadScene(0);
    }

    public static void SetScore(float score)
    {
        elementScore.text = score.ToString();
        elementScoreGameOver.text = score.ToString();
    }
}
