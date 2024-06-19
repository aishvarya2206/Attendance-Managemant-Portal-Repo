using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using AttendanceManagementPortal.Biometrics.BiometricSQL;


namespace AttendanceManagementPortal.Biometrics
{
    public class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile($"{AppDomain.CurrentDomain.BaseDirectory}\\appsettings.json", optional: false, reloadOnChange: true)
             .Build();

            // Read the file path from the configuration
            string csvFilePath = configuration.GetSection("csvFilePath").Value;
            // auto load every 10 min
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(10);

            var timer = new System.Threading.Timer((e) =>
            {
                // csv read and upload into database
                // Check if the file exists and has a .csv extension
                if (File.Exists(csvFilePath) && Path.GetExtension(csvFilePath).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    // Load the CSV file
                    var records = ReadCsvFile(csvFilePath);

                    // Process the biometric data records
                    foreach (var record in records)
                    {
                        Console.WriteLine($"Date: {record.Date}, Time: {record.Time}, Type: {record.Type}, Employee ID: {record.EmployeeID} ,Source: {record.Source}");
                        var biometricSQL = new biometricSQL(configuration);
                        biometricSQL.InsertBiometricRecord(record);
                    }
                }
                else
                {
                    Console.WriteLine("The specified file does not exist or has an incorrect extension.");
                }
                // csv upload ends


            }, null, startTimeSpan, periodTimeSpan);

           
            // auto load end

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        static List<BiometricRecord> ReadCsvFile(string csvFilePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };
             
            var records = new List<BiometricRecord>();

            try
            {
                using (var reader = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(reader, config))
                {
                    records = csv.GetRecords<BiometricRecord>().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV file: {ex.Message}");
                // Handle the exception as needed, e.g., log the error, display a user-friendly message, etc.
            }

            return records;
        }
    }
}