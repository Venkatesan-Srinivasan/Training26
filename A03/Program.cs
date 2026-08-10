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
      char[] seedList = { 'U', 'X', 'A', 'L', 'T', 'N', 'E' };
      List<string> validStrings = [];
      List<string> deleteStrings = [];
      // Each word must contain the first letter in seed list, if so add to the list.
      foreach (string i in words)
         if (i.Contains (seedList[0]) && i.Length > 3)
            validStrings.Add (i);
      // Adding the invalid words that contain other than seven letters in seed list.
      // Ensures string s contains only characters from seedList.
      foreach (string s in validStrings)
            if (s.All (seedList.Contains))
               continue;
            else
               deleteStrings.Add (s);
      // Deleting InValid words
      foreach (string st in deleteStrings)
         validStrings.Remove (st);
      // Console Layout
      int total = 0;
      foreach (string s in validStrings.OrderByDescending (s => Score (s))) {
         if (IsPangram (s))
            Console.ForegroundColor = ConsoleColor.Green;
         Console.WriteLine ($"{Score (s),3}. {s}");
         Console.ResetColor ();
         total += Score (s);
      }
      Console.WriteLine ($"----\n{total} total");

      #region Implementation ----------------------------------------
      // Method for finding whether the word is a pangram
      // Ensures string s contains all characters in seedList.
      bool IsPangram (string s) {
         return seedList.All (s.Contains);
      }
      // Method to calculate score
      int Score (string s) {
         if (s.Length == 4)
            return 1;
         if (IsPangram (s))
            return s.Length + 7;
         return s.Length;
      }
      #endregion
   }
}
#endregion
