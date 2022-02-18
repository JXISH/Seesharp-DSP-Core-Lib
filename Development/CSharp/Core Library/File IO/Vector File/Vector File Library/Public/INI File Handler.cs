using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SeeSharpTools.JXI.FileIO.VectorFile
{

    /// <summary> 
    /// Provides methods for reading and writing to an INI file. 
    /// </summary> 
    public class IniFileHandler
    {
        /// <summary> 
        /// The maximum size of a section in an ini file. 
        /// </summary> 
        /// <remarks> 
        /// This property defines the maximum size of the buffers used to retreive data from an ini file.  
        /// This value is the maximum allowed by the win32 functions GetPrivateProfileSectionNames() or GetPrivateProfileString(). 
        /// </remarks> 
        private const int MAX_SECTION_SIZE = 32767; // 32 KB 
        private const int HEADER_TAG_SIZE_MAXIMUM = 256;
        private const int SIZE_OF_I32 = 4;
        private string _filePath;
        IniStreamHandler _streamHandler;
        string _iniString = null;

        #region P/Invoke declares

        /// <summary> 
        /// A static class that provides the win32 P/Invoke signatures used by this class. 
        /// </summary> 
        /// <remarks> 
        /// Note:  In each of the declarations below, we explicitly set CharSet to Auto.  
        /// By default in C#, CharSet is set to Ansi, which reduces performance on windows 2000 and above 
        /// due to needing to convert strings from Unicode (the native format for all .Net strings) to Ansi before marshalling.
        /// Using Auto lets the marshaller select the Unicode version of these functions when available. 
        /// </remarks> 
        [System.Security.SuppressUnmanagedCodeSecurity]
        private static class NativeMethods
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern int GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, uint bufferSize, string filePath);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern uint GetPrivateProfileString
                (string section, string key, string defaultValue, StringBuilder returnedString, int bufferSize, string filePath);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern uint GetPrivateProfileString
                (string section, string key, string defaultValue, char[] lpReturnedString, int nSize, string lpFileName);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern int GetPrivateProfileString
                (string section, string key, string defaultValue, IntPtr lpszReturnBuffer, uint bufferSize, string filePath);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern int GetPrivateProfileInt
                (string section, string key, int defaultValue, string filePath);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern int GetPrivateProfileSection
                (string section, IntPtr lpszReturnBuffer, uint bufferSize, string filePath);

            // We explicitly enable the SetLastError attribute here because WritePrivateProfileString returns errors via SetLastError. 
            // Failure to set this can result in errors being lost during the marshal back to managed code. 
            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool WritePrivateProfileString(string section, string key, string value, string filePath);

        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="path"></param> The path to read and write of ini file       
        public IniFileHandler(string path)
        {
            _filePath = System.IO.Path.GetFullPath(path);
        }

        /// <summary> 
        /// Gets the full path of ini file this object instance is operating on. 
        /// </summary> 
        /// <value>A file path.</value> 
        public string Path
        {
            get { return _filePath; }
        }

        #region Get Value Methods

        /// <summary> 
        /// Gets the value of specified key in an ini file as a string. 
        /// </summary> 
        /// <param name="section">The name of the section to read from.</param> 
        /// <param name="key">The name of the key in section to read.</param> 
        /// <param name="defaultValue">The default value to return if the key cannot be found.</param> 
        /// <returns>The value of the key, if found.  Otherwise, returns 
        /// <paramref name="defaultValue"/></returns> 
        /// <remarks> 
        /// The retreived value must be less than 32KB in length. 
        /// </remarks> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="section"/> or <paramref name="key"/> are a null reference.
        /// </exception> 
        public string ReadKey(string section, string key, string defaultValue)
        {
            if (_iniString == null)
            {
                _iniString = ReadIniFile();
                _streamHandler = new IniStreamHandler(_iniString);
            }
            return _streamHandler.ReadKey(section, key, defaultValue);
        }

        /// <summary> 
        /// Gets the value of specified key in an ini file as a Int16 (short). 
        /// </summary> 
        /// <param name="section">The name of the section to read from.</param> 
        /// <param name="key">The name of the key in section to read.</param> 
        /// <param name="defaultValue">The default value to return if the key cannot be found.</param> 
        /// <returns>The value of the key, if found.  Otherwise, returns 
        /// <paramref name="defaultValue"/>.</returns> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="section"/> or <paramref name="key"/> are a null reference.
        /// </exception> 
        public int ReadKey(string section, string key, short defaultValue)
        {
            int returnValue = ReadKey(section, key, (int)defaultValue);
            return Convert.ToInt16(returnValue);
        }

        /// <summary> 
        /// Gets the value of specified key in an ini file as a Int32. 
        /// </summary> 
        /// <param name="section">The name of the section to read from.</param> 
        /// <param name="key">The name of the key in section to read.</param> 
        /// <param name="defaultValue">The default value to return if the key cannot be found.</param> 
        /// <returns>The value of the key, if found.  Otherwise, returns 
        /// <paramref name="defaultValue"/></returns> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="section"/> or <paramref name="key"/> are a null reference.
        /// </exception> 
        public int ReadKey(string section, string key, int defaultValue)
        {
            if (_iniString == null)
            {
                _iniString = ReadIniFile();
                _streamHandler = new IniStreamHandler(_iniString);
            }
            return _streamHandler.ReadKey(section, key, defaultValue);
        }

        /// <summary> 
        /// Gets the value of specified key in an ini file as a double. 
        /// </summary> 
        /// <param name="section">The name of the section to read from.</param> 
        /// <param name="key">The name of the key in section to read.</param> 
        /// <param name="defaultValue">The default value to return if the key cannot be found.</param> 
        /// <returns>The value of the key, if found.  Otherwise, returns 
        /// <paramref name="defaultValue"/></returns> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="section"/> or <paramref name="key"/> are a null reference.
        /// </exception> 
        public double ReadKey(string section, string key, double defaultValue)
        {
            string returnedString = ReadKey(section, key, "");

            if (returnedString == null || returnedString.Length == 0)
            { return defaultValue; }

            return Convert.ToDouble(returnedString, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Read the ini file and return data in string. 
        /// </summary>
        /// <returns></returns>
        private string ReadIniFile()
        {
            // the existing of the file indicting the read behavior
            // Read the file header and convert to string.    
            string headerInfoString = "";
            FileStream nativeFileStream = null;
            if (!File.Exists(_filePath)) { throw new VectorFileException(ExceptionEnum.InvalidFile); }
            else
            {
                try
                {
                    nativeFileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
                    //byte[] bufferForI32 = new byte[SIZE_OF_I32];
                    //// 先读出section的长度
                    //nativeFileStream.Read(bufferForI32, 0, bufferForI32.Length);
                    //int tagLength = BitConverter.ToInt32(bufferForI32, 0);
                    //if (tagLength <= 0 || tagLength > HEADER_TAG_SIZE_MAXIMUM) { throw new VectorFileException(ExceptionEnum.InvalidFile); }

                    //// Read tag string
                    //byte[] bufferForTag = new byte[tagLength];
                    //nativeFileStream.Read(bufferForTag, 0, bufferForTag.Length);
                    //string tag = Encoding.UTF8.GetString(bufferForTag);

                    //// Read the next 4 bytes which indicates size of remained header string.
                    //nativeFileStream.Read(bufferForI32, 0, bufferForI32.Length);
                    //int remainedLength = BitConverter.ToInt32(bufferForI32, 0);
                    //if (remainedLength <= 0) { throw new VectorFileException(ExceptionEnum.InvalidFile); }

                    // Read remained header string.
                    byte[] bufferForRemained = new byte[nativeFileStream.Length];
                    nativeFileStream.Read(bufferForRemained, 0, bufferForRemained.Length);
                    headerInfoString = Encoding.UTF8.GetString(bufferForRemained);
                }
                catch
                {
                    Console.WriteLine("INI 文件读取错误");
                }
                finally
                {
                    // close the file after reading.
                    nativeFileStream?.Close();
                }
            }
            return headerInfoString;

        }

        #endregion

        #region GetSectionValues Methods

        /// <summary> 
        /// Gets all of the values in a section as a list. 
        /// </summary> 
        /// <param name="section"> 
        /// Name of the section to retrieve values from. 
        /// </param> 
        /// <returns> 
        /// A <see cref="List{T}"/> containing <see cref="KeyValuePair{T1, T2}"/> objects that describe this section.
        /// Use this verison if a section may contain multiple items with the same key value.
        /// If you know that a section cannot contain multiple values with the same key name or you don't 
        /// care about the duplicates, use the more convenient <see cref="GetSectionAsDictionary"/> function. 
        /// </returns> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="section"/> is a null reference.
        /// </exception> 
        public List<KeyValuePair<string, string>> GetSectionAsListOfPairs(string section)
        {
            if (_iniString == null)
            {
                _iniString = ReadIniFile();
                _streamHandler = new IniStreamHandler(_iniString);
            }
            return _streamHandler.GetSectionAsListOfPairs(section);
        }

        /// <summary> 
        /// Gets all of the values in a section as a dictionary. 
        /// </summary> 
        /// <param name="section"> 
        /// Name of the section to retrieve values from. 
        /// </param> 
        /// <returns> 
        /// A <see cref="Dictionary{T, T}"/> containing the key/value pairs found in this section.   
        /// </returns> 
        /// <remarks> 
        /// If a section contains more than one key with the same name, this function only returns the first instance.
        /// If you need to get all key/value pairs within a section even when keys have the same name,
        /// use <see cref="GetSectionAsListOfPairs"/>. 
        /// </remarks> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="section"/> is a null reference.
        /// </exception> 
        public Dictionary<string, string> GetSectionAsDictionary(string section)
        {
            if (_iniString == null)
            {
                _iniString = ReadIniFile();
                _streamHandler = new IniStreamHandler(_iniString);
            }
            return _streamHandler.GetSectionAsDictionary(section);
        }

        #endregion

        #region Get Key/Section Names

        /// <summary> 
        /// Gets the names of all keys under a specific section in the ini file. 
        /// </summary> 
        /// <param name="section"> 
        /// The name of the section to read key names from. 
        /// </param> 
        /// <returns>An array of key names.</returns> 
        /// <remarks> 
        /// The total length of all key names in the section must be less than 32KB in length. 
        /// </remarks> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="section"/> is a null reference.
        /// </exception> 
        public string[] GetKeyNames(string section)
        {
            if (_iniString == null)
            {
                _iniString = ReadIniFile();
                _streamHandler = new IniStreamHandler(_iniString);
            }
            return _streamHandler.GetKeyNames(section);
        }

        /// <summary> 
        /// Gets the names of all sections in the ini file. 
        /// </summary> 
        /// <returns>An array of section names.</returns> 
        /// <remarks> 
        /// The total length of all section names in the section must be less than 32KB in length. 
        /// </remarks> 
        public string[] GetSectionNames()
        {
            if (_iniString == null)
            {
                _iniString = ReadIniFile();
                _streamHandler = new IniStreamHandler(_iniString);
            }
            return _streamHandler.GetSectionNames();
        }

        /// <summary> 
        /// Converts the null seperated pointer to a string into a string array. 
        /// </summary> 
        /// <param name="ptr">A pointer to string data.</param> 
        /// <param name="arrayLength"> 
        /// Length of the data pointed to by <paramref name="ptr"/>. 
        /// </param> 
        /// <returns> 
        /// An array of strings; one for each null found in the array of characters pointed 
        /// at by <paramref name="ptr"/>. 
        /// </returns> 
        private static string[] ConvertNullSeperatedStringToStringArray(IntPtr ptr, int arrayLength)
        {
            string[] stringArray;

            if (arrayLength == 0)
            {
                //Return an empty array. 
                stringArray = new string[0];
            }
            else
            {
                //Convert the buffer into a string.  Decrease the length by 1 so that we remove the second null off the end. 
                string buff = Marshal.PtrToStringAuto(ptr, arrayLength - 1);

                //Parse the buffer into an array of strings by searching for nulls. 
                stringArray = buff.Split('\0');
            }

            return stringArray;
        }

        #endregion

        #region Write Methods

        /// <summary> 
        /// Writes a string value of specified key to the ini file. 
        /// </summary> 
        /// <param name="section">The name of the section to write to .</param> 
        /// <param name="key">The name of the key to write to.</param> 
        /// <param name="value">The string value to write</param> 
        /// <exception cref="T:System.ComponentModel.Win32Exception"> 
        /// The write failed. 
        /// </exception> 
        private void WriteValueInternal(string section, string key, string value)
        {
            string iniDataString;
            if (File.Exists(_filePath))
            {
                // if the file exists, read all the ini data in string. 
                iniDataString = ReadIniFile();
            }
            else
            {
                // if the file not exist, create it.                
                iniDataString = "";
            }
            _streamHandler = new IniStreamHandler(iniDataString);
            //  add key and value to iniData.
            _streamHandler.WriteKey(section, key, value);
            // get string value of the iniData. 
            string infoToWrite = _streamHandler.GetAllText();
            // writes string value of iniData to the ini file.
            WriteIniFile(infoToWrite);
        }

        /// <summary> 
        /// Writes a string value of specified key to the ini file. 
        /// </summary> 
        /// <param name="section">The name of the section to write to .</param> 
        /// <param name="key">The name of the key to write to.</param> 
        /// <param name="value">The string value to write</param> 
        /// <exception cref="T:System.ComponentModel.Win32Exception"> 
        /// The write failed. 
        /// </exception> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="section"/> or <paramref name="key"/> or 
        /// <paramref name="value"/>  are a null reference.
        /// </exception> 
        public void WriteKey(string section, string key, string value)
        {
            if (section == null)
                throw new ArgumentNullException("sectionName");

            if (key == null)
                throw new ArgumentNullException("keyName");

            // If input value is null or empty, write 2 double quotes indicating an empty string.
            // we could NOT directly pass null to winAPI WritePrivateProfileString(),
            // since passing a null as value to WritePrivateProfileString() means to delete that key.
            string valueToWrite;
            if (string.IsNullOrEmpty(value)) { valueToWrite = "\"\""; }
            else { valueToWrite = value; }

            WriteValueInternal(section, key, valueToWrite);
        }

        /// <summary> 
        /// Writes an Int16 (short) value of specified key to the ini file. 
        /// </summary> 
        /// <param name="section">The name of the section to write to .</param> 
        /// <param name="key">The name of the key to write to.</param> 
        /// <param name="value">The value to write</param> 
        /// <exception cref="T:System.ComponentModel.Win32Exception"> 
        /// The write failed. 
        /// </exception> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="section"/> or <paramref name="key"/> or 
        /// <paramref name="value"/>  are a null reference.
        /// </exception> 
        public void WriteKey(string section, string key, short value)
        {
            WriteKey(section, key, (int)value);
        }

        /// <summary> 
        /// Writes an Int32 value of specified key to the ini file. 
        /// </summary> 
        /// <param name="section">The name of the section to write to .</param> 
        /// <param name="key">The name of the key to write to.</param> 
        /// <param name="value">The value to write</param> 
        /// <exception cref="T:System.ComponentModel.Win32Exception"> 
        /// The write failed. 
        /// </exception> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="section"/> or <paramref name="key"/> are a null reference.
        /// </exception> 
        public void WriteKey(string section, string key, int value)
        {
            WriteKey(section, key, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary> 
        /// Writes a double value of specified key to the ini file. 
        /// </summary> 
        /// <param name="section">The name of the section to write to .</param> 
        /// <param name="key">The name of the key to write to.</param> 
        /// <param name="value">The value to write</param> 
        /// <param name="format">The format string used to format "value" to string by its "ToString()" method.</param>
        /// <exception cref="T:System.ComponentModel.Win32Exception"> 
        /// The write failed. 
        /// </exception> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="section"/> or <paramref name="key"/> are a null reference.
        /// </exception> 
        public void WriteKey(string section, string key, double value, string format)
        {
            WriteKey(section, key, value.ToString(format));
        }

        /// <summary>
        /// Writes a string value to the ini file.
        /// </summary>
        /// <param name="iniString"></param>
        private void WriteIniFile(string iniString)
        {
            FileStream nativeFileStream = null;
            byte[] fullFileHeadData;
            try
            {
                // Pre-Allocated byte[] with size of Storage.HeaderSize, fill with space.
                fullFileHeadData = new byte[iniString.Length];
                for (int i = 0; i < fullFileHeadData.Length; i++) { fullFileHeadData[i] = 0x20; }// 固定字节数                                            

                //// 写入section的长度
                //bufferForI32 = BitConverter.GetBytes(section.Length );// 获取Section的字节长度
                //Buffer.BlockCopy(bufferForI32, 0, fullFileHeadData, 0, bufferForI32.Length);// 将section的长度写入
                //buffUpdatePosition += bufferForI32.Length;
                //// 写入section的内容
                //byte[] sectionByte = Encoding.UTF8.GetBytes(section); //将section的内容拷贝进fullFileHeadData
                //Buffer.BlockCopy(sectionByte, 0, fullFileHeadData, buffUpdatePosition, sectionByte.Length);
                //buffUpdatePosition += sectionByte.Length;
                //// 写入剩下数据的长度
                //// Update the next 4 bytes indicating size of rest header.
                //bufferForI32 = BitConverter.GetBytes((int)(fullFileHeadData.Length - buffUpdatePosition - sizeof(int)));
                //Buffer.BlockCopy(bufferForI32, 0, fullFileHeadData, buffUpdatePosition, bufferForI32.Length);
                //buffUpdatePosition += bufferForI32.Length;// 剩下数据的长度
                // 写入剩下数据的内容
                // Update information INI string, if size of information is larger than header size, throw an exception.
                //string iniStringToWrite = iniString.Replace(section, "");
                #region ----------Formating header string into Byte[] for writing to file-----------

                byte[] infoInByteArray = Encoding.UTF8.GetBytes(iniString);
                Buffer.BlockCopy(infoInByteArray, 0, fullFileHeadData, 0, infoInByteArray.Length);

                #endregion

                #region ---------- Write Byte[] info into File ----------

                // Call FileStream or KernelFileStream (according to input "disableBuffering") to write file.                       
                nativeFileStream = new FileStream(_filePath, FileMode.Create, FileAccess.ReadWrite);
                nativeFileStream.Seek(0, SeekOrigin.Begin);
                nativeFileStream.Write(fullFileHeadData, 0, fullFileHeadData.Length);
            }
            catch (Exception ex)
            {
                throw new VectorFileException(ExceptionEnum.InvalidFile);
            }
            finally
            {
                nativeFileStream?.Close();
            }
            #endregion
        }

        #endregion

        #region Delete Methods

        /// <summary> 
        /// Deletes the specified key from the specified section. 
        /// </summary> 
        /// <param name="section"> 
        /// Name of the section to remove the key from. 
        /// </param> 
        /// <param name="key"> 
        /// Name of the key to remove. 
        /// </param> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="section"/> or <paramref name="key"/> are a null reference.
        /// </exception> 
        public void DeleteKey(string section, string key)
        {
            if (section == null)
                throw new ArgumentNullException("sectionName");

            if (key == null)
                throw new ArgumentNullException("keyName");

            WriteValueInternal(section, key, null);
        }

        /// <summary> 
        /// Deletes a section from the ini file. 
        /// </summary> 
        /// <param name="section"> 
        /// Name of the section to delete. 
        /// </param> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="section"/> is a null reference.
        /// </exception> 
        public void DeleteSection(string section)
        {
            if (section == null)
                throw new ArgumentNullException("sectionName");

            WriteValueInternal(section, null, null);
        }

        #endregion
    }

}
