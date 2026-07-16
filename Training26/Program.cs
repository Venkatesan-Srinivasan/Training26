using System.Net.NetworkInformation;

namespace Training26;

class Program {
    static void Main (string[] args) {

        Console.WriteLine("Hello World!");
        //Calling the method to check if the brackets are balanced or not
        bool result = T1_Balanced_Brackets ("([])");
        //bool result = T1_Balanced_Brackets ("(]");

        Console.WriteLine (result);
    }

    public static bool T1_Balanced_Brackets (string st) {

        Stack<char> stack = new Stack<char> ();
        for (int i = 0; i < st.Length; i++) {
            char c = st[i];
            if (c == '[') {
                stack.Push (']');
                //Console.WriteLine (stack.Peek ());
            } else if (c == '(') {
                stack.Push (')');
                // Console.WriteLine (stack.Peek ());
            } else {
                //only the top elemnt matches with the current character then pop the top element from the stack

                if (stack.Peek () == c) {
                    stack.Pop ();
                }
            }

            if (stack.Count () == 0) {
                return true;
            }
        }
        return false;
    }
}
