using UnityEngine;

public class TimeSwitcher : MonoBehaviour
{
    [SerializeField] [Tooltip("FloorMoving script")] FloorMoving fm;
    [SerializeField] [Tooltip("PlayerMovement script")] PlayerMovement pm;

    ScoreCounter sc;
    public bool isPaused;

    private void Start()
    {
        sc = GetComponent<ScoreCounter>();
        
        fm.active = false;
        sc.active = false;
        pm.active = false;
    }

    private void Update()
    {
        if (isPaused)
        {
            fm.active = false;
            sc.active = false;
            pm.active = false;
        }
        else
        {
            fm.active = true;
            sc.active = true;
            pm.active = true;
        }
    }
}