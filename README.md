# 挖掘机工程工地游戏

一个有趣的 2D 卡通挖掘机游戏，专为小朋友设计！在工地上操控挖掘机完成各种工程任务。

## 📋 游戏特性

- 🚜 **可操控的挖掘机** - 用方向键移动，用上下键调节铲���
- 🏗️ **工程任务系统** - 完成挖土运土任务，获得分数
- 🎨 **卡通画风** - 色彩鲜艳，适合小朋友
- 💾 **无依赖** - 只需要 .NET 8 运行时

## 🎮 游戏操作

| 按键 | 功能 |
|------|------|
| **← →** | 移动挖掘机 |
| **↑ ↓** | 调节铲斗角度 |
| **空格** | 开始挖土 |
| **回车** | 把土倒到堆里 |

## 🚀 运行方式

### 方式一：使用 Visual Studio

1. 克隆仓库
   ```bash
   git clone https://github.com/DreamSeeker23/ExcavatorGame.git
   cd ExcavatorGame
   ```

2. 打开 Visual Studio 2022

3. 打开项目文件夹（打开 ExcavatorGame 文件夹）

4. 按 F5 运行

### 方式二：命令行运行

```bash
git clone https://github.com/DreamSeeker23/ExcavatorGame.git
cd ExcavatorGame
dotnet restore
dotnet run
```

## 📦 系统要求

- Windows 10 / Windows 11
- .NET 8 SDK 或以上

## 🎯 游戏目标

1. 移动挖掘机到土堆位置
2. 调节铲斗角度使其接近地面
3. 按空格开始挖土
4. 土满了以后按回车倒到堆里
5. 完成指定数量的挖土任务即可过关

## 💡 游戏玩法提示

- 铲斗角度越低，越容易挖到土
- 挖掘机需要在特定位置才能挖到土
- 每次倒土后任务目标可能会增加
- 小朋友可以自由探索工地，没有失败

## 🛠️ 技术栈

- **语言**: C#
- **框架**: .NET 8
- **UI**: Windows Forms
- **图形**: GDI+

## 📝 项目结构

```
ExcavatorGame/
├── Program.cs           # 程序入口
├── Form1.cs            # 主窗体和游戏逻辑
├── ExcavatorGame.csproj # 项目配置
└── README.md           # 说明文档
```

## 🎓 学习价值

这个项目展示了：
- Windows Forms 游戏开发
- GDI+ 图形绘制
- 游戏循环和计时器
- 键盘输入处理
- 碰撞检测基础

## 📄 许可证

MIT License - 自由使用和修改

## 🤝 反馈和改进

如果你有任何建议或发现 bug，欢迎提 Issue！

---

**祝小朋友们玩得开心！** 🎉
