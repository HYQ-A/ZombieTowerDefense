using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 防御塔对象
/// </summary>
public class TowerObj : MonoBehaviour
{
    //开火点
    public Transform gunPos;
    //塔的头部位置
    public Transform head;
    //旋转速度
    private float roundSpeed = 20;
    //防御塔数据
    private TowerInfo towerInfo;
    //攻击对象
    private MonsterObj targetObj;
    //当前时间
    private float nowTime;
    //用于记录怪物位置
    private Vector3 monsterPos;
    //当前要攻击的目标们
    private List<MonsterObj> targetObjs;

    /// <summary>
    /// 初始化炮台数据
    /// </summary>
    /// <param name="towerInfo"></param>
    public void InitInfo(TowerInfo towerInfo)
    {
        this.towerInfo = towerInfo;
    }

    // Update is called once per frame
    void Update()
    {
        //第一种攻击方式
        if (towerInfo.atkType == 1)
        {
            if(targetObj==null||targetObj.isDead||Vector3.Distance(this.transform.position,targetObj.transform.position)>towerInfo.atkRange)
            {
                targetObj = GameLevelMgr.Instance.FindMonster(this.transform.position, towerInfo.atkRange);
            }

            //如果未找到攻击对象 直接返回 炮台不应该旋转
            if (targetObj == null)
                return;

            //得到怪物位置 并设置好y轴坐标 同高
            monsterPos = targetObj.transform.position;
            monsterPos.y = head.position.y;
            //转动炮台头部 旋转
            head.rotation = Quaternion.Slerp(head.rotation, Quaternion.LookRotation(monsterPos - head.position), roundSpeed * Time.deltaTime);

            if (Vector3.Angle(head.forward, monsterPos - head.position) < 5 && Time.time - nowTime >= towerInfo.offsetTime)
            {
                //怪物受伤
                targetObj.Wound(towerInfo.atk);
                //播放音效
                GameDataMgr.Instance.PlaySound("Music/Music/Tower");
                //特效
                GameObject obj=GameObject.Instantiate(Resources.Load<GameObject>(towerInfo.eff),gunPos.transform.position,gunPos.transform.rotation); ;
                Destroy(obj, 0.2f);
                nowTime = Time.time;
            }
        }
        else
        {
            targetObjs = GameLevelMgr.Instance.FindMonsters(this.transform.position, towerInfo.atkRange);

            if(targetObjs.Count>0&&Time.time-nowTime>=towerInfo.offsetTime)
            {
                //特效
                GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>(towerInfo.eff), this.transform.position, this.transform.rotation); ;
                Destroy(obj, 0.2f);
                //怪物受伤
                for (int i = 0; i < targetObjs.Count; i++)
                {
                    targetObjs[i].Wound(towerInfo.atk);
                }
                nowTime = Time.time;
            }
        }
    }
}
