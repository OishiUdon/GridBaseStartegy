using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�N�V�������s����UI���Ǘ�����N���X
/// </summary>
public class ActionBusyUI : MonoBehaviour
{
    private void Start()
    {
        UnitActionSystem.Instance.OnBusyChanged += UnitActionSystem_OnBusyChanged;
        Hide();
    }

    /// <summary>
    /// UI��\������
    /// </summary>
    private void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// UI���\���ɂ���
    /// </summary>
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UnitActionSystem_OnBusyChanged(object sender, bool isBusy)
    {
        if (isBusy)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
