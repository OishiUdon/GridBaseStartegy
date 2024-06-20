using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ユニットを管理するクラス
/// </summary>
public class Unit : MonoBehaviour
{
    [SerializeField]
    private int maxActionPoints = 2;

    [SerializeField]
    private bool isEnemy;

    public static event EventHandler OnAnyActionPointsChanged;
    public static event EventHandler OnAnyUnitSpawned;
    public static event EventHandler OnAnyUnitDead;

    private GridPosition gridPosition;
    private HealthSystem healthSystem;
    private BaseAction[] baseActionArray;
    private int actionPoints = 2;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        baseActionArray = GetComponents<BaseAction>();
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        healthSystem.OnDead += HealthSystem_OnDead;

        OnAnyUnitSpawned?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newGridPosition != gridPosition)
        {
            GridPosition oldGridPosition = gridPosition;
            gridPosition = newGridPosition;
            LevelGrid.Instance.UnitMovedGridPosition(this, oldGridPosition, newGridPosition);
        }
    }

    /// <summary>
    /// 元のアクションを取得する
    /// </summary>
    public T GetAction<T>() where T : BaseAction
    {
        foreach(BaseAction baseAction in baseActionArray)
        {
            if(baseAction is T)
            {
                return (T)baseAction;
            }
        }
        return null;
    }

    /// <summary>
    /// グリッド上におけるユニットの座標を返す
    /// </summary>
    /// <returns>グリッド上における座標</returns>
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    /// <summary>
    /// ワールド上におけるユニットの座標を返す
    /// </summary>
    /// <returns>ワールド上における座標</returns>
    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    /// <summary>
    /// アクションの配列を返す
    /// </summary>
    /// <returns>アクションの配列</returns>
    public BaseAction[] GetBaseActionArray()
    {
        return baseActionArray;
    }

    /// <summary>
    /// アクションポイントを消費してアクションが実行可能かどうかを試みる
    /// </summary>
    /// <param name="baseAction">アクション</param>
    /// <returns>実行可能な場合True、不可な場合Falseを返す</returns>
    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if (CanSpendActionPointsToTakeAction(baseAction))
        {
            SpendActionPoints(baseAction.GetActionPointsCost());
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 選択されたアクションが現在のアクションポイント内で行えるかどうかを判定する
    /// </summary>
    /// <param name="baseAction">アクション</param>
    /// <returns>行える場合True、行えない場合Falseを返す</returns>
    public bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if(actionPoints >= baseAction.GetActionPointsCost())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// アクションポイントを消費する
    /// </summary>
    /// <param name="amount">消費する値</param>
    private void  SpendActionPoints(int amount)
    {
        actionPoints -= amount;

        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// 現在のアクションポイントの値を返す
    /// </summary>
    /// <returns>アクションポイント</returns>
    public int GetActionPoints()
    {
        return actionPoints;
    }

    private void TurnSystem_OnTurnChanged(object sender,EventArgs e)
    {
        if((IsEnemy() && !TurnSystem.Instance.IsPlayerTurn()) ||
           (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn()))
        {
            actionPoints = maxActionPoints;
            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// ユニットが敵側かどうかを返す
    /// </summary>
    /// <returns>敵側の場合True、そうでない場合Falseを返す</returns>
    public bool IsEnemy()
    {
        return isEnemy;
    }

    /// <summary>
    /// ユニットがダメージを受けた場合の処理を行う
    /// </summary>
    /// <param name="damageAmount">受けたダメージの値</param>
    public void Damage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
    }

    private void HealthSystem_OnDead(object sender,EventArgs e)
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition, this);
        Destroy(gameObject);
        OnAnyUnitDead?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// ユニットのHPの割合を返す
    /// </summary>
    /// <returns>ユニットのHPの割合</returns>
    public float GetHealthNormalized()
    {
        return healthSystem.GetHealthNormalized();
    }
}
