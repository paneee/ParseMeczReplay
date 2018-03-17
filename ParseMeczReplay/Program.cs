using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParseMeczReplay
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://meczreplay.blogspot.com/search?max-results=125";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("/html/body/div[1]/div[3]/div/div[2]/main/div/div/div[1]/article");

            foreach (HtmlNode node in nodes)
            {
                if (node != null)
                {
                    HtmlNodeCollection nodesTitleOrNameLinks = node.SelectNodes("div/div/div[3]/div[2]/div");
                    if (nodesTitleOrNameLinks != null)
                    {
                        string[] separators = new string[] { "<br>", "\n", "<b>", "</b>" };
                        string[] linesNodesA = nodesTitleOrNameLinks.First().InnerText.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string line in linesNodesA)
                        {
                            string lineNormalized = line.Replace("&#160", " ").Replace(";", "");
                            int indexHttp = lineNormalized.IndexOf("http");

                            if (indexHttp == -1)
                            {
                                string Title = lineNormalized;
                                Console.WriteLine(Title);
                            }
                            else
                            { 
                                string Name = lineNormalized.Remove(indexHttp);
                                string Link = lineNormalized.Remove(0, indexHttp);
                                Console.WriteLine(Name);
                                Console.WriteLine(Link);
                            }
                        }
                    }
                }
                HtmlNode nodeImg = node.SelectSingleNode("div/div/div[3]/div[1]/img");
                if (nodeImg != null)
                {
                    string Img = nodeImg.GetAttributeValue("src", "");
                    Console.WriteLine(Img);
                }
                Console.WriteLine("=====================================================");
            }
            Console.ReadKey();
        }
    }
}
