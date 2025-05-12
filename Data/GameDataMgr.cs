using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏数据管理类
/// </summary>
public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;

    //记录选择的角色数据 用于之后在场景中创建
    public RoleInfo nowSelRole;
    //音乐音效数据
    public MusicData musicData;
    //所有的角色数据
    public List<RoleInfo> roleInfoList;
    //玩家相关数据
    public PlayerData playerData;
    //所有的场景数据
    public List<SceneInfo> sceneInfoList;
    //所有的丧尸数据
    public List<MonsterInfo> monsterInfoList;
    //所有的防御塔数据
    public List<TowerInfo> towerInfoList;
    
    private GameDataMgr() 
    {
        //初始化音乐音效数据
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        //一开始就加载玩家数据  玩家！=角色
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        //一开始就初始化角色数据
        roleInfoList = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        //一开始就加载所有的场景数据
        sceneInfoList = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
        //一开始就加载所有的丧尸数据
        monsterInfoList = JsonMgr.Instance.LoadData<List<MonsterInfo>>("MonsterInfo");
        //一开始就加载所有的防御塔数据
        towerInfoList = JsonMgr.Instance.LoadData<List<TowerInfo>>("TowerInfo");
    }

    /// <summary>
    /// 存储音乐音效数据的方法
    /// </summary>
    /// <param name="musicData"></param>
    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }

    /// <summary>
    /// 存储玩家数据的方法
    /// </summary>
    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerData");
    }

    /// <summary>
    /// 播放音效的方法
    /// </summary>
    /// <param name="soundName"></param>
    public void PlaySound(string resName)
    {
        GameObject soundObj = new GameObject();
        AudioSource a = soundObj.AddComponent<AudioSource>();
        a.clip = Resources.Load<AudioClip>(resName);
        a.mute = !musicData.soundIsOpen;
        a.volume = musicData.soundValue;
        a.Play();
        GameObject.Destroy(soundObj, 1);
    }
    
}
