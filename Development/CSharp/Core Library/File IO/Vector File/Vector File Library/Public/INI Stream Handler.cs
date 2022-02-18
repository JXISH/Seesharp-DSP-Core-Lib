using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using IniParser.Parser;
using IniParser.Model;

namespace SeeSharpTools.JXI.FileIO.VectorFile
{

    /// <summary> 
    /// Provides methods for reading and writing to an INI file. 
    /// </summary> 
    public class IniStreamHandler
    {
        /// <summary> 
        /// The maximum size of a section in an ini file. 
        /// </summary> 
        /// <remarks> 
        /// This property defines the maximum size of the buffers used to retreive data from an ini file.  
        /// This value is the maximum allowed by the win32 functions GetPrivateProfileSectionNames() or GetPrivateProfileString(). 
        /// </remarks> 
        public const int MAX_SECTION_SIZE = 32767; // 32 KB 

        //The path of the file we are operating on. 
        //private string _filePath;
        private IniDataParser _iniDataParser;

        private IniData _iniData;

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
        /// <param name="iniString">the ini string.</param> 
        public IniStreamHandler(string iniString)
        {
            //Convert to the full path.  Because of backward compatibility, 
            // the win32 functions tend to assume the path should be the root Windows directory if it is not specified.
            // By calling GetFullPath, we make sure we are always passing the full path the win32 functions. 
            //_filePath = System.IO.Path.GetFullPath(path);
            _iniDataParser = new IniDataParser();
            _iniData = _iniDataParser.Parse(iniString);
        }

        /// <summary>
        /// get all ini text string
        /// </summary>
        /// <returns></returns>
        public string GetAllText()
        {
            return _iniData.ToString();
        }

        /// <summary> 
        /// Gets the full path of ini file this object instance is operating on. 
        /// </summary> 
        /// <value>A file path.</value> 
        public string Path
        {
            get { return _iniData.ToString(); }
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
            if (section == null || section.Length == 0)
            { throw new ArgumentNullException("sectionName"); }

            if (key == null || key.Length == 0)
            { throw new ArgumentNullException("keyName"); }

            var retStr = _iniData[section][key];
            return retStr == null ? defaultValue : retStr;
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
        public short ReadKey(string section, string key, short defaultValue)
        {
            string returnedString = ReadKey(section, key, "");

            if (returnedString == null || returnedString.Length == 0)
            { return defaultValue; }

            return Convert.ToInt16(returnedString, CultureInfo.InvariantCulture);
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
            string returnedString = ReadKey(section, key, "");

            if (returnedString == null || returnedString.Length == 0)
            { return defaultValue; }

            return Convert.ToInt32(returnedString, CultureInfo.InvariantCulture);
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

        #endregion

        #region GetSectionValues Methods

        /// <summary> 
        /// Gets all of the values in a section as a list. 
        /// </summary> 
        /// <param name="section"> 
        /// Name of the section to retrieve values from. 
        /// </param> 
        /// <returns> 
        /// <param name="trimDoubleQuotes">是否移除ini项Value首尾的双引号。</param>
        /// A <see cref="List{T}"/> containing <see cref="KeyValuePair{T1, T2}"/> objects that describe this section.
        /// Use this verison if a section may contain multiple items with the same key value.
        /// If you know that a section cannot contain multiple values with the same key name or you don't 
        /// care about the duplicates, use the more convenient <see cref="GetSectionAsDictionary"/> function. 
        /// </returns> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="section"/> is a null reference.
        /// </exception> 
        public List<KeyValuePair<string, string>> GetSectionAsListOfPairs(string section, bool trimDoubleQuotes = true)
        {
            List<KeyValuePair<string, string>> keyValuePairsList;
            if (section == null)
            { throw new ArgumentNullException("sectionName"); }

            var keys = _iniData[section];
            keyValuePairsList = new List<KeyValuePair<string, string>>(keys.Count);
            foreach (var k in keys)
            {
                keyValuePairsList.Add(new KeyValuePair<string, string>(k.KeyName, k.Value.Trim('"')));
            }


            return keyValuePairsList;
        }

        /// <summary> 
        /// Gets all of the values in a section as a dictionary. 
        /// </summary> 
        /// <param name="section"> 
        /// Name of the section to retrieve values from. 
        /// </param> 
        /// <param name="trimDoubleQuotes">是否移除ini项Value首尾的双引号。</param>
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
        public Dictionary<string, string> GetSectionAsDictionary(string section, bool trimDoubleQuotes = true)
        {
            List<KeyValuePair<string, string>> keyValuePairsList;
            Dictionary<string, string> dictionary;

            keyValuePairsList = GetSectionAsListOfPairs(section);

            //Convert list into a dictionary. 
            dictionary = new Dictionary<string, string>(keyValuePairsList.Count);

            foreach (KeyValuePair<string, string> keyValuePair in keyValuePairsList)
            {
                //Skip any key we have already seen. 
                if (!dictionary.ContainsKey(keyValuePair.Key))
                { dictionary.Add(keyValuePair.Key, keyValuePair.Value); }
            }

            return dictionary;
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
 
            string[] keyNames;

            if (section == null)
            { throw new ArgumentNullException("sectionName"); }

            var keys = _iniData[section];
            keyNames = new string[keys.Count];
            int i = 0;
            foreach (var k in keys)
            {
                keyNames[i++] = k.KeyName;
            }
            return keyNames;
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
            string[] sectionNames;

            //Allocate a buffer for the returned section names. 
            var sections = _iniData.Sections;
            sectionNames = new string[sections.Count];
            int i = 0;
            foreach (var s in sections)
            {
                sectionNames[i++] = s.SectionName;
          
		    }

            return sectionNames;
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
           _iniData[section][key] = value;
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
            if (section == null|| section.Length == 0)
                throw new ArgumentNullException("sectionName");

            if (key == null || key.Length ==0)
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
