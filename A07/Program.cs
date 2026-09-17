// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// A07: double.Parse
// Program to Implement double.Parse method that takes a string and returns a double
// Logic:
// 1. Trim and validate input.
// 2. Split the string into mantissa and exponent parts.
// 3. Parse the mantissa (integer and fractional portions).
// 4. Parse the exponent.
// 5. Compute: mantissa × 10^exponent
// ------------------------------------------------------------------------------------------------
namespace A07;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      var testCases = new List<string>{"0", "123", "-456", "+789","67.88902", "-45.67", "+5.123",
         ".5", "5.", ".0001", "1.", ".1",".0", "1e3", "1E3", "1.23e3", "4.56E-2", "-7.89e+2",
         "+3.2e-5", "NaN", "Infinity", "-Infinity", "  123.45   ", "000123.4500", "6.7.78",
         "45e-98", "9e999", "-0.0000001", "+0.0",};
      foreach (string str in testCases.Select (s => s.Trim ())) {
         double value = ConvertToDouble (str);
         Console.WriteLine ($"{str,15} ---> {value,15:G10}");
      }
   }

   #region Implementations ------------------------------------------------------------------------
   static double ConvertToDouble (string str) { // Converts a numeric string into a double value.
      if (string.IsNullOrWhiteSpace (str)) return double.NaN;
      string[] parts = str.Split ('e', 'E');
      if (parts.Length > MaxParts) return double.NaN;
      if (parts.Length == 1) return GetMantissa (parts[0]);
      else if (parts.Length == MaxParts) {
         return GetMantissa (parts[0]) * GetExponent (parts[1]);
      } else return double.NaN;
   }

   static double GetMantissa (string str) { // Parses the mantissa (integer and fractional part).
      if (string.IsNullOrEmpty (str)) return double.NaN;
      int sign = 1;
      if (str[0] == '+' || str[0] == '-') {
         if (str[0] == '-') sign = -1;
         str = str.Substring (1);
      }
      string[] parts = str.Split ('.');
      if (parts.Length > MaxParts) return double.NaN;
      double integer = 0;
      if (!string.IsNullOrEmpty (parts[0]))
         integer = GetDigits (parts[0]);
      if (parts.Length == 1) return sign * integer;
      double fractional = 0;
      if (parts.Length == MaxParts && !string.IsNullOrEmpty (parts[1])) {
         double decimals = GetDigits (parts[1]);
         fractional = decimals * Math.Pow (10, -parts[1].Length);
      }
      return sign * (integer + fractional);
   }

   // Parses the exponent and returns 10 raised to that power.
   static double GetExponent (string str) =>
      string.IsNullOrEmpty (str) ? double.NaN : Math.Pow (10, GetDigits (str));

   static double GetDigits (string str) { // Parses a signed integer string into a numeric value.
      if (string.IsNullOrEmpty (str)) return double.NaN;
      double digits = 0;
      int startIndex = (str[0] == '+' || str[0] == '-') ? 1 : 0;
      int sign = (startIndex == 1 && str[0] == '-') ? -1 : 1;
      for (int i = startIndex; i < str.Length; i++) {
         if (char.IsDigit (str[i])) digits = (digits * 10) + (str[i] - '0');
         else return double.NaN;
      }
      return sign * digits;
   }
   #endregion
   #region Private --------------------------------------------------------------------------------
   const int MaxParts = 2; // At most two parts: before and after '.' or 'e'
   #endregion
}
#endregion

