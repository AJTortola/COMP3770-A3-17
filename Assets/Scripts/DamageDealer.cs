using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int health;

    public void TakeDamage(int Damage)
    {

        this.health -= Damage;

    }

    public void SetHealth()
    {
        this.health = health;
    }

}
