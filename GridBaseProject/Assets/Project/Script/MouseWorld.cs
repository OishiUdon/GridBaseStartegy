using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���[���h��ɂ�����}�E�X���W�Ɋւ���N���X
/// </summary>
public class MouseWorld : MonoBehaviour
{
    private static MouseWorld instance;

    [SerializeField]
    private LayerMask mousePlaneLayerMask;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        transform.position = MouseWorld.GetPosition();
    }

    /// <summary>
    /// �}�E�X�̈ʒu���擾����
    /// </summary>
    /// <returns>�}�E�X���W�̈ʒu</returns>
    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask);
        return raycastHit.point;
    }
}
