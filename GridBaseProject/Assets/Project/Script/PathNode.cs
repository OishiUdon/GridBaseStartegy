using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 経路における地点を管理するクラス
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
    /// Gコストを返す
    /// </summary>
    /// <returns>開始地点から現在地点までかかったコスト(移動コスト)</returns>
    public int GetGCost()
    {
        return gCost;
    }

    /// <summary>
    /// Hコストを返す
    /// </summary>
    /// <returns>現在位置から終了地点までかかるであろうコスト(ヒューリスティック距離)</returns>
    public int GetHCost()
    {
        return hCost;
    }

    /// <summary>
    /// Fコストを返す
    /// </summary>
    /// <returns>トータルコスト</returns>
    public int GetFCost()
    {
        return fCost;
    }

    /// <summary>
    /// Gコストを設定する
    /// </summary>
    /// <param name="gCost">移動コスト</param>
    public void SetGCost(int gCost)
    {
        this.gCost = gCost;
    }

    /// <summary>
    /// Hコストを設定する
    /// </summary>
    /// <param name="hCost">現在位置から終了地点までかかるであろうコスト</param>
    public void SetHCost(int hCost)
    {
        this.hCost = hCost;
    }

    /// <summary>
    /// Fコストを計算する
    /// </summary>
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    /// <summary>
    /// 直前の経路地点をリセットする
    /// </summary>
    public void ResetCameFromPathNode()
    {
        cameFromPathNode = null;
    }

    /// <summary>
    /// 直前の経路地点を設定する
    /// </summary>
    /// <param name="pathNode">経路地点</param>
    public void SetCameFromPathNode(PathNode pathNode)
    {
        cameFromPathNode = pathNode;
    }

    /// <summary>
    /// どの経路地点から来たのかを返す
    /// </summary>
    /// <returns>直前の経路地点</returns>
    public PathNode GetCameFromPathNode()
    {
        return cameFromPathNode;
    }

    /// <summary>
    /// 経路地点のグリッド座標を返す
    /// </summary>
    /// <returns>グリッド上における座標</returns>
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    /// <summary>
    /// 歩行可能かどうかを返す
    /// </summary>
    /// <returns>歩行可能な場合True、不可な場合Falseを返す</returns>
    public bool IsWalkable()
    {
        return isWalkable;
    }

    /// <summary>
    /// 歩行可能かどうかを設定する
    /// </summary>
    /// <param name="isWalkable">歩行可能かどうか</param>
    public void SetIsWalkable(bool isWalkable)
    {
        this.isWalkable = isWalkable;
    }

}
