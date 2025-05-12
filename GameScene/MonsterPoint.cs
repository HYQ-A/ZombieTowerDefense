using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoint : MonoBehaviour
{
    //ɥʬ����
    public int maxWave;
    //ÿ��ɥʬ����
    public int monsterNumOneWave;
    //���ڼ�¼ ��ǰ������ɥʬ���ж���
    private int nowNum;
    //ɥʬID �漴������ͬ��ɥʬ
    public List<int> monsterIDs;
    //���ڼ�¼ ��ǰ���� ����ʲôID��ɥʬ
    private int nowID;
    //��ֻɥʬ�������ʱ��
    public float createOffsetTime;
    //���벨֮��ļ��ʱ��
    public float delayTime;
    //��һ��ɥʬ�����ļ��ʱ��
    public float firstDelayTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("CreateWave", firstDelayTime);
        //��¼���ֵ�
        GameLevelMgr.Instance.AddMonsterPoint(this);
        //���������
        GameLevelMgr.Instance.UpdateMaxNum(maxWave);
    }

    /// <summary>
    /// ��ʼ����һ����ɥʬ
    /// </summary>
    private void CreateWave()
    {
        nowID = monsterIDs[Random.Range(0, monsterIDs.Count)];
        nowNum = monsterNumOneWave;
        //����ɥʬ
        CreateMonster();
        //���ٲ���
        --maxWave;
        //֪ͨ�ؿ������� ���³��ֲ���
        GameLevelMgr.Instance.ChangeNowWaveNum(1);
    }

    /// <summary>
    /// ����ÿ��ɥʬ
    /// </summary>
    private void CreateMonster()
    {
        MonsterInfo info = GameDataMgr.Instance.monsterInfoList[nowID - 1];
        //����ɥʬԤ����
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.res), this.transform.position, Quaternion.identity);
        //���ɥʬ�ű� ��ʼ��
        MonsterObj monsterObj = obj.AddComponent<MonsterObj>();
        monsterObj.InitInfo(info);

        //���߹�������� ����������1
        //GameLevelMgr.Instance.ChangeMonsterNum(1);
        //��������ӵ��б���
        GameLevelMgr.Instance.AddMonster(monsterObj);

        //������һֻɥʧ�� ��ȥҪ������ɥʬ����1
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
    /// �ṩ���ⲿ�Ƿ���ɳ����߼�
    /// </summary>
    /// <returns></returns>
    public bool CheckOver()
    {
        return nowNum == 0 && maxWave == 0;
    }
}
