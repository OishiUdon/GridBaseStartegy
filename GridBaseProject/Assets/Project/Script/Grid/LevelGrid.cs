using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �O���b�h�̃��x����ݒ�E�Ǘ�����N���X
/// </summary>
public class LevelGrid : MonoBehaviour
{

    public static LevelGrid Instance { get; private set; }

    public event EventHandler OnAnyUnitMovedGridPosiiton;

    [SerializeField]
    private int width = 10;

    [SerializeField]
    private int height = 10;

    [SerializeField]
    private float cellSize = 2.0f;

    private GridSystem<GridObject> gridSystem;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one LevelGrid! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        gridSystem = new GridSystem<GridObject>(width, height, cellSize,
            (GridSystem<GridObject> g, GridPosition gridPosition) => new GridObject(g, gridPosition));
    }

    private void Start()
    {
        Pathfinding.Instance.Setup(width, height, cellSize);
    }

    /// <summary>
    /// �w��̃O���b�h�Ƀ��j�b�g�̏���������
    /// </summary>
    /// <param name="gridPosition">�O���b�h�ɂ�������W</param>
    /// <param name="unit">�Ώۂ̃��j�b�g</param>
    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }

    /// <summary>
    /// �w��̃O���b�h�Ƀ��j�b�g���X�g�̏���������
    /// </summary>
    /// <param name="gridPosition">�O���b�h�ɂ�������W</param>
    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnitList();
    }

    /// <summary>
    /// �w��̃O���b�h�Ƀ��j�b�g�̏�������
    /// </summary>
    /// <param name="gridPosition">�O���b�h�ɂ�������W</param>
    /// <param name="unit">�Ώۂ̃��j�b�g</param>
    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    /// <summary>
    /// ���j�b�g���ړ������ۂ̃O���b�h�̏������s��
    /// </summary>
    /// <param name="unit">�Ώۂ̃��j�b�g</param>
    /// <param name="fromGridPosition">�O���b�h��ňړ����J�n�������W</param>
    /// <param name="toGridPosition">�O���b�h��ňړ����I��������W</param>
    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition, unit);
        OnAnyUnitMovedGridPosiiton?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// �w�肵�����[���h��̈ʒu�ɂ�����GridPosition��Ԃ�
    /// </summary>
    /// <param name="worldPosition">���[���h�ɂ�������W</param>
    /// <returns>�Ή�����ʒu��GridPosition</returns>
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return gridSystem.GetGridPosition(worldPosition);
    }

    /// <summary>
    /// �w�肵���O���b�h��̈ʒu�ɂ�����WorldPosition��Ԃ�
    /// </summary>
    /// <param name="gridPosition">�O���b�h�ɂ�������W</param>
    /// <returns>�Ή�����ʒu��WorldPosition</returns>
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return gridSystem.GetWorldPosition(gridPosition);
    }

    /// <summary>
    /// �w�肵���O���b�h�̍��W������\���ǂ����𔻒肷��
    /// </summary>
    /// <param name="gridPosition">�O���b�h�̃}�X�ڂɂ�������W</param>
    /// <returns>����\�ȍ��W�̏ꍇTrue�A�s�̏ꍇ��False</returns>
    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridSystem.IsValidGridPosition(gridPosition);
    }

    /// <summary>
    /// �}�X�ڂ̉��̃T�C�Y��Ԃ�
    /// </summary>
    /// <returns>�}�X�ڂ̉��̃T�C�Y</returns>
    public int GetWidth()
    {
        return gridSystem.GetWidth();
    }

    /// <summary>
    /// �}�X�ڂ̏c�̃T�C�Y��Ԃ�
    /// </summary>
    /// <returns>�}�X�ڂ̏c�̃T�C�Y</returns>
    public int GetHeight()
    {
        return gridSystem.GetHeight();
    }

    /// <summary>
    /// �w��̃O���b�h�Ƀ��j�b�g�����ɑ��݂��Ă��邩�ǂ����𔻒肷��
    /// </summary>
    /// <param name="gridPosition">�O���b�h�̃}�X�ڂɂ�������W</param>
    /// <returns>���Ă���ꍇTrue�A���Ă��Ȃ��ꍇFalse��Ԃ�</returns>
    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.HasAnyUnit();
    }

    /// <summary>
    /// �w��̃O���b�h���W�ɑ��݂��Ă��郆�j�b�g�̏���Ԃ�
    /// </summary>
    /// <param name="gridPosition">�O���b�h�̃}�X�ڂɂ�������W</param>
    /// <returns>�Ώۂ̃��j�b�g</returns>
    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnit();
    }
}
