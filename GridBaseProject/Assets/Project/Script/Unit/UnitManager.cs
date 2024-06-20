using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ׂẴ��j�b�g���Ǘ�����N���X
/// </summary>
public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    public static event EventHandler OnAllFriendlyUnitDead;
    public static event EventHandler OnAllEnemyUnitDead;

    private List<Unit> unitList;
    private List<Unit> frinendlyUnitList;
    private List<Unit> enemyUnitList;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one UnitManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        unitList = new List<Unit>();
        frinendlyUnitList = new List<Unit>();
        enemyUnitList = new List<Unit>();
    }

    private void Start()
    {
        Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
    }

    private void Update()
    {
        if(frinendlyUnitList.Count <= 0)
        {
            OnAllFriendlyUnitDead?.Invoke(this, EventArgs.Empty);
        }
        if(enemyUnitList.Count <= 0)
        {
            OnAllEnemyUnitDead?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Unit_OnAnyUnitSpawned(object sender,EventArgs e)
    {
        Unit unit = sender as Unit;
        unitList.Add(unit);

        if (unit.IsEnemy())
        {
            enemyUnitList.Add(unit);
        }
        else
        {
            frinendlyUnitList.Add(unit);
        }
    }

    private void Unit_OnAnyUnitDead(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;
        unitList.Remove(unit);

        if (unit.IsEnemy())
        {
            enemyUnitList.Remove(unit);
        }
        else
        {
            frinendlyUnitList.Remove(unit);
        }
    }

    /// <summary>
    /// ���j�b�g�̃��X�g��Ԃ�
    /// </summary>
    /// <returns>���j�b�g�̃��X�g</returns>
    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    /// <summary>
    /// �������̃��j�b�g�̃��X�g��Ԃ�
    /// </summary>
    /// <returns>�������̃��j�b�g�̃��X�g</returns>
    public List<Unit> GetFrinendlyUnitList()
    {
        return frinendlyUnitList;
    }

    /// <summary>
    /// �G���̃��j�b�g�̃��X�g��Ԃ�
    /// </summary>
    /// <returns>�G���̃��j�b�g�̃��X�g</returns>
    public List<Unit> GetEnemyUnitList()
    {
        return enemyUnitList;
    }

}
