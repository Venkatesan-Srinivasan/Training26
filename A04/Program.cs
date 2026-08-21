// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------
// Program.cs
// A04: Program to build a frequency table with occurrence of all the letters in descending order.
// To display the first 7 letters with the count to be used as the seed letters from word list.
// ------------------------------------------------------------------------------------------------
namespace A04;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      Dictionary<char, int> frequencyTable = [];
      // Checks each character is alphabets & transforms lower case alphabets to uppercase
      foreach (var ch in File.ReadAllText ("../../../../Data/words.txt")
               .Where (char.IsLetter).Select (char.ToUpper))
         frequencyTable[ch] = frequencyTable.TryGetValue (ch, out int count) ? ++count : 1;
      // To display first 7 letters and its occurrences.
      Console.Write ("Letter | Occurrence");
      foreach (var item in frequencyTable.OrderByDescending (a => a.Value).Take (7))
         Console.Write ($"\n{item.Key,3}{'|',5}{item.Value,8}");
   }
}
#endregion