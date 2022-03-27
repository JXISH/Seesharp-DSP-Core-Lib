# JXI DSP-Core Design Note - Math Utility

## 挑战1 MathUtility

VST\Digital Signal Processing\Math Utility 搬移到DSP-Core找不到自然和谐的落脚点。

## 分析

### 原创内容

1. StatisticalNumericScalar

   可实例化的对象，统计输入数据的最大、最小、平均

   ** 貌似可改名为 ScalarProcessStatistics: 标量过程统计，可以用point-by-point Statistics。考虑enqueue(double)可能添加数组输入接口。如果进一步考虑将来即使增加其他数据类型，也在此以多态形式扩展，基础上也是pt-by-pt。 其他命名：Process Statistics和过程控制里面的统计容易混淆，Cumulative Statistics大多数情况仅仅指概率分布的统计

2. Const

   I16 I32表达的定点数相位到实数的转换比例常数

3. NoiseGenerator

   高斯和均匀白噪声数组生成(填充)

4. MathUtility

   线性内插public static double Interpolation(double xStart, double yStart, double xEnd, double yEnd, double x)

5. MathExtension

   sinc()

### 归类实施

1. StatisticalNumericScalar 》》Mathematics\PointByPointStatistics 缺范例
2. Const 》》Numerics 建立const.cs
3. NoiseGenerator 》》Signal Processing\Generation
4. MathUtility -线性内插》》Mathematics\Interpolation *新建
5. MathExtension - sinc()》》Numerics\MathExtension



## 挑战2 Coordinate

VST\Digital Signal Processing\Coordinate有3个cs归属。

### 归类实施

它们都属于Geometry，所以在Mathematics下面建立Geometry目录和同名cs

* 遗留问题scale是一些广义的标量变换。其中弧度-角度互换可以讲得过去是scaling，但是求余角看来有些讲不通，但也不属于任何类型，估计将来如果发现没有人用，可以删掉算了。

  

## 挑战3 Analog Modulation

### 来源

VST>> Analog Modulation (AM FM); Modulation Structure; Modulator

三个cs



### 建议方案

1. ~~RF Communicaitons \ Modulation 下分 Analog , Digital~~

2. ~~RF Communicaitons \ Analog Modulation，将来加 Digital Modulation减少一层，反正现在内容不多。但是这样Modem Structure里面一些DigitalModMessage和PulseMpdulationParam.cs放在这里不合适。~~

3. ~~RF Communicaitons \ Modulation 下面各色模拟数字调制解调相关cs都放在一起算了，反正不多。以后添加digitalModem可能会乱，需要建Digital子目录，造成AM FM在上一级目录~~

4. ~~手工拆开ModemStructure为AnalogModemStructure和DigitalModemStructure两个cs，分到2里面的目录结构~~

   

## 临时问题

* 在DSP下面导入了DFT, FIR Filter 其中reference ipp没有导入，请补充改完整，
* ~~FIR Filter 在VS2022抛异常需要具体种类，没有统一的exception，我简单换成了ArgumentException（大多数）或者InvalidOperationException（历史数据不空）请检查对不对~~
  * ~~yuanchao：我觉得没问题，如果要将异常进行规范的话，一般需要在解决方案中专门定义一个异常工程来预定义一组常用异常（类似于定义一个通用Error Code/Description表），但是这个工作非常耗费时间，我们还是各自在Project中自行定义吧，词能达意就可以了。~~
* 多级目录的nameSpace规范不统一，有的逐层添加，有的跳过目录层次命名，各有道理，需要统一意见
  * yuanchao：我觉得应该遵照设计文档中的功能树设计，将各工程在磁盘上的存储目录、在VS Solution中的Project管理目录、各Project的NameSapce，这三者均保持与文档中的功能树设计一致，易于管理维护。
  * Hui: 
    * NameSpace SeeSharpTools.JXI.Mathematics.Interpolation 下面库就叫Interpolation引用起来叫 SeeSharpTools.JXI.Mathematics.Interpolation.Interpolation 有些别扭是否就这样接受。将来可能会加AdvancedInterpolation类
    * SeeSharpTools.JXI.Mathematics.LinearAlgebra.Matrix 有类似困惑，不过我觉得没问题，看看就习惯了，而且using如果包括 整个namespace，类库名称重复就不显眼了
    * XXJ的部分namespace也缺一层
    * Conditioning下面诸如ADvancedFilters等子目录，以及Transform\DFT下面只有一个两个cs文件，是否可以删掉这层子目录，使得nameSpace简化？
* ~~ipp的x86和x64兼容问题没有解决，暂时可以只用x64，不支持x86~~
  * ~~yuanchao：暂时不需要解决。规范的做法是，我们同时提供ipp/mkl的C++ dll的x86/x64版本，并直接安装至目标机器的系统目录下即可，我们在C#中的代码本身就不需要区分x64/x86的，都按照any CPU去生成就可以了。~~
* 能否统计一下公开方法数量，最好分类库统计
* ~~新增public class 什么时候该用internal， static可能没搞对~~
  * ~~yuanchao：internal/public暂时不需要太规范，各project自行决定，全用public都可以。至于是否static，这是类库/方法功能定义时需要考虑的问题，需要具体功能具体讨论。~~
* ~~我将各个库.NET编译到4.6.1了，是否应该回到4 ?~~
  * ~~yuanchao：赞同都用较新的4.6.1，可以避免4.0的一些bug。后续如果某些特定项目因平台或向前兼容需要特定的.NET版本，到时候改一下重新生成就可以了，也不麻烦。~~



## Release Checklist

1. 完整性
   1. External / Dependency
   2. Namespace / .NET4.6.1
   3. Debug test / examples
   4. remove license control - vector file
   5. Startup guide - Hui
   6. Compile Settings - output dlls to dedicated bin folder
      1. APP Notes: select Release, re-build - find in release folder, right click;  copy dependency
2. 补充范例
   1. DFT
   2. Analog Modem
   3. Muti-rate filter
3. Users manual
   1. 分立app notes

