using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;

namespace Training26;

class Program {
    static void Main (string[] args) {

        Console.WriteLine ("Hello World!");

        int[,] TestCase1 = new int[,] { { 2, 7, 6 }, { 9, 5, 1 }, { 4, 3, 8 } };
        int[,] TestCase2 = new int[,] { { 8, 1, 6 }, { 3, 5, 7 }, { 4, 9, 2 } };
        int[,] TestCase3 = new int[,] { { 8, 1, 6 }, { 3, 5, 7 }, { 4, 2, 9 } };
        bool result1 = T2_MagicSquare (TestCase1);
        bool result2 = T2_MagicSquare (TestCase2);
        bool result3 = T2_MagicSquare (TestCase3);
        Console.WriteLine (result1);
        Console.WriteLine (result2);    
        Console.WriteLine (result3);

    }
    public static bool T2_MagicSquare (int[,] input) {
        int sum = input[0,0] + input[0, 1] + input[0, 2];
        // Check rows
        for (int i = 0; i < 3; i++) {
            int rowSum = 0;
            for (int j = 0; j < 3; j++) {
                rowSum += input[i, j];
            }
            if (rowSum != sum) {
                return false;
            }
        }
        // Check columns
        for (int j = 0; j < 3; j++) {
            int colSum = 0;
            for (int i = 0; i < 3; i++) {
                colSum += input[j, i];
            }
            if (colSum != sum) {
                return false;
            }
        }
        // Check diagonals
        int diag1Sum = 0;
        int diag2Sum = 0;
        for (int i = 0; i < 3; i++) {
            diag1Sum += input[i, i];
            diag2Sum += input[i, 2- i];
        }
        if (diag1Sum != sum || diag2Sum !=sum) {
            return false;
        }
        return true;

    }
}
