using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
namespace Yuan.Text.Format.ini
{
    public class iniFile
    {
        string inputText;
        public iniFile(Stream Input)
        {

        }
        public iniFile(string input)
        {
            inputText = input;
            //inputText += '\r';
        }
        public iniItem[] GetAll()
        {
            type Type = type.NULL;
            string[] Lines = System.Text.RegularExpressions.Regex.Split(inputText, System.Environment.NewLine);
            string Section = "";
            List<iniItem> items = new List<iniItem>();
            foreach(string Line in Lines)
            {
                Type = type.NULL;
                char[] charArray = Line.ToCharArray();
                iniItem iniItem = new iniItem();
                foreach(char Char in charArray)
                {
                    switch (Type)
                    {
                        case (type.NULL):
                            if(Char == ';')
                            {
                                Type = type.exit;
                                //Debug.Print("a");
                            }
                            else if (Char == '[')
                            {
                                Type = type.section;
                                Section = "";
                                //Debug.Print("section");
                                
;                            }
                            else
                            {
                                Type = type.item_name;
                                iniItem.Name += Char;
                                //Debug.Print("item");
                            }
                            break;
                        case (type.section):
                            if (Char == ']')
                            {
                                Type = type.exit;
                            }
                            else
                            {
                                Section += Char;
                            }
                            break;
                        case (type.item_name):
                            if (Char == '=')
                            {
                                Type = type.item_value;
                            }
                            else
                            {
                                iniItem.Name += Char;
                            }
                            break;
                        case (type.item_value):
                                iniItem.Value += Char;
                            break;
                    }
                    if (Type == type.exit)
                    {
                        break;
                    }
                }
                if (Type == type.item_value)
                {
                    //test_001*01?new
                    iniItem.Value = stringFormat(iniItem.Value);
                    if (Section != "")
                    {
                        iniItem.Section = new iniSection(Section);
                    }
                    items.Add(iniItem);
                }
            }
            return items.ToArray();
        }
        
        private enum type
        {
            section,item_name, item_value, exit,NULL
        }
        //test_001*01?new
        private string stringFormat(string input)
        {
            char[] charArray = input.ToCharArray();
            bool a = false;//"
            bool start = false;//start
            string output="";
            foreach(char Char in charArray)
            {
                if (a)
                {
                    if (Char == '"')
                    {
                        break;
                    }
                    else
                    {
                        output += Char;
                    }

                }
                else if(!a && start)
                {
                    if (Char == ';')
                    {
                        break;
                    }
                    else
                    {
                        output += Char;
                    }
                }
                else
                {
                    if (Char == '"')
                    {
                        a = true;
                        start = true;
                    }
                    else
                    {
                        start = true;
                        output += Char;
                    }
                }
            }
            return output;
        }
    }
    public class iniItem
    {
        private iniSection sec;
        public iniSection Section
        {
            get
            {
                return sec;
            }
            set
            {
                this.sec = value;
            }
        }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }
        private string value;
        public string Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }
        public iniItem(iniSection sec, string name, string value)
        {
            this.sec = sec;
            this.name = name;
            this.value = value;
        }
        public iniItem()
        {

        }
    }
    public class iniSection
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public iniSection()
        {

        }
        public iniSection(string Name)
        {
            this.Name = Name;
        }
    }
    public class iniReader
    {
        iniFile inif;
        iniItem[] ii;
        public iniReader(string input)
        {
            inif = new iniFile(input);
            ii = inif.GetAll();

        }
        public string[] GetSections()
        {
            List<string> output = new List<string>();
            string LastSection = "";
            foreach(iniItem Item in ii)
            {
                if(LastSection== Item.Section.Name)
                {
                    continue;
                }
                else
                {
                    output.Add(Item.Section.Name);
                }
                LastSection = Item.Section.Name;
            }
            return output.ToArray();
        }
        public string[] GetKeys(string Section)
        {
            List<string> output = new List<string>();
            foreach (iniItem Item in ii)
            {
                if (Item.Section.Name == Section)
                {
                    output.Add(Item.Name);
                }
            }
            return output.ToArray();
        }
        public string GetValue(string Section, string Key)
        {
            string output = "";
            foreach (iniItem Item in ii)
            {
                if (Item.Section.Name == Section && Item.Name==Key)
                {
                    output=Item.Value;
                }
            }
            return output;
        }
    }
    
}
