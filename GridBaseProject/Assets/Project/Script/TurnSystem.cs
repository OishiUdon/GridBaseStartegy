using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ターン制のシステムを管理するクラス
/// </summary>
public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }

    public event EventHandler OnTurnChanged;

    private int turnNumber = 1;
    private bool isPlayerTurn = true;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one TurnSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    /// <summary>
    /// 次のターンへと移行する
    /// </summary>
    public void NextTurn()
    {
        turnNumber++;
        isPlayerTurn = !isPlayerTurn;

        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// 現在のターン数を返す
    /// </summary>
    /// <returns>現在のターン数</returns>
    public int GetTurnNumber()
    {
        return turnNumber;
    }

    /// <summary>
    /// 現在がプレイヤーのターンかどうかを返す
    /// </summary>
    /// <returns>プレイヤーのターンの場合True、そうではない場合Falseを返す</returns>
    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}
