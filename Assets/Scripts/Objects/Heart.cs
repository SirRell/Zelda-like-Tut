using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Collectable
{
    public int amountToHeal = 1;
    
    public override void Collect(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        player.Heal(amountToHeal);
    }
}
