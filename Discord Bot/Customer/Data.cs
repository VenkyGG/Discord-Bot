using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Discord_Bot.Customer
{
    public sealed class Data
    {
        private static Data instance = null;
        private static readonly object padlock = new object();

        private string filePath = "Customer-Information.txt";

        private string customerID;
        private int customerTier;
        private float customerSpendings;

        public List<string> customerInformation;

        Data() // Constructor
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine(filePath + " not found. Generating a new .txt file");

                File.Create(filePath).Close();
            }
            else
            {
                init();
            }
        }

        public static Data Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Data();
                    }

                    return instance;
                }
            }
        }

        ~Data() // Destructor does not get called at all dont bother adding code inside.
        {
            writeFile();
        }

        private void init()
        {
            customerInformation = File.ReadAllLines(filePath).ToList();
        }

        private void obtainData(string discordID)
        {
            int tmpCounter = 1;

            int zeroTmp = 0;

            bool skipCheck = false;

            bool needWriteFile = false;

            if (customerInformation.Count == 0) // If list is empty, instantly add the data into the list.
            {
                customerID = discordID;
                customerTier = 0;
                customerSpendings = 0;

                skipCheck = true;

                needWriteFile = true;
            }

            foreach (string line in customerInformation)
            {
                string[] arrLine = line.Split(":");

                if (skipCheck == true)
                {
                    break;
                }

                if (discordID == arrLine[0])
                {
                    customerID = arrLine[0];
                    customerTier = int.Parse(arrLine[1]);
                    customerSpendings = float.Parse(arrLine[2]);

                    break;
                }

                if (tmpCounter == customerInformation.Count) // Falls under here if there is no database for requested user.
                {
                    customerID = discordID;
                    customerTier = 0;
                    customerSpendings = 0;

                    needWriteFile = true;

                    break;
                }

                tmpCounter++;
            }

            if (needWriteFile == true)
            {
                string tmp = customerID + ":" + customerTier.ToString() + ":" + customerSpendings.ToString();

                customerInformation.Add(tmp);

                writeFile();
            }
        }

        private void updateInformation(string discordID)
        {
            int tmpCounter = 0;

            foreach (string line in customerInformation)
            {
                string[] arrLine = line.Split(":");

                if (discordID == arrLine[0])
                {
                    break;
                }

                tmpCounter++;
            }

            if (customerInformation.Count != 0)
            {
                customerInformation.RemoveAt(tmpCounter);
            }

            customerInformation.Insert(tmpCounter, customerID + ":" + customerTier.ToString() + ":" + customerSpendings.ToString());

            writeFile();
        }

        private void writeFile()
        {
            foreach (string line in customerInformation)
            {
                File.WriteAllLines(filePath, customerInformation);
            }
        }

        public int getTier(string discordID) // Rewrite code
        {
            bool needToWriteFile = setTier(discordID);

            if (needToWriteFile == true)
            {
                updateInformation(discordID);
            }

            return customerTier;
        }

        private bool setTier(string discordID) // Checks and see if there's a need to update the customer's tier.
        {
            obtainData(discordID);

            float tempCustomerSpendings = getSpendings(discordID);

            int tempCustomerTier = 0;

            if (tempCustomerSpendings >= 50.0f && tempCustomerSpendings < 100.0f)
            {
                tempCustomerTier = 1;
            }
            else if (tempCustomerSpendings >= 100.0f && tempCustomerSpendings < 400.0f)
            {
                tempCustomerTier = 2;
            }
            else if (tempCustomerSpendings >= 400.0f && tempCustomerSpendings < 1000.0f)
            {
                tempCustomerTier = 3;
            }
            else if (tempCustomerSpendings >= 1000.0f)
            {
                tempCustomerTier = 4;
            }
            else
            {
                tempCustomerTier = 0;
            }

            if (tempCustomerTier == customerTier)
            {
                return false; // If returns false that means there no need to update Customer's tier.
            }
            else
            {
                customerTier = tempCustomerTier;

                return true; // If returns true that means there is a need to update Customer's Tier and rewrite file.
            }
        }

        public float getSpendings(string discordID) // Retrieves Customer's Spendings from whoever runs the command
        {
            obtainData(discordID);

            return customerSpendings;
        }

        public void addSpendings(string discordID, float amountToAdd)
        {
            obtainData(discordID);

            customerSpendings += amountToAdd;

            updateInformation(discordID);
        }

        public void removeSpendings(string discordID, float amountToRemove)
        {
            obtainData(discordID);

            customerSpendings -= amountToRemove;

            updateInformation(discordID);
        }

        public void setSpendings(string discordID, float amountToSet)
        {
            obtainData(discordID);

            customerSpendings = amountToSet;

            updateInformation(discordID);
        }
    }
}