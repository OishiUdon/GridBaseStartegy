using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���W��ʒu���}�X�ڏ�̃O���b�h�ŊǗ�����N���X
/// </summary>
/// <typeparam name="TGridObject">�O���b�h�I�u�W�F�N�g�̃N���X</typeparam>
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
    /// �w�肵���O���b�h��̈ʒu�ɂ�����WorldPosition��Ԃ�
    /// </summary>
    /// <param name="gridPosition">�O���b�h�ɂ�������W</param>
    /// <returns>�Ή�����ʒu��WorldPosition</returns>
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    /// <summary>
    /// �w�肵�����[���h��̈ʒu�ɂ�����GridPosition��Ԃ�
    /// </summary>
    /// <param name="worldPosition">���[���h�ɂ�������W</param>
    /// <returns>�Ή�����ʒu��GridPosition</returns>
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
        );
    }

    /// <summary>
    /// �w�肳�ꂽ�O���b�h��̈ʒu�ɂ�����GridObject��Ԃ�
    /// </summary>
    /// <param name="gridPosition">�O���b�h�̃}�X�ڂɂ�������W</param>
    /// <returns>�Ή�����ʒu��GridObject</returns>
    public TGridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }

    /// <summary>
    /// �w�肵���O���b�h�̍��W������\���ǂ����𔻒肷��
    /// </summary>
    /// <param name="gridPosition">�O���b�h�̃}�X�ڂɂ�������W</param>
    /// <returns>����\�ȍ��W�̏ꍇTrue�A�s�̏ꍇ��False</returns>
    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridPosition.x >= 0 &&
                gridPosition.z >= 0 &&
                gridPosition.x < width &&
                gridPosition.z < height;
    }

    /// <summary>
    /// �}�X�ڂ̉��̃T�C�Y��Ԃ�
    /// </summary>
    /// <returns>�}�X�ڂ̉��̃T�C�Y</returns>
    public int GetWidth()
    {
        return width;
    }

    /// <summary>
    /// �}�X�ڂ̏c�̃T�C�Y��Ԃ�
    /// </summary>
    /// <returns>�}�X�ڂ̏c�̃T�C�Y</returns>
    public int GetHeight()
    {
        return height;
    }
}
