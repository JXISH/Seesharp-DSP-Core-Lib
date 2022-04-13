# Microsoft .NET Framework 反向兼容

第一次运行 JXI DSP-Core Library的时候遇到了这个弹窗

![framework not installed](Resources\Backward Compatibility\framework not installed.png)

什么意思呢？就是这个project (JXI DSP-Core Library) 和目前电脑上装有的.NET framework版本不兼容，建议将当前project的framework更新到最新版本。

如果用户电脑上的.NET framework是旧版的，是无法编译装有新版本framework的程序的。

那咋办呢？

关于.NET framework不同版本兼容性的问题，Microsoft官网是这么说的：

![image-20220327234746276](Resources\Backward Compatibility\version compatibility.png)

后面提到了自Microsoft .NET Framework v4.5开始，之后的所有新版本都有反向兼容的功能。也就是说客户无论怎么升级自己的.NET Framework都能运行老版本的程序。反之，如果程序的.NET framework版本高于客户使用的，那么调用的时候.dll文件就会报错。

不得不说在这点上JXI-DSP Core Library做得很周到，为了让所有更多的人能使用，在相对旧的版本上开发。

