// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Trumpf Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// A12: Wordle
// Program to  implement the Wordle game
// There is a secret 5 - letter word you have to guess.
// You type in your guess, and different letters in your guess are colored blue, green or gray:
//    - Dark Gray: the given letter is not in the secret word
//    - Blue: the letter is found in the word, but not in this position
//    - Green: The letter is found in the word, and is in the correct position
// This feedback can improve your guess. The user have 6 tries in which the word must be guessed.
// ------------------------------------------------------------------------------------------------

using System.Text;

namespace Wordle;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      Console.OutputEncoding = Encoding.UTF8;
      var dc = new DisplayConsole ();
      string secretWord = GetSecretWord ();
      Console.CursorVisible = false;
      Redraw ();
      for (; ; ) {
         if (lst.Count == 6) {
            dc.PrintStatus ($"Sorry. The word was {secretWord}");
            return;
         }
         var option = Console.ReadKey (true).Key;
         switch (option) {
            case >= ConsoleKey.A and <= ConsoleKey.Z:
               char key = (char)option;
               keys += key + " ";
               Redraw ();
               if (keys.Length > 10) {
                  keys = keys[..^2];
                  Redraw ();
                  dc.PrintStatus ("Word is greater than 5 letter, enter backspace to continue");
               }
               break;
            case ConsoleKey.Backspace when keys.Length != 0:
               keys = keys[..^2];
               Redraw ();
               break;
            case ConsoleKey.Enter when keys.Length == 10:
               if (WordPresence (dc.TrimmedStrings (keys)) && !lst.Contains (keys)) {
                  lst.Add (keys);
                  currentLine++;
                  if (dc.TrimmedStrings (keys) == secretWord) {
                     keys = "";
                     Redraw ();
                     dc.PrintStatus ($"You found the word in {lst.Count} tries");
                     return;
                  }
                  keys = "";
                  Redraw ();
               } else {
                  Redraw ();
                  if (lst.Contains (keys)) { dc.PrintStatus ("Word already guessed"); continue; }
                  dc.PrintStatus ($"{dc.TrimmedStrings (keys)} is not a word");
               }
               break;
         }
      }
      void Redraw () {
         Console.Clear ();
         dc.DisplayBoard (keys, keys.Length / 2, currentLine, lst, secretWord);
      }
   }

   #region Implementations ------------------------------------------
   /// <summary>Gets the secretword in the puzzle list</summary>
   static string GetSecretWord () {
      string[] words = File.ReadAllLines ("../../../../Data/puzzle-5.txt");
      int rd = new Random ().Next (0, words.Length);
      return words[rd];
   }

   /// <summary>Checks for the word present in the dictionary</summary>
   static bool WordPresence (string str) =>
      File.ReadLines ("../../../../Data/Dict-5.txt")
                 .Any (line => line.Contains (str));
   #endregion

   #region Private --------------------------------------------------
   static List<string> lst = [];
   static string keys = "";
   static int currentLine = 0;
   #endregion
}
#endregion