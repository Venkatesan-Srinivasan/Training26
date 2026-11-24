// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Trumpf Metamation India.
// ------------------------------------------------------------------------------------------------
// DisplayConsole.cs
// A12: Wordle
// Contains the DisplayConsole class, which handles all the console output for the Wordle game.
//    - Draws the 6 x 5 board: submitted guesses, the guess being typed, and the empty rows
//    - Colors each submitted letter: Green (correct position), Blue (wrong position),
//      Dark Gray (not in the word)
//    - Shows the A-Z keypad, colored with the best result found so far for each letter
//    - Prints centered status messages
// ------------------------------------------------------------------------------------------------

namespace Wordle;

#region class DisplayConsole ----------------------------------------------------------------------
/// <summary>The class contains all the method required for displaying the board</summary>
class DisplayConsole {
   #region Methods --------------------------------------------------
   public void DisplayBoard (string str, int chCount, int currentLine, List<string> lst, string word) {
      for (int i = 0, count = 5; i < 6; i++) {
         string circle = "◌ ", dot = "· ", print = "";
         if (i < currentLine) { Validation (TrimmedStrings (lst[i]), word); continue; }
         print += (i == currentLine && chCount <= 5) ?
            str + ((chCount < 5) ? circle : "") + PrintSymbols (dot, count - chCount - 1)
            : PrintSymbols (dot, count);
         PrintCentered (print);
      }
      DisplayKeypad ();
   }

   public void PrintStatus (string str) => PrintCentered (str);

   public string TrimmedStrings (string str) {
      string trimStr = "";
      foreach (var ch in str) if (char.IsLetter (ch)) trimStr += ch;
      return trimStr;
   }
   #endregion

   #region Implementations ------------------------------------------
   void DisplayKeypad () {
      PrintCentered ("───────────────────────────\n");
      for (int i = 0; i < 26; i++) {
         char letter = (char)('A' + i);
         if (i % 7 == 0) {
            if (i > 0) Console.WriteLine ();
            Console.SetCursorPosition ((Console.WindowWidth - 25) / 2, Console.CursorTop);
         }
         Console.ForegroundColor = keyPad.GetValueOrDefault (letter, ConsoleColor.White);
         Console.Write (letter + "   ");
      }
      Console.ResetColor ();
      Console.WriteLine ();
      PrintCentered ("───────────────────────────\n");
   }
   void PrintCentered (string text) {
      int left = (Console.WindowWidth - text.Length) / 2;
      Console.SetCursorPosition (left, Console.CursorTop);
      Console.WriteLine (text);
   }
   string PrintSymbols (string str, int count) {
      string strOut = "";
      for (int i = 0; i < count; i++) strOut += str;
      return strOut;
   }

   void Validation (string guess, string secret) {
      int left = (Console.WindowWidth - guess.Length * 2) / 2;
      Console.SetCursorPosition (left, Console.CursorTop);
      for (int i = 0; i < guess.Length; i++) {
         var color = guess[i] == secret[i] ? ConsoleColor.Green
                   : secret.Contains (guess[i]) ? ConsoleColor.Blue
                   : ConsoleColor.DarkGray;
         WriteColored (guess[i], color);
         UpdateKeypad (guess[i], color);
      }
      Console.WriteLine ();
   }
   void UpdateKeypad (char ch, ConsoleColor color) {
      ch = char.ToUpper (ch);
      int Rank (ConsoleColor c) => c == ConsoleColor.Green ? 3
                                 : c == ConsoleColor.Blue ? 2
                                 : c == ConsoleColor.DarkGray ? 1 : 0;
      if (Rank (color) > Rank (keyPad.GetValueOrDefault (ch, ConsoleColor.Gray)))
         keyPad[ch] = color;
   }

   void WriteColored (char ch, ConsoleColor color) {
      Console.ResetColor ();
      Console.ForegroundColor = color;
      Console.Write (ch + " ");
      Console.ResetColor ();
   }
   #endregion

   #region Private --------------------------------------------------
   Dictionary<char, ConsoleColor> keyPad = [];
   #endregion
}
#endregion