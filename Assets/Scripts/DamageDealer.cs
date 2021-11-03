using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int TotalHealth;

    public void takeDamage(int Damage)
    {

        this.TotalHealth -= Damage;

    }

    public int getHealth()
    {
        return this.TotalHealth;
    }


    public void setHealth(int healthInput)
    {
        this.TotalHealth = healthInput;
    }

    public int calcDamageToDeal(int min, int max){

        return Random.Range(min, max);

    }

    public void heal (int healAmt){
        this.TotalHealth += healAmt;
    }

}
