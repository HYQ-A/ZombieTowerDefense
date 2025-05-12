using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

/// <summary>
/// 造塔点
/// </summary>
public class TowerPoint : MonoBehaviour
{
    //造塔点关联的 塔对象
    private GameObject towerObj = null;
    //造塔点关联的 塔的数据
    public TowerInfo nowTowerInfo = null;
    //可以建造的三个塔的ID
    public List<int> chooseIDs;

    /// <summary>
    /// 创建塔的方法
    /// </summary>
    /// <param name="id"></param>
    public void CreateTower(int id)
    {
        TowerInfo towerInfo = GameDataMgr.Instance.towerInfoList[id - 1];
        //比较钱够不够
        if (towerInfo.money > GameLevelMgr.Instance.player.money)
            return;
        //扣钱
        GameLevelMgr.Instance.player.AddMoney(-towerInfo.money);
        //先判断是否已经建造塔
        if (towerObj != null)
        {
            Destroy(towerObj);
            towerObj = null;
        }
        //实例化塔
        towerObj = Instantiate(Resources.Load<GameObject>(towerInfo.res), this.transform.position, Quaternion.identity);
        //初始化塔的信息
        towerObj.GetComponent<TowerObj>().InitInfo(towerInfo);
        //记录塔的数据
        nowTowerInfo = towerInfo;
        //更新建塔界面
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
        //自定义规则 如果塔为最高级则不显示 塔的建造与升级
        if (nowTowerInfo != null && nowTowerInfo.nextLev == 0)
            return;

        UIManager.Instance.GetPanel<GamePanel>().UpdateSelTower(this);
    }

    private void OnTriggerExit(Collider other)
    {
        //不希望显示则传空
        UIManager.Instance.GetPanel<GamePanel>().UpdateSelTower(null);
    }
}
