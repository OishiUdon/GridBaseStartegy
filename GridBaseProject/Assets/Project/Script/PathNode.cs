using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �o�H�ɂ�����n�_���Ǘ�����N���X
/// </summary>
public class PathNode
{
    private GridPosition gridPosition;
    private int gCost;
    private int hCost;
    private int fCost;
    private PathNode cameFromPathNode;
    private bool isWalkable = true;


    public PathNode(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }

    /// <summary>
    /// G�R�X�g��Ԃ�
    /// </summary>
    /// <returns>�J�n�n�_���猻�ݒn�_�܂ł��������R�X�g(�ړ��R�X�g)</returns>
    public int GetGCost()
    {
        return gCost;
    }

    /// <summary>
    /// H�R�X�g��Ԃ�
    /// </summary>
    /// <returns>���݈ʒu����I���n�_�܂ł�����ł��낤�R�X�g(�q���[���X�e�B�b�N����)</returns>
    public int GetHCost()
    {
        return hCost;
    }

    /// <summary>
    /// F�R�X�g��Ԃ�
    /// </summary>
    /// <returns>�g�[�^���R�X�g</returns>
    public int GetFCost()
    {
        return fCost;
    }

    /// <summary>
    /// G�R�X�g��ݒ肷��
    /// </summary>
    /// <param name="gCost">�ړ��R�X�g</param>
    public void SetGCost(int gCost)
    {
        this.gCost = gCost;
    }

    /// <summary>
    /// H�R�X�g��ݒ肷��
    /// </summary>
    /// <param name="hCost">���݈ʒu����I���n�_�܂ł�����ł��낤�R�X�g</param>
    public void SetHCost(int hCost)
    {
        this.hCost = hCost;
    }

    /// <summary>
    /// F�R�X�g���v�Z����
    /// </summary>
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    /// <summary>
    /// ���O�̌o�H�n�_�����Z�b�g����
    /// </summary>
    public void ResetCameFromPathNode()
    {
        cameFromPathNode = null;
    }

    /// <summary>
    /// ���O�̌o�H�n�_��ݒ肷��
    /// </summary>
    /// <param name="pathNode">�o�H�n�_</param>
    public void SetCameFromPathNode(PathNode pathNode)
    {
        cameFromPathNode = pathNode;
    }

    /// <summary>
    /// �ǂ̌o�H�n�_���痈���̂���Ԃ�
    /// </summary>
    /// <returns>���O�̌o�H�n�_</returns>
    public PathNode GetCameFromPathNode()
    {
        return cameFromPathNode;
    }

    /// <summary>
    /// �o�H�n�_�̃O���b�h���W��Ԃ�
    /// </summary>
    /// <returns>�O���b�h��ɂ�������W</returns>
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    /// <summary>
    /// ���s�\���ǂ�����Ԃ�
    /// </summary>
    /// <returns>���s�\�ȏꍇTrue�A�s�ȏꍇFalse��Ԃ�</returns>
    public bool IsWalkable()
    {
        return isWalkable;
    }

    /// <summary>
    /// ���s�\���ǂ�����ݒ肷��
    /// </summary>
    /// <param name="isWalkable">���s�\���ǂ���</param>
    public void SetIsWalkable(bool isWalkable)
    {
        this.isWalkable = isWalkable;
    }

}
