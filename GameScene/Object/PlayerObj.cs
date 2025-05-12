using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Purchasing;
using UnityEngine;

public class PlayerObj : MonoBehaviour
{
    //获取玩家属性
    private int atk;
    public int money;
    //动画控制器
    private Animator animator;
    //移动速度
    public float moveSpeed = 10;
    //旋转速度
    public float rotationSpeed = 50;
    //开火点位置
    public Transform firePos;
    //特效路径
    public string effStr;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    /// <summary>
    /// 初始化玩家属性的方法
    /// </summary>
    /// <param name="money"></param>
    /// <param name="atk"></param>
    public void InitPlayerInfo(int money,int atk)
    {
        this.money = money;
        this.atk = atk;
        //一开始就初始化金币数量
        UpdateMoney();
    }

    // Update is called once per frame
    void Update()
    {
        //移动
        animator.SetFloat("Hspeed", Input.GetAxis("Horizontal"));
        animator.SetFloat("Vspeed", Input.GetAxis("Vertical"));
        //旋转
        this.gameObject.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime);
        //下蹲
        if (Input.GetKeyDown(KeyCode.LeftControl))
            animator.SetLayerWeight(1, 1);
        if (Input.GetKeyUp(KeyCode.LeftControl))
            animator.SetLayerWeight(1, 0);
        //攻击
        if (Input.GetMouseButtonDown(0))
            animator.SetTrigger("Fire");
        //翻滚
        if (Input.GetKeyDown(KeyCode.C))
            animator.SetTrigger("Roll");

    }

    /// <summary>
    /// 范围检测的方法 伤害检测
    /// </summary>
    public void KnifeEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(this.gameObject.transform.position + this.gameObject.transform.forward + this.gameObject.transform.up, 1, 1 << LayerMask.NameToLayer("Monster"));

        //播放音效
        GameDataMgr.Instance.PlaySound("Music/Music/Knife");

        for (int i = 0; i < colliders.Length; i++)
        {
            MonsterObj monster = colliders[i].GetComponent<MonsterObj>();
            if (monster != null && !monster.isDead)
            {
                monster.Wound(atk);
                break;
            }
        }
    }

    /// <summary>
    /// 射线检测的方法 伤害检测
    /// </summary>
    public void ShootEvent()
    {
        RaycastHit[] hits = Physics.RaycastAll(new Ray(firePos.transform.position, firePos.transform.forward), 100, 1 << LayerMask.NameToLayer("Monster"));

        //播放音效
        GameDataMgr.Instance.PlaySound("Music/Music/Gun");

        for (int i = 0; i < hits.Length; i++)
        {
            MonsterObj monster = hits[i].collider.GetComponent<MonsterObj>();
            if (monster != null && !monster.isDead)
            {
                monster.Wound(atk);
                //播放攻击特效
                GameObject effObj = Instantiate(Resources.Load<GameObject>(GameDataMgr.Instance.nowSelRole.hitEff));
                effObj.transform.position = hits[i].point;
                effObj.transform.rotation = Quaternion.LookRotation(hits[i].normal);
                Destroy(effObj, 1);

                break;
            }
        }
    }

    /// <summary>
    /// 更新金币数量的方法
    /// </summary>
    public void UpdateMoney()
    {
        //间接更新界面上金币的数量
        UIManager.Instance.GetPanel<GamePanel>().UpdateMoney(money);
    }

    /// <summary>
    /// 添加金币数量的方法
    /// </summary>
    /// <param name="money"></param>
    public void AddMoney(int money)
    {
        this.money += money;
        UpdateMoney();
    }
}
