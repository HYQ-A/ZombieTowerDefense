using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoint : MonoBehaviour
{
    //丧尸波数
    public int maxWave;
    //每波丧尸个数
    public int monsterNumOneWave;
    //用于记录 当前波数的丧尸还有多少
    private int nowNum;
    //丧尸ID 随即创建不同的丧尸
    public List<int> monsterIDs;
    //用于记录 当前波数 创建什么ID的丧尸
    private int nowID;
    //单只丧尸创建间隔时间
    public float createOffsetTime;
    //波与波之间的间隔时间
    public float delayTime;
    //第一波丧尸创建的间隔时间
    public float firstDelayTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("CreateWave", firstDelayTime);
        //记录出怪点
        GameLevelMgr.Instance.AddMonsterPoint(this);
        //更新最大波数
        GameLevelMgr.Instance.UpdateMaxNum(maxWave);
    }

    /// <summary>
    /// 开始创建一波的丧尸
    /// </summary>
    private void CreateWave()
    {
        nowID = monsterIDs[Random.Range(0, monsterIDs.Count)];
        nowNum = monsterNumOneWave;
        //创建丧尸
        CreateMonster();
        //减少波数
        --maxWave;
        //通知关卡管理器 更新出怪波数
        GameLevelMgr.Instance.ChangeNowWaveNum(1);
    }

    /// <summary>
    /// 创建每波丧尸
    /// </summary>
    private void CreateMonster()
    {
        MonsterInfo info = GameDataMgr.Instance.monsterInfoList[nowID - 1];
        //创建丧尸预设体
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.res), this.transform.position, Quaternion.identity);
        //添加丧尸脚本 初始化
        MonsterObj monsterObj = obj.AddComponent<MonsterObj>();
        monsterObj.InitInfo(info);

        //告诉怪物管理器 怪物数量加1
        //GameLevelMgr.Instance.ChangeMonsterNum(1);
        //将怪物添加到列表中
        GameLevelMgr.Instance.AddMonster(monsterObj);

        //创建完一只丧失后 减去要创建的丧尸数量1
        --nowNum;
        if(nowNum == 0)
        {
            if (maxWave > 0)
                Invoke("CreateWave", delayTime);
        }
        else
        {
            Invoke("CreateMonster", createOffsetTime);
        }
    }

    /// <summary>
    /// 提供给外部是否完成出怪逻辑
    /// </summary>
    /// <returns></returns>
    public bool CheckOver()
    {
        return nowNum == 0 && maxWave == 0;
    }
}
