# MRTKtest
SUSTech 2022 多媒体课程project

前端：zal，zcz，leh；后段：zyq，zz

### Introduction

这是一个基于MRTK Hololens的骨科手术AR模拟平台。基于Hololens，我们设计了如下功能：

+ 操作状态与选择状态
+ 操作状态：骨骼模型的移动，旋转，放大缩小
+ 选择状态：一个或多个谷歌模型的选中/取消选中
+ 选择状态：选中物体的复制，删除，绑定。解绑
+ 切换不同骨骼手术案例场景
+ 合成语音辅助解释交互
+ 高亮展示骨折/骨裂片段

+ Demo场景：
  + 断骨拼接
  + 骨裂修复
  + 健康骨骼与亚健康骨骼对比示意

![](img\simulate.png)

![](img/real.png)

### Configuration

#### Hololens下载与环境配置

1. 下载windows10，VS以及相关包：https://docs.microsoft.com/en-us/windows/mixed-reality/develop/install-the-tools **这里Visual Studio 要下2019版！！**

2. 下载Unity 2020.3.x版本

3. 安装配置MRTK：https://docs.microsoft.com/en-us/windows/mixed-reality/develop/unity/welcome-to-mr-feature-tool

4. MRTK官方新手教程参考：https://docs.microsoft.com/en-us/learn/modules/learn-mrtk-tutorials

#### 生成并运行

1. 打开Unity进入项目。

1. 要新建文件夹里Unity build，生成完成后进入文件夹，用VS打开.sln文件

2. 在VS里调整这三项设置好：

![](img\VS_basic.png)

3. PC和Hololens均配置为developer mode （参考：https://docs.microsoft.com/en-us/windows/mixed-reality/develop/advanced-concepts/using-visual-studio?tabs=hl2）后点击run。

4. PC第一次连接Hololens，运行之后会弹出配对（Pair对话框）：

![](img\pair.png)

### Front-end structure



### 动图示意

（zaz这里放gif）

