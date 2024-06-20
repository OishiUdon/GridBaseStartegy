using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGMを管理するクラス
/// </summary>
public class BGMManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip BGMClip = null;

    [SerializeField]
    private float fadeOutTime = 0.5f;

    private AudioSource audioSource = null;

    private float fadeTime = 0.0f;
    private float defaultVol = 1.0f;

    private bool isEnd = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        fadeTime = 0.0f;
        defaultVol = audioSource.volume;

        isEnd = false;

        audioSource.clip = BGMClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void Update()
    {
        if (!isEnd)
        {
            return;
        }
        FadeOutBGM();
    }

    /// <summary>
    /// BGMの音量を徐々に小さくする
    /// </summary>
    private void FadeOutBGM()
    {
        fadeTime += Time.deltaTime;
        if (fadeTime >= fadeOutTime)
        {
            fadeTime = fadeOutTime;
        }
        audioSource.volume = (float)(defaultVol - fadeTime / fadeOutTime);
        if (audioSource.volume < 0)
        {
            audioSource.volume = 0;
        }
    }

    /// <summary>
    /// シーン遷移の際に呼び出す
    /// </summary>
    public void OnEndScene()
    {
        isEnd = true;
    }
}
