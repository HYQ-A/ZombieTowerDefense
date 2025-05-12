using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BkMusic : MonoBehaviour
{
    private static BkMusic instance;
    public static BkMusic Instance => instance;
    private AudioSource musicSource;

    private void Awake()
    {
        instance = this;

        musicSource = this.GetComponent<AudioSource>();
        MusicData data = GameDataMgr.Instance.musicData;
        //设置音乐音效的开启
        SetIsOpen(data.musicIsOpen);
        //设置音乐音效的大小
        ChangeValue(data.musicValue);
    }

    /// <summary>
    /// 改变音乐音效是否开启的方法
    /// </summary>
    /// <param name="isOpen"></param>
    public void SetIsOpen(bool isOpen)
    {
        musicSource.mute = !isOpen;
    }

    /// <summary>
    /// 改变音乐音效大小的方法
    /// </summary>
    /// <param name="value"></param>
    public void ChangeValue(float value)
    {
        musicSource.volume = value;
    }
}
