using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    

    [Header("Spawner")]
    string locationOfSpawner;
    Vector2 direction;
    Rigidbody2D rb2d;
    Spawner spawner;

    [Header("Obstacle Properties")]
    [SerializeField] float moveSpeed = 3f;

    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        rb2d = GetComponent<Rigidbody2D>();
        locationOfSpawner = gameObject.transform.root.name;
        direction = FindDirection();
    }

    void Update()
    {
        Move(direction);
    }

    Vector2 FindDirection()
    {
        if (locationOfSpawner == "Up")
        {
            return Vector2.down;
        }
        else if (locationOfSpawner == "Down")
        {
            return Vector2.up;
        }
        else if (locationOfSpawner == "Right")
        {
            return Vector2.left;
        }
        else if (locationOfSpawner == "Left")
        {
            return Vector2.right;
        }

        return Vector2.zero;
    }


    void Move(Vector2 direction)
    {
        rb2d.velocity = direction * moveSpeed * Time.deltaTime;
    }

    /* private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("something");
        if(collision.gameObject.GetComponentInChildren<CircleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Hit")))
        {
            //Debug.Log("something1");
            health.Hit(damage);
            Destroy(gameObject);
        }
    } */

    void OnBecameInvisible()
    {
        spawner.obstaclesInScene--;
        Destroy(gameObject, 3f);
    }
}
