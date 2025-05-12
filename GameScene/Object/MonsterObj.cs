using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterObj : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    //һЩ����Ļ�������
    private MonsterInfo monsterInfo;
    //��ǰѪ��
    private int hp;
    //�����Ƿ�����
    public bool isDead = false;
    //��һ�ι�����ʱ��
    private float frontTime;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// ɥʬ��ʼ���ķ���
    /// </summary>
    /// <param name="monsterInfo"></param>
    public void InitInfo(MonsterInfo info)
    {
        monsterInfo = info;
        //״̬������
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(info.animator);
        //Ҫ��ĵ�ǰѪ��
        hp = info.hp;
        //�ٶȳ�ʼ��
        agent.speed = info.moveSpeed;
        agent.angularSpeed = info.roundSpeed;
    }

    /// <summary>
    /// ɥʬ���˵ķ���
    /// </summary>
    /// <param name="dmg"></param>
    public void Wound(int dmg)
    {
        if (isDead)
            return;

        if (isDead) return;
        hp -= dmg;
        //�������˶���
        animator.SetTrigger("Wound");
        if (hp <= 0)
        {
            Dead();
        }
        else
        {
            //������Ч
            GameDataMgr.Instance.PlaySound("Music/Music/Wound");
        }

    }

    /// <summary>
    /// ɥʬ�����ķ���
    /// </summary>
    public void Dead()
    {
        isDead = true;
        agent.isStopped = true;
        animator.SetBool("Death", true);
        Destroy(this.gameObject, 2f);
        //������Ч
        GameDataMgr.Instance.PlaySound("Music/Music/dead");
        //��Ǯ
        GameLevelMgr.Instance.player.AddMoney(50);
    }

    /// <summary>
    /// ɥʬ���������������ķ���
    /// </summary>
    public void DeadEvent()
    {
        //֪ͨ�ؿ������� ����������1
        //GameLevelMgr.Instance.ChangeMonsterNum(-1);
        //��������б����Ƴ�
        GameLevelMgr.Instance.RemoveMonster(this);

        //�ڳ������Ƴ��Ѿ�������ɥʬ����
        Destroy(this.gameObject);

        //��ʾ��������
        if(GameLevelMgr.Instance.CheckOver())
        {
            UIManager.Instance.ShowPanle<GameOverPanel>().InitInfo(GameLevelMgr.Instance.player.money, true);
        }
    }

    /// <summary>
    /// ɥʬ���������������ķ���
    /// </summary>
    public void BornOver()
    {
        //����ɥʬ���ƶ�Ŀ��
        agent.SetDestination(MainTowerObj.Instance.gameObject.transform.position);
        //�����ƶ�����
        animator.SetBool("Run", true);
    }

    // Update is called once per frame
    void Update()
    {
        //���ʲôʱ��ͣ��������
        if (isDead) return;

        //�����ٶ� ������������ʲô
        animator.SetBool("Run", agent.velocity != Vector3.zero);
        //����Ŀ���ﵽ�ƶ�����ʱ �͹���
        if (Vector3.Distance(this.gameObject.transform.position, MainTowerObj.Instance.gameObject.transform.position) <=3 && Time.time - frontTime >= monsterInfo.atkOffest)
        {
            //��¼��ι���ʱ��ʱ��
            frontTime = Time.time;
            animator.SetTrigger("Atk");
        }
    }

    /// <summary>
    /// �˺����ķ���
    /// </summary>
    public void AtkEvent()
    {
        //��Χ�������˺��ж�
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + this.transform.forward + this.transform.up, 1, 1 << LayerMask.NameToLayer("MainTower"));

        //������Ч
        GameDataMgr.Instance.PlaySound("Music/Music/Eat");

        for (int i = 0; i < colliders.Length; i++)
        {
            if(MainTowerObj.Instance.gameObject==colliders[i].gameObject)
            {
                //����Ѫ
                MainTowerObj.Instance.Wound(monsterInfo.atk);
            }
        }
    }
}
