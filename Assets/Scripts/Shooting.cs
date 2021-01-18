///On each lvl we earn new ability. Here is the full list of them:
///1) start
///2) can shoot
///3) can shoot rockets
///4) enabling the laser for aiming
///5) get time slow machine

using UnityEngine;

public class Shooting : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform[] bulletBarrels;
    [SerializeField] private Transform rocketBarrel;

    [Space(10)]
    [Header("Instantiate objects")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject rocket;
    [Space(5)]
    [SerializeField] private ParticleSystem[] bulletShootEffect;
    [SerializeField] private ParticleSystem rocketShootEffect;

    [Space(10)]
    [SerializeField] private int bulletSpeed = 300;
    [SerializeField] [Tooltip("Firerate of a bullet")][Range(0.01f, 2)] private float bulletRate;
    [Space(5)]
    [SerializeField] private int rocketSpeed;
    [SerializeField] [Tooltip("Firerate of a rocket")][Range(0.5f, 5)] private float rocketRate;
    [Space(5)]
    [SerializeField] [Tooltip("Couldown of time machine")] private int timeMachCD;
    [SerializeField] [Tooltip("TimeScale of time machine")] [Range(0, 1)] private float timeMachPower;
    [SerializeField] [Tooltip("How long does it works")] [Range(0, 2)] private float timeMachRate;
    
    int curLvl = 1;
    bool canBullet = false, canRocket = false, canTimeMach = false; //Can we do any of this stuff or not
    #endregion

    #region Unity-time
    private void Update()
    {
        //Changing the level if dificulty changed
        if (curLvl != Main.Instance.CurLvl)
        {
            curLvl = Main.Instance.CurLvl;

            switch (Main.Instance.CurLvl)
            {
                case 2:
                    canBullet = true;
                    break;
                case 3:
                    canRocket = true;
                    break;
                case 5:
                    canTimeMach = true;
                    break;
                //case 6: //Добавить сюда выбранную опцию из Upgrader-a
            }
        }

        if (Input.GetButton("Fire1") && canBullet)
        {
            ShootBullet();
            ReverceBullet();
            Invoke("ReverceBullet", bulletRate);
        }
        else if (Input.GetButtonDown("Fire2") && canRocket)
        {
            ShootRocket();
            ReverceRocket();
            Invoke("ReverceRocket", rocketRate);
        }
        
        if(Input.GetButtonDown("Slow") && canTimeMach)
        {
            Time.timeScale = timeMachPower;
            ReverceTimeMach();
            Invoke("ReverceTimeMach", timeMachCD);
            Invoke("TimeSlowEnd", timeMachRate);
        }

        if (!canTimeMach && GetComponent<Upgrader>().GetTimeMachLight.color != Color.red)
            GetComponent<Upgrader>().GetTimeMachLight.color = Color.red;
        else if (canTimeMach && GetComponent<Upgrader>().GetTimeMachLight.color != Color.green)
            GetComponent<Upgrader>().GetTimeMachLight.color = Color.green;
    }
    #endregion

    void ShootBullet()
    {
        var tempBull1 = Instantiate(bullet, bulletBarrels[0].position, Quaternion.identity);
        bulletShootEffect[0].Play();
        tempBull1.GetComponent<Rigidbody>().AddForce(bulletBarrels[0].forward * bulletSpeed);
        Destroy(tempBull1, 2);

        var tempBull2 = Instantiate(bullet, bulletBarrels[1].position, Quaternion.identity);
        bulletShootEffect[1].Play();
        tempBull2.GetComponent<Rigidbody>().AddForce(bulletBarrels[1].forward * bulletSpeed);
        Destroy(tempBull2, 2);
    }

    void ShootRocket()
    {
        var tempRocket = Instantiate(rocket, rocketBarrel.position, Quaternion.identity);
        rocketShootEffect.Play();
        tempRocket.GetComponent<Rigidbody>().velocity = Vector3.forward * rocketSpeed;
        Destroy(tempRocket, 5);
    }

    /// <summary>
    /// Runs time in normal speed
    /// </summary>
    void TimeSlowEnd()
    {
        Time.timeScale = 1;
    }

    //Reverce of parameters
    void ReverceBullet() { canBullet = !canBullet; }    
    void ReverceRocket() { canRocket = !canRocket; }
    void ReverceTimeMach() { canTimeMach = !canTimeMach; }
}