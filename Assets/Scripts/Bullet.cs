﻿using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
            Destroy(gameObject);
    }
}
