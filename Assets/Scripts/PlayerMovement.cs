using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] [Tooltip("Mouse sensitivity(if use it)")] [Range(0.01f, 0.5f)] private float mouseSens;
    [SerializeField] [Tooltip("Slide speed")] private float speedSlide = 10;
    [SerializeField] [Tooltip("Max slide distance")] private float maxSlide = 4;

    [SerializeField] [Tooltip("Normal speed")] private float speed;
    [SerializeField] [Tooltip("Boosted speed")] private float speedBoost;

    private Vector3 posX;
    float slide = 0;

    [HideInInspector] public bool active;
    #endregion

    #region Unity-time
    void Awake()
    {
        //Write start position in variable
        posX = transform.position;
    }

    private void Update()
    {
        if (!active)
            return;

        if (Input.GetButton("Horizontal"))
            Moving("Horizontal");
        else
            Moving("MouseX");

        if (!Input.GetButton("Horizontal") && Input.GetAxis("MouseX") == 0)
        {
            GetComponentInChildren<Animator>().SetBool("Left turn", false);
            GetComponentInChildren<Animator>().SetBool("Right turn", false);
        }
    }
    #endregion

    void Moving(string axis)
    {
        if (axis == "Horizontal")
            slide = Input.GetAxis("Horizontal") * speedSlide;
        else
            slide = Input.GetAxis("MouseX") * speedSlide * mouseSens;

        //Animation
        if (slide < 0)
            GetComponentInChildren<Animator>().SetBool("Left turn", true);
        else if (slide > 0)
            GetComponentInChildren<Animator>().SetBool("Right turn", true);

        posX.x += slide * Time.deltaTime;

        posX.x = Mathf.Clamp(posX.x, -maxSlide, maxSlide);

        transform.position = posX;
    }
}