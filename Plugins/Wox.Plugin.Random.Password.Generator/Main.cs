using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wox.Plugin;

namespace Wox.Plugin.Random.Password.Generator
{
    class Main : IPlugin
    {
        private System.Random R = new System.Random();
        int Capsloock=0;
        public List<Result> Query(Query query)
        {
            List<Result> Lists = new List<Result>();
            string[] args = query.Search.Split(' ');
            int number = 1;
            int length = 0;
            Capsloock = 0;
            switch (args[0])
            {
                case "l":
                    Capsloock = 1;
                    break;
                case "u":
                    Capsloock = 2;
                    break;
                case "lu":
                    Capsloock = 3;
                    break;
            }
            if (Capsloock == 0)
            {
                if (args.Length == 2)
                {
                    int.TryParse(args[0], out number);
                    int.TryParse(args[1], out length);
                }
                else
                {
                    int.TryParse(args[0], out length);
                }
                if (length == 0)
                    return Lists;
            }
            else
            {
                if (args.Length == 3)
                {
                    int.TryParse(args[1], out number);
                    int.TryParse(args[2], out length);
                }
                else
                {
                    int.TryParse(args[1], out length);
                }
                if (length == 0)
                    return Lists;
            }
            
            
            for (int i = 0; i < number; i++)
            {
                var p = RandomPasswordGenerator(length);
                   var result = new Result
                {
                    Title = "Random Password Generator",
                    SubTitle = $"Select To copy : {p}",// $"Length : {length} ,Select To copy password"
                    IcoPath =  "Images\\app.png",
                    Action = _ =>
                    {
                        Clipboard.SetText(p);
                        return true;
                    }
                };
                Lists.Add(result);
            }


            return Lists;
        }
        string Chars = "mnopqrstuvwxyz12345abcdefghijkl67890#%&*_<>?";
        string RandomPasswordGenerator(int Length)
        {
            string password = "";
            for(int i = 0; i < Length; i++)
            {
                switch (Capsloock) {
                    case 0:
                        password += Chars[R.Next(Chars.Length)];
                        break;
                    case 1:
                        password += Chars[R.Next(Chars.Length)].ToString().ToLower();
                        break;
                    case 2:
                        password += Chars[R.Next(Chars.Length)].ToString().ToUpper();
                        break;
                    case 3:
                        if(R.Next(20)>10)
                            password += Chars[R.Next(Chars.Length)].ToString().ToUpper();
                        else
                            password += Chars[R.Next(Chars.Length)].ToString().ToLower();
                        break;
                }
                    
            }
            return password;
        }
        public void Init(PluginInitContext context)
        {
            
        }
    }
}
