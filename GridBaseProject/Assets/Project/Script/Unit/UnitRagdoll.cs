using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットのラグドールを管理するクラス
/// </summary>
public class UnitRagdoll : MonoBehaviour
{
    [SerializeField]
    private Transform ragdollRootBone;

    [SerializeField]
    private float explosionForce = 300f;

    [SerializeField]
    private float explosionRange = 10.0f;

    /// <summary>
    /// ラグドールの設定を行う
    /// </summary>
    /// <param name="originalRootBone">元となるオブジェクトのボーン</param>
    public void Setup(Transform originalRootBone)
    {
        MatchAllChildTransforms(originalRootBone, ragdollRootBone);
        ApplyExplosionToRagdoll(ragdollRootBone, explosionForce, transform.position, explosionRange);
    }

    /// <summary>
    /// すべてのボーンの位置を元の位置と合わせる処理を行う
    /// </summary>
    /// <param name="root">元となるボーンの位置</param>
    /// <param name="clone">複製するボーンの位置</param>
    private void MatchAllChildTransforms(Transform root,Transform clone)
    {
        foreach(Transform child in root)
        {
            Transform cloneChild = clone.Find(child.name);
            if(cloneChild != null)
            {
                cloneChild.position = child.position;
                cloneChild.rotation = child.rotation;

                MatchAllChildTransforms(child, cloneChild);
            }
        }
    }

    /// <summary>
    /// ラグドールを生成した際に少し跳ねる処理を行う
    /// </summary>
    /// <param name="root">元となるボーンの位置</param>
    /// <param name="explosionForce">跳ねさせる力</param>
    /// <param name="explosionPosition">跳ねる位置</param>
    /// <param name="explosionRange">跳ねる範囲</param>
    private void ApplyExplosionToRagdoll(Transform root, float explosionForce, Vector3 explosionPosition, float explosionRange)
    {
        foreach (Transform child in root)
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidBody))
            {
                childRigidBody.AddExplosionForce(explosionForce, explosionPosition, explosionRange);
            }
            ApplyExplosionToRagdoll(child, explosionForce, explosionPosition, explosionRange);
        }
    }
}
