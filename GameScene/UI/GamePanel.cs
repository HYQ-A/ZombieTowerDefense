using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GamePanel : BasePanel
{
    public UnityEngine.UI.Button backBtn;
    public UnityEngine.UI.Image hpImg;
    public Text hpTex;
    public Text waveTex;
    public Text moneyTex;

    //下方防御塔组合按键父对象 控制显隐
    public Transform bot;

    //血量宽度
    public float hpW = 500;

    //防御塔的组合控件
    public List<TowerBtn> towerBtnList = new List<TowerBtn>();

    //当前进入和选中的造塔点
    private TowerPoint nowSelTowerPoint;
    //是否可造塔
    private bool checkInput;

    public override void Init()
    {
        backBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanle<GamePanel>();
            //清空当前关卡的数据
            GameLevelMgr.Instance.ClearInfo();
            //跳转到开始场景
            SceneManager.LoadScene("BeginScene");
        });

        //一开始就隐藏防御塔
        bot.gameObject.SetActive(false);

        //锁定鼠标
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
    }

    /// <summary>
    /// 更新血量的方法
    /// </summary>
    /// <param name="nowHp"></param>
    /// <param name="maxHp"></param>
    public void UpdateTowerHp(int nowHp,int maxHp)
    {
        hpTex.text = nowHp + "/" + maxHp;
        //更新血条长度
        (hpImg.transform as RectTransform).sizeDelta = new Vector2((float)nowHp / maxHp * hpW, 35);
    }

    /// <summary>
    /// 更新丧尸波数的方法
    /// </summary>
    /// <param name="nowWave"></param>
    /// <param name="maxWave"></param>
    public void UpdateWave(int nowWave,int maxWave)
    {
        waveTex.text = nowWave + "/" + maxWave;
    }

    /// <summary>
    /// 更新拥有的金币数量
    /// </summary>
    /// <param name="money"></param>
    public void UpdateMoney(int money)
    {
        moneyTex.text = money.ToString();
    }

    /// <summary>
    /// 更新当前选中造塔点 界面的一些变化
    /// </summary>
    /// <param name="point"></param>
    public void UpdateSelTower(TowerPoint point)
    {
        nowSelTowerPoint = point;

        if (nowSelTowerPoint == null)
        {
            checkInput = false;
            bot.gameObject.SetActive(false);
        }
        else
        {
            checkInput = true;
            bot.gameObject.SetActive(true);
            if (nowSelTowerPoint.nowTowerInfo == null)
            {
                for (int i = 0; i < towerBtnList.Count; i++)
                {
                    towerBtnList[i].gameObject.SetActive(true);
                    towerBtnList[i].InitInfo(nowSelTowerPoint.chooseIDs[i], "Number" + (i + 1));
                }
            }
            else
            {
                //升级
                for (int i = 0; i < towerBtnList.Count; i++)
                {
                    towerBtnList[i].gameObject.SetActive(false);
                }
                towerBtnList[1].gameObject.SetActive(true);
                towerBtnList[1].InitInfo(nowSelTowerPoint.nowTowerInfo.nextLev, "Space");
            }
        }
    }

    protected override void Update()
    {
        base.Update();

        //不可建造直接返回 不进入下述逻辑
        if (!checkInput)
            return;

        //初始建造
        if (nowSelTowerPoint.nowTowerInfo == null)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.chooseIDs[0]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.chooseIDs[1]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.chooseIDs[2]);
            }
        }
        else
        {
            //升级
            if(Input.GetKeyDown(KeyCode.Space))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.nowTowerInfo.nextLev);
            }
        }
    }
}
