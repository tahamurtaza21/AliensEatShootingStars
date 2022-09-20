using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{

    [Header("Damage")]
    Health health;
    [SerializeField] int damage = 10;

    void Start()
    {
        health = gameObject.transform.root.GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("something");
        if(collision.gameObject.tag == "Obstacle")
        {
            health.Hit(damage);
            Destroy(collision.gameObject);
        }
    }
}
