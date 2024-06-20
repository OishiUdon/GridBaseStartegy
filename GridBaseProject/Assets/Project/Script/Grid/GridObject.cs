using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �O���b�h�̃}�X�̃I�u�W�F�N�g��ݒ肷��N���X
/// </summary>
public class GridObject
{
    private GridSystem<GridObject> gridSystem;
    private GridPosition gridPosition;
    private List<Unit> unitList;

    public GridObject(GridSystem<GridObject> gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        unitList = new List<Unit>();
    }

    public override string ToString()
    {
        string unitString = "";
        foreach (Unit unit in unitList)
        {
            unitString += unit + "\n";
        }

        return gridPosition.ToString() + "\n" + unitString;
    }

    /// <summary>
    /// ���j�b�g�̃��X�g�Ƀ��j�b�g�̏���������
    /// </summary>
    /// <param name="unit">�Ώۂ̃��j�b�g</param>
    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    /// <summary>
    /// ���j�b�g�̃��X�g���烆�j�b�g�̏�������
    /// </summary>
    /// <param name="unit">�Ώۂ̃��j�b�g</param>
    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
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
    /// ���̃O���b�h�Ƀ��j�b�g�����݂��邩�ǂ����𔻒肷��
    /// </summary>
    /// <returns>���݂���ꍇTrue�A���݂��Ȃ��ꍇFalse</returns>
    public bool HasAnyUnit()
    {
        return unitList.Count > 0;
    }

    /// <summary>
    /// ���j�b�g�����݂���ꍇ�A���̃��j�b�g�̏���Ԃ�
    /// </summary>
    /// <returns>�Ώۂ̃��j�b�g</returns>
    public Unit GetUnit()
    {
        if (HasAnyUnit())
        {
            return unitList[0];
        }
        else
        {
            return null;
        }
    }
}
