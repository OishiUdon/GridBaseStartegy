using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �x�[�X�ƂȂ�A�N�V������ݒ肷��N���X
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
    /// �A�N�V�����̖��̂�Ԃ�
    /// </summary>
    /// <returns></returns>
    public abstract string GetActionName();

    /// <summary>
    /// �A�N�V���������s����
    /// </summary>
    /// <param name="gridPosition">�O���b�h��ɂ�������W</param>
    /// <param name="onActionComplete">�ΏۂƂȂ�A�N�V����</param>
    public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

    /// <summary>
    /// �I�������A�N�V�������O���b�h���W�Ŏ��s�\���ǂ����𔻒肷��
    /// </summary>
    /// <param name="gridPosition">�O���b�h��ɂ�������W</param>
    /// <returns>���s�\�ȏꍇTrue�A�s�ȏꍇFalse��Ԃ�</returns>
    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    /// <summary>
    /// �A�N�V���������s�\�ȃO���b�h���W�̃��X�g��Ԃ�
    /// </summary>
    /// <returns></returns>
    public abstract List<GridPosition> GetValidActionGridPositionList();

    /// <summary>
    /// �A�N�V���������s����ۂɏ����A�N�V�����|�C���g�̒l��Ԃ�
    /// </summary>
    /// <returns>�����A�N�V�����|�C���g�̒l</returns>
    public virtual int GetActionPointsCost()
    {
        return 1;
    }

    /// <summary>
    /// �A�N�V�����̎��s���J�n�����ۂɍs������
    /// </summary>
    /// <param name="onActionComplete">�A�N�V����</param>
    protected void ActionStart(Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;

        OnAnyActionStarted?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// �A�N�V�����̎��s���I�������ۂɍs������
    /// </summary>
    protected void ActionComplete()
    {
        isActive = false;
        onActionComplete();
     
        OnAnyActionCompleted?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// ���j�b�g�̏���Ԃ�
    /// </summary>
    /// <returns>�Ώۂ̃��j�b�g</returns>
    public Unit GetUnit()
    {
        return unit;
    }

    /// <summary>
    /// �G���ɂ����Ĉ�ԗǂ��A�N�V������Ԃ�
    /// </summary>
    /// <returns>�G���̃A�N�V����</returns>
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
    /// �G���̃A�N�V������Ԃ�
    /// </summary>
    /// <param name="gridPosition">�O���b�h��ɂ�������W</param>
    /// <returns>�G���̃A�N�V����</returns>
    public abstract EnemyAIAction GetEnemyAIAction(GridPosition gridPosition);
}
