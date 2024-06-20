using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ベースとなるアクションを設定するクラス
/// </summary>
public abstract class BaseAction : MonoBehaviour
{
    public static event EventHandler OnAnyActionStarted;
    public static event EventHandler OnAnyActionCompleted;

    protected Unit unit;
    protected bool isActive;
    protected Action onActionComplete;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    /// <summary>
    /// アクションの名称を返す
    /// </summary>
    /// <returns></returns>
    public abstract string GetActionName();

    /// <summary>
    /// アクションを実行する
    /// </summary>
    /// <param name="gridPosition">グリッド上における座標</param>
    /// <param name="onActionComplete">対象となるアクション</param>
    public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

    /// <summary>
    /// 選択したアクションがグリッド座標で実行可能かどうかを判定する
    /// </summary>
    /// <param name="gridPosition">グリッド上における座標</param>
    /// <returns>実行可能な場合True、不可な場合Falseを返す</returns>
    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    /// <summary>
    /// アクションが実行可能なグリッド座標のリストを返す
    /// </summary>
    /// <returns></returns>
    public abstract List<GridPosition> GetValidActionGridPositionList();

    /// <summary>
    /// アクションを実行する際に消費するアクションポイントの値を返す
    /// </summary>
    /// <returns>消費するアクションポイントの値</returns>
    public virtual int GetActionPointsCost()
    {
        return 1;
    }

    /// <summary>
    /// アクションの実行を開始した際に行う処理
    /// </summary>
    /// <param name="onActionComplete">アクション</param>
    protected void ActionStart(Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;

        OnAnyActionStarted?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// アクションの実行が終了した際に行う処理
    /// </summary>
    protected void ActionComplete()
    {
        isActive = false;
        onActionComplete();
     
        OnAnyActionCompleted?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// ユニットの情報を返す
    /// </summary>
    /// <returns>対象のユニット</returns>
    public Unit GetUnit()
    {
        return unit;
    }

    /// <summary>
    /// 敵側において一番良いアクションを返す
    /// </summary>
    /// <returns>敵側のアクション</returns>
    public EnemyAIAction GetBestEnemyAIAction()
    {
        List<EnemyAIAction> enemyAIActionList = new List<EnemyAIAction>();

        List<GridPosition> validActionGridPositionList = GetValidActionGridPositionList();

        foreach(GridPosition gridPosition in validActionGridPositionList)
        {
            EnemyAIAction enemyAIAction = GetEnemyAIAction(gridPosition);
            enemyAIActionList.Add(enemyAIAction);
        }

        if(enemyAIActionList.Count > 0)
        {
            enemyAIActionList.Sort((EnemyAIAction a, EnemyAIAction b) => b.actionValue - a.actionValue);
            return enemyAIActionList[0];
        }
        else
        {
            return null;
        }
       
    }

    /// <summary>
    /// 敵側のアクションを返す
    /// </summary>
    /// <param name="gridPosition">グリッド上における座標</param>
    /// <returns>敵側のアクション</returns>
    public abstract EnemyAIAction GetEnemyAIAction(GridPosition gridPosition);
}
