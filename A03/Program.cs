// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------
// Program.cs
// Program to help solve a New-York Times style Spelling Bee.
// The program can assume a word list is given as a text file, and that the daily choice of
// 7 letters is provided as an array of 7 chars:
// { 'U', 'X', 'A', 'L', 'T', 'N', 'E' }.
// ------------------------------------------------------------------------------------------------
namespace A03;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      // Reading a file
      string[] words = File.ReadAllLines ("../../../Data/words.txt");
      // Console Layout
      int total = 0;
      foreach (var item in words.Where (IsValid).Select (s => new { Word = s, Points = Score (s) })
         .OrderByDescending (x => x.Points)) {
         if (IsPangram (item.Word))
            Console.ForegroundColor = ConsoleColor.Green;
         Console.WriteLine ($"{item.Points,3}. {item.Word}");
         Console.ResetColor ();
         total += item.Points;
      }
      Console.WriteLine ($"----\n{total} total");
   }

   #region Implementations ------------------------------------------
   // Method for Valid Strings
   // Each word contains 4 letters, first letter in seedlist,
   // uses only 7 letters.
   static bool IsValid (string s) => s.Length >= 4 &&
      s.Contains (sSeedList[0]) && s.All (sSeedList.Contains);

   // Method for finding whether the word is a pangram
   // Ensures string s contains all characters in seedList.
   static bool IsPangram (string s) => sSeedList.All (s.Contains);

   // Method to calculate score
   static int Score (string s) =>
      (s.Length == 4 ? 1 : s.Length) + (IsPangram (s) ? 7 : 0);
   #endregion

   #region Private --------------------------------------------------
   static char[] sSeedList = { 'U', 'X', 'A', 'L', 'T', 'N', 'E' };
   #endregion
}
#endregion