namespace A01;

class Program {
    static void Main (string[] args) {

        int randomNumber = new Random ().Next (1, 101);

        //to prompt the user to guess the Random number
        int guessNumber = getNumber ("Enter a number from 1 to 100: ");

        //check the guess accuracy

        for (int i = 6; i >= 0; i--) {

            if (guessNumber < randomNumber && i != 0) {
                Console.WriteLine ("Your guess is too low");
                guessNumber = getNumber ($"You have {i} try. Guess correctly:  ");

            } else if (guessNumber > randomNumber && i != 0) {
                Console.WriteLine ("Your guess is too high");
                guessNumber = getNumber ($"You have {i} try. Guess correctly:  ");
            } else if (guessNumber == randomNumber) {
                Console.WriteLine ("You guessed correctly");
                break;
            } else Console.WriteLine ($"The game is over and the number is {randomNumber}");
        }

        //function to get a valid number from 1 to 100
        int getNumber (string str) {
            for (; ; ) {
                Console.Write (str);
                if (int.TryParse (Console.ReadLine (), out int result) && (result <= 100 && result > 0)) return result;
                else Console.WriteLine ($"INVALID!");
            }
        }

    }
}
