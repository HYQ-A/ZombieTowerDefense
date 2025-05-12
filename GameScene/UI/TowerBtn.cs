using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��Ͽؼ� ��Ҫ�������ǿ��� ������� UI�ĸ����߼�
/// </summary>
public class TowerBtn : MonoBehaviour
{
    public Image towerImg;
    public Text tipTex;
    public Text moneyTex;

    /// <summary>
    /// ��ʼ�� ��ť��Ϣ�ķ���
    /// </summary>
    /// <param name="id"></param>
    /// <param name="inputStr"></param>
    public void InitInfo(int id,string inputStr)
    {
        TowerInfo info = GameDataMgr.Instance.towerInfoList[id - 1];
        towerImg.sprite = Resources.Load<Sprite>(info.imgRes);
        tipTex.text = inputStr;
        moneyTex.text = "��" + info.money;
        //�ж� Ǯ������
        if (info.money > GameLevelMgr.Instance.player.money)
            moneyTex.text = "Lack Money";
    }
}
