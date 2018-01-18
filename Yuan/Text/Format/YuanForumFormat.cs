using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yuan.Text.Format
{
    public class YuanForumFormat
    {
        string Text = "";
        public YuanForumFormat(string Text)
        {
            this.Text = Text;
        }
        public string ConvertToHTML()
        {
            List<string> list = new List<string>();
            bool IsTag = false;
            bool IsTagEnter = false; //標籤開頭
            bool IsTagExit = false; //標籤結尾
            bool IsTagIn = false; //標籤內
            bool IsQu = false;
            char[] CharArray = Text.Replace("<", "&lt;").Replace(">", "&gt;").ToCharArray();
            list.AddRange(new string[] { "<p>", "", "</p>" });
            int index = 1;
            int spanindex = 0;
            string temp = "";
            foreach (char Char in CharArray)
            {
                if (!IsTag)
                {
                    if (Char == '{')
                    {
                        IsTag = true;
                        IsTagEnter = true;
                    }
                    else if (Char == '"')
                    {
                        IsQu = true;
                    }
                    else if (IsQu)
                    {
                        if (Char == '"')
                        {
                            list[index] = $@"<q>{temp}</q>";
                            list.Insert(index + 1, "");
                            index += 1;
                            IsQu = false;
                        }
                        else
                        {
                            temp += Char;
                        }
                    }
                    else
                    {
                        list[index] += "";
                    }
                }
                else
                {

                }
            }
            return "";
        }
        public class CSSItem
        {
            
            public CSSItem()
            {

            }
            
        }
    }
}
