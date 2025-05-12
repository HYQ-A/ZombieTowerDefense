using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ����������
/// </summary>
public class TowerObj : MonoBehaviour
{
    //�����
    public Transform gunPos;
    //����ͷ��λ��
    public Transform head;
    //��ת�ٶ�
    private float roundSpeed = 20;
    //����������
    private TowerInfo towerInfo;
    //��������
    private MonsterObj targetObj;
    //��ǰʱ��
    private float nowTime;
    //���ڼ�¼����λ��
    private Vector3 monsterPos;
    //��ǰҪ������Ŀ����
    private List<MonsterObj> targetObjs;

    /// <summary>
    /// ��ʼ����̨����
    /// </summary>
    /// <param name="towerInfo"></param>
    public void InitInfo(TowerInfo towerInfo)
    {
        this.towerInfo = towerInfo;
    }

    // Update is called once per frame
    void Update()
    {
        //��һ�ֹ�����ʽ
        if (towerInfo.atkType == 1)
        {
            if(targetObj==null||targetObj.isDead||Vector3.Distance(this.transform.position,targetObj.transform.position)>towerInfo.atkRange)
            {
                targetObj = GameLevelMgr.Instance.FindMonster(this.transform.position, towerInfo.atkRange);
            }

            //���δ�ҵ��������� ֱ�ӷ��� ��̨��Ӧ����ת
            if (targetObj == null)
                return;

            //�õ�����λ�� �����ú�y������ ͬ��
            monsterPos = targetObj.transform.position;
            monsterPos.y = head.position.y;
            //ת����̨ͷ�� ��ת
            head.rotation = Quaternion.Slerp(head.rotation, Quaternion.LookRotation(monsterPos - head.position), roundSpeed * Time.deltaTime);

            if (Vector3.Angle(head.forward, monsterPos - head.position) < 5 && Time.time - nowTime >= towerInfo.offsetTime)
            {
                //��������
                targetObj.Wound(towerInfo.atk);
                //������Ч
                GameDataMgr.Instance.PlaySound("Music/Music/Tower");
                //��Ч
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
                //��Ч
                GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>(towerInfo.eff), this.transform.position, this.transform.rotation); ;
                Destroy(obj, 0.2f);
                //��������
                for (int i = 0; i < targetObjs.Count; i++)
                {
                    targetObjs[i].Wound(towerInfo.atk);
                }
                nowTime = Time.time;
            }
        }
    }
}
