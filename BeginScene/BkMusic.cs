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
        //����������Ч�Ŀ���
        SetIsOpen(data.musicIsOpen);
        //����������Ч�Ĵ�С
        ChangeValue(data.musicValue);
    }

    /// <summary>
    /// �ı�������Ч�Ƿ����ķ���
    /// </summary>
    /// <param name="isOpen"></param>
    public void SetIsOpen(bool isOpen)
    {
        musicSource.mute = !isOpen;
    }

    /// <summary>
    /// �ı�������Ч��С�ķ���
    /// </summary>
    /// <param name="value"></param>
    public void ChangeValue(float value)
    {
        musicSource.volume = value;
    }
}
