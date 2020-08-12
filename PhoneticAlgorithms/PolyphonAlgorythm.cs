using PhoneticAlgorithms.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace PhoneticAlgorithms
{
    public class PolyphonAlgorythm
    {
        static List<Tuple<string[], string>> PolyphoneReplaces;
        static StringBuilder builder = new StringBuilder();


        public static string ReplaceDoubleLetters(string word)
        {
            for (int i = 1; i < word.Length; i++)
                if (word[i] == word[i - 1])
                    word = word.Replace($"{word[i]}{word[i - 1]}", $"{word[i]}");
            return word;
        }

        public static string ReadXmlTag(string tag)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(Resources.ReplaceDictionary);
            string doc = xmlDocument.GetElementsByTagName(tag)[0].InnerText;
            return Regex.Replace(doc, @"[ \r\n\t]", "");

        }

        public static int FuzzyPolyphone(string metaWord)
        {
            string keys = ReadXmlTag("FUZZY_POLYPHONE");
            List<string[]> keysArray = new List<string[]>();
            keysArray.AddRange(keys.Split(';').Select(x => x.Split(':')));
            int fuzzy = 0;
            foreach (var item in keysArray)
            {
                metaWord.Select(x => x.ToString().Equals(item[0]) ? fuzzy += int.Parse(item[1]) : 1).ToArray();
            }
            return fuzzy;
        }

        public static string Polyphone(string word)
        {
            word = word.ToUpper();
            string replaces = ReadXmlTag("POLYPHONE");
            PolyphoneReplaces = new List<Tuple<string[], string>>();

            PolyphoneReplaces.AddRange(replaces.Split(';').Select(x => x.Split(':')).Select(x =>
               (new Tuple<string[], string>(x[0].Split(','), x[1]))));

            foreach (var item in PolyphoneReplaces)
            {
                item.Item1.Select(x => word = word.Replace(x, item.Item2)).ToArray();
            }
            word = ReplaceDoubleLetters(word);
            return word;
        }
    }
}
