using SeeSharpTools.JXI.FileIO.VectorFile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace IniFileReadExample
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private IniFileHandler _iniFile;
        List<KeyValuePair<string, string>>[] _iniPair;
        int rowsMax = 0;

        /// <summary>
        /// 选择文件并读出全部信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GuiReadFile_Click(object sender, EventArgs e)
        {
            if (_iniFile == null)
            {
                if (_guiFileBrowseDialog.ShowDialog() == DialogResult.Cancel) { return; }

                // 获取文件名。
                _guiFilePath.Text = _guiFileBrowseDialog.FileName;
                // 实例化INI文件的对象。
                _iniFile = new IniFileHandler(_guiFilePath.Text);
            }
            // 获取section
            string[] sectionName = _iniFile.GetSectionNames();
            _iniPair = new List<KeyValuePair<string, string>>[sectionName.Length];
            // 获取文件中的全部信息。
            for (int i = 0; i < sectionName.Length; i++)
            {
                _iniPair[i] = _iniFile.GetSectionAsListOfPairs(sectionName[i]);
                // 获取最大行数
                if (rowsMax < _iniPair[i].Count) { rowsMax = _iniPair[i].Count; }
            }

            // 先清空显示控件。
            _guiDataGridView.Rows.Clear();
            // 设置显示控件的行数和列数 。           
            _guiDataGridView.ColumnCount = 2 * sectionName.Length;
            _guiDataGridView.Rows.Add(rowsMax + 1);
            // 显示全部信息          
            for (int i = 0; i < _iniPair.Length; i++)
            {
                // 由于各个section的长度不同，因此禁止重新排列
                _guiDataGridView.Columns[i * 2].SortMode = DataGridViewColumnSortMode.NotSortable;
                _guiDataGridView.Columns[i * 2 + 1].SortMode = DataGridViewColumnSortMode.NotSortable;
                _guiDataGridView.Columns[i * 2].HeaderText = sectionName[i];
                _guiDataGridView.Rows[0].Cells[i].Value = "Key";
                _guiDataGridView.Rows[0].Cells[i + 1].Value = "Value";
                for (int j = 0; j < _iniPair[i].Count; j++)
                {
                    _guiDataGridView.Rows[j + 1].Cells[i * 2].Value = _iniPair[i][j].Key;
                    _guiDataGridView.Rows[j + 1].Cells[i * 2 + 1].Value = _iniPair[i][j].Value;
                }
            }
            _guiDataGridView.Font = new Font("Cambria", 12, FontStyle.Regular);
            _guiDataGridView.Refresh();
        }

        /// <summary>
        /// 增加信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _GuiAddINI_Click(object sender, EventArgs e)
        {
            _iniFile.WriteKey(_guiSection.Text, _guiKey.Text, _guiValue.Text);
            GuiReadFile_Click(sender, e);
        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GuiDeleteINI_Click(object sender, EventArgs e)
        {
            int sectionIndex = _guiDataGridView.CurrentCell.ColumnIndex / 2;
            int keyIndex = _guiDataGridView.CurrentCell.RowIndex - 1;
            if (keyIndex > _iniPair[sectionIndex].Count) { return; }
            // 删除key
            _iniFile.DeleteKey(_iniFile.GetSectionNames()[sectionIndex], _iniPair[sectionIndex][keyIndex].Key);
            GuiReadFile_Click(sender, e);
        }


    }
}
