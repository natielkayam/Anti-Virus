using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Collections;
using System.Xml;

namespace Cyber_Func
{
    class Malicious
    {
        private static Malicious instance;
        private List<White_Item> whitelist;
        private Dictionary<string, string[]> detectedList;
        public event Action<int> DetectedListCountChanged;


        //private readonly Dictionary<string, string> detectedList;
        //private readonly Dictionary<List<KeyValuePair<string, string>> , string> expertdetectedList;


        private Malicious()
        {
            this.whitelist = new List<White_Item>();
            this.detectedList = new Dictionary<string, string[]>();

            //string path = "whitelist.xml";
            string path = "C:\\Users\\User\\Documents\\project\\Cyber_Func\\whitelist.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            XmlNodeList itemNodes = xmlDoc.SelectNodes("/whitelist/item");
            foreach (XmlNode itemNode in itemNodes)
            {
                string md5 = itemNode.Attributes["md5"].Value;
                string name = itemNode.Attributes["name"].Value;
                string version = itemNode.Attributes["version"].Value;

                this.whitelist.Add(new White_Item(md5, name, version));
            }
        }

        public static Malicious GetMaliciousInstance()
        {
            if( instance == null)
            {
                instance = new Malicious();
            }
            return instance;
        }

        internal bool IsMalicious(string input, string filename, string location, bool IsExpert = false)
        {
            //TODO: 
            //check if the hash from input belong to whitelist

            //improve the search time , store all the hashes for first lookup
            HashSet<string> whiteListHashes = new HashSet<string>(this.whitelist.Select(item => item.GetKeyHash()));
            HashSet<string> detectedListHashes = new HashSet<string>(this.detectedList.Select(item => item.Key));

            if (detectedListHashes.Contains(input))
            {
                System.Windows.Forms.MessageBox.Show($"{filename} is already in the detected list");
                return true;
            }
            string timestamp = DateTime.Now.ToString();
            
            //TODO: check similiraty and add this to the condition
            double maxSimilarity = 0;
            string maxSimilarityKeyHash = null;
            foreach (var key in this.detectedList.Keys)
            {
                double similarity = CheckSimilarity(input, key);
                if (similarity > maxSimilarity)
                {
                    maxSimilarity = similarity;
                    maxSimilarityKeyHash = key;
                }
            }
            if (maxSimilarity >= 80)
            {
                //Console.WriteLine("detected due to similarity to 1 from the detected list");
                //System.Windows.Forms.MessageBox.Show("detected due to similarity to 1 from the detected list");
                string[] arr = { filename, location, timestamp, "similarity" };
                AddToDetectedList(input, arr);
                return true;
            }
            //TODO:check if the whitelist not contain the input
            if (!whiteListHashes.Contains(input))
            {
                if (!this.detectedList.ContainsKey(input))
                {
                    //System.Windows.Forms.MessageBox.Show("detected due to whitelist policy");
                    string[] arr = { filename, location, timestamp ,"whitelist" };
                    AddToDetectedList(input, arr);
                }
                return true;
            }
            return false;
        }
        public void AddToDetectedList(string md5, string[] arr)
        {
            this.detectedList.Add(md5,arr);
            OnDetectedListCountChanged(detectedList.Count);
        }

        private void OnDetectedListCountChanged(int count)
        {
            DetectedListCountChanged?.Invoke(count);
        }

        internal double CheckSimilarity(string md5Hash1, string md5Hash2)
        {
            if (md5Hash1.Length != md5Hash2.Length)
            {
                return 0;
            }

            byte[] hashBytes1 = StringToByteArray(md5Hash1);
            byte[] hashBytes2 = StringToByteArray(md5Hash2);

            int matches = 0;
            for (int i = 0; i < hashBytes1.Length; i++)
            {
                if (hashBytes1[i] == hashBytes2[i])
                {
                    matches++;
                }
            }

            double similarity = (matches / (double)hashBytes1.Length) * 100;
            return similarity;
        }

        private byte[] StringToByteArray(string input)
        {
            int numberChars = input.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(input.Substring(i, 2), 16);
            }
            return bytes;
        }

        internal Dictionary<string, string[]> GetDetected()
        {
            return this.detectedList;
        }
    }
}