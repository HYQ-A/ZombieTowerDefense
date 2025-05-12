using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChooseHeroPanel : BasePanel
{
    public Button leftBtn;
    public Button rightBtn;
    public Button startBtn;
    public Button backBtn;
    public Button unlockBtn;
    public Text unlockTex;
    public Text moneyTex;
    public Text heroNameTex;

    //角色位置
    public Transform heroPos;
    //当前角色数据
    private RoleInfo nowRoleData;
    //当前角色索引值
    private int nowIndex;
    //当前角色
    private GameObject heroObj;

    public override void Init()
    {
        //一开始就更新金币信息
        moneyTex.text = GameDataMgr.Instance.playerData.hadMoney.ToString();
        //找到设置玩家的位置
        heroPos = GameObject.Find("HeroPos").transform;

        ChangeHero();

        leftBtn.onClick.AddListener(() =>
        {
            --nowIndex;
            if (nowIndex < 0)
                nowIndex = GameDataMgr.Instance.roleInfoList.Count - 1;

            //更新角色
            ChangeHero();
        });

        rightBtn.onClick.AddListener(() =>
        {
            ++nowIndex;
            if (nowIndex >= GameDataMgr.Instance.roleInfoList.Count)
                nowIndex = 0;

            //更新角色
            ChangeHero();
        });

        startBtn.onClick.AddListener(() =>
        {
            //记录当前选择的角色
            GameDataMgr.Instance.nowSelRole = nowRoleData;
            UIManager.Instance.HidePanle<ChooseHeroPanel>();
            //显示选择场景的面板
            UIManager.Instance.ShowPanle<ChooseScenePanel>();
        });

        backBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanle<ChooseHeroPanel>();
            Camera.main.GetComponent<CameraAnimator>().TurnRight(() =>
            {
                UIManager.Instance.ShowPanle<BeginPanel>();
            });
        });

        unlockBtn.onClick.AddListener(() =>
        {
            //点击解锁按钮的逻辑
            PlayerData data = GameDataMgr.Instance.playerData;
            if (data.hadMoney >= nowRoleData.lockMoney)
            {
                data.hadMoney -= nowRoleData.lockMoney;
                //更新界面显示
                moneyTex.text = data.hadMoney.ToString();
                //记录购买的id
                data.hadHero.Add(nowRoleData.id);
                //保存数据
                GameDataMgr.Instance.SavePlayerData();
                //更新解锁按钮
                UpdateUnlockBtn();
                //显示提示内容
                UIManager.Instance.ShowPanle<TipPanel>().ChangeInfo("Successful Purchase");
            }
            else
            {
                UIManager.Instance.ShowPanle<TipPanel>().ChangeInfo("Failed Purchase");
            }

        });
    }

    /// <summary>
    /// 更新角色
    /// </summary>
    public void ChangeHero()
    {
        //先删除场景上的角色 再改变
        if (heroObj != null)
        {
            DestroyImmediate(heroObj);
            heroObj = null;
        }

        nowRoleData = GameDataMgr.Instance.roleInfoList[nowIndex];
        //实例化角色
        heroObj = Instantiate(Resources.Load<GameObject>(nowRoleData.res), heroPos.transform.position, heroPos.rotation);
        //更新角色名字
        heroNameTex.text = nowRoleData.tips;
        //由于对象身上挂载了PlayerObj脚本 此处不适用 则移除
        Destroy(heroObj.GetComponent<PlayerObj>());
        //调用更新解锁按钮的方法
        UpdateUnlockBtn();
    }

    /// <summary>
    /// 更新解锁按钮的方法
    /// </summary>
    public void UpdateUnlockBtn()
    {
        //如果需要的金额大于0 且未曾购买拥有角色则显示购买按钮 隐藏开始按钮
        if (nowRoleData.lockMoney > 0 && !GameDataMgr.Instance.playerData.hadHero.Contains(nowRoleData.id))
        {
            unlockBtn.gameObject.SetActive(true);
            startBtn.gameObject.SetActive(false);
            //更新需要购买的金额
            unlockTex.text = "￥" + nowRoleData.lockMoney;
        }
        else
        {
            //显示开始按钮 隐藏购买按钮
            unlockBtn.gameObject.SetActive(false);
            startBtn.gameObject.SetActive(true);
        }
    }

    public override void HideMe(UnityAction callBack)
    {
        base.HideMe(callBack);
        if (heroObj != null)
        {
            DestroyImmediate(heroObj.gameObject);
            heroObj = null;
        }
    }
}
