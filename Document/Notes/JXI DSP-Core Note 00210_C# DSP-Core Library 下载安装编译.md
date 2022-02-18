# C# DSP-Core Library 下载安装和编译

Feb-6-2022

## 下载

聚星仪器:registered: 提供的下载目录或git仓库同步，建立你的 DSP-Core目录作为整个库的根目录。 下载解压后最基本的目录如下：

DSP-Core

​	Development

​		CSharp

​			Core Library  //solution file所在目录				

​	Document  //文档所在目录



## 安装

* 你需要Microsoft:registered: VisualStudio (VS) 2022，安装C#并支持Windows应用开发。可以从微软官网下载安装，支持社区版。 VS 2015 也可以编译运行一部分项目。

* 你需要安装Intel MKL (Math Kernel Library) 2017.1.143 x64，建议注册简仪官网(JYTek.com)，下载JYPedia excel 文件。你在JYPedia的“Drivers”页面可以找到 JXIDSPRuntimeMKL2017.1.143 x64的安装包链接。

## 编译

* 如果你在测试，可以在VS工具栏选择[Debug]，然后指定编译你希望的类库或例程。编译结果保存在工程(Project)目录\bin\Debug。

Tip: 你可以轻松地在Solution Explorer，右键工程，在弹出窗选择Open Folder in File Explorer打开工程目录。

* 如果你要发布，可以在VS工具栏选择[Release]，然后指定编译你希望的类库或例程。编译结果保存在工程(Project)目录\bin\Release。

Tip: 本解决方案使用 .NET framework 4.6.1， x64 CPU。如果遇到编译错误，请检查对应设置。 其中CPU可以是x64或any CPU，不勾选 preferred x86选项。

* 类库依赖的外部库存放在DSP-Core\Development\CSharp\Core Library\Public\External，其中有些依赖项编译自C++，保存在External\Dependency。你需要将这些dll拷贝到系统盘Windows\System32目录下，才能正常运行某些例程。为保留版本多样性，你也可以将Dependency目录下的文件拷贝到编译运行的目录。

## FAQ

Q: 我的应用不是VS2022版本怎么办？

A: 在VS2022编译类库工程，将需要的类库拷贝到你的应用开发目录下使用。

Q: 我想用x86编译可以吗？

A: 有些工程支持x86 CPU设置，有些不支持。主要是用到ipp (Intel:registered:   Integrated Performance Primitives) 库的工程目前只支持x64 CPU。