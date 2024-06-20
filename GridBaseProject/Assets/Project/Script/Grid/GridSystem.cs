using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 座標や位置をマス目状のグリッドで管理するクラス
/// </summary>
/// <typeparam name="TGridObject">グリッドオブジェクトのクラス</typeparam>
public class GridSystem<TGridObject>
{
    private int width;
    private int height;
    private float cellSize;
    private TGridObject[,] gridObjectArray;

    public GridSystem(int width, int height, float cellSize, Func<GridSystem<TGridObject>, GridPosition, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObjectArray = new TGridObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectArray[x, z] = createGridObject(this, gridPosition);
            }
        }
    }

    /// <summary>
    /// 指定したグリッド上の位置におけるWorldPositionを返す
    /// </summary>
    /// <param name="gridPosition">グリッドにおける座標</param>
    /// <returns>対応する位置のWorldPosition</returns>
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    /// <summary>
    /// 指定したワールド上の位置におけるGridPositionを返す
    /// </summary>
    /// <param name="worldPosition">ワールドにおける座標</param>
    /// <returns>対応する位置のGridPosition</returns>
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
        );
    }

    /// <summary>
    /// 指定されたグリッド上の位置におけるGridObjectを返す
    /// </summary>
    /// <param name="gridPosition">グリッドのマス目における座標</param>
    /// <returns>対応する位置のGridObject</returns>
    public TGridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }

    /// <summary>
    /// 指定したグリッドの座標が判定可能かどうかを判定する
    /// </summary>
    /// <param name="gridPosition">グリッドのマス目における座標</param>
    /// <returns>判定可能な座標の場合True、不可の場合はFalse</returns>
    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridPosition.x >= 0 &&
                gridPosition.z >= 0 &&
                gridPosition.x < width &&
                gridPosition.z < height;
    }

    /// <summary>
    /// マス目の横のサイズを返す
    /// </summary>
    /// <returns>マス目の横のサイズ</returns>
    public int GetWidth()
    {
        return width;
    }

    /// <summary>
    /// マス目の縦のサイズを返す
    /// </summary>
    /// <returns>マス目の縦のサイズ</returns>
    public int GetHeight()
    {
        return height;
    }
}
