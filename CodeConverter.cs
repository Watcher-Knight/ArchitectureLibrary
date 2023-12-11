using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArchitectureLibrary
{
    public static class CodeConverter
    {
        public static float GetValue(string input, bool validate = false)
        {
            if (IsNumber(input)) return float.Parse(input);
            return 0;
        }

        public static bool Compare(float value1, float value2, Comparison comparisonType = Comparison.EqualTo)
        {
            return Compare<float>(value1, value2, comparisonType);
        }
        public static bool Compare<T>(T value1, T value2, Comparison comparisonType = Comparison.EqualTo) where T : IComparable<T>
        {
            switch (comparisonType)
            {
                case Comparison.EqualTo: default: return value1.CompareTo(value2) == 0;
                case Comparison.LessThan: return value1.CompareTo(value2) < 0;
                case Comparison.GreaterThan: return value1.CompareTo(value2) > 0;
                case Comparison.LessThanOrEqualTo: return value1.CompareTo(value2) <= 0;
                case Comparison.GreaterThanOrEqualTo: return value1.CompareTo(value2) >= 0;
            }
        }

        public static bool ValidateParameter(string parameter)
        {
            if (IsNumber(parameter)) return true;
            return false;
        }

        public static bool IsNumber(string str)
        {
            if (str == "") return false;
            foreach (char c in str) if ((c < '0' || c > '9') && c != '.') return false;
            int decimalAmount = 0;
            foreach (char c in str) if (c == '.') decimalAmount += 1;
            return decimalAmount <= 1;
        }
    }
}