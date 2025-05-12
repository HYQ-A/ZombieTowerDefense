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
        //一开始就初始化设置面板数据
        MusicData data = GameDataMgr.Instance.musicData;
        musicTog.isOn = data.musicIsOpen;
        soundTog.isOn = data.soundIsOpen;
        musicSlider.value = data.musicValue;
        soundSlider.value = data.soundValue;


        closeBtn.onClick.AddListener(() =>
        {
            //为了节省性能 只在关闭设置面板时存储修改后的数据
            GameDataMgr.Instance.SaveMusicData();
            UIManager.Instance.HidePanle<SettingPanel>();
        });

        musicTog.onValueChanged.AddListener((isOpen) =>
        {
            //让背景音乐进行开关
            BkMusic.Instance.SetIsOpen(isOpen);
            //记录数据
            GameDataMgr.Instance.musicData.musicIsOpen = isOpen;
        });

        soundTog.onValueChanged.AddListener((isOpne) =>
        {
            //记录数据
            GameDataMgr.Instance.musicData.soundIsOpen = isOpne;
        });

        musicSlider.onValueChanged.AddListener((value) =>
        {
            //改变音乐大小
            BkMusic.Instance.ChangeValue(value);
            //记录数据
            GameDataMgr.Instance.musicData.musicValue = value;
        });

        soundSlider.onValueChanged.AddListener((value) =>
        {
            //记录数据
            GameDataMgr.Instance.musicData.soundValue = value;
        });

    }
}
