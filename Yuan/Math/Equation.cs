using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
namespace Yuan.Math
{

    /// <summary>
    /// 代表一個方程式解釋器，該動態類型可將一個方程式分割，輸入形式必須為ax^b+bx^c....而係數必為整數或是小數。
    /// </summary>
    public class Equation
    {
        public char[] number = new char[] {'0','1','2','3','4','5','6','7','8','9','.'};
        public char[] var = new char[] {'a','b', 'c' ,'d', 'e','f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public char[] Operator = new char[] {'+','-'};
        public enum Mode
        {
            Parameter,var,Equal
        }
        public Equation(string equation)
        {
            char[] CharArray = equation.ToCharArray();
            string Parameter="";
            string var = "";
            bool IsBracket = false;
            int insidebrackcount = 0;
            string temp="";
            bool IsEqual = false;
            Mode mode = Mode.Parameter; ;
            List<EquationItem> output = new List<EquationItem>();
            bool Ended = false;
            bool var_plus = false;
            foreach (char Char in CharArray)
            {

                Debug.Print("Debug::Parameter:{0} var:{1}", Parameter, var);
                switch (mode)
                {
                    case (Mode.Parameter):
                        if (number.Contains(Char))
                        {
                            Parameter += Char;
                        }
                        else if (this.var.Contains(Char))
                        {
                            mode = Mode.var;
                            var += Char;
                        }
                        else if (Operator.Contains(Char))
                        {
                            mode = Mode.Parameter;
                            EquationItem ei = new EquationItem(Parameter, null);
                            output.Add(ei);
                            temp = "";
                            Parameter = "";
                            Parameter += Char;
                        }
                        else if (Char == '=')
                        {
                            mode = Mode.Equal;
                        }
                        else if(Char == '(')
                        {
                            mode = Mode.var;
                            IsBracket = true;
                            temp = "";
                            var += Char;
                            insidebrackcount = 0;
                            
                        }
                        
                        break;
                    case (Mode.var):
                        if (IsBracket)
                        {
                            if (Char == '(')
                            {
                                insidebrackcount += 1;
                            }
                            else if (Char == ')')
                            {
                                if (insidebrackcount > 0)
                                {
                                    insidebrackcount -= 1;
                                    var += Char;
                                }
                                else
                                {
                                    IsBracket = false;
                                    var += Char;
                                }

                            }
                            else
                            {
                                var += Char;
                                Debug.Print("Line:98,Char:{0},var:{1}", Char, var);
                            }
                        }
                        else
                        {
                            if (Operator.Contains(Char))
                            {
                                mode = Mode.Parameter;
                                EquationItem ei = new EquationItem(Parameter, var);
                                Debug.Print("Line:65 var:{0}", var);
                                output.Add(ei);
                                temp = "";
                                Parameter = "";
                                var = "";
                                Parameter += Char;

                            }
                            else if (Char == '^' || number.Contains(Char))
                            {
                                var += Char;
                            }
                            else if (Char == '=')
                            {
                                mode = Mode.Equal;
                            }
                            else if (Char == '(')
                            {
                                IsBracket = true;
                                var += Char;
                                insidebrackcount = 0;
                            }
                        }
                        break;
                    case (Mode.Equal):
                        Result += Char;
                        break;
                }
            }
            if (!Ended)
            {
                EquationItem eii = new EquationItem(Parameter, var);
                output.Add(eii);
                
            }
            else
            {
                if (temp != "")
                {
                    Result = temp;
                    temp = "";
                }
            }
            equationItems = output.ToArray();
        }
        public EquationItem[] equationItems { get; set; }
        public string Result { get; set; }

        /// <summary>
        /// 將數值帶入方程式中的變數當中，並且計算值。
        /// </summary>
        /// <param name="varname"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Double Calculate(string varname,Double value)
        {
            return 94.87;
        }

        /// <summary>
        /// 計算該算式的值，該算式不可含有未知數，否則將擲回例外狀況。
        /// </summary>
        /// <returns></returns>
        public Double Calculate()
        {
            return 94.87;
        }
    }
    public class EquationItem
    {
        public EquationItem(string parameter,string var){Parameter = parameter!=null?Convert.ToDouble(parameter):0;this.var = var; }
        public double Parameter { get; set; }
        public string var { get; set; }
    }
    
}
