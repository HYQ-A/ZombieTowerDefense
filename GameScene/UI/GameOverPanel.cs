using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    public Button sureBtn;
    public Text stateTex;
    public Text getMoneyTex;
    public Text moneyTex;

    public override void Init()
    {
        sureBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanle<GameOverPanel>();
            UIManager.Instance.HidePanle<GamePanel>();
            //清空当前关卡的数据
            GameLevelMgr.Instance.ClearInfo();
            SceneManager.LoadScene("BeginScene");
        });
    }

    /// <summary>
    /// 提供给外部初始化数据的方法
    /// </summary>
    /// <param name="money"></param>
    /// <param name="isWin"></param>
    public void InitInfo(int money,bool isWin)
    {
        stateTex.text = isWin ? "Win" : "Failed";
        getMoneyTex.text = isWin ? "Your Get Win Money" : "Your Get Failed Money";
        moneyTex.text = "￥" + money;
        GameDataMgr.Instance.playerData.hadMoney += money;
        GameDataMgr.Instance.SavePlayerData();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //解锁鼠标
        Cursor.lockState = CursorLockMode.None;
    }
}
