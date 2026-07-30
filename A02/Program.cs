namespace A02;

class Program
{
    static void Main(string[] args)
    {
        //LET USER THINK OF A RANDOM NUMBER
        //THE MACHINE SHOULD GUESS WHAT IS THE NUMBER

        int min = 0;
        int max = 127;
        Console.WriteLine ("Answer [Y]es or [N]o for the following");
        string result = "";
        SecretCheck (min, max);

        void SecretCheck (int l_min, int l_max) {
            for (int i = 7; i > 0; i--) {
                int l_mid = (l_min + l_max + 1) / 2;
                Console.Write ($"Is the number less than {l_mid}: ");
                var option = Console.ReadKey (true).Key;
                if (option == ConsoleKey.Y) {
                    Console.WriteLine ("Yes");
                    l_max = l_mid;
                    result += "0";
                    //if (i == 1)
                    //   Console.WriteLine ($"{l_mid - 1} is your guess number");
                }
                if (option == ConsoleKey.N) {
                    Console.WriteLine ("No");
                    l_min = l_mid;
                    result += "1";
                    //if (i == 1)
                    //   Console.WriteLine ($"{l_mid} is your guess Number");
                }
                if (option != ConsoleKey.Y && option != ConsoleKey.N)
                    i++;
            }
        }
        Console.WriteLine ($"the answer: {Convert.ToInt32 (result, 2)}");
    }
}
