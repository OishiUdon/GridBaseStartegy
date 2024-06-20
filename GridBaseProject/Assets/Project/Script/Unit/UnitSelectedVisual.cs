using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���j�b�g��I�������ۂ̌����ڂ��Ǘ�����N���X
/// </summary>
public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField]
    private Unit unit = null;

    private MeshRenderer meshRenderer = null;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UpdateVisual();
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs empty)
    {
        UpdateVisual();
    }

    /// <summary>
    /// �\�����X�V����
    /// </summary>
    private void UpdateVisual()
    {
        if (UnitActionSystem.Instance.GetSelectedUnit() == unit)
        {
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }

    private void OnDestroy()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged -= UnitActionSystem_OnSelectedUnitChanged;
    }
}
