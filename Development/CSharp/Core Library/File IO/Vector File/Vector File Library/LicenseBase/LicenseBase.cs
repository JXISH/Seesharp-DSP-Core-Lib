using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using SeeSharpTools.LicenseManager;

/*******************************************************
 * 
 * *****************************************************/
namespace SeeSharpTools.JXI.FileIO.VectorFile.LicenseBase
{
    /// <summary>
    /// <para>License Manager Base Class</para>
    /// <para>Chinese Simplified：License Manager Base Class 类 </para>
    /// </summary>
    public static class LicenseBase
    {
        #region----------------------------Construct-------------------------------------
        /// <summary>
        /// 构造函数 
        /// </summary>
        static LicenseBase()
        {

        }
        #endregion

        #region ---------------------------Public Methods-------------------------
        /// <summary>
        /// Validate；Chinese Simplified：激活
        /// </summary>
        /// <param name="computerID">computerID；Chinese Simplified：电脑ID，可以通过激活界面查询</param>
        /// <param name="activeCode">activeCode；Chinese Simplified：激活码</param>
        /// <param name="activeTime">active Time,uint is second；Chinese Simplified：激活时间</param>
        /// <returns></returns>
        public static  bool Validate(string computerID, string activeCode, string activeTime)
        {
#if LICENSEOFF
            return true;
#else
            bool status=false;
            //利用反射获取版本号
            Assembly fileAssembly = Assembly.GetExecutingAssembly();
            var fileVersion = fileAssembly.GetName().Version.ToString();//获取版本号
            var firstPointIndex = fileVersion.IndexOf('.');
            var secondPointIndex = fileVersion.Substring(firstPointIndex + 1, fileVersion.Length - firstPointIndex - 1).IndexOf('.');
            string strProduct = "SeeSharpTools.JXI.FileIO.VectorFile";
            string strVersion = fileVersion.Substring(0, firstPointIndex + 1 + secondPointIndex);//获取大版本号1.2
            var a = LicenseManager.LicenseManager.ComputerID;//为了解析出core.dll
            status = LicenseManager.LicenseBase.Validate(strProduct, strVersion, computerID, activeCode, activeTime);
            return status;
#endif
        }
#endregion
    }
}
