using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamageable<T, V> : IDestroyable
{
    void TakeDamage(T damageTaken, V damageGiver);
}

public interface IDestroyable
{
    void Destroy();
}

