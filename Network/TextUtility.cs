using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using UnityEngine;

namespace NetworkProject
{
    public static class TextUtility
    {
        private static char START_VARIABLE = '[';
        private static char END_VARIABLE = ']';
        private static char START_OPERATION = '<';
        private static char END_OPERATION = '>';

        public static void SetMultilineText(string text, GUIText guiText, int maxWidth)
        {
            var words = text.Split(' ');

            string result = "";

            guiText.text = "";

            for (int i = 0; i < words.Length; i++)
            {
                if (i != 0)
                {
                    guiText.text += " ";
                }

                guiText.text += words[i];

                if (guiText.GetScreenRect().width > maxWidth)
                {
                    guiText.text = result;
                    guiText.text += '\n';
                    guiText.text += words[i];


                }

                result = guiText.text;
            }
        }

        public static string ReplaceVariablesAndMathExpresionsByNumbers(string input, object data)
        {
            input = ReplaceVariablesByNumbers(input, data);

            return ReplaceMathExpresionsByNumbers(input);
        }

        public static string ReplaceVariablesByNumbers(string input, object data)
        {
            int startIndex = 0;
            int newStartIndex = 0;
            string variable;
            var returnString = new StringBuilder(input);

            while (true)
            {
                newStartIndex = FindValueBetweenChars(returnString.ToString(), startIndex,
                    START_VARIABLE, END_VARIABLE, out variable);

                if (newStartIndex == -1)
                {
                    break;
                }

                // -2 i +2 są ponieważ usuneliśmy prefix i sufix
                int startIndexToReplace = newStartIndex - variable.Length - 2;
                returnString.Remove(startIndexToReplace, variable.Length + 2);
                returnString.Insert(startIndexToReplace, GetVariableValue(variable, data));
            }

            return returnString.ToString();
        }

        public static string ReplaceMathExpresionsByNumbers(string input)
        {
            int startIndex = 0;
            int newStartIndex = 0;
            string expresion;
            var returnString = new StringBuilder(input);

            while (true)
            {
                newStartIndex = FindValueBetweenChars(returnString.ToString(), startIndex,
                    START_OPERATION, END_OPERATION, out expresion);

                if (newStartIndex == -1)
                {
                    break;
                }

                // -2 i +2 są ponieważ usuneliśmy prefix i sufix
                int startIndexToReplace = newStartIndex - expresion.Length - 2;
                returnString.Remove(startIndexToReplace, expresion.Length + 2);
                returnString.Insert(startIndexToReplace, EvaluateExpresion(expresion));
            }

            return returnString.ToString();
        }

        private static int FindValueBetweenChars(string input, int startPosition, char prefix, char sufix, out string value)
        {
            int startValuePosition = input.IndexOf(prefix, startPosition);
            int endValuePosition = input.IndexOf(sufix, startValuePosition + 1);

            if (startValuePosition == -1 && endValuePosition != -1 || startValuePosition != -1 && endValuePosition == -1)
            {
                throw new InvalidOperationException("Nie znaleziono znaku zaczynającego lub kończącego zmienną.");
            }
            else if (startValuePosition != -1 && endValuePosition != -1)
            {
                value = input.Substring(startValuePosition, endValuePosition - startValuePosition + 1);
                value = DeletePrefixAndSufix(value);

                return endValuePosition + 1;
            }

            value = "";

            return -1;
        }

        private static string GetVariableValue(string variableName, object data)
        {
            Type type = data.GetType();

            var property = type.GetProperty(variableName);

            var value = property.GetValue(data, null);

            string textValue = value.ToString();

            return textValue.Replace(',', '.');
        }

        private static string DeletePrefixAndSufix(string text)
        {
            text = text.Remove(0, 1);
            return text.Remove(text.Length - 1, 1);
        }

        private static string EvaluateExpresion(string expresion)
        {
            double value = Convert.ToDouble(new DataTable().Compute(expresion, null));

            value = Math.Round((double)value, 2, MidpointRounding.AwayFromZero);

            string textValue = value.ToString();

            return textValue.Replace(',', '.');
        }
    } 
}
