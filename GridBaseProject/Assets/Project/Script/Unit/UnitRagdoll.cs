using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���j�b�g�̃��O�h�[�����Ǘ�����N���X
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
    /// ���O�h�[���̐ݒ���s��
    /// </summary>
    /// <param name="originalRootBone">���ƂȂ�I�u�W�F�N�g�̃{�[��</param>
    public void Setup(Transform originalRootBone)
    {
        MatchAllChildTransforms(originalRootBone, ragdollRootBone);
        ApplyExplosionToRagdoll(ragdollRootBone, explosionForce, transform.position, explosionRange);
    }

    /// <summary>
    /// ���ׂẴ{�[���̈ʒu�����̈ʒu�ƍ��킹�鏈�����s��
    /// </summary>
    /// <param name="root">���ƂȂ�{�[���̈ʒu</param>
    /// <param name="clone">��������{�[���̈ʒu</param>
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
    /// ���O�h�[���𐶐������ۂɏ������˂鏈�����s��
    /// </summary>
    /// <param name="root">���ƂȂ�{�[���̈ʒu</param>
    /// <param name="explosionForce">���˂������</param>
    /// <param name="explosionPosition">���˂�ʒu</param>
    /// <param name="explosionRange">���˂�͈�</param>
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
