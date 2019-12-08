using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectiles : MonoBehaviour
{
    [HideInInspector]
    public bool firing;
    public GameObject projectile;
    public Vector2 shootDelayRange = new Vector2(0,1);
    float shootDelayTimer;
    GameObject target;

    private void Start()
    {
        shootDelayTimer = Random.Range(shootDelayRange.x, shootDelayRange.y);
        target = FindObjectOfType<Player>().gameObject;
    }

    private void Update()
    {
        if (firing)
        {
            shootDelayTimer -= Time.deltaTime;
            if(shootDelayTimer <= 0)
            {
                Vector2 temp = target.transform.position - transform.position;
                GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
                proj.transform.right = temp;
                shootDelayTimer = Random.Range(shootDelayRange.x, shootDelayRange.y);
            }
        }
    }
}
