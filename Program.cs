using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;


namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {   
            //Read an file
            string fileName = @"C:\Users\sanka\OneDrive\Desktop\Pranav\students.xml";
            string text;
            using (StreamReader streamReader = File.OpenText(fileName))
            {
                 text = streamReader.ReadToEnd();
                Console.WriteLine(text);

            }
            //create a stack
            Stack<string> stack = new Stack<string>();
            stack.Push("0");

            //create dictonary of schema
            Dictionary<string, Int16> Schema = new Dictionary<string, Int16>();
            Schema.Add("0", 0);
            Schema.Add("<students>", 1);
            Schema.Add("<student>", 2);
            Schema.Add("<name>", 3);
            Schema.Add("<age>", 3);
            Schema.Add("<subject>", 3);
            Schema.Add("<gender>", 3);


            Int16 line = 0;
            Dictionary<Int16, string> missing = new Dictionary<Int16, string>();

            string pattern = Regex.Escape("<") + "(.*?)>";
            

            MatchCollection matches = Regex.Matches(text, pattern);
            var myResultList = new List<string>();
            foreach (Match match in matches)
            {
                myResultList.Add(match.Value);
            }

            for (int i = 0; i < myResultList.Count; i++)
            {
                //Console.WriteLine(myResultList[i].ToString());
                string k = myResultList[i].ToString();
                int poped = 0;
                if (k.IndexOf("/")== -1)
                {
                    if (Schema[stack.Peek()] < Schema[k])
                    {
                        stack.Push(k);
                    }
                    else
                    {
                        missing.Add(line, stack.Pop());
                        stack.Push(k); 
                    }
                }
                else
                {
                    poped = Schema[stack.Pop()];
                
                }
                if (poped != 3)
                {
                    line += 1;
                }

            }
            foreach (var kvp in missing)
            {
                Console.WriteLine("line no. = {0}, tag = {1}", kvp.Key, kvp.Value);
            }


        }
    }
}
