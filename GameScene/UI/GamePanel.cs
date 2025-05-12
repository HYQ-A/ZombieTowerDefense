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

    //�·���������ϰ��������� ��������
    public Transform bot;

    //Ѫ�����
    public float hpW = 500;

    //����������Ͽؼ�
    public List<TowerBtn> towerBtnList = new List<TowerBtn>();

    //��ǰ�����ѡ�е�������
    private TowerPoint nowSelTowerPoint;
    //�Ƿ������
    private bool checkInput;

    public override void Init()
    {
        backBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanle<GamePanel>();
            //��յ�ǰ�ؿ�������
            GameLevelMgr.Instance.ClearInfo();
            //��ת����ʼ����
            SceneManager.LoadScene("BeginScene");
        });

        //һ��ʼ�����ط�����
        bot.gameObject.SetActive(false);

        //�������
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
    }

    /// <summary>
    /// ����Ѫ���ķ���
    /// </summary>
    /// <param name="nowHp"></param>
    /// <param name="maxHp"></param>
    public void UpdateTowerHp(int nowHp,int maxHp)
    {
        hpTex.text = nowHp + "/" + maxHp;
        //����Ѫ������
        (hpImg.transform as RectTransform).sizeDelta = new Vector2((float)nowHp / maxHp * hpW, 35);
    }

    /// <summary>
    /// ����ɥʬ�����ķ���
    /// </summary>
    /// <param name="nowWave"></param>
    /// <param name="maxWave"></param>
    public void UpdateWave(int nowWave,int maxWave)
    {
        waveTex.text = nowWave + "/" + maxWave;
    }

    /// <summary>
    /// ����ӵ�еĽ������
    /// </summary>
    /// <param name="money"></param>
    public void UpdateMoney(int money)
    {
        moneyTex.text = money.ToString();
    }

    /// <summary>
    /// ���µ�ǰѡ�������� �����һЩ�仯
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
                //����
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

        //���ɽ���ֱ�ӷ��� �����������߼�
        if (!checkInput)
            return;

        //��ʼ����
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
            //����
            if(Input.GetKeyDown(KeyCode.Space))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.nowTowerInfo.nextLev);
            }
        }
    }
}
