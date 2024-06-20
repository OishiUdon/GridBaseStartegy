using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ���j�b�g���Ǘ�����N���X
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
    /// ���̃A�N�V�������擾����
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
    /// �O���b�h��ɂ����郆�j�b�g�̍��W��Ԃ�
    /// </summary>
    /// <returns>�O���b�h��ɂ�������W</returns>
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    /// <summary>
    /// ���[���h��ɂ����郆�j�b�g�̍��W��Ԃ�
    /// </summary>
    /// <returns>���[���h��ɂ�������W</returns>
    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    /// <summary>
    /// �A�N�V�����̔z���Ԃ�
    /// </summary>
    /// <returns>�A�N�V�����̔z��</returns>
    public BaseAction[] GetBaseActionArray()
    {
        return baseActionArray;
    }

    /// <summary>
    /// �A�N�V�����|�C���g������ăA�N�V���������s�\���ǂ��������݂�
    /// </summary>
    /// <param name="baseAction">�A�N�V����</param>
    /// <returns>���s�\�ȏꍇTrue�A�s�ȏꍇFalse��Ԃ�</returns>
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
    /// �I�����ꂽ�A�N�V���������݂̃A�N�V�����|�C���g���ōs���邩�ǂ����𔻒肷��
    /// </summary>
    /// <param name="baseAction">�A�N�V����</param>
    /// <returns>�s����ꍇTrue�A�s���Ȃ��ꍇFalse��Ԃ�</returns>
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
    /// �A�N�V�����|�C���g�������
    /// </summary>
    /// <param name="amount">�����l</param>
    private void  SpendActionPoints(int amount)
    {
        actionPoints -= amount;

        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// ���݂̃A�N�V�����|�C���g�̒l��Ԃ�
    /// </summary>
    /// <returns>�A�N�V�����|�C���g</returns>
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
    /// ���j�b�g���G�����ǂ�����Ԃ�
    /// </summary>
    /// <returns>�G���̏ꍇTrue�A�����łȂ��ꍇFalse��Ԃ�</returns>
    public bool IsEnemy()
    {
        return isEnemy;
    }

    /// <summary>
    /// ���j�b�g���_���[�W���󂯂��ꍇ�̏������s��
    /// </summary>
    /// <param name="damageAmount">�󂯂��_���[�W�̒l</param>
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
    /// ���j�b�g��HP�̊�����Ԃ�
    /// </summary>
    /// <returns>���j�b�g��HP�̊���</returns>
    public float GetHealthNormalized()
    {
        return healthSystem.GetHealthNormalized();
    }
}
