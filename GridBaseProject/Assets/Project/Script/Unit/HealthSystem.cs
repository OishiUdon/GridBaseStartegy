using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���j�b�g��HP���Ǘ�����N���X
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
    /// �_���[�W�𕉂����ۂ̏���
    /// </summary>
    /// <param name="damageAmount">�󂯂��_���[�W�̒l</param>
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
    /// ���j�b�g�����S�����ۂ̏���
    /// </summary>
    private void Die()
    {
        OnDead?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// ���j�b�g�̗̑͂̊�����Ԃ�
    /// </summary>
    /// <returns>���j�b�g�̗̑͂̊���</returns>
    public float GetHealthNormalized()
    {
        return (float)health / healthMax;
    }
}
