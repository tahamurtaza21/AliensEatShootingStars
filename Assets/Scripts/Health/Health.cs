using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;

    public void Hit(int damage)
    {
        health -= damage;
        Debug.Log(health);
    }



}
