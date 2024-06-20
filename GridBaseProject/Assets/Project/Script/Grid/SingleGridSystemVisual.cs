using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �P�̂ɂ�����O���b�h�̃}�X�̌����ڂ��Ǘ�����N���X
/// </summary>
public class SingleGridSystemVisual : MonoBehaviour
{

    [SerializeField]
    private MeshRenderer meshRenderer;

    /// <summary>
    /// �O���b�h�̃}�X��\������
    /// </summary>
    /// <param name="material"></param>
    public void Show(Material material)
    {
        meshRenderer.enabled = true;
        meshRenderer.material = material;
    }

    /// <summary>
    /// �O���b�h�̃}�X���\���ɂ���
    /// </summary>
    public void Hide()
    {
        meshRenderer.enabled = false;
    }
}
