using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˂����e�̌����ڂ��Ǘ�����N���X
/// </summary>
public class BulletProjectile : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 200.0f;

    [SerializeField]
    private TrailRenderer trailRenderer;

    [SerializeField]
    private Transform bulletHitVfxPrefab;

    private Vector3 targetPosition;

    /// <summary>
    /// �����ݒ���s��
    /// </summary>
    /// <param name="targetPosition">�ˌ��̑Ώۂ̈ʒu</param>
    public void Setup(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    private void Update()
    {
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        float distanceBeforeMoving = Vector3.Distance(transform.position, targetPosition);
        transform.position = moveDirection * moveSpeed * Time.deltaTime;
        float distanceAfterMoving = Vector3.Distance(transform.position, targetPosition);

        if (distanceBeforeMoving < distanceAfterMoving)
        {
            transform.position = targetPosition;
            trailRenderer.transform.parent = null;

            Destroy(gameObject);

            Instantiate(bulletHitVfxPrefab, targetPosition, Quaternion.identity);
        }
    }

}
