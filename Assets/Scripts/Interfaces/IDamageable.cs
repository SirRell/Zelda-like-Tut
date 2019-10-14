using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamageable<T> : IDestroyable
{
    void TakeDamage(T damageTaken);
}

public interface IDestroyable
{
    void Destroy();
}

