using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using ManagedNativeWifi;
using Newtonsoft.Json;


using NativeWifi;



namespace AttendanceManagementPortal.Desktop
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(AddressChangedCallback);
            Console.ReadLine();

        }
        static string GetSsid()
        {
            var wlan = new WlanClient();
            var connectedSsids = new List<string>();

            foreach (var i in wlan.Interfaces)
            {
                var wlanSsid = i.CurrentConnection.wlanAssociationAttributes.dot11Ssid;

                var len = (int)wlanSsid.SSIDLength;

                var ssid = Encoding.ASCII.GetString(wlanSsid.SSID);

                connectedSsids.Add(ssid);
            }

            return connectedSsids?.FirstOrDefault() ?? string.Empty;
        }
        static void AddressChangedCallback(object sender, EventArgs e)
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

            bool disconnected = true;
            var currentSsid = string.Empty;
            var ipAddress = IPAddress.None;
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7095/api/AttendanceLog/");
            PostData postData = new PostData();
            var local = NetworkInterface.GetAllNetworkInterfaces().Where(i => i.Name == "Local Area Connection").FirstOrDefault();

            foreach (NetworkInterface n in adapters)
            {
                
                if (n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    currentSsid = GetSsid();
                    Console.WriteLine($"Wifi SSid: {currentSsid}");

                    disconnected = false;
                    // call api here for connected to wifi
                    //------------------------IP Address---------------------

                    var stringAddress = local.GetIPProperties().UnicastAddresses[0].Address.ToString();
                    ipAddress = IPAddress.Parse(stringAddress);

                    Console.WriteLine($"  IP Address ........................ :  : {ipAddress}");
                    ////-----------------------API ----------------------------------

                    
                    string trimmedStr = currentSsid.TrimEnd('\0');

                    
                    postData.WifiSsid = trimmedStr;
                    postData.IpAddress = ipAddress.ToString();

                    

                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("WifiSsid", postData.WifiSsid),
                        new KeyValuePair<string, string>("IpAddress", postData.IpAddress)
                    });


                    try
                    {
                        var response = httpClient.PostAsync("", formContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = response.Content.ReadAsStringAsync().Result;
                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };

                            var postResponse = System.Text.Json.JsonSerializer.Deserialize<PostResponse>(responseContent, options);
                            Console.WriteLine("Post successful! ID: " + postResponse.Id);

                        }
                        else
                        {
                            Console.WriteLine("Error: " + response.StatusCode);
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }

            if (disconnected)
            {
                Console.WriteLine($"Disconnected");
                // call api here for disconnection with no SSID which will mark as disconnected from the last SSID
                var stringAddress = local.GetIPProperties().UnicastAddresses[0].Address.ToString();
                ipAddress = IPAddress.Parse(stringAddress);
                postData.WifiSsid = null;
                postData.IpAddress = ipAddress.ToString();
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("WifiSsid", postData.WifiSsid),
                    new KeyValuePair<string, string>("IpAddress", postData.IpAddress)
                });
                try
                {
                    var response = httpClient.PostAsync("", formContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsStringAsync().Result;
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        var postResponse = System.Text.Json.JsonSerializer.Deserialize<PostResponse>(responseContent, options);
                        Console.WriteLine("Post successful! ID: " + postResponse.Id);

                    }
                    else
                    {
                        Console.WriteLine("Error: " + response.StatusCode);
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                Console.WriteLine($"Connnected To: {currentSsid}");
            }
        }


    }
}
