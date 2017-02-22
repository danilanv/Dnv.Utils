using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dnv.Utils
{

    public class StringHelper
    {
        Regex _regex = new Regex("[ ]{2,}", RegexOptions.None);

        /// <summary>
        /// Заменяет множественные пробелы на 1
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ReplaceMultipleSpaces(string input)
        {
            return _regex.Replace(input, " ");
        }

        /// <summary>
        /// Удаляет перевод на новуюстроку и заменяет множественные пробелы на 1.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SanitizeString(string input)
        {
            var t = input.Trim().Replace("\n", "").Replace("\t", "").Replace("\r", "");
            return ReplaceMultipleSpaces(t);
        }

        /// <summary>
        /// 
        /// </summary>
        public string LastErrorMessage { get; protected set; }

        /// <summary>
        /// Преобразует строку с ценой во float. Цена моет содержать в качестве разделителя знаки ',' или '.'. Определяется автоматически.
        /// </summary>
        /// <param name="priceString">Строка с ценой.</param>
        /// <param name="defaultPrice">Будет возвращено в случае ошибки</param>
        /// <param name="currencySymbol">Знак валюты</param>
        /// <returns></returns>
        public float PriceToFloat(string priceString, float defaultPrice = -1, string currencySymbol = "$")
        {
            var notModified = priceString;
            try
            {
                priceString = SanitizeString(priceString.Replace(currencySymbol, ""));

                if (priceString.Contains(",") && priceString.Contains("."))
                    priceString = priceString.Replace(",", "");

                if (priceString.Contains("."))
                    priceString = priceString.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                else if (priceString.Contains(","))
                    priceString = priceString.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                return float.Parse(priceString);
            }
            catch
            {
                LastErrorMessage = $"PriceToFloat: error. notModified: {notModified}, priceString: {priceString}";
            }
            return defaultPrice;
        }

        /// <summary>
        /// Splits string into list by Environment.NewLine
        /// </summary>
        /// <returns></returns>
        public static List<string> SplitByNewLines(string input)
        {
            return input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None). ToList();
        }
    }
}
