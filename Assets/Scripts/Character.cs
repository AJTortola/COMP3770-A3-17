using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{

    protected int health;
    protected int maxHealth;

    void start()
    {

    }

    void update()
    {

    }

    public void setName(string name)
    {

        this.name = name;

    }

    // for setting initial health at beginning of sim
    public void setHealth(int health)
    {

        this.health = health;
        this.maxHealth = health;

    }


    // for checking if sim is over
    public int getHealth()
    {

        return this.health;

    }

    public string getName()
    {

        return this.name;

    }


    public void heal(int healthToAdd)
    {

        if (this.maxHealth < healthToAdd + this.health)
        {
            this.health = maxHealth;
        }
        else
        {
            this.health += healthToAdd;
        }

    }


    //Take an int as an argument, and decrement health by that value
    public void takeDamage(int damage)
    {

        if (health < damage)
        {
            this.health = 0;
        }
        else
        {
            this.health -= damage;
        }

    }

    public bool isDead()
    {

        return (health == 0);

    }

}