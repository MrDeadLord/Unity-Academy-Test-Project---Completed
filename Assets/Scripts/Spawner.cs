using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform[] spawnPoints;
    [Space(5)]
    [SerializeField] [Tooltip("How many and what object will be spawning")] private Transform[] spawnObjects;

    /// <summary>
    /// Existing points
    /// </summary>
    private List<Vector3> exPos = new List<Vector3>();
    /// <summary>
    /// Position of the player
    /// </summary>
    private Vector3 playerPos;
    /// <summary>
    /// Spawning object
    /// </summary>
    private Transform tempObj;
    /// <summary>
    /// Existing/spawned objects that are visible
    /// </summary>
    private List<Transform> exObjects = new List<Transform>();
    /// <summary>
    /// To make sure it wouldn't spawn forever
    /// </summary>
    private bool spawned = false;

    private int line1 = 0, line2 = 0;   //To make sure pass not blocked at all
    #endregion

    #region Unity-time
    private void Start()
    {
        playerPos = Main.Instance.Player.position;
    }

    private void Update()
    {
        //Make sure asteroids wouldn't spawn on player or too close to him
        if (transform.position.z > playerPos.z + 20)
            switch (Main.Instance.CurLvl)
            {
                case 1:
                    if (!spawned)
                        Spawn(Random.Range(0, 2));
                    break;
                case 2:
                    if (!spawned)
                        Spawn(Random.Range(0, 3));
                    break;
                case 3:
                    if (!spawned)
                        Spawn(Random.Range(1, 4));
                    break;
                case 4:
                    if (!spawned)
                        Spawn(Random.Range(1, 5));
                    break;
                case 5:
                    if (!spawned)
                        Spawn(Random.Range(2, 6));
                    break;
                case 6:
                    if (!spawned)
                        Spawn(Random.Range(3, 6));
                    break;
            }

        if (transform.position.z <= playerPos.z - 10)
            ClearPlate();
    }
    #endregion

    /// <summary>
    /// Spawn object count times
    /// </summary>
    /// <param name="count">How many objects needed?</param>
    void Spawn(int count)
    {
        int index;  //random index of spawn point
        Vector3 pos;    //selected position

        for (int i = 0; i < count; i++)
        {
            //Keep randomizing till get unique, that we don't have on plate
            do
            {
                index = Random.Range(0, spawnPoints.Length - 1);
                pos = spawnPoints[index].position;
            }
            while (exPos.Contains(pos));

            //+1 existing position & object
            if (line1 < 3 && line2 < 3)
            {
                tempObj = spawnObjects[i];  //selecting object

                exPos.Add(pos);
                exObjects.Add(tempObj);

                //Adding count on line
                if (exPos[exPos.IndexOf(pos)].z < spawnPoints[0].position.z)
                    line1++;
                else
                    line2++;

                //Turning on created object
                tempObj.position = pos;
                tempObj.GetComponent<Renderer>().enabled = true;
                tempObj.GetComponent<Animator>().enabled = true;
            }
            //If line is full(3 asteroids)
            else
            {
                return;
            }
        }

        spawned = true;
    }

    /// <summary>
    /// Clear plate from objects on it
    /// </summary>
    void ClearPlate()
    {
        exPos.Clear();

        //Making every object invisible and not animated
        foreach (Transform temp in exObjects)
        {
            temp.GetComponent<Renderer>().enabled = false;
            temp.GetComponent<Animator>().enabled = false;
        }

        exObjects.Clear();
        spawned = false;    //To make objects spawn again
    }
}