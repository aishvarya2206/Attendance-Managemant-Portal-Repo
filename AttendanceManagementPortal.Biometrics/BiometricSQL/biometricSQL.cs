using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Remoting.Messaging;

namespace AttendanceManagementPortal.Biometrics.BiometricSQL
{
    internal class biometricSQL
    {
        private string _connectionString;

        public biometricSQL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public void InsertBiometricRecord(BiometricRecord record)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    // mantaining attendance log everytime
                    cmd.CommandText = $"INSERT INTO [AttendanceManagementPortalDb].[dbo].[AttendanceLogs] ([Date], [Time], [Type], [EmployeeID], [Source]) VALUES (cast('{record.Date.ToString("yyyy-MM-dd HH:mm:ss")}'as datetime), cast('{record.Time.ToString("yyyy-MM-dd HH:mm:ss")}'as datetime), '{record.Type.ToString()}', '{record.EmployeeID}', '')";
                    
                    try
                    {
                        cmd.Connection = con;
                        con.Open();
                        int TotalRowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine("Total rows Inserted = " + TotalRowsAffected.ToString());
                        if(TotalRowsAffected > 0 )
                        {
                            SqlCommand cmdforempattend = new SqlCommand();
                            cmdforempattend.CommandText = $"Select * from [AttendanceManagementPortalDb].[dbo].[EmployeesAttendances] where [Date] = cast('{DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss")}'as datetime) and [EmployeeID] = {record.EmployeeID}";
                            cmdforempattend.Connection = con;
                            // get number of rows found 
                            int AttendFound;
                            using (SqlDataReader reader = cmdforempattend.ExecuteReader())
                            {
                                int rowCount = 0;
                                while (reader.Read())
                                {
                                    rowCount++;
                                }

                                // rowCount will contain the number of rows found
                                AttendFound = rowCount;
                            }

                            //int AttendFound = cmdforempattend.ExecuteNonQuery();
                            // if employee attendance does not exist 
                            int res = DateTime.Compare(record.Date, DateTime.Today);

                            if (AttendFound <= 0 && res == 0)
                            {
                                SqlCommand attendcreate = new SqlCommand();
                                
                                attendcreate.CommandText = $"INSERT INTO [AttendanceManagementPortalDb].[dbo].[EmployeesAttendances] ([CheckIn], [CheckOut], [Date], [EmployeeID]) VALUES ('{record.Time.ToString("HH:mm:ss")}', '', cast('{record.Date.ToString("yyyy-MM-dd HH:mm:ss")}'as datetime), {record.EmployeeID})";
                               
                                attendcreate.Connection = con;
                                int AttendCreate = attendcreate.ExecuteNonQuery();
                            }
                            // if employee attendance already exist
                            else
                            {
                                // if TYPE is OUT then update checkout time
                                Console.WriteLine(record.Type.ToUpper());
                                if (record.Type.ToUpper() == "OUT")
                                {
                                    SqlCommand attendcreate = new SqlCommand();

                                    attendcreate.CommandText = $"UPDATE [AttendanceManagementPortalDb].[dbo].[EmployeesAttendances] SET [CheckOut] = '{record.Time.ToString("HH:mm:ss")}' WHERE [EmployeeID] = {record.EmployeeID} and [Date] = cast('2024-06-27 00:00:00'as datetime)";

                                    attendcreate.Connection = con;
                                    int AttendCreate = attendcreate.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                    

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
