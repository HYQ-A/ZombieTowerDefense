using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseScenePanel : BasePanel
{
    public Button startBtn;
    public Button backBtn;
    public Button leftBtn;
    public Button rightBtn;
    public Text scribTex;
    public Image sceneImg;

    //当前场景的索引值
    private int nowIndex;
    //当前场景数据
    private SceneInfo nowSceneInfo;

    public override void Init()
    {
        startBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanle<ChooseScenePanel>();
            //异步加载场景
            AsyncOperation ao = SceneManager.LoadSceneAsync(nowSceneInfo.sceneName);
            //异步加载完成后 关卡初始化 
            ao.completed += (obj) =>
            {
                GameLevelMgr.Instance.InitInfo(nowSceneInfo);
            };
        });

        backBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanle<ChooseScenePanel>();
            UIManager.Instance.ShowPanle<ChooseHeroPanel>();
        });

        leftBtn.onClick.AddListener(() =>
        {
            --nowIndex;
            if (nowIndex < 0)
                nowIndex = GameDataMgr.Instance.sceneInfoList.Count - 1;
            //更新场景数据
            ChangeScne();
        });

        rightBtn.onClick.AddListener(() =>
        {
            ++nowIndex;
            if (nowIndex >= GameDataMgr.Instance.sceneInfoList.Count)
                nowIndex = 0;
            //更新场景数据
            ChangeScne();
        });

        //一开始就显示场景有关的数据
        ChangeScne();
    }

    /// <summary>
    /// 改变场景数据显示的方法
    /// </summary>
    public void ChangeScne()
    {
        nowSceneInfo = GameDataMgr.Instance.sceneInfoList[nowIndex];
        //改变图片
        sceneImg.sprite = Resources.Load<Sprite>(nowSceneInfo.imgRes);
        //改变描述
        scribTex.text = "Name:\n" + nowSceneInfo.name + "\n" + "Description:\n" + nowSceneInfo.tips;
    }
}
