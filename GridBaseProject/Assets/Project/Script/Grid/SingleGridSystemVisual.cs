using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 単体におけるグリッドのマスの見た目を管理するクラス
/// </summary>
public class SingleGridSystemVisual : MonoBehaviour
{

    [SerializeField]
    private MeshRenderer meshRenderer;

    /// <summary>
    /// グリッドのマスを表示する
    /// </summary>
    /// <param name="material"></param>
    public void Show(Material material)
    {
        meshRenderer.enabled = true;
        meshRenderer.material = material;
    }

    /// <summary>
    /// グリッドのマスを非表示にする
    /// </summary>
    public void Hide()
    {
        meshRenderer.enabled = false;
    }
}
