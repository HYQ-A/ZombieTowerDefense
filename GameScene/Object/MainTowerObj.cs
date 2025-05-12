using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerObj : MonoBehaviour
{
    //血量相关
    private int hp;
    private int maxHp;
    //是否死亡
    private bool isDead;
    //写成单例模式方便外部获取
    private static MainTowerObj instance;
    public static MainTowerObj Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 更新血量的方法
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="maxHp"></param>
    public void UpdateHp(int hp,int maxHp)
    {
        this.hp = hp;
        this.maxHp = maxHp;

        //界面上的血量也更新
        UIManager.Instance.GetPanel<GamePanel>().UpdateTowerHp(hp, maxHp);
    }

    /// <summary>
    /// 塔受伤的方法
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="maxHp"></param>
    public void Wound(int dmg)
    {
        //如果塔已经死亡 则直接返回
        if (isDead) return;
        //受伤逻辑
        hp -= dmg;
        //死亡逻辑
        if (hp <= 0)
        {
            isDead = true;
            hp = 0;
            //游戏结束
            UIManager.Instance.ShowPanle<GameOverPanel>().InitInfo(50, false);
        }
        //更新血量
        UpdateHp(hp, maxHp);
    }

    /// <summary>
    /// 过场景时将塔的instance至为空 更安全wwd
    /// </summary>
    private void OnDestroy()
    {
        instance = null;
    }
}
