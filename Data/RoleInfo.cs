using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色数据类
/// </summary>
public class RoleInfo
{
    public int id;
    //角色资源路径
    public string res;
    public int atk;
    //角色名字
    public string tips;
    //角色解锁金额
    public int lockMoney;
    //角色攻击方式 1为范围检测 2为射线检测
    public int type;
    //攻击特效路径
    public string hitEff;
}
