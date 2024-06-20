using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ワールド上におけるマウス座標に関するクラス
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
    /// マウスの位置を取得する
    /// </summary>
    /// <returns>マウス座標の位置</returns>
    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask);
        return raycastHit.point;
    }
}
