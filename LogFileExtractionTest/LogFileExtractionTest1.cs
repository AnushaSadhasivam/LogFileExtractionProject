using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Linq;


namespace LogFileExtractionTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        [Test]
        public void Get_ReturnsIpAddressesCount()
        {
            string path = "/users/sadhaanu/Desktop/Anusha/Log/programming-task-example-data.log";


            Dictionary<IPAddress, int> ipInstances = new Dictionary<IPAddress, int>();
            Dictionary<string, int> urlinstances = new Dictionary<string, int>();
            List<string> ips = new List<string>();
            string[] lines = File.ReadAllLines(path);

           
            foreach (string line in lines)
            {
                Regex IPAd = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
                MatchCollection MatchResult = IPAd.Matches(line);
                MatchCollection urls = Regex.Matches(line, @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                ips.Add(MatchResult[0].ToString());
               
            }
            Assert.AreEqual(23, ips.Count);
        }
        [Test]
        public void Get_ReturnsTop3IpAddresses()
        {
            string path = "/users/sadhaanu/Desktop/Anusha/Log/programming-task-example-data.log";


            Dictionary<IPAddress, int> ipInstances = new Dictionary<IPAddress, int>();
            Dictionary<string, int> urlinstances = new Dictionary<string, int>();
            List<string> ips = new List<string>();
            IPAddress ip;
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                Regex IPAd = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
                MatchCollection MatchResult = IPAd.Matches(line);
                MatchCollection urls = Regex.Matches(line, @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                ips.Add(MatchResult[0].ToString());
                ip = IPAddress.Parse(MatchResult[0].ToString());//extract your IP from the line string


                if (!ipInstances.ContainsKey(ip))
                {
                    ipInstances.Add(ip, 0);
                }
                ipInstances[ip]++;

            }
            var orderipInstances = ipInstances.OrderByDescending(kvp => kvp.Value).Take(3).Select(kvp => kvp.Key).ToList();
            Assert.AreEqual("168.41.191.40", orderipInstances[0].ToString());
            Assert.AreEqual("177.71.128.21", orderipInstances[1].ToString());
            Assert.AreEqual("50.112.0.11", orderipInstances[2].ToString());
        }
        [Test]
        public void Get_ReturnsTop3Urls()
        {
            string path = "/users/sadhaanu/Desktop/Anusha/Log/programming-task-example-data.log";


            Dictionary<IPAddress, int> ipInstances = new Dictionary<IPAddress, int>();
            Dictionary<string, int> urlinstances = new Dictionary<string, int>();
            List<string> ips = new List<string>();
            IPAddress ip;
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                Regex IPAd = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
                MatchCollection MatchResult = IPAd.Matches(line);
                MatchCollection urls = Regex.Matches(line, @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                string url = line.Substring(line.IndexOf("GET")).Split('"').First();

                if (!urlinstances.ContainsKey(url))
                {
                    urlinstances.Add(url, 0);
                }
                urlinstances[url]++;
            }

        
            var orderedUrls = urlinstances.OrderByDescending(kvp => kvp.Value).Take(3).Select(kvp => kvp.Key).ToList();
            Assert.AreEqual("GET /docs/manage-websites/ HTTP/1.1", orderedUrls[0].ToString());
            Assert.AreEqual("GET /intranet-analytics/ HTTP/1.1"  , orderedUrls[1].ToString());
            Assert.AreEqual("GET http://example.net/faq/ HTTP/1.1", orderedUrls[2].ToString());
        }
    }
}
