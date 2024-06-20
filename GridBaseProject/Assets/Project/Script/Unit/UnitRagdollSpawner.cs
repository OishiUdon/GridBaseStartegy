using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットのラグドールを生成するクラス
/// </summary>
public class UnitRagdollSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform raddollPrefab;

    [SerializeField]
    private Transform originalRootBone;

    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead(object sender,EventArgs e)
    {
        Transform ragdollTransform = Instantiate(raddollPrefab, transform.position, transform.rotation);
        UnitRagdoll unitRagdoll = ragdollTransform.GetComponent<UnitRagdoll>();
        unitRagdoll.Setup(originalRootBone);
    }
}
