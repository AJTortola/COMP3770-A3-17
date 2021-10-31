using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    public int TotalHealth;
    public int TotalMana;

    // Start is called before the first frame update
    public void heal(int heal, int mana)
    {
        this.TotalHealth += heal;
        this.TotalMana -= mana;
        
    }
}
