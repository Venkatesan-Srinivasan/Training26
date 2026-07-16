using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;

namespace Training26;

class Program {
    static void Main (string[] args) {
        Console.WriteLine ("Hello World!");
        int[] TestCase = new int[] { 30, 31, 29, 32, 28, 27, 30, 25 };
        int[] result = T3_CoolerDay (TestCase);
        Console.Write ('[');
        foreach (int res in result) {
            Console.Write (res);
            Console.Write (' ');
        }
        Console.Write (']');

    }

    public static int[] T3_CoolerDay (int[] input) {
        int n = input.Length;
        int[] result = new int[n];
        int[] check = new int[n];
        for (int i = 0, j = 0; i < n - 1; i++) {
            if (input[i] > input[i + 1]) {
                check[j] = input[i + 1];
                j++;
            }
        }
        for(int i=0,j=0,k=0;i<n;i++) {
            if (check[j] == input[i]) j++;
            //Console.WriteLine(input.IndexOf (check[j]));
            if ((input.IndexOf (check[j]) - i) > 0) {
                result[k] = input.IndexOf (check[j]) - i;
                Console.WriteLine (result[k]);
                k++;
            }
        }
        foreach (int j in result) {
            Console.WriteLine (j);
        }
        return result;

    }

}
