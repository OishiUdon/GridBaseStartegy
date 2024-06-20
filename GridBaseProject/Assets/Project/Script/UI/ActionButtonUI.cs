using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アクションを実行するボタンのUIを管理するクラス
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
    /// 元のアクションを設定する
    /// </summary>
    /// <param name="baseAction">元のアクション</param>
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
    /// 選択した時の見た目を更新する
    /// </summary>
    public void UpdateSelectedVisual()
    {
        BaseAction selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();
        selectedGameObject.SetActive(selectedBaseAction == baseAction);
    }
}
