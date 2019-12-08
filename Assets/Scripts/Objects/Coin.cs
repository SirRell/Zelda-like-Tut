using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public int coinValue = 1;

    public override void Collect(Inventory player)
    {
        player.ReceiveItem(gameObject);
    }

}
