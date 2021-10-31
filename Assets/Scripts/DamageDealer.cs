using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int TotalHealth;

    public void TakeDamage(int Damage)
    {

        this.TotalHealth -= Damage;

    }

    public void SetHealth()
    {
        this.TotalHealth = TotalHealth;
    }

}
