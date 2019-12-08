using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Item
{
    public int amountToHeal = 1;
    
    public override void Collect(Inventory other)
    {
        Player player = other.GetComponent<Player>();
        player.Heal(amountToHeal);
    }
}
