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

    //��ǰ����������ֵ
    private int nowIndex;
    //��ǰ��������
    private SceneInfo nowSceneInfo;

    public override void Init()
    {
        startBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanle<ChooseScenePanel>();
            //�첽���س���
            AsyncOperation ao = SceneManager.LoadSceneAsync(nowSceneInfo.sceneName);
            //�첽������ɺ� �ؿ���ʼ�� 
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
            //���³�������
            ChangeScne();
        });

        rightBtn.onClick.AddListener(() =>
        {
            ++nowIndex;
            if (nowIndex >= GameDataMgr.Instance.sceneInfoList.Count)
                nowIndex = 0;
            //���³�������
            ChangeScne();
        });

        //һ��ʼ����ʾ�����йص�����
        ChangeScne();
    }

    /// <summary>
    /// �ı䳡��������ʾ�ķ���
    /// </summary>
    public void ChangeScne()
    {
        nowSceneInfo = GameDataMgr.Instance.sceneInfoList[nowIndex];
        //�ı�ͼƬ
        sceneImg.sprite = Resources.Load<Sprite>(nowSceneInfo.imgRes);
        //�ı�����
        scribTex.text = "Name:\n" + nowSceneInfo.name + "\n" + "Description:\n" + nowSceneInfo.tips;
    }
}
