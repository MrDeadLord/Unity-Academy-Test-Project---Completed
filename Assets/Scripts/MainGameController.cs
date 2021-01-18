using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controls time. Pause/unpause/gameOver
/// </summary>
public class MainGameController : MonoBehaviour
{
    #region Variables
    [Header("Cameras")]
    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera pauseCam;

    [Space(10)]
    [Header("Pause canvases")]
    [SerializeField] [Tooltip("Main menu")] private Canvas canvasStart;
    [SerializeField] [Tooltip("Pause or game over screen")] private Canvas canvasPause;
    [SerializeField] [Tooltip("Input/settings view canvas")] private Canvas canvasSettings;

    [Space(10)]
    [Header("Buttons")]
    [SerializeField] Button[] buttSettings;
    [SerializeField] Button buttBack;
    [SerializeField] Button buttToStart;
    [SerializeField] Button buttResume;
    [SerializeField] Button[] buttQuit;

    /// <summary>
    /// Hiscore path
    /// </summary>
    string path = string.Empty;
    bool gameOver = false;
    TimeSwitcher ts;

    enum sost
    {
        StartScreen,
        PauseScreen,
        GameOverScreen
    }
    /// <summary>
    /// Current Menu screen(for BACK button)
    /// </summary>
    sost curSost;
    #endregion

    #region Unity-time
    private void Start()
    {
        //Disabling all canvases
        canvasPause.enabled = false;
        canvasSettings.enabled = false;
        canvasStart.enabled = false;

        ts = GetComponent<TimeSwitcher>();
        path = Application.dataPath + "/SaveData/HightestScore.txt";
        StartScreen();

        foreach (Button butt in buttSettings)
            butt.onClick.AddListener(Settings);
        buttToStart.onClick.AddListener(StartScreen);
        buttBack.onClick.AddListener(BackFromSett);
        buttToStart.onClick.AddListener(GameStart);
        buttResume.onClick.AddListener(GameResume);
        foreach (Button butt in buttQuit)
            butt.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && !ts.isPaused)
            GamePause();
        else if (Input.GetButtonDown("Pause") && ts.isPaused)
            GameResume();

        if (gameOver)
        {
            if (Input.anyKeyDown)
                SceneManager.LoadScene(0);
        }
    }
    #endregion

    #region Methods
    public void StartScreen()
    {
        ts.isPaused = true;

        curSost = sost.StartScreen;

        mainCam.enabled = false;
        pauseCam.enabled = true;

        canvasStart.enabled = true;

        if (File.Exists(path))
        {
            int score = int.Parse(File.ReadAllText(path));

            foreach (Text tr in canvasStart.GetComponentsInChildren<Text>())
            {
                if (tr.name == "Hiscore")
                    tr.text = "hightest score:\n" + score;
            }
        }
    }

    void GameStart()
    {
        //Disabling cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pauseCam.enabled = false;
        mainCam.enabled = true;

        canvasStart.enabled = false;

        ts.isPaused = false;
    }

    public void GamePause()
    {
        //Enabling cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        ts.isPaused = true;

        curSost = sost.PauseScreen;

        pauseCam.enabled = true;
        mainCam.enabled = false;

        canvasPause.enabled = true;

        foreach (Text line in canvasPause.GetComponentsInChildren<Text>())
        {
            line.enabled = true;

            switch (line.name)
            {
                case "Title":
                    line.text = "Game Paused";
                    line.color = Color.green;
                    break;
                case "HiScore":
                    line.text = Main.Instance.GetScore.GetUIhighestScore.text;
                    break;
                case "Current Score":
                    line.text = Main.Instance.GetScore.GetUIcurScore.text;
                    break;
                case "Asteroids":
                    line.text = Main.Instance.GetScore.GetUIasteroids.text;
                    break;
                case "Time":
                    line.text = Main.Instance.GetScore.GetUItimer.text;
                    break;
            }
        }
    }

    void GameResume()
    {
        //Disabling cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        ts.isPaused = false;

        canvasPause.enabled = false;
        pauseCam.enabled = false;
        mainCam.enabled = true;
    }

    public void Settings()
    {
        //Enabling cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        canvasPause.enabled = false;
        canvasStart.enabled = false;
        canvasSettings.enabled = true;
    }

    public void GameOver()
    {
        //Enabling cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameOver = true;

        ts.isPaused = true;

        curSost = sost.GameOverScreen;

        pauseCam.enabled = true;
        mainCam.enabled = false;
        canvasPause.enabled = true;

        foreach (Text line in canvasPause.GetComponentsInChildren<Text>())
        {
            switch (line.name)
            {
                case "Title":
                    if (Main.Instance.GetScore.GetCurScore == Main.Instance.GetScore.GetHiScore)
                    {
                        line.text = "congratulations! you've beated best!";
                        line.color = Color.green;
                    }
                    else
                    {
                        line.text = "Game Over";
                        line.color = Color.red;
                    }
                    break;
                case "HiScore":
                    line.text = Main.Instance.GetScore.GetUIhighestScore.text;
                    break;
                case "Current Score":
                    line.text = Main.Instance.GetScore.GetUIcurScore.text;
                    break;
                case "Asteroids":
                    line.text = Main.Instance.GetScore.GetUIasteroids.text;
                    break;
                case "Time":
                    line.text = Main.Instance.GetScore.GetUItimer.text;
                    break;
            }
        }

        //Record hiscore if it's better than previous
        if (Main.Instance.GetScore.GetCurScore >= Main.Instance.GetScore.GetHiScore)
            File.WriteAllText(path, Main.Instance.GetScore.GetCurScore.ToString());
    }

    void BackFromSett()
    {
        switch (curSost)
        {
            case sost.StartScreen:
                canvasSettings.enabled = false;
                StartScreen();
                break;
            case sost.PauseScreen:
                canvasSettings.enabled = false;
                GamePause();
                break;
            case sost.GameOverScreen:
                canvasSettings.enabled = false;
                GameOver();
                break;
        }
    }

    void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}