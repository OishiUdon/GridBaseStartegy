using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �^�[�����̃V�X�e���Ɋւ���UI���Ǘ�����N���X
/// </summary>
public class TurnSystemUI : MonoBehaviour
{
    [SerializeField]
    private Button endTurnButton;

    [SerializeField]
    private TextMeshProUGUI turnNumberText;

    [SerializeField]
    private GameObject enemyTurnVisualGameObject;

    private void Start()
    {
        endTurnButton.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
        });

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        UpdateTurnText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisibility();
    }

    private void TurnSystem_OnTurnChanged(object sender,EventArgs e)
    {
        UpdateTurnText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisibility();
    }

    /// <summary>
    /// �^�[���\���̃e�L�X�g���X�V����
    /// </summary>
    private void UpdateTurnText()
    {
        turnNumberText.text = "TURN " + TurnSystem.Instance.GetTurnNumber();
    }

    /// <summary>
    /// �G���̃^�[���Ƃ������Ƃ�\��UI�̕\����ݒ肷��
    /// </summary>
    private void UpdateEnemyTurnVisual()
    {
        enemyTurnVisualGameObject.SetActive(!TurnSystem.Instance.IsPlayerTurn());
    }

    /// <summary>
    /// �^�[�����I������{�^����UI�̕\����ݒ肷��
    /// </summary>
    private void UpdateEndTurnButtonVisibility()
    {
        endTurnButton.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
    }
}