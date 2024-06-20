using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// すべてのユニットを管理するクラス
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
    /// ユニットのリストを返す
    /// </summary>
    /// <returns>ユニットのリスト</returns>
    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    /// <summary>
    /// 味方側のユニットのリストを返す
    /// </summary>
    /// <returns>味方側のユニットのリスト</returns>
    public List<Unit> GetFrinendlyUnitList()
    {
        return frinendlyUnitList;
    }

    /// <summary>
    /// 敵側のユニットのリストを返す
    /// </summary>
    /// <returns>敵側のユニットのリスト</returns>
    public List<Unit> GetEnemyUnitList()
    {
        return enemyUnitList;
    }

}
