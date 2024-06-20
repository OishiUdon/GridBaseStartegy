using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ƒQ[ƒ€‚ÌŸ‚¿•‰‚¯‚ğ”»’è‚·‚éƒNƒ‰ƒX
/// </summary>
public class JudgeSystem : MonoBehaviour
{
    [SerializeField]
    private Animator fadeImageAnimator = null;

    [SerializeField]
    private GameObject finishUI = null;

    [SerializeField]
    private GameObject playerWinUI = null;

    [SerializeField]
    private GameObject playerLoseUI = null;

    [SerializeField]
    private float sceneMoveTimeLimit = 3.5f;

    [SerializeField]
    private float sceneFadeTime = 0.5f;

    private float sceneMoveTime = 0;
    private bool isFinish;


    private void Start()
    {
        isFinish = false;
        finishUI.SetActive(false);
        playerWinUI.SetActive(false);
        playerLoseUI.SetActive(false);

        UnitManager.OnAllFriendlyUnitDead += UnitManager_OnAllFriendlyUnitDead;
        UnitManager.OnAllEnemyUnitDead += UnitManager_OnAllEnemyUnitDead;
    }

    private void Update()
    {
        if (!isFinish)
        {
            return;
        }

        sceneMoveTime += Time.deltaTime;

        if(sceneMoveTime >= sceneMoveTimeLimit - sceneFadeTime)
        {
            fadeImageAnimator.SetTrigger("FadeOut");
        }
        if (sceneMoveTime >= sceneMoveTimeLimit)
        {
            SceneManager.LoadScene("TitleScene");
            sceneMoveTime = 0.0f;
        }
    }


    private void UnitManager_OnAllEnemyUnitDead(object sender, EventArgs e)
    {
        finishUI.SetActive(true);
        playerWinUI.SetActive(true);
        isFinish = true;
    }

    private void UnitManager_OnAllFriendlyUnitDead(object sender, EventArgs e)
    {
        finishUI.SetActive(true);
        playerLoseUI.SetActive(true);
        isFinish = true;
    }

}
