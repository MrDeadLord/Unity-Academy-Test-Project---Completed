using UnityEngine;

public class FloorMoving : MonoBehaviour
{
    #region Variables
    [SerializeField] [Tooltip("Start normal speed")] [Range(1, 20)]private float startSpeed = 10;
    [SerializeField] [Tooltip("Textured plates of the floor")] private Transform[] plates;

    /// <summary>
    /// Player's position
    /// </summary>
    private Vector3 playerPos;
    /// <summary>
    /// Last position in real world plates line
    /// </summary>
    private int lastPosIndex;
    /// <summary>
    /// Current dificulty
    /// </summary>
    private int curLvl;
    /// <summary>
    /// Current speed
    /// </summary>
    private float speed;

    [HideInInspector] public bool active;
    #endregion

    #region Unity-time
    private void Start()
    {
        playerPos = Main.Instance.Player.position;
        lastPosIndex = plates.Length - 1;

        curLvl = Main.Instance.CurLvl;
    }

    private void Update()
    {
        if (!active)
            return;

        //Changing lvl if dificulty changed
        if (curLvl != Main.Instance.CurLvl)
        {
            curLvl = Main.Instance.CurLvl;            
        }

        if (Input.GetButton("Boost"))
            Moving(true);
        else
            Moving(false);

        //Trigger the animation only once
        if(Input.GetButtonDown("Boost"))
            Main.Instance.Player.GetComponentInChildren<Animator>().SetTrigger("Boost");
    }
    #endregion

    /// <summary>
    /// Plates moving
    /// </summary>
    /// <param name="isBoost">Is it boosted speed?</param>
    void Moving(bool isBoost)
    {        
        speed = startSpeed * curLvl;    //Selecting speed depending on current lvl

        float moveSpeed = (isBoost ? speed * 2 : speed);

        foreach (Transform obj in plates)
        {
            Vector3 pos = obj.position; //Start position
            pos.z -= moveSpeed * Time.deltaTime;

            //If plate's out of vision, it teleports in the beginning of a line
            if (obj.position.z <= playerPos.z - 10)
            {
                pos.z = plates[lastPosIndex].position.z + 10; //New spawn point = last in array +10

                if (lastPosIndex == plates.Length - 1)
                    lastPosIndex = 0;
                else
                    lastPosIndex++;
            }

            obj.position = pos; //Moved position
        }
    }
}
