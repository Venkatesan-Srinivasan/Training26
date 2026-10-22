// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Trumpf Metamation India.
// ------------------------------------------------------------------------------------------------
// Evaluator.cs
// A09.2: Expression Evaluator
// Evaluates arithmetic expressions using operator and operand stacks
// ------------------------------------------------------------------------------------------------

namespace Eval;

class EvalException : Exception {
   public EvalException (string message) : base (message) { }
}

class Evaluator {
   #region Properties -----------------------------------------------
   public int BasePriority { get; set; }
   #endregion

   #region Methods --------------------------------------------------
   public double Evaluate (string text) {
      Reset ();
      List<Token> tokens = []; //the number of tokens is  not known
      var tokenizer = new Tokenizer (this, text);
      for (; ; ) {
         var token = tokenizer.Next ();
         if (token is TEnd) break;
         if (token is TError err) throw new EvalException (err.Message);
         tokens.Add (token);
      }
      TVariable? tVariable = null;
      if (tokens.Count > 2 && tokens[0] is TVariable tvar && tokens[1] is TOpBinary { Op: "=" }) {
         tVariable = tvar;
         tokens.RemoveRange (0, 2);
      }
      foreach (var t in tokens) Process (t);
      while (_operators.Count > 0) ApplyOperator ();
      if (BasePriority != 0) throw new EvalException ("Mismatched parentheses");
      if (_operators.Count > 0) throw new EvalException ("Too many operators");
      if (_operands.Count > 1) throw new EvalException ("Too many operands");
      double result = _operands.Pop ();
      if (tVariable != null) _vars[tVariable.Name] = result;
      return result;
   }

   public double GetVariable (string name) {
      if (_vars.TryGetValue (name, out double value)) return value;
      throw new EvalException ($"Unknown variable: {name}");
   }
   #endregion

   #region Implementations ------------------------------------------
   void ApplyOperator () {
      var op = _operators.Pop ();
      if (_operands.Count < 1) throw new EvalException ("Too few operators");
      var r = _operands.Pop ();
      if (op is TUnary unary) {
         _operands.Push (unary.Apply (r));
      } else if (op is TOpFunction func) {
         _operands.Push (func.Apply (r));
      } else if (op is TOpBinary bin) {
         if (_operands.Count < 1) throw new EvalException ("Too few operands");
         var l = _operands.Pop ();
         _operands.Push (bin.Apply (l, r));
      }
   }

   bool OkToPush (TOperator op) {
      if (_operators.Count == 0) return true;
      var top = _operators.Peek ();
      return op.FinalPriority > top.FinalPriority;
   }

   void Process (Token token) {
      switch (token) {
         case TNumber num:
            _operands.Push (num.Value);
            break;
         case TOperator op:
            op.FinalPriority = op.Priority + BasePriority;
            while (!OkToPush (op)) ApplyOperator ();
            _operators.Push (op);
            break;
         case TPunctuation p:
            BasePriority += p.Punct == '(' ? 10 : -10;
            break;
         default: throw new EvalException ($"Unexpected token: {token}");
      }
   }

   void Reset () {
      _operators.Clear ();
      _operands.Clear ();
      BasePriority = 0;
   }
   #endregion

   #region Private --------------------------------------------------
   readonly Stack<double> _operands = [];
   readonly Stack<TOperator> _operators = [];
   readonly Dictionary<string, double> _vars = [];
   #endregion
}