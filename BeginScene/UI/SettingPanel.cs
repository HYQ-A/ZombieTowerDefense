using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Button closeBtn;
    public Toggle musicTog;
    public Toggle soundTog;
    public Slider musicSlider;
    public Slider soundSlider;

    public override void Init()
    {
        //һ��ʼ�ͳ�ʼ�������������
        MusicData data = GameDataMgr.Instance.musicData;
        musicTog.isOn = data.musicIsOpen;
        soundTog.isOn = data.soundIsOpen;
        musicSlider.value = data.musicValue;
        soundSlider.value = data.soundValue;


        closeBtn.onClick.AddListener(() =>
        {
            //Ϊ�˽�ʡ���� ֻ�ڹر��������ʱ�洢�޸ĺ������
            GameDataMgr.Instance.SaveMusicData();
            UIManager.Instance.HidePanle<SettingPanel>();
        });

        musicTog.onValueChanged.AddListener((isOpen) =>
        {
            //�ñ������ֽ��п���
            BkMusic.Instance.SetIsOpen(isOpen);
            //��¼����
            GameDataMgr.Instance.musicData.musicIsOpen = isOpen;
        });

        soundTog.onValueChanged.AddListener((isOpne) =>
        {
            //��¼����
            GameDataMgr.Instance.musicData.soundIsOpen = isOpne;
        });

        musicSlider.onValueChanged.AddListener((value) =>
        {
            //�ı����ִ�С
            BkMusic.Instance.ChangeValue(value);
            //��¼����
            GameDataMgr.Instance.musicData.musicValue = value;
        });

        soundSlider.onValueChanged.AddListener((value) =>
        {
            //��¼����
            GameDataMgr.Instance.musicData.soundValue = value;
        });

    }
}
