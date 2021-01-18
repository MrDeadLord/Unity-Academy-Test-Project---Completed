///On each lvl we earn new ability. Here is the full list of them:
///1) start
///2) can shoot
///3) can shoot rockets
///4) enabling the laser for aiming
///5) get time slow machine

using UnityEngine;

public class Upgrader : MonoBehaviour
{
    #region Variables
    [Space(5)]
    [Header("Upgrade attachments")]
    [SerializeField] private GameObject laser;
    [SerializeField] [Tooltip("Time Machine")] private GameObject timeSlower;    
    //Не успел доделать прокачку до 6 лвл, где юзер бы выбирал что прокачать
    //[SerializeField] [Tooltip("Time Machine upgraded")] private GameObject timeSlowerPowered;
    //[SerializeField] [Tooltip("Bullets power upgrade")] private GameObject bulletPowered;

    [Space(5)]
    [SerializeField] [Tooltip("Upgrade effect")] private ParticleSystem effect;

    int curLvl = 1;
    #endregion

    #region Unity-time
    private void Update()
    {
        //Changing the level if dificulty changed
        if (curLvl != Main.Instance.CurLvl)
        {
            curLvl = Main.Instance.CurLvl;
            LvlUp(curLvl);
        }
    }
    #endregion

    void LvlUp(int lvl)
    {
        effect.Play();

        switch (lvl)
        {
            case 4:
                laser.GetComponent<Renderer>().enabled = true;
                break;
            case 5:
                timeSlower.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// Get time machine's light
    /// </summary>
    public Light GetTimeMachLight { get { return timeSlower.GetComponentInChildren<Light>(); } }
}