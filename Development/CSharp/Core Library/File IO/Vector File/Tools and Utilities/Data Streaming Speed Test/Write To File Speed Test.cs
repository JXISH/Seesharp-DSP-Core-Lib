using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UtilityWin32WriteBlockSizeTest
{
    public partial class MainForm : Form
    {
        #region------------------------- 常量 -------------------------

        private const int NumberOfFiles = 1;

        private readonly double[] BlockSizeItems = new double[] { 4194304, 8388608, 16777216, 33554432,67108864 };

        #endregion

        #region-------------------------私有成员 -------------------------

        private int _mode;
        private int _completedFileNumbers;
        private double _totalSpeed;
        private List<string> _drivesName;
        private List<double> _usedTime;
        private List<SingleFileParam> _files;
        private WriteStreamingDataAndTestSpeedTask[] _writeSpeedTask;
        private SerialWriteStreamingDataAndTestSpeedTask[] _serialWriteSpeedTask;
        private bool _ignoreFileCellValueChanged;
        private bool _ignoreNumberOfFilesChanged;

        #endregion

        #region------------------------- 构造函数 -------------------------

        public MainForm()
        {
            _usedTime = new List<double>();
            _files = new List<SingleFileParam>();
            InitializeComponent();
        }

        #endregion

        #region------------------------- GUI事件处理 -------------------------

        private void MainForm_Load(object sender, EventArgs e)
        {
            _guiWriteMode.SelectedIndex = 0;
            _completedFileNumbers = 0;
            _ignoreNumberOfFilesChanged = true;
            for (int i = 0; i < NumberOfFiles; i++)
            {
                var file = new SingleFileParam();
                file.BlockSize = 4194304;
                file.RecordFolder = "C:\\data";
                _files.Add(file);
            }
            UpdateConfigFormsControls();
            _ignoreNumberOfFilesChanged = false;

        }

        private void GuiNumberOfChannels_ValueChanged(object sender, EventArgs e)
        {

            if (!_ignoreNumberOfFilesChanged)
            {
                _ignoreFileCellValueChanged = true;
                int originNumberOfFiles = _files.Count;
                int newNumberOfFiles = (int)_guiNumberOfFiles.Value;

                // 如果增加文件数，新增加的和第一个文件属性配置一致
                if (originNumberOfFiles <= newNumberOfFiles)
                {
                    int addFiles = newNumberOfFiles - originNumberOfFiles;
                    if (addFiles != 0)
                    {
                        for (int i = 0; i < addFiles; i++)
                        {
                            var file = new SingleFileParam();
                            file.BlockSize = _files[0].BlockSize;
                            file.RecordFolder = _files[0].RecordFolder;
                            _files.Add(file);
                        }
                    }
                }

                // 如果减少通道数，移除最后的通道
                else
                {
                    int reduceFiles = originNumberOfFiles - newNumberOfFiles;
                    for (int i = 0; i < reduceFiles; i++) { _files.RemoveAt(_files.Count - 1); }
                }
                // 更新控件显示
                UpdataFileInfoGridView(_files.Count);

                _ignoreFileCellValueChanged = false;
            }
        }

        private void GuiStart_Click(object sender, EventArgs e)
        {
            _totalSpeed = 0;
            _completedFileNumbers = 0;
            _guiStart.Enabled = false;
            _guiStop.Enabled = true;
            _guiSpeedResultView.Rows.Clear();
            _guiTimerForStatus.Enabled = true;
            _mode = _guiWriteMode.SelectedIndex;
            if (_mode==0)
            {
                _writeSpeedTask = new WriteStreamingDataAndTestSpeedTask[_files.Count];
                 for (int i = 0; i < _files.Count; i++)
                {
                    _writeSpeedTask[i] = new WriteStreamingDataAndTestSpeedTask(i);
                    _writeSpeedTask[i].Length = (double)_guiWriteLength.Value;
                    _writeSpeedTask[i].NumberOfBlocks = (int)_guiWriteLength.Value;
                    _writeSpeedTask[i].DisableBuffering = _guiDisableBuffering.Checked;
                    _writeSpeedTask[i].TaskCompleted += MainForm_TaskCompleted;
                    _writeSpeedTask[i].FileParam.CopyFrom(_files[i]);
                    _writeSpeedTask[i].Start();
                }
            }
            else
            {
                _drivesName = new List<string>();
                for (int i = 0; i < _files.Count; i++)
                {
                    var drive = Path.GetPathRoot(_files[i].RecordFolder);
                    if (_drivesName.FindIndex(t => t.Equals(drive)) < 0) { _drivesName.Add(drive); }
                }
                var fileGroup = new List<SingleFileParam>[_drivesName.Count];
                for (int i = 0; i < _drivesName.Count; i++)
                {
                    fileGroup[i] = new List<SingleFileParam>();
                    for (int j = 0; j < _files.Count; j++)
                    {
                        var drive = Path.GetPathRoot(_files[j].RecordFolder);
                        if (drive == _drivesName[i]) { fileGroup[i].Add(_files[j]); }
                    }
                }
                _serialWriteSpeedTask = new SerialWriteStreamingDataAndTestSpeedTask[_drivesName.Count];
                for (int i=0;i< _drivesName.Count;i++)
                {
                    _serialWriteSpeedTask[i] = new SerialWriteStreamingDataAndTestSpeedTask();
                    _serialWriteSpeedTask[i].NumberOfBlocks = (double)_guiWriteLength.Value;
                    _serialWriteSpeedTask[i].TaskCompleted += MainForm_TaskCompleted;
                    _serialWriteSpeedTask[i].FileParam.AddRange(fileGroup[i]);
                    _serialWriteSpeedTask[i].Start();
                }
            }
        }

        private void MainForm_TaskCompleted(object sender, TaskCompletedEventArgs e)
        {
            if(_mode == 0)
            {
                _completedFileNumbers++;
                if (_completedFileNumbers == _files.Count)
                {
                    this.Invoke(new Action(() => { UpdataSpeedResultView(_files.Count); }));
                }
            }
            else
            {
                _completedFileNumbers++;
                if(_completedFileNumbers == _drivesName.Count)
                this.Invoke(new Action(() => { UpdataSpeedResultView(_files.Count); }));
            }
        }

        private void GuiStop_Click(object sender, EventArgs e)
        {
            if (_mode == 0)
            {
                for (int i = 0; i < _files.Count; i++)
                {
                    _writeSpeedTask[i].TaskCompleted -= MainForm_TaskCompleted;
                    _writeSpeedTask[i].Stop();
                }
            }
            else
            {
                for (int i = 0; i < _files.Count; i++)
                {
                    _serialWriteSpeedTask[i].TaskCompleted -= MainForm_TaskCompleted;
                    _serialWriteSpeedTask[i].Stop();
                }
            }
            UpdataSpeedResultView(_files.Count);
        }

        private void GuiFileView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!_ignoreFileCellValueChanged)
            {
                int fileIndex = e.RowIndex;
                if (fileIndex < 0 | e.ColumnIndex < 0) return;
                var selectValue = _guiFileView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                try
                {
                    switch (e.ColumnIndex)
                    {
                        case 1://第一列：<设置>文件写入的块大小
                            DataGridViewComboBoxCell groupNames = (DataGridViewComboBoxCell)_guiFileView.Rows[fileIndex].Cells["BlockSize"];
                            for (int j = 0; j < groupNames.Items.Count; j++)
                            {
                                if ((string)selectValue == Convert.ToString(groupNames.Items[j]))
                                {
                                    _files[fileIndex].BlockSize = BlockSizeItems[j]; break;
                                }
                            }
                            break;
                        case 2:
                            _files[fileIndex].RecordFolder = Convert.ToString(selectValue);
                            _guiSelectedChRecordFolder.Text = _files[fileIndex].RecordFolder;
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex) { throw ex; }
            }
        }

        private void GuiBrowseFolder_Click(object sender, EventArgs e)
        {
            if (_guiFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                _guiSelectedChRecordFolder.Text = _guiFolderBrowserDialog.SelectedPath;
            }
            var selectRowIndex = _guiFileView.CurrentRow.Index;
            _guiFileView.Rows[selectRowIndex].Cells["FileFolder"].Value = _guiSelectedChRecordFolder.Text;
            _files[selectRowIndex].RecordFolder = _guiSelectedChRecordFolder.Text;
        }

        #endregion

        #region------------------------- 私有方法 -------------------------

        private void UpdataSpeedResultView(int fileNumber)
        {
            _guiTimerForStatus.Enabled = false;
            _guiStart.Enabled = true;
            _guiStop.Enabled = false;
            _guiSpeedResultView.Rows.Clear();
            _guiSpeedResultView.Rows.Add(_files.Count);

            if(_mode==0)
            {
                for (int i = 0; i < _files.Count; i++)
                {
                    _guiSpeedResultView.Rows[i].Cells["FileCount"].Value = (i + 1).ToString();
                    _guiSpeedResultView.Rows[i].Cells["Speed"].Value = (_writeSpeedTask[i].WriteStatus.WriteSpeed / 1E6).ToString("f2");
                    _totalSpeed += (_writeSpeedTask[i].WriteStatus.WriteSpeed / 1E6);
                }
                _guiTotalSpeed.Text = _totalSpeed.ToString("f2");
                _guiToolProgressBar.Value = (int)_writeSpeedTask[0].WriteStatus.progress;
            }
            else
            {
                for (int i = 0; i < _drivesName.Count; i++)
                {
                    _guiSpeedResultView.Rows[i].Cells["FileCount"].Value = _drivesName[i];
                    _guiSpeedResultView.Rows[i].Cells["Speed"].Value = (_serialWriteSpeedTask[i].WriteStatus.WriteSpeed / 1E6).ToString("f2");
                    _totalSpeed += (_serialWriteSpeedTask[i].WriteStatus.WriteSpeed / 1E6);
                }
                _guiTotalSpeed.Text = _totalSpeed.ToString("f2");
                _guiToolProgressBar.Value = (int)_serialWriteSpeedTask[0].WriteStatus.progress;
            }
        }
        private void UpdataFileInfoGridView(int fileNumber)
        {
            DataGridViewComboBoxCell groupNames;
            _guiFileView.Rows.Clear();
            _guiFileView.Rows.Add(fileNumber);
            for (int i = 0; i < fileNumber; i++)
            {
                groupNames = (DataGridViewComboBoxCell)_guiFileView.Rows[i].Cells["BlockSize"];
                for (int j = 0; j < BlockSizeItems.Length; j++)
                {
                    if (BlockSizeItems[j] == _files[i].BlockSize)
                    {
                        _guiFileView.Rows[i].Cells["BlockSize"].Value = groupNames.Items[j]; break;
                    }
                }
                _guiFileView.Rows[i].Cells["Count"].Value = (i + 1).ToString();
                _guiFileView.Rows[i].Cells["FileFolder"].Value = _files[i].RecordFolder;
            }
        }

         private void UpdateConfigFormsControls()
        {
            int numberOfFiles = _files.Count;
            _guiNumberOfFiles.Value = numberOfFiles;
            UpdataFileInfoGridView(numberOfFiles);
            _guiSelectedChRecordFolder.Text = _guiFileView.Rows[0].Cells["FileFolder"].Value.ToString();
        }

        #endregion

        #region------------------------- 定时事件处理 -------------------------

        private void GuiTimerForStatus_Tick(object sender, EventArgs e)
        {
            _guiTimerForStatus.Enabled = false;
            if(_mode == 0)
            {
                _usedTime.Clear();
                for (int i = 0; i < _files.Count; i++)
                {
                    _usedTime.Add(_writeSpeedTask[i].WriteStatus.progress);
                }
            }
            else
            {
                for (int i = 0; i < _drivesName.Count; i++)
                {
                    _usedTime.Add(_serialWriteSpeedTask[i].WriteStatus.progress);
                }
            }
            _guiToolProgressBar.Value = (int)(_usedTime.Min());
            _guiTimerForStatus.Enabled = true;
        }

        #endregion

       
    }


}
