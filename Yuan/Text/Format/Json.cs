using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;
using System.IO;

namespace Yuan.Text.Format.JSON
{
    public class JSONItem
    {
        public JSONItemType ItemType
        {
            get
            {
                if (Item == null) return JSONItemType.Null;
                string type = Item.GetType().ToString();
                JSONItemType itemType = JSONItemType.Null;
                if (type == (typeof(String).ToString()))
                {
                    itemType = JSONItemType.String;
                }
                else if (type == (typeof(JSONObject).ToString()))
                {
                    itemType = JSONItemType.Object;
                }
                else if (type == (typeof(JSONArray).ToString()))
                {
                    itemType = JSONItemType.Array;
                }
                else if (type == (typeof(long).ToString()))
                {
                    itemType = JSONItemType.Number;
                }
                return itemType;
            }
        }
        public object Item { get; set; }
        public JSONItem(JSONArray source)
        {
            Item = source;
        }
        public JSONItem(JSONObject source)
        {
            Item = source;
        }
        public JSONItem(string Input)
        {
            //Convert JSONSourceText To JSONItem Class.
            char[] CharArray = Input.Replace("\r\n", "").ToCharArray();
            string Temp_Name = "";
            string Temp_Value = "";
            JSONItemType temp_type = JSONItemType.Null;
            int subitemcount = 0;
            bool object_name = false;
            bool object_value = false;
            string[] boolean = new string[] { "true", "false" };
            char[] number = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'e', 'E', '-', '+', '.' };
            //
            object temp_out = new object();
            bool escape_str = false;
            bool IsString = false;
            foreach (char Char in CharArray)
            {
                switch (temp_type)
                {
                    case (JSONItemType.Null):
                        if (Char != ' ')
                        {
                            switch (Char)
                            {
                                case ('{'):
                                    temp_type = JSONItemType.Object;
                                    temp_out = new JSONObject();
                                    break;
                                case ('['):
                                    temp_type = JSONItemType.Array;
                                    temp_out = new JSONArray();
                                    break;
                                case ('"'):
                                    //Debug.Print(Input);
                                    temp_type = JSONItemType.String;
                                    temp_out = "";
                                    break;
                                default:

                                    if (boolean.Contains(Input.ToLower()))
                                    {
                                        temp_type = JSONItemType.Bool;
                                        Item = Convert.ToBoolean(Input);
                                        break;
                                    }
                                    else if (number.Contains(Char))
                                    {
                                        temp_type = JSONItemType.Number;
                                        temp_out = 0;
                                        Temp_Value = "";
                                        Temp_Value += Char;
                                    }
                                    else
                                    {

                                    }
                                    break;
                            }
                        }
                        break;
                    case (JSONItemType.Array):
                        //[value,value].......
                        if (Char == '"')
                        {
                            if (IsString)
                            {
                                if (!escape_str) IsString = false;
                                else escape_str = false;
                            }
                            else
                            {
                                IsString = true;
                            }
                        }
                        if (Char == ']' && subitemcount == 0)
                        {
                            ((JSONArray)temp_out).Add(new JSONItem(Temp_Value));
                            Temp_Value = "";
                            Item = ((JSONArray)temp_out);
                            object_name = false;
                            object_value = false;
                            Temp_Name = "";
                            Temp_Value = "";
                            temp_out = null;
                            break;
                        }
                        else if (Char != ',' ||(Char == ',' && subitemcount > 0))
                        {
                            if (Char == '{' || Char == '['&&!IsString)
                            {
                                subitemcount++;
                            }
                            else if (Char == '}' || Char == ']' && !IsString)
                            {
                                subitemcount--;
                            }
                            Temp_Value += Char;
                        }
                        else if (Char == ',' && subitemcount == 0)
                        {
                            ((JSONArray)temp_out).Add(new JSONItem(Temp_Value));
                            Temp_Value = "";
                        }
                        if (Char == '\\' && !escape_str)
                        {
                            escape_str = true;
                        }
                        else if (escape_str)
                        {
                            escape_str = false;
                        }
                        break;
                    case (JSONItemType.Object):
                        //{name(string):value,.......}
                        if (object_value)
                         {
                            if (Char == '"')
                            {
                                if (IsString)
                                {
                                    if (!escape_str) IsString = false;
                                    else escape_str = false;
                                }
                                else
                                {
                                    IsString = true;
                                }
                            }
                            if (Char == '}' && subitemcount == 0)
                            {
                                ((JSONObject)temp_out).Add(new JSONSubObject() { Name = Temp_Name, Value = new JSONItem(Temp_Value) });
                                Item = ((JSONObject)temp_out);
                                object_name = false;
                                object_value = false;
                                Temp_Name = "";
                                Temp_Value = "";
                                break;
                            }
                            else if ((!(Char == ',' && subitemcount == 0)))
                            {
                                if (Char == '{' || Char == '['&& !IsString)
                                {
                                    subitemcount++;
                                }
                                else if (Char == '}' || Char == ']' && !IsString)
                                {
                                    subitemcount--;
                                }
                                Temp_Value += Char;
                            }
                            else if (Char == ',' && subitemcount == 0&&!IsString)
                            {
                                ((JSONObject)temp_out).Add(new JSONSubObject() { Name = Temp_Name, Value = new JSONItem(Temp_Value) });
                                Temp_Name = "";
                                Temp_Value = "";
                                object_value = false;
                                object_name = false;
                            }
                            if (Char == '\\'&&!escape_str)
                            {
                                escape_str = true;
                            }
                            else if (escape_str)
                            {
                                escape_str = false;
                            }
                        }
                        else if (object_name)
                        {
                            if (Char == '"')
                            {
                                object_name = false;
                            }
                            else
                            {
                                Temp_Name += Char;
                            }
                        }
                        else if (!object_name && !object_value)
                        {
                            if (Char == '"')
                            {
                                object_name = true;
                            }
                            else if (Char == ':')
                            {
                                object_value = true;
                            }
                        }


                        break;
                    case (JSONItemType.String):
                        //""
                        if (!escape_str)
                        {
                            if (Char == '"')
                            {
                                Item = Temp_Value;
                                //Debug.Print("T:{0} I:{1}",Temp_Value,Input);
                                break;
                            }
                            else if (Char == '\\')
                            {
                                escape_str = true;
                            }
                            else
                            {
                                Temp_Value += Char;
                            }
                        }
                        else
                        {
                            switch (Char)
                            {
                                case '\\':
                                    Temp_Value += '\\';
                                    escape_str = false;
                                    break;
                                case 'r':
                                    Temp_Value += '\r';
                                    escape_str = false;
                                    break;
                                case 'n':
                                    Temp_Value += '\n';
                                    escape_str = false;
                                    break;
                                case '"':
                                    Temp_Value += '"';
                                    escape_str = false;
                                    break;
                                case 't':
                                    Temp_Value += '\t';
                                    escape_str = false;
                                    break;
                                case 'b':
                                    Temp_Value += '\b';
                                    escape_str = false;
                                    break;
                                case 'f':
                                    Temp_Value += '\f';
                                    escape_str = false;
                                    break;
                                case '\'':
                                    Temp_Value += '\'';
                                    escape_str = false;
                                    break;
                                default:
                                    escape_str = false;
                                    break;
                            }
                        }

                        break;
                    case (JSONItemType.Bool):
                        //True and False

                        break;
                    case (JSONItemType.Number):
                        //Number
                        if (number.Contains(Char))
                        {
                            Temp_Value += Char;
                        }

                        break;
                }
            }
            if (temp_type == JSONItemType.Number)
            {
                Item = Convert.ToInt64(Temp_Value, 10);
            }
        }
        public string ToJSONFormat()
        {
            switch (ItemType)
            {
                case (JSONItemType.Array):
                    return ((JSONArray)Item).ToJSONFormat();
                case (JSONItemType.Bool):
                    return ((JSONArray)Item).ToString();
                case (JSONItemType.Null):
                    return "";
                case (JSONItemType.Number):
                    return ((int)Item).ToString();
                case (JSONItemType.Object):
                    return ((JSONObject)Item).ToJSONFormat();
                case (JSONItemType.String):
                    return ((string)Item);
                default:
                    return "";
            }
        }
    }
    public class JSONSubObject
    {
        public string Name { get; set; }
        public JSONItem Value { get; set; }
        public JSONSubObject()
        {

        }
        public string ToJSONFormat()
        {
            return $@"{Name}:{Value.ToJSONFormat()}";
        }
    }
    public class JSONObject : IEnumerable
    {
        public JSONSubObject[] Items { get { return items.ToArray(); } }
        private List<JSONSubObject> items = new List<JSONSubObject>();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public JSONObjectEnum GetEnumerator()
        {
            return new JSONObjectEnum(Items);
        }
        public JSONSubObject this[int index]
        {
            get
            {
                return Items[index];
            }
            set
            {
                items[index] = value;
            }
        }
        public JSONSubObject this[string Name]
        {
            get
            {
                return Array.Find(Items, x => x.Name == Name);
            }
            set
            {
                int index = Array.FindIndex(Items, x => x.Name == Name);
                items[index] = value;
            }
        }
        public void Add(JSONSubObject item)
        {
            items.Add(item);
        }
        public void Remove(JSONSubObject item)
        {
            items.Remove(item);
        }
        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }
        public string ToJSONFormat()
        {
            string[] subobject_jsonformat = Array.ConvertAll(items.ToArray(), x => x.ToJSONFormat());
            return "{"+$@"{String.Join(",", subobject_jsonformat)}"+"}";
        }
    }
    public class JSONObjectEnum : IEnumerator
    {
        public JSONSubObject[] Items = new JSONSubObject[] { };
        int position = -1;
        public JSONObjectEnum(JSONSubObject[] items)
        {
            Items = items;
        }
        public bool MoveNext()
        {
            position++;
            return (position < Items.Length);
        }
        public void Reset()
        {
            position = -1;
        }
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
        public JSONSubObject Current
        {
            get
            {
                try
                {
                    return Items[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
    public class JSONArray : IEnumerable
    {
        public JSONItem[] Items { get { return items.ToArray(); } }
        private List<JSONItem> items = new List<JSONItem>();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public JSONArrayEnum GetEnumerator()
        {
            return new JSONArrayEnum(Items);
        }
        public JSONItem this[int index]
        {
            get
            {
                return Items[index];
            }
            set
            {
                items[index] = value;
            }
        }
        public void Add(JSONItem item)
        {
            items.Add(item);
        }
        public void Remove(JSONItem item)
        {
            items.Remove(item);
        }
        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }
        public string ToJSONFormat()
        {
            string[] item_json_format = Array.ConvertAll(items.ToArray(), x => x.ToJSONFormat());
            return $@"[{String.Join(",",item_json_format)}]";
        }
    }
    public class JSONArrayEnum : IEnumerator
    {
        public JSONItem[] Items = new JSONItem[] { };
        int position = -1;
        public JSONArrayEnum(JSONItem[] items)
        {
            Items = items;
        }
        public bool MoveNext()
        {
            position++;
            return (position < Items.Length);
        }
        public void Reset()
        {
            position = -1;
        }
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
        public JSONItem Current
        {
            get
            {
                try
                {
                    return Items[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
    public enum JSONItemType
    {
        String, Number, Object, Array, Bool, Null
    }
}