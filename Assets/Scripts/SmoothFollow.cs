using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] [Tooltip("Speed of FOV changing")] [Range(0, 1)] private float speed;
    private float startFOV;

    private void Start()
    {
        startFOV = GetComponent<Camera>().fieldOfView;
    }
    private void FixedUpdate()
    {
        if (Input.GetButton("Boost") && GetComponent<Camera>().fieldOfView < 90)
        {
            GetComponent<Camera>().fieldOfView += speed;
        }

        if (!Input.GetButton("Boost") && GetComponent<Camera>().fieldOfView > startFOV)
            GetComponent<Camera>().fieldOfView -= speed * 2;
    }
}