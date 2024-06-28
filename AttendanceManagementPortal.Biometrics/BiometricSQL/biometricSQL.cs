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

                    // Use parameters to pass the date and time values
                    cmd.CommandText = "INSERT INTO [AttendanceManagementPortalDb].[dbo].[AttendanceLogs] ([Date], [Time], [Type], [EmployeeID], [Source]) VALUES (@Date, @Time, @Type, @EmployeeID, '')";
                    cmd.Parameters.AddWithValue("@Date", record.Date);
                    cmd.Parameters.AddWithValue("@Time", record.Time);
                    cmd.Parameters.AddWithValue("@Type", record.Type.ToString());
                    cmd.Parameters.AddWithValue("@EmployeeID", record.EmployeeID);

                    try
                    {
                        cmd.Connection = con;
                        con.Open();
                        int TotalRowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine("Total rows Inserted = " + TotalRowsAffected.ToString());
                        if (TotalRowsAffected > 0)
                        {
                            SqlCommand cmdforempattend = new SqlCommand();
                            cmdforempattend.CommandText = "SELECT COUNT(*) FROM [AttendanceManagementPortalDb].[dbo].[EmployeesAttendances] WHERE [Date] = @Date AND [EmployeeID] = @EmployeeID";
                            cmdforempattend.Parameters.AddWithValue("@Date", record.Date.ToString("yyyy-MM-dd"));
                            cmdforempattend.Parameters.AddWithValue("@EmployeeID", record.EmployeeID);
                            cmdforempattend.Connection = con;

                            int AttendFound = (int)cmdforempattend.ExecuteScalar();
                            if (AttendFound <= 0)
                            {
                                SqlCommand attendcreate = new SqlCommand();
                                attendcreate.CommandText = "INSERT INTO [AttendanceManagementPortalDb].[dbo].[EmployeesAttendances] ([CheckIn], [CheckOut], [Date], [EmployeeID]) VALUES (@Time, @Time, @Date, @EmployeeID)";
                                attendcreate.Parameters.AddWithValue("@Time", record.Time.ToString());
                                attendcreate.Parameters.AddWithValue("@Date", record.Date.ToString("yyyy-MM-dd"));
                                attendcreate.Parameters.AddWithValue("@EmployeeID", record.EmployeeID);
                                attendcreate.Connection = con;
                                int AttendCreate = attendcreate.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
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

/*
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

                    cmd.CommandText = $"INSERT INTO [AttendanceManagementPortalDb].[dbo].[AttendanceLogs] ([Date], [Time], [Type], [EmployeeID], [Source]) VALUES ({record.Date}, {record.Time}, '{record.Type.ToString()}', '{record.EmployeeID}', '')";

                    try
                    {
                        cmd.Connection = con;
                        con.Open();
                        int TotalRowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine("Total rows Inserted = " + TotalRowsAffected.ToString());
                        if (TotalRowsAffected > 0)
                        {
                            SqlCommand cmdforempattend = new SqlCommand();
                            cmdforempattend.CommandText = $"Select * from [AttendanceManagementPortalDb].[dbo].[EmployeesAttendances] where [Date] = '{DateTime.Today.ToString("yyyy-MM-dd")}' and [EmployeeID] = {record.EmployeeID}";
                            cmdforempattend.Connection = con;

                            int AttendFound = cmdforempattend.ExecuteNonQuery();
                            if (AttendFound <= 0)
                            {
                                SqlCommand attendcreate = new SqlCommand();
                                attendcreate.CommandText = $"INSERT INTO [AttendanceManagementPortalDb].[dbo].[EmployeesAttendances] ([CheckIn], [CheckOut], [Date], [EmployeeID]) VALUES ('{record.Time.ToString()}', '{record.Time.ToString()}', '{record.Date}', {record.EmployeeID})";
                                attendcreate.Connection = con;
                                int AttendCreate = attendcreate.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
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
}*/
