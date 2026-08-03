// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------
// Program.cs
// Program to guess the Secret Number in 7 attempts guessed by the user.
// --------------------------------------------------------------------------------------------
namespace A02;
#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main (string[] args) {
      // Let user think a random number
      int min = 0;
      int max = 127;
      Console.WriteLine ("Guess a number from 1 to 100 \n\nAnswer [Y]es or [N]o for the following");
      SecretCheck (min, max);
      //Method to guess the secret number at 7 attempts
      #region Method ------------------------------------------------
      void SecretCheck (int l_min, int l_max) {
         for (int i = 7; i > 0; i--) {
            int l_mid = (l_min + l_max + 1) / 2;
            Console.Write ($"\tIs the number less than {l_mid}: ");
            var option = Console.ReadKey (true).Key;
            if (option == ConsoleKey.Y) {
               Console.WriteLine ("Yes");
               l_max = l_mid;
               if (i == 1)
                  Console.WriteLine ($"\n\t{l_mid - 1} is your guess number");
            }
            if (option == ConsoleKey.N) {
               Console.WriteLine ("No");
               l_min = l_mid;
               if (i == 1)
                  Console.WriteLine ($"\n\t{l_mid} is your guess Number");
            }
            if (option != ConsoleKey.Y && option != ConsoleKey.N)
               i++;
         }
      }
      #endregion
   }
}
#endregion