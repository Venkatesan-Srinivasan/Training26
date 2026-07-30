namespace A03;

class Program {
    static void Main (string[] args) {
        //Reading a file

        string[] words = File.ReadAllLines ("C:/Users/srinivasanve/Downloads/words.txt");

        //->adding words that contains "U" to the list.
        List<string> validStrings = new List<string> ();
        List<string> deleteStrings = new List<string> ();

        //each word must contain a letter "U"    
        foreach (string i in words)
            if (i.Contains ("U") && i.Length > 3)
                validStrings.Add (i);

        //adding the invalid words that cotain other than seven letters
        foreach (string s in validStrings)
            foreach (char c in s)
                if (c == 'U' || c == 'X' || c == 'A' || c == 'L' || c == 'T' || c == 'N' || c == 'E')
                    continue;
                else
                    deleteStrings.Add (s);

        //Deleting InValid words
        foreach (string st in deleteStrings)
            validStrings.Remove (st);

        //Console Layout
        int total = 0;
        foreach (string s in validStrings.OrderByDescending (s => Score (s))) {
            if (IsPangram (s))
                Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine ($"{Score (s),3}. {s}");
            Console.ResetColor ();
            total += Score (s);
        }
        Console.WriteLine ($"----\n{total} total");

        //Is pangram
        bool IsPangram (string s) {
            return s.Contains ('U')
                && s.Contains ('X')
                && s.Contains ('A')
                && s.Contains ('L')
                && s.Contains ('T')
                && s.Contains ('N')
                && s.Contains ('E');
        }
        //Calculate score 
        int Score (string s) {
            if (s.Length == 4)
                return 1;

            if (IsPangram (s))
                return s.Length + 7;

            return s.Length;
        }

    }
}
