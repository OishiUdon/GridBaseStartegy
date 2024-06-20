using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���[���h���W��ɔz�u���郆�j�b�g��UI���Ǘ�����N���X
/// </summary>
public class UnitWorldUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI actionPointsText;

    [SerializeField]
    private Unit unit;

    [SerializeField]
    private Image healthBarImage;

    [SerializeField]
    private HealthSystem healthSystem;

    private void Start()
    {
        Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        UpdateActionPointsText();
    }

    /// <summary>
    /// �A�N�V�����|�C���g��\���e�L�X�g���X�V����
    /// </summary>
    private void UpdateActionPointsText()
    {
        actionPointsText.text = unit.GetActionPoints().ToString();
    }

    private void Unit_OnAnyActionPointsChanged(object sender,EventArgs e)
    {
        UpdateActionPointsText();
    }

    /// <summary>
    /// ���j�b�g��HP��\���o�[���X�V����
    /// </summary>
    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = healthSystem.GetHealthNormalized();
    }

    private void HealthSystem_OnDamaged(object sender,EventArgs e)
    {
        UpdateHealthBar();
    }

}
