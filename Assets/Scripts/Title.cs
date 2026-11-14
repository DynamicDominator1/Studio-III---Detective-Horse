using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public Button startButton; 
    public Button creditsButton; 
    public Button quitButton;

    public string gameScene = "Load";
    public string creditsScene = "Credits";

    void Start()
    {
        startButton.onClick.AddListener(() => SceneManager.LoadScene(gameScene)); // load the load scene to start game
        creditsButton.onClick.AddListener(() => SceneManager.LoadScene(creditsScene)); // load credits scene
        quitButton.onClick.AddListener(() => Application.Quit()); // quit the game
    }
}