using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// グリッドのマスのオブジェクトを設定するクラス
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
    /// ユニットのリストにユニットの情報を加える
    /// </summary>
    /// <param name="unit">対象のユニット</param>
    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    /// <summary>
    /// ユニットのリストからユニットの情報を除く
    /// </summary>
    /// <param name="unit">対象のユニット</param>
    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
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
    /// このグリッドにユニットが存在するかどうかを判定する
    /// </summary>
    /// <returns>存在する場合True、存在しない場合False</returns>
    public bool HasAnyUnit()
    {
        return unitList.Count > 0;
    }

    /// <summary>
    /// ユニットが存在する場合、そのユニットの情報を返す
    /// </summary>
    /// <returns>対象のユニット</returns>
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
