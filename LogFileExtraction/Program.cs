using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Linq;
namespace LogFileExtraction
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Checking the log file!");
            string path = "/users/sadhaanu/Desktop/Anusha/Log/programming-task-example-data.log";
            

            Dictionary<IPAddress, int> ipInstances = new Dictionary<IPAddress, int>();
            Dictionary<string, int> urlinstances = new Dictionary<string, int>();
            List<string> ips = new List<string>();
            List<string> listUrls = new List<string>();
            string[] lines = File.ReadAllLines(path);
            IPAddress ip;


            foreach (string line in lines)
            {
                Regex IPAd = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
                MatchCollection MatchResult = IPAd.Matches(line);
                    MatchCollection urls = Regex.Matches(line, @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                ips.Add(MatchResult[0].ToString());
                try
                {
                    ip = IPAddress.Parse(MatchResult[0].ToString());//extract your IP from the line string


                    if (!ipInstances.ContainsKey(ip))
                    {
                        ipInstances.Add(ip, 0);
                    }
                    ipInstances[ip]++;

                }
                catch (Exception e)
               {
                Console.WriteLine("Exception caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
              }
            string url=line.Substring(line.IndexOf("GET")).Split('"').First();

                if(!urlinstances.ContainsKey(url))
                {
                    urlinstances.Add(url, 0);
                }
                urlinstances[url]++;
            }
           
            var orderipInstances = ipInstances.OrderByDescending(kvp => kvp.Value).Take(3).Select(kvp => kvp.Key).ToList();
            var orderedUrls = urlinstances.OrderByDescending(kvp => kvp.Value).Take(3).Select(kvp => kvp.Key).ToList(); 
            Console.WriteLine("No of ip addresses found in the log file are " +ips.Count);
            Console.WriteLine("Top 3 ip addresses found in the file are "+orderipInstances[0] +" ," + orderipInstances[1] + " and "+ orderipInstances[2]);
            Console.WriteLine("Top 3 urls found in the log file are " + orderedUrls[0] +" , " + orderedUrls[1] +" and  " + orderedUrls[2]);
         
        }
    }
}
