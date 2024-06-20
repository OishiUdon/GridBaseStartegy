using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �A�N�V���������s����{�^����UI���Ǘ�����N���X
/// </summary>
public class ActionButtonUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    [SerializeField]
    private Button button;

    [SerializeField]
    private GameObject selectedGameObject;


    private BaseAction baseAction;

    /// <summary>
    /// ���̃A�N�V������ݒ肷��
    /// </summary>
    /// <param name="baseAction">���̃A�N�V����</param>
    public void SetBaseAction(BaseAction baseAction)
    {
        this.baseAction = baseAction;
        textMeshPro.text = baseAction.GetActionName().ToUpper();
        button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(baseAction);
        });
    }

    /// <summary>
    /// �I���������̌����ڂ��X�V����
    /// </summary>
    public void UpdateSelectedVisual()
    {
        BaseAction selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();
        selectedGameObject.SetActive(selectedBaseAction == baseAction);
    }
}
