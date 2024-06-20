using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// �^�C�g����ʂ̑J�ڂɊւ���N���X
/// </summary>
public class TitleSceneMove : MonoBehaviour
{
    [SerializeField]
    private float fadeAnimationTime = 0.5f;

    [SerializeField]
    private Animator fadeImageAnimator = null;

    [SerializeField]
    private BGMManager bgmManager = null;

    private float fadeTime = 0.0f;
    private bool isClick = false;

    private void Update()
    {
        if (!isClick)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                isClick = true;
                fadeImageAnimator.SetTrigger("FadeOut");
                bgmManager.OnEndScene();
            }
            return;
        }

        OnLoadGameScene();
    }

    /// <summary>
    /// GameScene�����[�h����
    /// </summary>
    private void OnLoadGameScene()
    {
        fadeTime += Time.deltaTime;

        if(fadeTime >= fadeAnimationTime)
        {
            SceneManager.LoadScene("InGameScene");
            fadeTime = 0.0f;
        }
    }
}
