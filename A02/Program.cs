// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------
// Program.cs
// Program to guess the Secret Number in 7 attempts guessed by the user.
// ------------------------------------------------------------------------------------------------
namespace A02;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      // Let user think a random number
      int min = 0;
      int max = 127;
      Console.WriteLine ("Guess a number from 1 to 100 \nAnswer [Y]es or [N]o for the following");
      SecretCheck (min, max);
   }

   #region Implementation -------------------------------------------
   // Method to guess the secret number at 7 attempts
   static void SecretCheck (int min, int max) {
      for (int i = 7; i > 0; i--) {
         int mid = (min + max + 1) / 2;
         Console.Write ($"\tIs the number less than {mid}: ");
         var option = Console.ReadKey (true).Key;
         if (option == ConsoleKey.Y) {
            Console.WriteLine ("Yes");
            max = mid;
         } else if (option == ConsoleKey.N) {
            Console.WriteLine ("No");
            min = mid;
         } else {
            Console.WriteLine ("Invalid! Press [Y]es or [N]o ");
            i++;
         }
      }
      Console.WriteLine ($"\n\t{min} is your guess number");
   }
   #endregion
}
#endregion