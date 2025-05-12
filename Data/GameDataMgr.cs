using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸ���ݹ�����
/// </summary>
public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;

    //��¼ѡ��Ľ�ɫ���� ����֮���ڳ����д���
    public RoleInfo nowSelRole;
    //������Ч����
    public MusicData musicData;
    //���еĽ�ɫ����
    public List<RoleInfo> roleInfoList;
    //����������
    public PlayerData playerData;
    //���еĳ�������
    public List<SceneInfo> sceneInfoList;
    //���е�ɥʬ����
    public List<MonsterInfo> monsterInfoList;
    //���еķ���������
    public List<TowerInfo> towerInfoList;
    
    private GameDataMgr() 
    {
        //��ʼ��������Ч����
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        //һ��ʼ�ͼ����������  ��ң�=��ɫ
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        //һ��ʼ�ͳ�ʼ����ɫ����
        roleInfoList = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        //һ��ʼ�ͼ������еĳ�������
        sceneInfoList = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
        //һ��ʼ�ͼ������е�ɥʬ����
        monsterInfoList = JsonMgr.Instance.LoadData<List<MonsterInfo>>("MonsterInfo");
        //һ��ʼ�ͼ������еķ���������
        towerInfoList = JsonMgr.Instance.LoadData<List<TowerInfo>>("TowerInfo");
    }

    /// <summary>
    /// �洢������Ч���ݵķ���
    /// </summary>
    /// <param name="musicData"></param>
    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }

    /// <summary>
    /// �洢������ݵķ���
    /// </summary>
    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerData");
    }

    /// <summary>
    /// ������Ч�ķ���
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
