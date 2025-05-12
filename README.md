# TowerDefenseGuardian

![Unity Version](https://img.shields.io/badge/Unity-2021.3%2B-blue)
![License](https://img.shields.io/badge/License-MIT-green)

> 一款基于Unity3D开发的3D塔防游戏，玩家通过策略建造防御塔抵御丧尸波次进攻，守护核心主塔。项目采用模块化设计与数据驱动架构，实现高效可扩展的游戏逻辑。

---

## 🎮 核心玩法

- **动态波次系统**：按配置生成多类型丧尸波次，支持随机怪物组合与生成节奏
- **塔防建造**：提供3种可升级防御塔（单体/范围/减速），支持拖拽建造与资源管理
- **智能寻敌**：防御塔自动索敌并计算最优攻击路径，丧尸采用NavMesh动态寻路
- **成长体系**：金币击杀奖励机制，主塔血量持久化存档

---

## 🔧 技术架构

### 技术栈
- **引擎**: Unity3D 2021.3 LTS
- **语言**: C# 
- **AI**: NavMesh路径规划 + 有限状态机(FSM)
- **数据**: JSON序列化 + ScriptableObject配置
- **物理**: Physics碰撞检测 + 射线追踪(Raycast)
- **架构**: 观察者模式 + 单例模式

### 核心模块

#### 🛠️ 防御塔系统
- **动态建造**  
  ```csharp
  // TowerPoint.cs
  public void CreateTower(int id) {
    TowerInfo data = GameDataMgr.Instance.towerInfoList[id-1];
    GameObject tower = Instantiate(Resources.Load(data.res), position);
    tower.GetComponent<TowerObj>().InitInfo(data);
  }
