using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private int hp;

    /// <summary>
    /// Start position
    /// </summary>
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        //If it's invisible - do nothing
        if (!GetComponent<Renderer>().enabled)
            return;

        if (hp <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);

            transform.position = startPos;
            GetComponent<Renderer>().enabled = false;
            GetComponent<Animator>().enabled = false;
        }

        //return on start position is it's not on player's vision or killed by player
        if (transform.position.z < Main.Instance.Player.position.z)
        {
            Main.Instance.GetScore.GetCurScore += 5;
            Main.Instance.GetScore.GetPassedAst++;
            Main.Instance.GetScore.GetUIasteroids.GetComponentInChildren<ParticleSystem>().Play();

            transform.position = startPos;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Main.Instance.GetMainGC.GameOver();
        }
        else if (collision.collider.tag == "Rocket")
        {
            Destroy(collision.collider.gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);

            transform.position = startPos;
            GetComponent<Renderer>().enabled = false;
            GetComponent<Animator>().enabled = false;
        }
        else if (collision.collider.tag == "Bullet")
        {
            hp--;
            HitEffectGone();
            Invoke("HitEffectGone", 0.05f);
            Destroy(collision.collider.gameObject);
        }
    }

    /// <summary>
    /// Disabling hit effect visability
    /// </summary>
    void HitEffectGone() { hitEffect.GetComponent<Renderer>().enabled = !hitEffect.GetComponent<Renderer>().enabled; }
}
