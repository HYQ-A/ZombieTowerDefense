using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Purchasing;
using UnityEngine;

public class PlayerObj : MonoBehaviour
{
    //��ȡ�������
    private int atk;
    public int money;
    //����������
    private Animator animator;
    //�ƶ��ٶ�
    public float moveSpeed = 10;
    //��ת�ٶ�
    public float rotationSpeed = 50;
    //�����λ��
    public Transform firePos;
    //��Ч·��
    public string effStr;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    /// <summary>
    /// ��ʼ��������Եķ���
    /// </summary>
    /// <param name="money"></param>
    /// <param name="atk"></param>
    public void InitPlayerInfo(int money,int atk)
    {
        this.money = money;
        this.atk = atk;
        //һ��ʼ�ͳ�ʼ���������
        UpdateMoney();
    }

    // Update is called once per frame
    void Update()
    {
        //�ƶ�
        animator.SetFloat("Hspeed", Input.GetAxis("Horizontal"));
        animator.SetFloat("Vspeed", Input.GetAxis("Vertical"));
        //��ת
        this.gameObject.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime);
        //�¶�
        if (Input.GetKeyDown(KeyCode.LeftControl))
            animator.SetLayerWeight(1, 1);
        if (Input.GetKeyUp(KeyCode.LeftControl))
            animator.SetLayerWeight(1, 0);
        //����
        if (Input.GetMouseButtonDown(0))
            animator.SetTrigger("Fire");
        //����
        if (Input.GetKeyDown(KeyCode.C))
            animator.SetTrigger("Roll");

    }

    /// <summary>
    /// ��Χ���ķ��� �˺����
    /// </summary>
    public void KnifeEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(this.gameObject.transform.position + this.gameObject.transform.forward + this.gameObject.transform.up, 1, 1 << LayerMask.NameToLayer("Monster"));

        //������Ч
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
    /// ���߼��ķ��� �˺����
    /// </summary>
    public void ShootEvent()
    {
        RaycastHit[] hits = Physics.RaycastAll(new Ray(firePos.transform.position, firePos.transform.forward), 100, 1 << LayerMask.NameToLayer("Monster"));

        //������Ч
        GameDataMgr.Instance.PlaySound("Music/Music/Gun");

        for (int i = 0; i < hits.Length; i++)
        {
            MonsterObj monster = hits[i].collider.GetComponent<MonsterObj>();
            if (monster != null && !monster.isDead)
            {
                monster.Wound(atk);
                //���Ź�����Ч
                GameObject effObj = Instantiate(Resources.Load<GameObject>(GameDataMgr.Instance.nowSelRole.hitEff));
                effObj.transform.position = hits[i].point;
                effObj.transform.rotation = Quaternion.LookRotation(hits[i].normal);
                Destroy(effObj, 1);

                break;
            }
        }
    }

    /// <summary>
    /// ���½�������ķ���
    /// </summary>
    public void UpdateMoney()
    {
        //��Ӹ��½����Ͻ�ҵ�����
        UIManager.Instance.GetPanel<GamePanel>().UpdateMoney(money);
    }

    /// <summary>
    /// ��ӽ�������ķ���
    /// </summary>
    /// <param name="money"></param>
    public void AddMoney(int money)
    {
        this.money += money;
        UpdateMoney();
    }
}
