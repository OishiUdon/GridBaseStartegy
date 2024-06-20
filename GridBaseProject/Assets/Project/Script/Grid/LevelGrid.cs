using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// グリッドのレベルを設定・管理するクラス
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
    /// 指定のグリッドにユニットの情報を加える
    /// </summary>
    /// <param name="gridPosition">グリッドにおける座標</param>
    /// <param name="unit">対象のユニット</param>
    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }

    /// <summary>
    /// 指定のグリッドにユニットリストの情報を加える
    /// </summary>
    /// <param name="gridPosition">グリッドにおける座標</param>
    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnitList();
    }

    /// <summary>
    /// 指定のグリッドにユニットの情報を除く
    /// </summary>
    /// <param name="gridPosition">グリッドにおける座標</param>
    /// <param name="unit">対象のユニット</param>
    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    /// <summary>
    /// ユニットが移動した際のグリッドの処理を行う
    /// </summary>
    /// <param name="unit">対象のユニット</param>
    /// <param name="fromGridPosition">グリッド上で移動を開始した座標</param>
    /// <param name="toGridPosition">グリッド上で移動を終了する座標</param>
    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition, unit);
        OnAnyUnitMovedGridPosiiton?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// 指定したワールド上の位置におけるGridPositionを返す
    /// </summary>
    /// <param name="worldPosition">ワールドにおける座標</param>
    /// <returns>対応する位置のGridPosition</returns>
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return gridSystem.GetGridPosition(worldPosition);
    }

    /// <summary>
    /// 指定したグリッド上の位置におけるWorldPositionを返す
    /// </summary>
    /// <param name="gridPosition">グリッドにおける座標</param>
    /// <returns>対応する位置のWorldPosition</returns>
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return gridSystem.GetWorldPosition(gridPosition);
    }

    /// <summary>
    /// 指定したグリッドの座標が判定可能かどうかを判定する
    /// </summary>
    /// <param name="gridPosition">グリッドのマス目における座標</param>
    /// <returns>判定可能な座標の場合True、不可の場合はFalse</returns>
    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridSystem.IsValidGridPosition(gridPosition);
    }

    /// <summary>
    /// マス目の横のサイズを返す
    /// </summary>
    /// <returns>マス目の横のサイズ</returns>
    public int GetWidth()
    {
        return gridSystem.GetWidth();
    }

    /// <summary>
    /// マス目の縦のサイズを返す
    /// </summary>
    /// <returns>マス目の縦のサイズ</returns>
    public int GetHeight()
    {
        return gridSystem.GetHeight();
    }

    /// <summary>
    /// 指定のグリッドにユニットが既に存在しているかどうかを判定する
    /// </summary>
    /// <param name="gridPosition">グリッドのマス目における座標</param>
    /// <returns>している場合True、していない場合Falseを返す</returns>
    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.HasAnyUnit();
    }

    /// <summary>
    /// 指定のグリッド座標に存在しているユニットの情報を返す
    /// </summary>
    /// <param name="gridPosition">グリッドのマス目における座標</param>
    /// <returns>対象のユニット</returns>
    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnit();
    }
}
