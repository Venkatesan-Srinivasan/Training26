// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------
// Program to build a frequency table with occurrence of all the letters in descending order.
// To display the first 7 letters to be used as the seed letters from word list.
// ------------------------------------------------------------------------------------------------
namespace A04;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      // Initialize a dictionary => keys: upper case alphabets, values: 0
      Dictionary<char, int> frequencyTable = new ();
      for (char c = 'A'; c <= 'Z'; c++)
         frequencyTable[c] = 0;
      // Transforms lower case alphabets to uppercase and counts each character and update values
      foreach (var ch in File.ReadAllText ("../../../../Data/words.txt")
               .Select (a => char.ToUpper (a)))
         if (frequencyTable.ContainsKey (ch))
            frequencyTable[ch]++;
      // To display first 7 letters uses as a seed list for spelling bee.
      Console.Write ("The SeedList is: ");
      foreach (var item in frequencyTable.OrderByDescending (a => a.Value).Take (7))
         Console.Write (item.Key);
   }
}
#endregion