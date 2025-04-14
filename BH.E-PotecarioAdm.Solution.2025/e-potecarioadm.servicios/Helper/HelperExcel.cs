using BH.EPotecario.Adm.Dominio.Excel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BH.EPotecario.Adm.Dominio.RegistroExcelBeneficio;
using static NPOI.SS.Formula.Functions.Countif;

namespace BH.EPotecario.Adm.Servicios
{
    public static class HelperExcel
    {
        public static TipoArchivoExcel ObtenerTipoArchivoExcel(Stream excelStream)
        {
            // Crear el libro de trabajo desde el stream
            IWorkbook workbook = new XSSFWorkbook(excelStream);

            // Obtener la primera hoja
            ISheet sheet = workbook.GetSheetAt(0);

            //Obtengo la primera fila
            IRow row = sheet.GetRow(0);

            if (row.Cells.Count == 13)  //Posible archivo de BENEFICIOS
                return TipoArchivoExcel.ExcelBeneficios;
            else if (row.Cells.Count == 14)  //Posible archivo de ALIANZAS
                return TipoArchivoExcel.ExcelAlianzas;
            else if (row.Cells.Count == 11)  //Posible archivo de SUCURSALES
                return TipoArchivoExcel.ExcelSucursales;
            return 0;
        }

        public static bool GetCellValueAsBool(ICell cell, bool defaultValue = false)
        {
            if (cell == null)
                return defaultValue;

            try
            {
                switch (cell.CellType)
                {
                    case CellType.Boolean:
                        return cell.BooleanCellValue;
                    case CellType.Numeric:
                        return cell.NumericCellValue != 0;
                    case CellType.String:
                        string boolStr = cell.StringCellValue.ToLower();
                        if (boolStr == "1" || boolStr == "true" || boolStr == "sí" || boolStr == "si" || boolStr == "s" || boolStr == "yes" || boolStr == "y")
                            return true;
                        if (boolStr == "0" || boolStr == "false" || boolStr == "no" || boolStr == "n")
                            return false;
                        return defaultValue;
                    case CellType.Formula:
                        if (cell.CachedFormulaResultType == CellType.Boolean)
                            return cell.BooleanCellValue;
                        if (cell.CachedFormulaResultType == CellType.Numeric)
                            return cell.NumericCellValue != 0;
                        if (cell.CachedFormulaResultType == CellType.String)
                        {
                            boolStr = cell.StringCellValue.ToLower();
                            if (boolStr == "1" || boolStr == "true" || boolStr == "sí" || boolStr == "si" || boolStr == "s" || boolStr == "yes" || boolStr == "y")
                                return true;
                            if (boolStr == "0" || boolStr == "false" || boolStr == "no" || boolStr == "n")
                                return false;
                        }
                        return defaultValue;
                    default:
                        return defaultValue;
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        public static decimal? GetCellValueAsDecimal(ICell cell)
        {
            if (cell == null)
                return null;

            try
            {
                switch (cell.CellType)
                {
                    case CellType.Numeric:
                        return (decimal)cell.NumericCellValue;
                    case CellType.String:
                        string decimalStr = cell.StringCellValue;
                        decimal result;
                        if (decimal.TryParse(decimalStr, out result))
                            return result;
                        return null;
                    case CellType.Formula:
                        if (cell.CachedFormulaResultType == CellType.Numeric)
                            return (decimal)cell.NumericCellValue;
                        if (cell.CachedFormulaResultType == CellType.String)
                        {
                            decimalStr = cell.StringCellValue;
                            if (decimal.TryParse(decimalStr, out result))
                                return result;
                        }
                        return null;
                    default:
                        return null;
                }
            }
            catch
            {
                return null;
            }
        }

        // Método para obtener valores enteros de celdas
        public static int? GetCellValueAsInt(ICell cell)
        {
            if (cell == null)
                return null;

            try
            {
                switch (cell.CellType)
                {
                    case CellType.Numeric:
                        return (int)cell.NumericCellValue;
                    case CellType.String:
                        string intStr = cell.StringCellValue;
                        int result;
                        if (int.TryParse(intStr, out result))
                            return result;
                        return null;
                    case CellType.Formula:
                        if (cell.CachedFormulaResultType == CellType.Numeric)
                            return (int)cell.NumericCellValue;
                        if (cell.CachedFormulaResultType == CellType.String)
                        {
                            intStr = cell.StringCellValue;
                            if (int.TryParse(intStr, out result))
                                return result;
                        }
                        return null;
                    default:
                        return null;
                }
            }
            catch
            {
                return null;
            }
        }

        // Versión sobrecargada con valor predeterminado
        public static int GetCellValueAsInt(ICell cell, int defaultValue = 0)
        {
            int? value = GetCellValueAsInt(cell);
            return value ?? defaultValue;
        }

        // Métodos auxiliares para obtener valores de celdas
        public static string GetCellValueAsString(ICell cell)
        {
            if (cell == null)
                return string.Empty;

            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.StringCellValue?.Trim() ?? string.Empty;
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                        return cell.DateCellValue.ToString();// ("dd/MM/yyyy");
                    return cell.NumericCellValue.ToString();
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Formula:
                    switch (cell.CachedFormulaResultType)
                    {
                        case CellType.String:
                            return cell.StringCellValue?.Trim() ?? string.Empty;
                        case CellType.Numeric:
                            if (DateUtil.IsCellDateFormatted(cell))
                                return cell.DateCellValue.ToString();// ("dd/MM/yyyy");
                            return cell.NumericCellValue.ToString();
                        default:
                            return string.Empty;
                    }
                default:
                    return string.Empty;
            }
        }

        public static DateTime? GetCellValueAsDateTime(ICell cell)
        {
            if (cell == null)
                return null;

            try
            {
                switch (cell.CellType)
                {
                    case CellType.Numeric:
                        if (DateUtil.IsCellDateFormatted(cell))
                            return cell.DateCellValue;
                        return null;
                    case CellType.String:
                        string dateStr = cell.StringCellValue;
                        DateTime result;
                        if (DateTime.TryParse(dateStr, out result))
                            return result;
                        return null;
                    case CellType.Formula:
                        if (cell.CachedFormulaResultType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
                            return cell.DateCellValue;
                        if (cell.CachedFormulaResultType == CellType.String)
                        {
                            dateStr = cell.StringCellValue;
                            if (DateTime.TryParse(dateStr, out result))
                                return result;
                        }
                        return null;
                    default:
                        return null;
                }
            }
            catch
            {
                return null;
            }
        }


    }
}
