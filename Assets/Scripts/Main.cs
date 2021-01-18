using UnityEngine;

/// <summary>
/// Storage of public information, whitch needed in many classes
/// </summary>
[RequireComponent(typeof(ScoreCounter))]
public class Main : MonoBehaviour
{
    #region Variables
    [SerializeField] [Tooltip("Player's prefab")] private Transform player;
    [SerializeField] [Tooltip("Current dificulty")] private int curLlv;
    [SerializeField] private bool cheatsOn;

    /// <summary>
    /// Active score counter
    /// </summary>
    private ScoreCounter scorer;
    /// <summary>
    /// Main game controller
    /// </summary>
    private MainGameController mainGC;
    /// <summary>
    /// Controls timeflow of the game
    /// </summary>
    private TimeSwitcher timeSwitch;
    public static Main Instance { get; private set; }
    #endregion

    #region Unity-time
    private void Awake()
    {
        Instance = this;
        scorer = GetComponent<ScoreCounter>();
        mainGC = GetComponent<MainGameController>();
        timeSwitch = GetComponent<TimeSwitcher>();
    }

    void Update()
    {
        if (!cheatsOn)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            curLlv = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            curLlv = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            curLlv = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            curLlv = 4;
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            curLlv = 5;
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            curLlv = 6;
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            player.GetComponentInChildren<Collider>().enabled = false;  //Immortality
    }
    #endregion

    #region Get Variables from other classes
    /// <summary>
    /// Player's prefab on the scene
    /// </summary>
    public Transform Player { get { return player; } }

    /// <summary>
    /// Current dificulty
    /// </summary>
    public int CurLvl
    {
        get { return curLlv; }
        set { curLlv = value; }
    }

    /// <summary>
    /// Current score script
    /// </summary>
    public ScoreCounter GetScore
    {
        get { return scorer; }
        set { scorer = value; }
    }

    /// <summary>
    /// Get main cotroller of timeline
    /// </summary>
    public MainGameController GetMainGC { get { return mainGC; } }

    public TimeSwitcher GetTimeSwitcher { get { return timeSwitch; } }
    #endregion
}