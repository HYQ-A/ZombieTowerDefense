using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerObj : MonoBehaviour
{
    //Ѫ�����
    private int hp;
    private int maxHp;
    //�Ƿ�����
    private bool isDead;
    //д�ɵ���ģʽ�����ⲿ��ȡ
    private static MainTowerObj instance;
    public static MainTowerObj Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// ����Ѫ���ķ���
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="maxHp"></param>
    public void UpdateHp(int hp,int maxHp)
    {
        this.hp = hp;
        this.maxHp = maxHp;

        //�����ϵ�Ѫ��Ҳ����
        UIManager.Instance.GetPanel<GamePanel>().UpdateTowerHp(hp, maxHp);
    }

    /// <summary>
    /// �����˵ķ���
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="maxHp"></param>
    public void Wound(int dmg)
    {
        //������Ѿ����� ��ֱ�ӷ���
        if (isDead) return;
        //�����߼�
        hp -= dmg;
        //�����߼�
        if (hp <= 0)
        {
            isDead = true;
            hp = 0;
            //��Ϸ����
            UIManager.Instance.ShowPanle<GameOverPanel>().InitInfo(50, false);
        }
        //����Ѫ��
        UpdateHp(hp, maxHp);
    }

    /// <summary>
    /// ������ʱ������instance��Ϊ�� ����ȫwwd
    /// </summary>
    private void OnDestroy()
    {
        instance = null;
    }
}
