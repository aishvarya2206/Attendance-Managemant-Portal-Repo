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


namespace AttendanceManagementPortal.Biometrics
{
    public class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("C:\\Users\\khuwa\\source\\repos\\Attendance-Managemant-Portal-Repo\\AttendanceManagementPortal.Biometrics\\appsettings.json", optional: false, reloadOnChange: true)
             .Build();

            // Read the file path from the configuration
            string csvFilePath = configuration.GetSection("csvFilePath").Value;

            // Check if the file exists and has a .csv extension
            if (File.Exists(csvFilePath) && Path.GetExtension(csvFilePath).Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                // Load the CSV file
                var records = ReadCsvFile(csvFilePath);

                // Process the biometric data records
                foreach (var record in records)
                {
                    Console.WriteLine($"Employee ID: {record.Employee_ID}, Employee Name: {record.Employee_FullName}, DateTime IN: {record.DateTime_IN}, DateTime OUT: {record.DateTime_OUT}");
                    // datbase send using form data 
                    var _httpClient = new HttpClient();
                    _httpClient.BaseAddress = new Uri("https://localhost:7095/api/AttendanceLog");
                    var formContent = new FormUrlEncodedContent(
                        new[]
                        {
                            new KeyValuePair<string, string>("Date" , DateTime.Today.ToString()),
                            new KeyValuePair<string, string>("Time" , DateTime.Now.ToLongDateString()),
                            new KeyValuePair<string, string>("Type" , "In"),
                            new KeyValuePair<string, string>("EmployeeId" , record.Employee_ID.ToString()),
                            new KeyValuePair<string, string>("Source","")

                        }
                        );
                        try
                        {
                            var response = _httpClient.PostAsync("",formContent).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                Console.WriteLine("saved successfully");
                            }
                            else
                            {
                                Console.WriteLine("Error : " + response.StatusCode);
                            }
                        }
                        catch( Exception ex ) 
                        {
                            throw new Exception(ex.Message);
                        }

                    // db send stop

                }
            }
            else
            {
                Console.WriteLine("The specified file does not exist or has an incorrect extension.");
            }

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