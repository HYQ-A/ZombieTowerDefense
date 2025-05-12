using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

/// <summary>
/// ������
/// </summary>
public class TowerPoint : MonoBehaviour
{
    //����������� ������
    private GameObject towerObj = null;
    //����������� ��������
    public TowerInfo nowTowerInfo = null;
    //���Խ������������ID
    public List<int> chooseIDs;

    /// <summary>
    /// �������ķ���
    /// </summary>
    /// <param name="id"></param>
    public void CreateTower(int id)
    {
        TowerInfo towerInfo = GameDataMgr.Instance.towerInfoList[id - 1];
        //�Ƚ�Ǯ������
        if (towerInfo.money > GameLevelMgr.Instance.player.money)
            return;
        //��Ǯ
        GameLevelMgr.Instance.player.AddMoney(-towerInfo.money);
        //���ж��Ƿ��Ѿ�������
        if (towerObj != null)
        {
            Destroy(towerObj);
            towerObj = null;
        }
        //ʵ������
        towerObj = Instantiate(Resources.Load<GameObject>(towerInfo.res), this.transform.position, Quaternion.identity);
        //��ʼ��������Ϣ
        towerObj.GetComponent<TowerObj>().InitInfo(towerInfo);
        //��¼��������
        nowTowerInfo = towerInfo;
        //���½�������
        if (nowTowerInfo.nextLev != 0)
        {
            UIManager.Instance.GetPanel<GamePanel>().UpdateSelTower(this);
        }
        else
        {
            UIManager.Instance.GetPanel<GamePanel>().UpdateSelTower(null);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //�Զ������ �����Ϊ��߼�����ʾ ���Ľ���������
        if (nowTowerInfo != null && nowTowerInfo.nextLev == 0)
            return;

        UIManager.Instance.GetPanel<GamePanel>().UpdateSelTower(this);
    }

    private void OnTriggerExit(Collider other)
    {
        //��ϣ����ʾ�򴫿�
        UIManager.Instance.GetPanel<GamePanel>().UpdateSelTower(null);
    }
}
