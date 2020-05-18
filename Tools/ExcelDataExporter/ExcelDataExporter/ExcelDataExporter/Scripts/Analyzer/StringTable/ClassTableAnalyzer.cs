using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data;
using Structure;

namespace Analyzer
{
    public class ClassTableAnalyzer : StructureTableAnalyzer
    {
        private const int m_idColmIndex = 0;
        private const int m_colmStartColmIndex = 1;
        private const int m_tagRowIndex = 1;
        private const int m_fieldNameRowIndex = 2;
        private const int m_filedStructureNameRowIndex = 3;
        private const int m_defaultValueRowIndex = 4;
        private const int m_foreignRowIndex = 5;
        private const int m_dataStartRowIndex = 7;
        private const int m_dataStartColmIndex = 1;
        private const string m_idNameStr = "Id";

        private Dictionary<int, FieldAnalyzer> m_fieldAnalyzerMap = new Dictionary<int, FieldAnalyzer>();
        
        public Action<BaseStructureInfo, string> actionOnAddMemeber;
        public Action<int, DataObject> actionOnAddData;

        public ClassTableAnalyzer(StringTable table, TableHeaderAnalyzer headerAnalyzer) : base(table, headerAnalyzer)
        {
        }

        public void AnalyzeMember(UserContext context)
        {
            AddMember(EBasicStructureType.Int32, m_idNameStr);
            for (int i = m_colmStartColmIndex; i < m_table.colmCount; ++i)
            {
                string tagStr = m_table[m_tagRowIndex, i];
                if (string.IsNullOrEmpty(tagStr))
                {
                    continue;
                }
                string memberStructureNameStr = m_table[m_filedStructureNameRowIndex, i];
                string memberNameStr = m_table[m_fieldNameRowIndex, i];
                string memberName = m_table[m_fieldNameRowIndex, i];
                string defaultValue = m_table[m_defaultValueRowIndex, i];
                string foreign = m_table[m_foreignRowIndex, i];
                FieldAnalyzer fieldAnalyzer = new FieldAnalyzer(tagStr, memberStructureNameStr, memberName, defaultValue, foreign);
                fieldAnalyzer.Analyze();
                AddMember(fieldAnalyzer.structureInfo, memberNameStr);
                m_fieldAnalyzerMap.Add(i, fieldAnalyzer);
            }
        }

        public void CheckDefaultValue(UserContext context)
        {
            foreach (var kv in m_fieldAnalyzerMap)
            {
                FieldAnalyzer fieldAnalyzer = kv.Value;
                fieldAnalyzer.CheckDefaultValid();
            }
        }

        public void AnalyzeData(UserContext context)
        {
            for (int i = m_dataStartRowIndex; i < m_table.rowCount; ++i)
            {
                string idStr = m_table[i, m_idColmIndex].Trim();
                if (string.IsNullOrEmpty(idStr))
                {
                    continue;
                }
                int id;
                if (!int.TryParse(idStr, out id))
                {
                    Debug.LogError(StaticString.WrongFieldValueFormat, name, "Id", idStr);
                    continue;
                }
                DataObject dataObject = new DataObject();
                for (int j = m_dataStartColmIndex; j < m_table.colmCount; ++j)
                {
                    string valueStr = m_table[i, j];
                    FieldAnalyzer fieldAnalyzer;
                    if (!m_fieldAnalyzerMap.TryGetValue(j, out fieldAnalyzer))
                    {
                        continue;
                    }
                    valueStr = string.IsNullOrEmpty(valueStr) ? fieldAnalyzer.defaultValue : valueStr;
                    if (string.IsNullOrEmpty(valueStr))
                    {
                        continue;
                    }
                    DataField field = new DataField();
                    field.structureInfo = fieldAnalyzer.structureInfo;
                    field.name = fieldAnalyzer.name;
                    field.value = AnalyzeDataValue(field.structureInfo, fieldAnalyzer.complexMemberNameList, valueStr);
                    dataObject.fieldList.Add(field);
                }
                AddData(id, dataObject);
            }
        }

        private void AddMember(EBasicStructureType structureType, string memberName)
        {
            BaseStructureInfo structureInfo = StructureManager.Instance.GetBasicStructureInfo(structureType);
            if (structureInfo == null)
            {
                return;
            }
            if (actionOnAddMemeber != null)
            {
                actionOnAddMemeber(structureInfo, memberName);
            }
        }

        private void AddMember(BaseStructureInfo structureInfo, string memberName)
        {
            if (actionOnAddMemeber != null)
            {
                actionOnAddMemeber(structureInfo, memberName);
            }
        }

        private void AddData(int id, DataObject dataObject)
        {
            if (actionOnAddData != null)
            {
                actionOnAddData(id, dataObject);
            }
        }

        private DataValue AnalyzeDataValue(BaseStructureInfo structureInfo, List<string> memberNameList, string str)
        {
            switch (structureInfo.structureType)
            {
                case EStructureType.Basic:
                    return AnalyzeBasicDataValue(structureInfo as BasicStructureInfo, str);
                case EStructureType.Enum:
                    return AnalyzeEnumDataValue(structureInfo as EnumStructureInfo, str);
                case EStructureType.Class:
                    return AnalyzeClassDataValue(structureInfo as ClassStructureInfo, memberNameList, str);
                case EStructureType.List:
                    return AnalyzeListDataValue(structureInfo as ListStructureInfo, memberNameList, str);
            }
            return null;
        }

        private DataValue AnalyzeBasicDataValue(BasicStructureInfo structureInfo, string str)
        {
            BasicDataValue value = new BasicDataValue();
            value.value = str;
            return value;
        }

        private DataValue AnalyzeEnumDataValue(EnumStructureInfo structureInfo, string str)
        {
            EnumDataValue value = new EnumDataValue();
            EnumMemberInfo memberInfo = structureInfo.GetMember(str);
            int v;
            if (memberInfo != null)
            {
                value.value = memberInfo.index;
            }
            else if (int.TryParse(str, out v))
            {
                value.value = v;
            }
            else
            {
                Debug.LogError("");
                return null;
            }
            return value;
        }

        private DataValue AnalyzeClassDataValue(ClassStructureInfo structureInfo, List<string> memberNameList, string str)
        {
            ClassDataValue dataValue = new ClassDataValue();
            string[] valueSplits = str.Split(new[] {','}, null, null, false, true);
            if (memberNameList == null || memberNameList.Count != valueSplits.Length)
            {
                Debug.LogError("");
                return null;
            }
            for (int i = 0; i < valueSplits.Length; ++i)
            {
                string memberName = memberNameList[i];
                ClassMemberInfo memberInfo = structureInfo.GetMember(memberName);
                DataValue value = AnalyzeDataValue(memberInfo.structureInfo, null, valueSplits[i]);
                DataField field = new DataField();
                field.structureInfo = memberInfo.structureInfo;
                field.name = memberName;
                field.value = value;
                dataValue.value.fieldList.Add(field);
            }
            return dataValue;
        }

        private DataValue AnalyzeListDataValue(ListStructureInfo structureInfo, List<string> memberNameList, string str)
        {
            ListDataValue dataValue = new ListDataValue();
            string[] itemSplits = str.Split(new[] {'|'}, null, null, false, true);
            for (int i = 0; i < itemSplits.Length; ++i)
            {
                DataValue itemValue = AnalyzeDataValue(structureInfo.valueStructureInfo, memberNameList, itemSplits[i]);
                dataValue.valueList.Add(itemValue);
            }
            return dataValue;
        }

    }
}
