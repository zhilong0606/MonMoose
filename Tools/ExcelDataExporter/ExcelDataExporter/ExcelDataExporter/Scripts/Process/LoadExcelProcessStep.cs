using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analyzer;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Structure;

public class LoadExcelProcessStep : ExportContextProcessStep
{
    private List<StringTable> m_tableList = new List<StringTable>();
    private List<StructureGroupAnalyzer> m_structureAnalyzerList = new List<StructureGroupAnalyzer>();

    protected override EExportProcessType processType
    {
        get { return EExportProcessType.LoadExcel; }
    }

    public LoadExcelProcessStep(ExportContext context) : base(context)
    {
    }

    protected override void OnExecute()
    {
        SendMsg(0, "开始加载表格");
        InitStringTableList();
        AnlayzeStringTable();
        AnalyzeStructureGroup();
        SendMsg(1, "加载表格完毕");
    }

    private void InitStringTableList()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Config.instance.excelFolderPath);
        List<FileInfo> fileInfoList = new List<FileInfo>(directoryInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly));
        for (int i = fileInfoList.Count - 1; i >= 0; --i)
        {
            FileInfo fileInfo = fileInfoList[i];
            if (fileInfo.Name.EndsWith("-"))
            {
                FileAttributes attr = File.GetAttributes(fileInfo.FullName);
                File.SetAttributes(fileInfo.FullName, attr & ~FileAttributes.ReadOnly);
                File.Delete(fileInfo.FullName);
                fileInfoList.RemoveAt(i);
            }
            if (fileInfo.Name.StartsWith("~$"))
            {
                fileInfoList.RemoveAt(i);
            }
        }
        for (int i = 0; i < fileInfoList.Count; ++i)
        {
            FileInfo fileInfo = fileInfoList[i];
            SendMsg((double)i * 100 / fileInfoList.Count, string.Format("正在加载表: {0}", fileInfo.Name));
            string newFilePath = string.Format("{0}{1}", fileInfo.FullName, "-");
            File.Copy(fileInfo.FullName, newFilePath);
            FileAttributes attr = File.GetAttributes(newFilePath);
            File.SetAttributes(newFilePath, attr & ~FileAttributes.ReadOnly);
            LoadExcel(newFilePath);
            File.Delete(newFilePath);
        }
    }

    private void LoadExcel(string path)
    {
        IWorkbook workbook = null;
        int sheetCount = 0;
        if (path.Contains(".xlsx"))
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                XSSFWorkbook wb = new XSSFWorkbook(fs);
                sheetCount = wb.Count;
                workbook = wb;
            }
        }
        else if (path.Contains(".xls"))
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                HSSFWorkbook wb = new HSSFWorkbook(fs);
                sheetCount = wb.Count;
                workbook = wb;
            }
        }
        if (workbook != null)
        {
            for (int i = 0; i < sheetCount; ++i)
            {
                ISheet worksheet = workbook.GetSheetAt(i);
                if (worksheet != null)
                {
                    LoadSheet(worksheet);
                }
            }
        }
    }

    private void LoadSheet(ISheet worksheet)
    {
        int rowCount = worksheet.LastRowNum;
        List<List<string>> grid = new List<List<string>>(rowCount);
        for (int i = 0; i <= rowCount; ++i)
        {
            IRow row = worksheet.GetRow(i);
            if (row == null)
            {
                continue;
            }
            List<string> list = new List<string>(row.LastCellNum);
            grid.Add(list);
            for (int j = 0; j < row.LastCellNum; ++j)
            {
                ICell cell = row.GetCell(j);
                string cellValue = null;
                if (cell != null)
                {
                    cellValue = LoadCell(cell).Trim();
                }
                list.Add(cellValue);
            }
        }
        StringTable table = new StringTable(grid);
        m_tableList.Add(table);
    }

    private string LoadCell(ICell cell)
    {
        if (cell != null)
        {
            string format = cell.CellStyle.GetDataFormatString();
            if (format == "General")
            {
                format = string.Empty;
            }
            switch (cell.CellType)
            {
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString(format);
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Formula:
                    switch (cell.CachedFormulaResultType)
                    {
                        case CellType.Boolean:
                            return cell.BooleanCellValue.ToString();
                        case CellType.Numeric:
                            return cell.NumericCellValue.ToString(format);
                        case CellType.String:
                            return cell.StringCellValue;
                    }
                    break;
            }
        }
        return string.Empty;
    }

    private void AnlayzeStringTable()
    {
        foreach(StringTable table in m_tableList)
        {
            TableHeaderAnalyzer tableHeaderAnalyzer = new TableHeaderAnalyzer(table);
            tableHeaderAnalyzer.Analyze();
            StructureTableAnalyzer structureTableAnalyzer = null;
            switch (tableHeaderAnalyzer.dataType)
            {
                case ETableDataType.Data:
                    structureTableAnalyzer = new ClassTableAnalyzer(table, tableHeaderAnalyzer);
                    break;
                case ETableDataType.Enum:
                    structureTableAnalyzer = new EnumTableAnalyzer(table, tableHeaderAnalyzer);
                    break;
                default:
                    continue;
            }
            string structureName = tableHeaderAnalyzer.name;
            StructureGroupAnalyzer structureGroupAnalyzer = GetStructureAnalyzer(structureName);
            if (structureGroupAnalyzer == null)
            {
                switch (tableHeaderAnalyzer.dataType)
                {
                    case ETableDataType.Data:
                        structureGroupAnalyzer = new ClassGroupAnalyzer(structureName);
                        break;
                    case ETableDataType.Enum:
                        structureGroupAnalyzer = new EnumGroupAnalyzer(structureName);
                        break;
                    default:
                        continue;
                }
                structureGroupAnalyzer.CreateStructure();
                m_structureAnalyzerList.Add(structureGroupAnalyzer);
            }
            structureGroupAnalyzer.AddTableAnalyzer(structureTableAnalyzer);
        }
    }

    private void AnalyzeStructureGroup()
    {
        for (int i = 0; i < (int)EAnalyzeStep.Max; ++i)
        {
            foreach (StructureGroupAnalyzer analyzer in m_structureAnalyzerList)
            {
                analyzer.AnalyzeStep((EAnalyzeStep)i, null);
            }
        }
    }

    private StructureGroupAnalyzer GetStructureAnalyzer(string name)
    {
        for (int i = 0; i < m_structureAnalyzerList.Count; ++i)
        {
            if (m_structureAnalyzerList[i].name == name)
            {
                return m_structureAnalyzerList[i];
            }
        }
        return null;
    }
}
