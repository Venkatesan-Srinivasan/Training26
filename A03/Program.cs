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

      List<string> validStrings = [];
      List<string> deleteStrings = [];
      // Each word must contain the first letter in seed list, if so add to the list.
      foreach (string i in words)
         if (i.Contains (sSeedList[0]) && i.Length > 3)
            validStrings.Add (i);
      // Adding the invalid words that contain other than seven letters in seed list.
      // Ensures string s contains only characters from seedList.
      foreach (string s in validStrings)
         if (s.All (sSeedList.Contains))
            continue;
         else
            deleteStrings.Add (s);
      // Deleting InValid words
      foreach (string st in deleteStrings)
         validStrings.Remove (st);
      // Console Layout
      int total = 0;
      foreach (string s in validStrings.OrderByDescending (Score)) {
         if (IsPangram (s))
            Console.ForegroundColor = ConsoleColor.Green;
         Console.WriteLine ($"{Score (s),3}. {s}");
         Console.ResetColor ();
         total += Score (s);
      }
      Console.WriteLine ($"----\n{total} total");
   }

   #region Implementations ------------------------------------------
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
