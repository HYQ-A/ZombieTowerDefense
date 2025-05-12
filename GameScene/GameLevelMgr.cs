using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelMgr
{
    private static GameLevelMgr instance = new GameLevelMgr();
    public static GameLevelMgr Instance => instance;

    public PlayerObj player;

    //���еĳ��ֵ�
    private List<MonsterPoint> points = new List<MonsterPoint>();
    //��¼��ǰ����
    private int nowWave;
    //�����
    private int maxWave;
    ////��¼��ǰ�����ϵĹ�������
    //private int nowMonsterNum;
    //��¼��ǰ�����ϵĹ��� �б�
    private List<MonsterObj> monsterList = new List<MonsterObj>();

    private GameLevelMgr() 
    {
        
    }

    /// <summary>
    /// ��ʼ������ ��̬�������
    /// </summary>
    public void InitInfo(SceneInfo info)
    {
        //��ʾ��Ϸ����
        UIManager.Instance.ShowPanle<GamePanel>();
        //��ҵĴ���
        //��ȡ�������
        RoleInfo roleInfo = GameDataMgr.Instance.nowSelRole;
        //��ȡ��ҵĳ���λ��
        Transform heroPos = GameObject.Find("HeroPos").transform;
        //ʵ�������Ԥ���� ����λ�ã��Ƕ�
        GameObject heroObj = GameObject.Instantiate(Resources.Load<GameObject>(roleInfo.res), heroPos.transform.position, heroPos.transform.rotation);
        //����ҽ��г�ʼ��
        player = heroObj.GetComponent<PlayerObj>();
        //��ʼ����ҵĻ�������
        player.InitPlayerInfo(info.money, roleInfo.atk);
        //������� ����̬�������������
        Camera.main.GetComponent<CameraMove>().SetTarget(heroObj.transform);
        //��ʼ�� ���������Ѫ��
        MainTowerObj.Instance.UpdateHp(info.towerHp, info.towerHp);
    }

    /// <summary>
    /// ���ڼ�¼���յ�ķ���
    /// </summary>
    /// <param name="point"></param>
    public void AddMonsterPoint(MonsterPoint point)
    {
        points.Add(point);
    }

    /// <summary>
    /// ����һ���ж��ٲ���
    /// </summary>
    /// <param name="num"></param>
    public void UpdateMaxNum(int num)
    {
        maxWave += num;
        nowWave = maxWave;
        //���½���
        UIManager.Instance.GetPanel<GamePanel>().UpdateWave(nowWave, maxWave);
    }

    /// <summary>
    /// ���µ�ǰ�����ķ���
    /// </summary>
    /// <param name="num"></param>
    public void ChangeNowWaveNum(int num)
    {
        nowWave -= num;
        //���½���
        UIManager.Instance.GetPanel<GamePanel>().UpdateWave(nowWave, maxWave);
    }

    /// <summary>
    /// �Ƿ�ʤ��
    /// </summary>
    public bool CheckOver()
    {
        for (int i = 0; i < points.Count; i++)
        {
            //�Ƿ������ û����δʤ��
            if (!points[i].CheckOver())
                return false;
        }

        //�����ϻ���ɥʬ����ʱδʤ��
        if (monsterList.Count > 0)
            return false;

        Debug.Log("��Ϸʤ��");
        return true;
    }

    ///// <summary>
    ///// �ı䵱ǰ�����Ϲ��������
    ///// </summary>
    ///// <param name="num"></param>
    //public void ChangeMonsterNum(int num)
    //{
    //    nowMonsterNum += num;
    //}

    /// <summary>
    /// ��¼���ﵽ�б���
    /// </summary>
    /// <param name="monsterObj"></param>
    public void AddMonster(MonsterObj monsterObj)
    {
        monsterList.Add(monsterObj);
    }

    /// <summary>
    /// ��������б����Ƴ�
    /// </summary>
    /// <param name="monsterObj"></param>
    public void RemoveMonster(MonsterObj monsterObj)
    {
        monsterList.Remove(monsterObj);
    }

    /// <summary>
    /// Ѱ�����������ĵ���ɥʬ����
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public MonsterObj FindMonster(Vector3 pos, int range)
    {
        //�ڹ����б���Ѱ������������ɥʬ����
        for (int i = 0; i < monsterList.Count; i++)
        {
            if (!monsterList[i].isDead && Vector3.Distance(pos, monsterList[i].transform.position)<=range)
                return monsterList[i];
        }

        return null;
    }

    /// <summary>
    /// �ҵ���������������ɥʬ����
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
    /// ��յ�ǰ�ؿ����� ����Ӱ��֮���ٽ���ϷʱӰ��
    /// </summary>
    public void ClearInfo()
    {
        points.Clear();
        monsterList.Clear();
        nowWave = maxWave = 0;
        player = null;
    }

}
