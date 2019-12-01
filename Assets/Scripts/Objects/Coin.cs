using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{
    public int coinValue = 1;
    public override void Collect(Collider2D other)
    {
        Inventory player = other.GetComponent<Inventory>();
        player.ReceiveCollectable(this);
    }

}
