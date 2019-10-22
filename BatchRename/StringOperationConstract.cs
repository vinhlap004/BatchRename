using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class StringArgs
    {

    }

    public class ReplaceArgs : StringArgs
    {
        public string From { get; set; }
        public string To { get; set; }
    }

    public class NewCaseArgs : StringArgs
    {
        public string Input { get; set; }
    }

    public class FullnameNormalizeArg : StringArgs
    {
        public string Input { get; set; }

    }

    public class Move : StringArgs
    {

    }



    abstract class StringOperation
    {
        public StringArgs Args { get; set; }
        public abstract string Operate(string origin);
    }

    class ReplaceOperation : StringOperation
    {

        public override string Operate(string origin)
        {
            var args = Args as ReplaceArgs;
            var from = args.From;
            var to = args.To;

            return origin.Replace(from, to);
        }
    }

    class StringUpperOperation : StringOperation
    {
        public override string Operate(string origin)
        {
            var args = Args as NewCaseArgs;
            var input = args.Input;
            origin = input.ToUpper();
            return origin;

        }
    }

    class StringLowerOperation : StringOperation
    {
        public override string Operate(string origin)
        {
            var args = Args as NewCaseArgs;
            var input = args.Input;
            origin = input.ToLower();
            return origin;
        }
    }


    class StringNormalizeNewCase : StringOperation
    {
        public override string Operate(string origin)
        {
            var args = Args as NewCaseArgs;
            var input = args.Input;
            input = input.ToLower();

            char[] letters = input.ToCharArray();

            letters[0] = char.ToUpper(letters[0]);
            for (int i = 0; i < letters.Length - 1; i++)
            {
                if (letters[i] == ' ' && letters[i + 1] != ' ')
                {
                    letters[i + 1] = char.ToUpper(letters[i + 1]);
                }
            }

            input = new string(letters);
            return input;
        }
    }

    class FullnameNormalize : StringOperation
    {
        public override string Operate(string origin)
        {
            string result = "";
            var args = Args as FullnameNormalizeArg;
            var input = args.Input;
            // remove space in first and last of string
            input = input.Trim();

            // replace two space to one space
            while (input.IndexOf("  ") != -1)
            {
                input = input.Replace("  ", " ");
            }
            input = input.ToLower();

            string[] subName = input.Split(' ');
            for (int i = 0; i < subName.Length; i++)
            {
                string fistCharacter = subName[i].Substring(0, 1).ToUpper();
                string otherCharacter = subName[i].Substring(1);
                result += fistCharacter + otherCharacter + " ";
            }


            input = result;
            return input;
        }
    }
}
