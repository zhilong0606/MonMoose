using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using Structure;

namespace ExcelDataExporter
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string m_clientTagName = "Client";
        private const string m_serverTagName = "Server";
        private const string m_allTagName = "All";

        private Dictionary<int, ExportMode> m_exportModeMap = new Dictionary<int, ExportMode>();
        private Dictionary<string, Dictionary<EExportModeType, CheckBox>> m_exportModeCheckBoxGroupMap = new Dictionary<string, Dictionary<EExportModeType, CheckBox>>();

        public MainWindow()
        {
            InitializeExporter();
            InitializeComponent();
            InitializeWidget();
            Config.LoadConfig();
            InitializeByConfig();
        }

        private void InitializeExporter()
        {
            //Dictionary<int, string[]> basicNamesMap = new Dictionary<int, string[]>()
            //{
            //    //------------------------------------------Flat---------Proto-------Teet--------Json
            //    {(int)EBasicStructureType.Bool,     new[] {"boolean",   "bool",     "bool",     "Boolean"}},
            //    {(int)EBasicStructureType.Int8,     new[] {"integer",   "int32",    "int8",     "Integer"}},
            //    {(int)EBasicStructureType.Int16,    new[] {"integer",   "int32",    "int16",    "Integer"}},
            //    {(int)EBasicStructureType.Int32,    new[] {"integer",   "int32",    "int32",    "Integer"}},
            //    {(int)EBasicStructureType.Int64,    new[] {"long",      "int64",    "int64",    "Integer"}},
            //    {(int)EBasicStructureType.Single,   new[] {"float",     "float",    "single",   "Number"}},
            //    {(int)EBasicStructureType.Double,   new[] {"double",    "double",   "double",   "Number"}},
            //    {(int)EBasicStructureType.String,   new[] {"string",    "string",   "string",   "String"}},
            //};
            Dictionary<int, Type[]> exportTypesMap = new Dictionary<int, Type[]>
            {
                {(int)EExportModeType.Flat, new[] {typeof(FlatStructureExporter), typeof(FlatDataExporter), typeof(ProtoLoaderExporter) }},
                {(int)EExportModeType.Proto, new[] {typeof(ProtoStructureExporter), typeof(ProtoDataExporter), typeof(ProtoLoaderExporter)}},
                {(int)EExportModeType.Teet, new[] {typeof(TeetStructureExporter), typeof(TeetDataExporter), typeof(ProtoLoaderExporter) }},
                {(int)EExportModeType.Json, new[] {typeof(JsonStructureExporter), typeof(JsonDataExporter), typeof(ProtoLoaderExporter) }},
            };
            foreach (var kv in exportTypesMap)
            {
                ExportMode mode = new ExportMode();
                mode.modeType = (EExportModeType)kv.Key;
                mode.structureExporterType = kv.Value[0];
                mode.dataExporterType = kv.Value[1];
                mode.loaderExporterType = kv.Value[2];
                m_exportModeMap.Add(kv.Key, mode);
            }
        }

        private void InitializeWidget()
        {
            Dictionary<EExportModeType, CheckBox> clientExportModeCheckBoxMap = new Dictionary<EExportModeType, CheckBox>();
            clientExportModeCheckBoxMap.Add(EExportModeType.Flat, clientExportFlatCheckBox);
            clientExportModeCheckBoxMap.Add(EExportModeType.Proto, clientExportProtoCheckBox);
            clientExportModeCheckBoxMap.Add(EExportModeType.Teet, clientExportTeetCheckBox);
            clientExportModeCheckBoxMap.Add(EExportModeType.Json, clientExportJsonCheckBox);
            m_exportModeCheckBoxGroupMap.Add(m_clientTagName, clientExportModeCheckBoxMap);

            Dictionary<EExportModeType, CheckBox> serverExportModeCheckBoxMap = new Dictionary<EExportModeType, CheckBox>();
            serverExportModeCheckBoxMap.Add(EExportModeType.Flat, serverExportFlatCheckBox);
            serverExportModeCheckBoxMap.Add(EExportModeType.Proto, serverExportProtoCheckBox);
            serverExportModeCheckBoxMap.Add(EExportModeType.Teet, serverExportTeetCheckBox);
            serverExportModeCheckBoxMap.Add(EExportModeType.Json, serverExportJsonCheckBox);
            m_exportModeCheckBoxGroupMap.Add(m_serverTagName, serverExportModeCheckBoxMap);
        }

        private void InitializeByConfig()
        {
            excelFolderPathButton.Content = Config.instance.excelFolderPath;

            clientExportCheckBox.IsChecked = Config.instance.client.needExport;
            clientNameSpaceTextBox.Text = Config.instance.client.nameSpace;
            clientPrefixTextBox.Text = Config.instance.client.prefix;
            clientPostfixTextBox.Text = Config.instance.client.postfix;
            clientStructureExportPathButton.Content = Config.instance.client.structureExportPath;
            clientDataExportPathButton.Content = Config.instance.client.dataExportPath;
            clientExportFlatCheckBox.IsChecked = Config.instance.client.exportMode == EExportModeType.Flat;
            clientExportProtoCheckBox.IsChecked = Config.instance.client.exportMode == EExportModeType.Proto;
            clientExportTeetCheckBox.IsChecked = Config.instance.client.exportMode == EExportModeType.Teet;
            clientExportJsonCheckBox.IsChecked = Config.instance.client.exportMode == EExportModeType.Json;

            serverExportCheckBox.IsChecked = Config.instance.server.needExport;
            serverNameSpaceTextBox.Text = Config.instance.server.nameSpace;
            serverPrefixTextBox.Text = Config.instance.server.prefix;
            serverPostfixTextBox.Text = Config.instance.server.postfix;
            serverStructureExportPathButton.Content = Config.instance.server.structureExportPath;
            serverDataExportPathButton.Content = Config.instance.server.dataExportPath;
            serverExportFlatCheckBox.IsChecked = Config.instance.server.exportMode == EExportModeType.Flat;
            serverExportProtoCheckBox.IsChecked = Config.instance.server.exportMode == EExportModeType.Proto;
            serverExportTeetCheckBox.IsChecked = Config.instance.server.exportMode == EExportModeType.Teet;
            serverExportJsonCheckBox.IsChecked = Config.instance.server.exportMode == EExportModeType.Json;
            
            generateStructureCheckBox.IsChecked = Config.instance.needGenerateStructure;
            generateDataCheckBox.IsChecked = Config.instance.needGenerateData;
            generateLoaderCheckBox.IsChecked = Config.instance.needGenerateLoader;

        }

        private void excelFolderPathButton_Click(object sender, RoutedEventArgs e)
        {
            Config.instance.excelFolderPath = SelectPath(Config.instance.excelFolderPath);
            excelFolderPathButton.Content = Config.instance.excelFolderPath;

            Config.SaveConfig();
        }

        private void excelFolderPathGotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenPath(Config.instance.excelFolderPath);
        }

        private void clientStructureExportPathButton_Click(object sender, RoutedEventArgs e)
        {
            string path = Config.instance.client.structureExportPath;
            path = SelectPath(path);
            clientStructureExportPathButton.Content = path;
            Config.instance.client.structureExportPath = path;

            Config.SaveConfig();
        }

        private void clientStructureExportPathGotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenPath(Config.instance.client.structureExportPath);
        }

        private void clientDataExportPathButton_Click(object sender, RoutedEventArgs e)
        {
            string path = Config.instance.client.dataExportPath;
            path = SelectPath(path);
            clientDataExportPathButton.Content = path;
            Config.instance.client.dataExportPath = path;

            Config.SaveConfig();
        }

        private void clientDataExportPathGotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenPath(Config.instance.client.dataExportPath);
        }

        private void serverStructureExportPathButton_Click(object sender, RoutedEventArgs e)
        {
            string path = Config.instance.server.structureExportPath;
            path = SelectPath(path);
            serverStructureExportPathButton.Content = path;
            Config.instance.server.structureExportPath = path;

            Config.SaveConfig();
        }

        private void serverStructureExportPathGotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenPath(Config.instance.server.structureExportPath);
        }

        private void serverDataExportPathButton_Click(object sender, RoutedEventArgs e)
        {
            string path = Config.instance.server.dataExportPath;
            path = SelectPath(path);
            serverDataExportPathButton.Content = path;
            Config.instance.server.dataExportPath = path;

            Config.SaveConfig();
        }

        private void serverDataExportPathGotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenPath(Config.instance.server.dataExportPath);
        }

        private void exportBySelectProcessButton_Click(object sender, RoutedEventArgs e)
        {
            ExportContext context = new ExportContext();
            context.needGenerateStructure = Config.instance.needGenerateStructure;
            context.needGenerateData = Config.instance.needGenerateData;
            context.needGenerateLoader = Config.instance.needGenerateLoader;

            UserContext clientContext = new UserContext();
            Config.instance.client.nameSpace = clientNameSpaceTextBox.Text;
            Config.instance.client.prefix = clientPrefixTextBox.Text;
            Config.instance.client.postfix = clientPostfixTextBox.Text;
            UpdateContext(clientContext, m_clientTagName, Config.instance.client);
            context.userContextList.Add(clientContext);

            UserContext serverContext = new UserContext();
            Config.instance.server.nameSpace = serverNameSpaceTextBox.Text;
            Config.instance.server.prefix = serverPrefixTextBox.Text;
            Config.instance.server.postfix = serverPostfixTextBox.Text;
            UpdateContext(serverContext, m_serverTagName, Config.instance.server);
            context.userContextList.Add(serverContext);

            Config.SaveConfig();

            ProgramProcessStep m_program = new ProgramProcessStep(context);
            m_program.actionOnProcessMsgSend = OnProcessMsgSend;
            m_program.Execute();
        }

        private void UpdateContext(UserContext ctx, string name, Config.User user)
        {
            ctx.needExport = user.needExport;
            ctx.tagNameList.Add(name);
            ctx.tagNameList.Add(m_allTagName);
            ctx.name = name;
            ctx.namespaceStr = user.nameSpace;
            ctx.prefixStr = user.prefix;
            ctx.postfixStr = user.postfix;
            ctx.structureExportPath = user.structureExportPath;
            ctx.dataExportPath = user.dataExportPath;
            ctx.exportMode = m_exportModeMap[(int)user.exportMode];
        }

        private void clientExportCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Config.instance.client.needExport = true;

            Config.SaveConfig();
        }

        private void clientExportCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Config.instance.client.needExport = false;

            Config.SaveConfig();
        }

        private void serverExportCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Config.instance.server.needExport = true;

            Config.SaveConfig();
        }

        private void serverExportCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Config.instance.server.needExport = false;

            Config.SaveConfig();
        }

        private void exportModeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateExportModeCheckBoxGroup(sender as CheckBox);
        }

        private void exportModeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateExportModeCheckBoxGroup(sender as CheckBox);
        }

        private void OnProcessMsgSend(double processValue, string content)
        {
            exportProcessBar.Dispatcher.Invoke(new Action<DependencyProperty, object>(exportProcessBar.SetValue), DispatcherPriority.Background, RangeBase.ValueProperty, processValue * 100);
            exportProcessLabel.Dispatcher.Invoke(new Action<string>(UpdateProcessContent), DispatcherPriority.Background, content);
        }

        private void UpdateProcessContent(string content)
        {
            exportProcessLabel.Content = content;
        }

        private void UpdateExportModeCheckBoxGroup(CheckBox checkBox)
        {
            string tagName = string.Empty;
            Dictionary<EExportModeType, CheckBox> exportModeCheckBoxMap = null;
            foreach (var kv in m_exportModeCheckBoxGroupMap)
            {
                if (kv.Value.ContainsValue(checkBox))
                {
                    tagName = kv.Key;
                    exportModeCheckBoxMap = kv.Value;
                    break;
                }
            }
            if (exportModeCheckBoxMap == null)
            {
                return;
            }
            int checkCount = 0;
            foreach (var kv in exportModeCheckBoxMap)
            {
                if (kv.Value.IsChecked.HasValue && kv.Value.IsChecked.Value)
                {
                    checkCount++;
                }
            }
            if (checkCount == 0)
            {
                checkBox.IsChecked = true;
            }
            else if (checkCount > 1)
            {
                foreach (var kv in exportModeCheckBoxMap)
                {
                    if (kv.Value.IsChecked.HasValue && kv.Value.IsChecked.Value && kv.Value != checkBox)
                    {
                        kv.Value.IsChecked = false;
                    }
                }
            }
            EExportModeType exportModeType = EExportModeType.None;
            foreach (var kv in exportModeCheckBoxMap)
            {
                if (kv.Value == checkBox)
                {
                    exportModeType = kv.Key;
                    break;
                }
            }
            if (tagName == m_clientTagName)
            {
                Config.instance.client.exportMode = exportModeType;
            }
            else if (tagName == m_serverTagName)
            {
                Config.instance.server.exportMode = exportModeType;
            }

            Config.SaveConfig();
        }

        private string SelectPath(string path)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.DefaultDirectory = path;
            CommonFileDialogResult result = dialog.ShowDialog();
            string newPath = path;
            if (result == CommonFileDialogResult.Ok)
            {
                newPath =  dialog.FileName;
            }
            string relatePath;
            if (PathUtility.TryGetRelativePosition(newPath, out relatePath))
            {
                return relatePath;
            }
            return path;
        }

        private void OpenPath(string path)
        {
            PathUtility.TryGetAbsolutePosition(path, out path);
            System.Diagnostics.Process.Start("explorer.exe", path);
        }
    }
}
