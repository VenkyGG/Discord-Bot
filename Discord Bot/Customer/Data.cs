using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Discord_Bot.Customer
{
    public class Data
    {
        private string filePath = "Customer-Information.txt";

        private string customerID;
        private int customerTier;
        private float customerSpendings;

        public Data(string sendersID) // Constructor
        {
            obtainData(sendersID);
        }

        private void obtainData(string tf)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();

            foreach (var theLines in lines)
            {
                string[] entries = theLines.Split(':');

                customerID = entries[0];

                customerTier = int.Parse(entries[1]);

                customerSpendings = float.Parse(entries[2]); // Doesnt work for decimal.

                if (customerID == tf)
                {
                    break;
                }
            }
        }

        private void writeFile()
        {
            int lte;

            string newText;

            string[] arrLine = File.ReadAllLines(filePath);

            //arrLine[lte - 1] = newText;

            File.WriteAllLines(filePath, arrLine);
        }

        ~Data() // Deconstructor
        {

        }

        public string getID() // Retrieves Customer's unique Discord ID from whoever runs the command.
        {
            return customerID;
        }

        public int getTier() // Retrieves Customer's Tier from whoever runs the command.
        {
            return customerTier;
        }

        public void setTier()
        {
            double tempCustomerSpendings = getSpendings();

            if (tempCustomerSpendings > 50.0f && tempCustomerSpendings <= 99.99f)
            {
                customerTier = 1;
            }
            else if (tempCustomerSpendings > 100.0f && tempCustomerSpendings <= 399.99f)
            {
                customerTier = 2;
            }
            else if (tempCustomerSpendings > 400.0f && tempCustomerSpendings <= 999.99f)
            {
                customerTier = 3;
            }
            else if (tempCustomerSpendings > 1000.0f)
            {
                customerTier = 4;
            }
            else
            {
                customerTier = 0;
            }

            writeFile();
        }

        public float getSpendings() // Retrieves Customer's Spendings from whoever runs the command
        {
            return customerSpendings;
        }

        public void addSpendings(string whoo, float amount)
        {
            obtainData(whoo);

            customerSpendings += amount;

            writeFile();
        }
    }
}
