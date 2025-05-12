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

    //��ɫλ��
    public Transform heroPos;
    //��ǰ��ɫ����
    private RoleInfo nowRoleData;
    //��ǰ��ɫ����ֵ
    private int nowIndex;
    //��ǰ��ɫ
    private GameObject heroObj;

    public override void Init()
    {
        //һ��ʼ�͸��½����Ϣ
        moneyTex.text = GameDataMgr.Instance.playerData.hadMoney.ToString();
        //�ҵ�������ҵ�λ��
        heroPos = GameObject.Find("HeroPos").transform;

        ChangeHero();

        leftBtn.onClick.AddListener(() =>
        {
            --nowIndex;
            if (nowIndex < 0)
                nowIndex = GameDataMgr.Instance.roleInfoList.Count - 1;

            //���½�ɫ
            ChangeHero();
        });

        rightBtn.onClick.AddListener(() =>
        {
            ++nowIndex;
            if (nowIndex >= GameDataMgr.Instance.roleInfoList.Count)
                nowIndex = 0;

            //���½�ɫ
            ChangeHero();
        });

        startBtn.onClick.AddListener(() =>
        {
            //��¼��ǰѡ��Ľ�ɫ
            GameDataMgr.Instance.nowSelRole = nowRoleData;
            UIManager.Instance.HidePanle<ChooseHeroPanel>();
            //��ʾѡ�񳡾������
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
            //���������ť���߼�
            PlayerData data = GameDataMgr.Instance.playerData;
            if (data.hadMoney >= nowRoleData.lockMoney)
            {
                data.hadMoney -= nowRoleData.lockMoney;
                //���½�����ʾ
                moneyTex.text = data.hadMoney.ToString();
                //��¼�����id
                data.hadHero.Add(nowRoleData.id);
                //��������
                GameDataMgr.Instance.SavePlayerData();
                //���½�����ť
                UpdateUnlockBtn();
                //��ʾ��ʾ����
                UIManager.Instance.ShowPanle<TipPanel>().ChangeInfo("Successful Purchase");
            }
            else
            {
                UIManager.Instance.ShowPanle<TipPanel>().ChangeInfo("Failed Purchase");
            }

        });
    }

    /// <summary>
    /// ���½�ɫ
    /// </summary>
    public void ChangeHero()
    {
        //��ɾ�������ϵĽ�ɫ �ٸı�
        if (heroObj != null)
        {
            DestroyImmediate(heroObj);
            heroObj = null;
        }

        nowRoleData = GameDataMgr.Instance.roleInfoList[nowIndex];
        //ʵ������ɫ
        heroObj = Instantiate(Resources.Load<GameObject>(nowRoleData.res), heroPos.transform.position, heroPos.rotation);
        //���½�ɫ����
        heroNameTex.text = nowRoleData.tips;
        //���ڶ������Ϲ�����PlayerObj�ű� �˴������� ���Ƴ�
        Destroy(heroObj.GetComponent<PlayerObj>());
        //���ø��½�����ť�ķ���
        UpdateUnlockBtn();
    }

    /// <summary>
    /// ���½�����ť�ķ���
    /// </summary>
    public void UpdateUnlockBtn()
    {
        //�����Ҫ�Ľ�����0 ��δ������ӵ�н�ɫ����ʾ����ť ���ؿ�ʼ��ť
        if (nowRoleData.lockMoney > 0 && !GameDataMgr.Instance.playerData.hadHero.Contains(nowRoleData.id))
        {
            unlockBtn.gameObject.SetActive(true);
            startBtn.gameObject.SetActive(false);
            //������Ҫ����Ľ��
            unlockTex.text = "��" + nowRoleData.lockMoney;
        }
        else
        {
            //��ʾ��ʼ��ť ���ع���ť
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
