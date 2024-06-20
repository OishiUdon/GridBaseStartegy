using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットのHPを管理するクラス
/// </summary>
public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDead;
    public event EventHandler OnDamaged;

    [SerializeField]
    private int health = 100;

    private int healthMax;

    private void Awake()
    {
        healthMax = health;
    }

    /// <summary>
    /// ダメージを負った際の処理
    /// </summary>
    /// <param name="damageAmount">受けたダメージの値</param>
    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if(health < 0)
        {
            health = 0;
        }

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if(health == 0)
        {
            Die();
        }

    }

    /// <summary>
    /// ユニットが死亡した際の処理
    /// </summary>
    private void Die()
    {
        OnDead?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// ユニットの体力の割合を返す
    /// </summary>
    /// <returns>ユニットの体力の割合</returns>
    public float GetHealthNormalized()
    {
        return (float)health / healthMax;
    }
}
