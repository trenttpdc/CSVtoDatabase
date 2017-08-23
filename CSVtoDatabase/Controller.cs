using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Data;
using TableBuilderLibrary;

namespace CSVtoDatabase
{
    class Controller
    {
        public void Run()
        {
            //ConfigurationSetup();
            //FolderStructureSetup();

            //ImportCSV
            List<string> csvStringList = FileIO.ImportFileToStringList("D:\\CSVtoDatabase\\table.csv");

            //Get Headers
            string[] headers = Parser.ParseHeadersFromCsvStringList(csvStringList);

            //Create Table
            DataSet dataset = new DataSet();
           
            
            DataTable table = TableBuilder.BuildTableSchema("Table", headers);
            
            //Populate Table
            table = TableBuilder.PopulateTableFromCsv(table, csvStringList, true);

            //Show Preview
            //gridEmployees.DataContext = table.DefaultView;

            //Export to Database




            Console.WriteLine("Exiting...");
            System.Console.ReadKey();
        }
        
        private void FolderStructureSetup()
        {

            if (!Directory.Exists(Configuration.RootPath))
            {
                Configuration.setDefaultConfiguration();
                Console.WriteLine("Folder structure is missing. Reconstructing using configuration settings...");
                Configuration.setDefaultConfiguration();
                Directory.CreateDirectory(Configuration.RootPath);
                Directory.CreateDirectory(Configuration.DownloadPath);
                Directory.CreateDirectory(Configuration.ImportPath);
                Directory.CreateDirectory(Configuration.TablePath);
                if (Directory.Exists(Configuration.RootPath) && Directory.Exists(Configuration.DownloadPath) &&
                    Directory.Exists(Configuration.ImportPath) && Directory.Exists(Configuration.TablePath))
                {
                    Console.WriteLine("Folder creation successful. Root folder located at: " + Configuration.RootPath);
                }
                else
                {
                    Console.WriteLine("Folder creation failed. Attempted root folder creation at: " + Configuration.RootPath);
                }
            }
        }
        private void ConfigurationSetup()
        {
            Console.WriteLine("Attempting to import configuration file at " + Configuration.ConfigurationFilePath);
            
            if (!Directory.Exists(Configuration.ConfigurationFolderPath)) //Config file is missing
            {
                Directory.CreateDirectory(Configuration.ConfigurationFolderPath);
            }
            if (!File.Exists(Configuration.ConfigurationFilePath))
            {
                Console.WriteLine("Configuration file does not exist. Recreating with default settings.");
                Configuration.setDefaultConfiguration(); //sets default values

                FileIO.ExportStringListToFile(Configuration.ConfigurationFilePath, Configuration.getCurrentConfiguration());
                if (!File.Exists(Configuration.ConfigurationFilePath))
                {
                    Console.WriteLine("Something went wrong. Configuration file not saved.");
                }
                else
                {
                    Console.WriteLine("Configuration file saved successfully.");
                }
            }
            else // Config File exists, import
            {
                Console.WriteLine("Configuration file detected. Importing...");
                List<string> configLines = FileIO.ImportFileToStringList(Configuration.ConfigurationFilePath);
                List<Object> configValues = Parser.ParseConfigFile(configLines);
                Configuration.setNewConfiguration(configValues);
                Console.WriteLine("Configuration settings imported.");
            }
        }
        //private void SetupTable()
        //{
        //    //Check if file exists
        //    if (File.Exists(Configuration.TablePath + "\\" + Configuration.TableName + ".csv")) //If exists
        //    {
        //        Console.WriteLine("CSV file exists. Importing...");
        //        //Import file
        //        List<string> csv = FileIO.ImportCsvToStringList(Configuration.TablePath + "\\" + Configuration.TableName);

        //        //Build table with schema
        //        t.BuildTableSchema();
        //        //Populate table from file
        //        t.populateTableFromCsv(csv, true);
        //    }
        //    else //If not exists
        //    {
        //        Console.WriteLine("CSV file does not exists. Generating...");
        //        //Build table from scratch
        //        t.BuildTableSchema();
        //        FileIO.ExportDataTableToCSV(TableBuilder.Table, Configuration.TablePath, Configuration.TableName);
        //        Console.WriteLine("CSV file saved.");
        //    }
        //}
    }
}
