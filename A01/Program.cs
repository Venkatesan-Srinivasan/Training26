// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------
// Program.cs
// Program to implement a simple guessing game. The computer thinks of a random number between 1
// and 100, and the user has to guess it.
// The user can enter an number, and the computer will respond with one of these:
//  - Your guess is too high
//  - Your guess is too low
//  - You guessed correctly.
// ------------------------------------------------------------------------------------------------
namespace A01;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      int randomNumber = new Random ().Next (1, 101);
      Console.WriteLine ("Enter a number from 1 to 100: ");
      // Check the guess accuracy
      int maxTries = 7;
      for (int triesLeft = maxTries; triesLeft > 0; triesLeft--) {
         int guessNumber = GetNumber ($"You have ({triesLeft} tries left): ");
         if (guessNumber < randomNumber)
            Console.WriteLine ("Your guess is too low");
         else if (guessNumber > randomNumber)
            Console.WriteLine ("Your guess is too high");
         else{
            Console.WriteLine ("You guessed correctly");
            break;
         }
      }

      #region Implemenetation ---------------------------------------
      // Function to get a valid number from 1 to 100
      int GetNumber (string str) {
         for (; ; ) {
            Console.Write (str);
            if (int.TryParse (Console.ReadLine (), out int result)
               && (result <= 100 && result > 0)) return result;
            else Console.WriteLine ($"INVALID! Enter a number from 1 to 100");
         }
      }
      #endregion
   }
}
#endregion