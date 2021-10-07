using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        // Initialize fields
        static string path = @".\Accounts.txt";
        List<Account> accounts = ReadFileForAccounts();

        public static List<Account> ReadFileForAccounts()
        {
            // Initialize fields
            List<Account> accounts = new List<Account>();

            // Add file lines to accounts list
            if (File.Exists(path))
            {
                // Read all file lines
                string[] rows = File.ReadAllLines(path);

                // Add file lines to accounts list
                for (int i = 1; i < rows.Length; i++)
                {
                    // Add row parameter into columns
                    string[] columns = rows[i].Split(',');

                    // Instantiate classes
                    Account account = new Account();
                    AccountType type = new AccountType();

                    // Get account type from final column
                    switch (columns[3])
                    {
                        case "F":
                            type = AccountType.Free;
                            break;
                        case "B":
                            type = AccountType.Basic;
                            break;
                        case "P":
                            type = AccountType.Premium;
                            break;
                        default:
                            Console.WriteLine($"Error: Incorrect account type was processed from the read File.  Contact IT.");
                            break;
                    }

                    // Pass parameters to account
                    account.AccountNumber = columns[0];
                    account.Name = columns[1];
                    account.Balance = decimal.Parse(columns[2]);
                    account.Type = type;

                    // Add account into accounts list
                    accounts.Add(account);
                }

                // Return accounts list
                return accounts;
            }

            else
            {
                Console.WriteLine($"Error: File path does not exist.");
                return null;
            }
        }

        public Account LoadAccount(string AccountNumber)
        {
            // Check account number entered to account
            for (int i = 0; i < accounts.Count; i++)
            {
                // Check if account number entered equals existing account number
                if (AccountNumber == accounts[i].AccountNumber)
                {
                    return accounts[i];
                }
            }

            return null;
        }

        public void SaveAccount(Account account)
        {
            // Read in file header for new file write
            string header = File.ReadAllLines(path).First();

            // Create string of lines to write to new file
            string[] writeLines = new string[accounts.Capacity + 1];

            // Set first line of new file to header
            writeLines[0] = header;

            // Add all account information into writeLines
            for (int i = 0; i < accounts.Capacity - 1; i++)
            {
                // Set new information in current account
                if (account.AccountNumber == accounts[i].AccountNumber)
                {
                    accounts[i] = account;
                    break;
                }
            }

            // Add all account information into writeLines
            for (int i = 0; i < accounts.Count; i++)
            {
                // Initialize fields
                string type = null;

                // Get account type from final column
                switch (accounts[i].Type)
                {
                    case AccountType.Free:
                        type = "F";
                        break;
                    case AccountType.Basic:
                        type = "B";
                        break;
                    case AccountType.Premium:
                        type = "P";
                        break;
                    default:
                        Console.WriteLine($"Error: Incorrect account type was processed from the read File.  Contact IT.");
                        break;
                }

                // Write new files based on curent account information
                writeLines[i + 1] = $"{accounts[i].AccountNumber},{accounts[i].Name},{accounts[i].Balance},{type}";
            }

            // Shave null lines
            writeLines = writeLines.Take(writeLines.Count() - 1).ToArray();

            // Write string array to file
            File.WriteAllLines(path, writeLines);
        }
    }
}
