using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家数据
/// </summary>
public class PlayerData
{
    //当前拥有的游戏币
    public int hadMoney = 200;
    //当前已解锁的角色
    public List<int> hadHero = new List<int>();
}
