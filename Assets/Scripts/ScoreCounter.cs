using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    #region Variables
    [SerializeField] private Text UIhighestScore;
    [SerializeField] private Text UIcurScore;
    [SerializeField] private Text UIasteroids;
    [SerializeField] private Text UItimer;

    public bool active;

    /// <summary>
    /// Current score
    /// </summary>
    private int curScore;
    /// <summary>
    /// Passed asteroids count
    /// </summary>
    private int passAst;    //Pluses in Asetroid script
    /// <summary>
    /// How much time passed from start
    /// </summary>
    private float timer;

    private int hightestScore;

    /// <summary>
    /// to make sure score scored only per second
    /// </summary>
    private int startTimer;
    #endregion

    #region Unity-time
    void Start()
    {
        curScore = 0;

        string path = Application.dataPath + "/SaveData/HightestScore.txt";

        if (File.Exists(path))
            hightestScore = int.Parse(File.ReadAllText(path));
        else
            hightestScore = 0;

        UIhighestScore.text += hightestScore;
    }

    void Update()
    {
        if (!active)
            return;

        startTimer = (int)timer;
        timer += Time.deltaTime;

        //Each time timer change int state, we do +1 or +2
        if ((int)timer > startTimer)
        {
            if (Input.GetButton("Boost"))
                curScore += 2;
            else
                curScore++;
        }

        if (curScore > hightestScore)
            hightestScore = curScore;

        UIhighestScore.text = "highest score:\n" + hightestScore;
        UIcurScore.text = "current score:\n" + curScore;
        UIasteroids.text = "asteroids passed:\n" + passAst;
        UItimer.text = "played time:\n" + timer;

        //Dificulty change
        switch (curScore / 100)
        {
            case 1:
                Main.Instance.CurLvl = 2;
                break;
            case 3:
                Main.Instance.CurLvl = 3;
                break;
            case 6:
                Main.Instance.CurLvl = 4;
                break;
            case 9:
                Main.Instance.CurLvl = 5;
                break;
            case 12:
                Main.Instance.CurLvl = 6;
                break;
        }
    }
    #endregion

    #region Public "Get" methods
    /// <summary>
    /// Get Current score
    /// </summary>
    public int GetCurScore
    {
        get { return curScore; }
        set { curScore = value; }
    }

    /// <summary>
    /// Get count of passed asteroids
    /// </summary>
    public int GetPassedAst
    {
        get { return passAst; }
        set { passAst = value; }
    }

    public float GetTimer { get { return timer; } }

    public int GetHiScore { get { return hightestScore; } }

    public Text GetUIhighestScore { get { return UIhighestScore; } }
    public Text GetUIcurScore { get { return UIcurScore; } }
    public Text GetUIasteroids { get { return UIasteroids; } }
    public Text GetUItimer { get { return UItimer; } }
    #endregion
}
