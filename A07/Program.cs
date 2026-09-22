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
      var testCases = new List<string>
      {
         // Basic numbers
         "0", "123", "-456", "+789",
          // Decimal formats
          "67.88902", "-45.67", ".5", "5.", "000123.4500",
          // Scientific notation
          "1e3", "1E-3", "-7.89e+2", ".5e2", "5.e2",
          // Very large / small values
          "1e300", "9e999", "1e-300", "1e-999",
          // Precision
          "0.1234567890123456789", "123456789.123456789", "999999999999999",
          // Invalid decimal / exponent / signs
          "1.2.3", "1e2e3", "e10", "1e", "1e+", "1e++2", "1e+-2", "++1", "+-1", "1+2",
          // No actual number
          "", " ", "+", "-", ".", "-.", "abc", "12abc", "1.2f",
          // Special double values
          "NaN", "Infinity", "-Infinity", "+Infinity",
          // Whitespace
          "   123.45   ", "12 34"
      };
      foreach (string str in testCases.Select (s => s.Trim ())) {
         double value = ConvertToDouble (str);
         double parsed;
         try {
            parsed = double.Parse (str);
         } catch (FormatException) {
            parsed = double.NaN;
         }
         if (!value.Equals (parsed)) Console.ForegroundColor = ConsoleColor.Red;
         Console.WriteLine ($"{str,25} ---> {value,25:G17} | {parsed:G17}");
         Console.ResetColor ();
      }
   }

   #region Implementations ------------------------------------------------------------------------
   // Converts a numeric string into a double value.
   static double ConvertToDouble (string str) {
      if (string.IsNullOrWhiteSpace (str)) return double.NaN;
      str = str.Trim ();
      double? specialValue = str.ToLowerInvariant () switch {
         "NaN" => double.NaN,
         "infinity" => double.PositiveInfinity,
         "+infinity" => double.PositiveInfinity,
         "-infinity" => double.NegativeInfinity,
         _ => null
      };
      if (specialValue.HasValue) return specialValue.Value;
      string[] parts = str.Split ('e', 'E');
      if (parts.Length > MAXPARTS) return double.NaN;
      if (parts.Length == 1) return GetMantissa (parts[0]);
      else if (parts.Length == MAXPARTS) {
         return GetMantissa (parts[0]) * GetExponent (parts[1]);
      } else return double.NaN;
   }

   // Parses the mantissa (integer and fractional part).
   static double GetMantissa (string str) {
      if (string.IsNullOrEmpty (str)) return double.NaN;
      var (sign, unsignedStr) = ExtractSign (str);
  //    if (!unsignedStr.Any (char.IsDigit)) return double.NaN;
      string[] parts = unsignedStr.Split ('.');
      if (parts.Length > MAXPARTS) return double.NaN;
      string integerStr = parts[0];
      double integer = 0;
      if (!string.IsNullOrEmpty (integerStr))
         integer = GetDigits (integerStr);
      if (parts.Length == 1) return sign * integer;
      string fractionalStr = parts[1];
      double fractional = 0;
      if (parts.Length == MAXPARTS && !string.IsNullOrEmpty (fractionalStr)) {
         double decimals = GetDigits (fractionalStr);
         fractional = decimals * Math.Pow (10, -fractionalStr.Length);
      }
      return sign * (integer + fractional);
   }

   // Parses the exponent and returns 10 raised to that power.
   static double GetExponent (string str) {
      if (string.IsNullOrEmpty (str)) return double.NaN;
      var (sign, unsignedStr) = ExtractSign (str);
      return Math.Pow (10, sign * GetDigits (unsignedStr));
   }

   // Parses a integer string into a numeric value.
   static double GetDigits (string str) {
      if (string.IsNullOrEmpty (str)) return double.NaN;
      double digits = 0;
      for (int i = 0; i < str.Length; i++) {
         if (char.IsDigit (str[i])) digits = (digits * 10) + (str[i] - '0');
         else return double.NaN;
      }
      return digits;
   }

   // Extracts the leading sign and returns the remaining string.
   static (int sign, string str) ExtractSign (string str) {
      if (string.IsNullOrEmpty (str)) return (1, str);
      int sign = 1;
      char signChar = str[0];
      if (signChar == '+' || signChar == '-') {
         if (signChar == '-') sign = -1;
         str = str[1..];
      }
      return (sign, str);
   }
   #endregion

   #region Private --------------------------------------------------------------------------------
   const int MAXPARTS = 2; // At most two parts: before and after '.' or 'e'
   #endregion
}
#endregion

