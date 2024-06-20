using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �^�[�����̃V�X�e�����Ǘ�����N���X
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
    /// ���̃^�[���ւƈڍs����
    /// </summary>
    public void NextTurn()
    {
        turnNumber++;
        isPlayerTurn = !isPlayerTurn;

        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// ���݂̃^�[������Ԃ�
    /// </summary>
    /// <returns>���݂̃^�[����</returns>
    public int GetTurnNumber()
    {
        return turnNumber;
    }

    /// <summary>
    /// ���݂��v���C���[�̃^�[�����ǂ�����Ԃ�
    /// </summary>
    /// <returns>�v���C���[�̃^�[���̏ꍇTrue�A�����ł͂Ȃ��ꍇFalse��Ԃ�</returns>
    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}
