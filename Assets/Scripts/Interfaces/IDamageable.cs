using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamageable<T>
{
    void TakeDamage(T damageTaken);
}

public interface IDestroyable
{
    void Destroy();
}

