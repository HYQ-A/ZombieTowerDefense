using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelMgr
{
    private static GameLevelMgr instance = new GameLevelMgr();
    public static GameLevelMgr Instance => instance;

    public PlayerObj player;

    //所有的出怪点
    private List<MonsterPoint> points = new List<MonsterPoint>();
    //记录当前波数
    private int nowWave;
    //最大波数
    private int maxWave;
    ////记录当前场景上的怪物数量
    //private int nowMonsterNum;
    //记录当前场景上的怪物 列表
    private List<MonsterObj> monsterList = new List<MonsterObj>();

    private GameLevelMgr() 
    {
        
    }

    /// <summary>
    /// 初始化方法 动态创建玩家
    /// </summary>
    public void InitInfo(SceneInfo info)
    {
        //显示游戏界面
        UIManager.Instance.ShowPanle<GamePanel>();
        //玩家的创建
        //获取玩家数据
        RoleInfo roleInfo = GameDataMgr.Instance.nowSelRole;
        //获取玩家的出生位置
        Transform heroPos = GameObject.Find("HeroPos").transform;
        //实例化玩家预设体 包括位置，角度
        GameObject heroObj = GameObject.Instantiate(Resources.Load<GameObject>(roleInfo.res), heroPos.transform.position, heroPos.transform.rotation);
        //对玩家进行初始化
        player = heroObj.GetComponent<PlayerObj>();
        //初始化玩家的基本属性
        player.InitPlayerInfo(info.money, roleInfo.atk);
        //让摄像机 看向动态创建出来的玩家
        Camera.main.GetComponent<CameraMove>().SetTarget(heroObj.transform);
        //初始化 保护区域的血量
        MainTowerObj.Instance.UpdateHp(info.towerHp, info.towerHp);
    }

    /// <summary>
    /// 用于记录出拐点的方法
    /// </summary>
    /// <param name="point"></param>
    public void AddMonsterPoint(MonsterPoint point)
    {
        points.Add(point);
    }

    /// <summary>
    /// 更新一共有多少波怪
    /// </summary>
    /// <param name="num"></param>
    public void UpdateMaxNum(int num)
    {
        maxWave += num;
        nowWave = maxWave;
        //更新界面
        UIManager.Instance.GetPanel<GamePanel>().UpdateWave(nowWave, maxWave);
    }

    /// <summary>
    /// 更新当前波数的方法
    /// </summary>
    /// <param name="num"></param>
    public void ChangeNowWaveNum(int num)
    {
        nowWave -= num;
        //更新界面
        UIManager.Instance.GetPanel<GamePanel>().UpdateWave(nowWave, maxWave);
    }

    /// <summary>
    /// 是否胜利
    /// </summary>
    public bool CheckOver()
    {
        for (int i = 0; i < points.Count; i++)
        {
            //是否出怪完 没有则还未胜利
            if (!points[i].CheckOver())
                return false;
        }

        //场景上还有丧尸怪物时未胜利
        if (monsterList.Count > 0)
            return false;

        Debug.Log("游戏胜利");
        return true;
    }

    ///// <summary>
    ///// 改变当前场景上怪物的数量
    ///// </summary>
    ///// <param name="num"></param>
    //public void ChangeMonsterNum(int num)
    //{
    //    nowMonsterNum += num;
    //}

    /// <summary>
    /// 记录怪物到列表中
    /// </summary>
    /// <param name="monsterObj"></param>
    public void AddMonster(MonsterObj monsterObj)
    {
        monsterList.Add(monsterObj);
    }

    /// <summary>
    /// 将怪物从列表中移除
    /// </summary>
    /// <param name="monsterObj"></param>
    public void RemoveMonster(MonsterObj monsterObj)
    {
        monsterList.Remove(monsterObj);
    }

    /// <summary>
    /// 寻找满足条件的单个丧尸怪物
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public MonsterObj FindMonster(Vector3 pos, int range)
    {
        //在怪物列表中寻找满足条件的丧尸怪物
        for (int i = 0; i < monsterList.Count; i++)
        {
            if (!monsterList[i].isDead && Vector3.Distance(pos, monsterList[i].transform.position)<=range)
                return monsterList[i];
        }

        return null;
    }

    /// <summary>
    /// 找到所有满足条件的丧尸怪物
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public List<MonsterObj> FindMonsters(Vector3 pos, int range)
    {
        List<MonsterObj> monsterObjs = new List<MonsterObj>();
        for (int i = 0;i < monsterList.Count;i++)
        {
            if (!monsterList[i].isDead && Vector3.Distance(pos, monsterList[i].transform.position) <= range)
                monsterObjs.Add(monsterList[i]);
        }

        return monsterObjs;
    }

    /// <summary>
    /// 清空当前关卡数据 避免影响之后再进游戏时影响
    /// </summary>
    public void ClearInfo()
    {
        points.Clear();
        monsterList.Clear();
        nowWave = maxWave = 0;
        player = null;
    }

}
