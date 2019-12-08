using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public GameObject[] collectableItems;
    public int chanceOfSpawn = 50;

    public GameObject GetRandomItem()
    {
        int chance = Random.Range(0, 100);

        if(chance <= chanceOfSpawn)
        {
            return collectableItems[Random.Range(0, collectableItems.Length)];
        }

        return null;
    }
}
