using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class StringArgs
    {
       
    }

    public class ReplaceArgs : StringArgs, INotifyPropertyChanged
    {
        public string From { get; set; }
        public string To { get; set; }

        public bool ReplaceName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class NewCaseArgs : StringArgs, INotifyPropertyChanged
    {
        public int optionNewCase { get; set; } // =1: UPPER ; =2: lower; =3: Name normalize
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class FullnameNormalizeArg : StringArgs, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class MoveArgs : StringArgs, INotifyPropertyChanged
    {
        public int Start { get; set; }
        public int End { get; set; }
        // variable to check user want to move cut string to before or end of stringInput
        public bool Before { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }


    public class UniqueNameArgs : StringArgs, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }


    abstract class StringOperation : INotifyPropertyChanged
    {
        public StringArgs Args { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public abstract string Name { get;}
        public abstract string Description { get; }

        public abstract StringOperation Clone();

        public abstract bool Config();
        public abstract string Operate(string origin,string extension);
    }

    class ReplaceOperation : StringOperation, INotifyPropertyChanged
    {

        public override string Name => "Replace";

        public override string Description
        {
            get
            {
                var args = Args as ReplaceArgs;
                if(args.ReplaceName == true)
                { 
                return $"Replace Name from '{args.From}' to '{args.To}'";
                }
                else
                {
                    return $"Replace Extension from '{args.From}' to '{args.To}'";
                }
            }
        }

        public override StringOperation Clone()
        {
            var oldArgs = Args as ReplaceArgs;
            return new ReplaceOperation()
            {
                Args = new ReplaceArgs()
                {
                    From = oldArgs.From,
                    To = oldArgs.To,
                    ReplaceName = oldArgs.ReplaceName
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public override bool Config()
        {
            var screen = new ReplaceControl(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs("Description"));
                return true;
            }
            return false;
        }

        public override string Operate(string origin, string extension)
        {
            var args = Args as ReplaceArgs;
            var from = args.From;
            var to = args.To;
            if (from == "" && to == "")
                return origin + extension;
            if(extension==null)
            {
                return origin.Replace(from, to);
            }
            else
            {
                if (args.ReplaceName == false)
                {
                    return origin + extension.Replace(from, to);
                }
                return origin.Replace(from, to) + extension;
            }
        }
    }

    class NewCaseOperation : StringOperation
    {
        public override string Name => "New Case";

        public override string Description
        {
            get
            {
                var args = Args as NewCaseArgs;
                if (args.optionNewCase == 1)
                {
                    return $"Make name UPPER CASE";
                }
                else
                {
                    if(args.optionNewCase ==2)
                    {
                        return $"Make name lower case";
                    }
                }
                return $"Make name Name normalize";
            }
        }

        public override StringOperation Clone()
        {
            var oldArgs = Args as NewCaseArgs;
            return new NewCaseOperation()
            {
                Args = new NewCaseArgs()
                {
                    optionNewCase = oldArgs.optionNewCase
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public override bool Config()
        {
            var screen = new NewCaseControl(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs("Description"));
                return true;
            }
            return false;
        }

        public override string Operate(string origin, string extension)
        {
            var args = Args as NewCaseArgs;

            var optionNewCase = args.optionNewCase;

            var result = "";

            if(optionNewCase == 1)
            {
                result= origin.ToUpper();
            }
            else
            {
                if(optionNewCase ==2)
                {
                    result = origin.ToLower();
                }
                else
                {
                    result = origin.ToLower();
                    char[] letters = result.ToCharArray();

                    letters[0] = char.ToUpper(letters[0]);
                    for (int i = 0; i < letters.Length - 1; i++)
                    {
                        if (letters[i] == ' ' && letters[i + 1] != ' ')
                        {
                            letters[i + 1] = char.ToUpper(letters[i + 1]);
                        }
                    }

                    result = new string(letters);
                }
            }
            
            return result +extension;
        }
    }

    //class StringUpperOperation : NewCaseOperation
    //{
    //    public override string Name => "String Upper";

    //    public override string Description
    //    {
    //        get
    //        {
    //            var args = Args as NewCaseArgs;
    //            return $"Upper";
    //        }
    //    }


    //    public override string Operate(string origin)
    //    {
    //        var args = Args as NewCaseArgs;
    //        return origin.ToUpper();
    //    }
    //}

    //class StringLowerOperation : NewCaseOperation
    //{
    //    public override string Name => "String Lower";

    //    public override string Description
    //    {
    //        get
    //        {
    //            var args = Args as NewCaseArgs;
    //            return $"Lower";
    //        }
    //    }


    //    public override string Operate(string origin)
    //    {
            
    //        var args = Args as NewCaseArgs;
    //        return origin.ToLower();
    //    }
    //}


    //class StringNormalizeNewCaseOperation : NewCaseOperation
    //{
    //    public override string Name => "Normalize";

    //    public override string Description
    //    {
    //        get
    //        {
    //            var args = Args as NewCaseArgs;
    //            return $"Normalize";
    //        }
    //    }

    //    public override string Operate(string origin)
    //    {
    //        var args = Args as NewCaseArgs;
    //        string result = "";
    //        result = origin.ToLower();

    //        char[] letters = result.ToCharArray();

    //        letters[0] = char.ToUpper(letters[0]);
    //        for (int i = 0; i < letters.Length - 1; i++)
    //        {
    //            if (letters[i] == ' ' && letters[i + 1] != ' ')
    //            {
    //                letters[i + 1] = char.ToUpper(letters[i + 1]);
    //            }
    //        }

    //        result = new string(letters);
    //        return result;
    //    }
    //}

    class FullnameNormalizeOperation : StringOperation
    {
        public override string Name => "Fullname Normalize";

        public override string Description
        {
            get
            {
                var args = Args as ReplaceArgs;
                return $"Fullname Normalize";
            }
        }

        public override StringOperation Clone()
        {
            var oldArgs = Args as FullnameNormalizeArg;
            return new FullnameNormalizeOperation()
            {
                Args = new FullnameNormalizeArg()
                {

                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public override bool Config()
        {
            var screen = new FullNameNormalizeControl(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs("Description"));
                return true;
            }
            return false;
        }

        public override string Operate(string origin, string extension)
        {

            var args = Args as FullnameNormalizeArg;
            string result = "";
            string input = "";
            // remove space in first and last of string
            input = origin.Trim();

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

            return result + extension;
        }
    }


    /// <summary>
    /// example string have format ISBN FileName, move ISBN to before FileName or opposite 
    /// </summary>
    class MoveOperation : StringOperation
    {
        public override string Name => "Move";

        public override string Description
        {
            get
            {
                var args = Args as MoveArgs;
                if (args.Before == true)
                    return $"Move {args.End- args.Start} character at position {args.Start} -> {args.End} to before FileName";
                return $"Move {args.End - args.Start} character at position {args.Start} -> {args.End} to after FileName";
            }
        }

        public override StringOperation Clone()
        {
            var oldArgs = Args as MoveArgs;
            return new MoveOperation()
            {
                Args = new MoveArgs()
                {
                    Before = oldArgs.Before,
                    Start = oldArgs.Start,
                    End = oldArgs.End
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public override bool Config()
        {
            var screen = new MoveControl(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs("Description"));
                return true;
            }
            return false;
        }

        public override string Operate(string origin, string extension)
        {
            var args = Args as MoveArgs;

            string input = origin;
            var start = args.Start;
            var end = args.End;
            var before = args.Before;
            if (input.Length <= end-start)
                return input+extension;
            string firstString = input.Substring(start, end).Trim();
            string secondString = input.Substring(end).Trim();
            string result = "";
            
            if (before == true)
            {
                result = firstString + " " + secondString;
            }
            else
            {
                result += secondString + " " + firstString;
            }

            return result + extension;
        }
    }

    class UniqueNameOperation : StringOperation
    {
        public override string Name => "Unique Name";

        public override string Description
        {
            get
            {
                var args = Args as UniqueNameArgs;
                return $"Change to Unique Name (GUID)";
            }
        }

        public override StringOperation Clone()
        {
            var oldArgs = Args as UniqueNameArgs;
            return new UniqueNameOperation()
            {
                Args = new UniqueNameArgs()
                {
                }
            };
        }

       public event PropertyChangedEventHandler PropertyChanged;
        public override bool Config()
        {
            var screen = new UniqueNameControl(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs("Description"));
                return true;
            }
            return false;
        }

        public override string Operate(string origin, string extension)
        {
            string result = "";
            Guid id;
            id = Guid.NewGuid();
            result += id;
            return result + extension;
        }
    }


}
