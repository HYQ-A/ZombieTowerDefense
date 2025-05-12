using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button beginBtn;
    public Button settingBtn;
    public Button aboutBtn;
    public Button exitBtn;

    public override void Init()
    {
        beginBtn.onClick.AddListener(() =>
        {
            Camera.main.GetComponent<CameraAnimator>().TurnLeft(() =>
            {
                UIManager.Instance.ShowPanle<ChooseHeroPanel>();
            });

            UIManager.Instance.HidePanle<BeginPanel>();
        });

        settingBtn.onClick.AddListener(() =>
        {

            UIManager.Instance.ShowPanle<SettingPanel>();
        });

        aboutBtn.onClick.AddListener(() =>
        {

        });

        exitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
