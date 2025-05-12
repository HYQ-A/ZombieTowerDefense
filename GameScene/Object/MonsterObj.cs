using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterObj : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    //一些不变的基础数据
    private MonsterInfo monsterInfo;
    //当前血量
    private int hp;
    //怪物是否死亡
    public bool isDead = false;
    //上一次攻击的时间
    private float frontTime;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// 丧尸初始化的方法
    /// </summary>
    /// <param name="monsterInfo"></param>
    public void InitInfo(MonsterInfo info)
    {
        monsterInfo = info;
        //状态机加载
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(info.animator);
        //要变的当前血量
        hp = info.hp;
        //速度初始化
        agent.speed = info.moveSpeed;
        agent.angularSpeed = info.roundSpeed;
    }

    /// <summary>
    /// 丧尸受伤的方法
    /// </summary>
    /// <param name="dmg"></param>
    public void Wound(int dmg)
    {
        if (isDead)
            return;

        if (isDead) return;
        hp -= dmg;
        //播放受伤动画
        animator.SetTrigger("Wound");
        if (hp <= 0)
        {
            Dead();
        }
        else
        {
            //播放音效
            GameDataMgr.Instance.PlaySound("Music/Music/Wound");
        }

    }

    /// <summary>
    /// 丧尸死亡的方法
    /// </summary>
    public void Dead()
    {
        isDead = true;
        agent.isStopped = true;
        animator.SetBool("Death", true);
        Destroy(this.gameObject, 2f);
        //播放音效
        GameDataMgr.Instance.PlaySound("Music/Music/dead");
        //加钱
        GameLevelMgr.Instance.player.AddMoney(50);
    }

    /// <summary>
    /// 丧尸死亡动画播放完后的方法
    /// </summary>
    public void DeadEvent()
    {
        //通知关卡管理器 怪物数量减1
        //GameLevelMgr.Instance.ChangeMonsterNum(-1);
        //将怪物从列表中移除
        GameLevelMgr.Instance.RemoveMonster(this);

        //在场景中移除已经死亡的丧尸怪物
        Destroy(this.gameObject);

        //显示结束界面
        if(GameLevelMgr.Instance.CheckOver())
        {
            UIManager.Instance.ShowPanle<GameOverPanel>().InitInfo(GameLevelMgr.Instance.player.money, true);
        }
    }

    /// <summary>
    /// 丧尸出生动画播放完后的方法
    /// </summary>
    public void BornOver()
    {
        //设置丧尸的移动目标
        agent.SetDestination(MainTowerObj.Instance.gameObject.transform.position);
        //播放移动动画
        animator.SetBool("Run", true);
    }

    // Update is called once per frame
    void Update()
    {
        //检测什么时候停下来攻击
        if (isDead) return;

        //根据速度 决定动画播放什么
        animator.SetBool("Run", agent.velocity != Vector3.zero);
        //检测和目标点达到移动条件时 就攻击
        if (Vector3.Distance(this.gameObject.transform.position, MainTowerObj.Instance.gameObject.transform.position) <=3 && Time.time - frontTime >= monsterInfo.atkOffest)
        {
            //记录这次攻击时的时间
            frontTime = Time.time;
            animator.SetTrigger("Atk");
        }
    }

    /// <summary>
    /// 伤害检测的方法
    /// </summary>
    public void AtkEvent()
    {
        //范围检测进行伤害判断
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + this.transform.forward + this.transform.up, 1, 1 << LayerMask.NameToLayer("MainTower"));

        //播放音效
        GameDataMgr.Instance.PlaySound("Music/Music/Eat");

        for (int i = 0; i < colliders.Length; i++)
        {
            if(MainTowerObj.Instance.gameObject==colliders[i].gameObject)
            {
                //塔扣血
                MainTowerObj.Instance.Wound(monsterInfo.atk);
            }
        }
    }
}
