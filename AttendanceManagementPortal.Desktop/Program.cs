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



namespace AttendanceManagementPortal.Desktop
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            NetworkChange.NetworkAvailabilityChanged += OnNetworkAvailabilityChanged;
           
            bool available = false;
            void OnNetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs networkAvailability)
            {
                Console.WriteLine($"Network is available: {networkAvailability.IsAvailable}");
                available = networkAvailability.IsAvailable;
            }
            
            NetworkChange.NetworkAddressChanged += OnNetworkAddressChanged;
            async void OnNetworkAddressChanged(object sender, EventArgs eventargs)
            {
                if (available == false)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    var local = NetworkInterface.GetAllNetworkInterfaces().Where(i => i.Name == "Local Area Connection").FirstOrDefault();
                    var stringAddress = local.GetIPProperties().UnicastAddresses[0].Address.AddressFamily.ToString();
                    var ipAddress = IPAddress.Parse(stringAddress);

                    Console.WriteLine($"  IP Address ........................ :  : {ipAddress}");
                    /*var ipProperties = local.GetIPProperties();
                    var ipv6Addresses = ipProperties.UnicastAddresses
                                                   .Where(a => a.Address.AddressFamily == AddressFamily.InterNetworkV6)
                                                   .ToList();

                    
                    Console.WriteLine($"  IP Address IPv6 ........................ :  : {ipv6Addresses[0].Address}");
                   */
                }
                else
                {
                    // Native Wifi start
                    var availableNetwork = NativeWifi.EnumerateAvailableNetworks();
                    var firstNetwork = availableNetwork.FirstOrDefault();
                    if (firstNetwork != null)
                    {
                        Console.WriteLine($"Wifi SSID is : {firstNetwork.Ssid}");
                        Console.WriteLine($"Physical address : ");

                        //--------------- Physical address----------------
                        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                        foreach (NetworkInterface adapter in nics)
                        {
                            Console.Write("  Physical address ........................ : ");
                            PhysicalAddress address = adapter.GetPhysicalAddress();
                            byte[] bytes = address.GetAddressBytes();
                            for (int i = 0; i < bytes.Length; i++)
                            {
                                // Display the physical address in hexadecimal.
                                Console.Write("{0}", bytes[i].ToString("X2"));
                                // Insert a hyphen after each byte, unless we're at the end of the address.
                                if (i != bytes.Length - 1)
                                {
                                    Console.Write("-");
                                }
                            }
                            Console.WriteLine();
                        }
                        //------------------------IP Address---------------------
                        Console.WriteLine();
                        Console.WriteLine();
                        var local = NetworkInterface.GetAllNetworkInterfaces().Where(i => i.Name == "Local Area Connection").FirstOrDefault();
                        var stringAddress = local.GetIPProperties().UnicastAddresses[0].Address.ToString();
                        var ipAddress = IPAddress.Parse(stringAddress);

                        Console.WriteLine($"  IP Address ........................ :  : {ipAddress}");


                        ////-----------------------API ----------------------------------

                        var httpClient = new HttpClient();

                        PostData postData = new PostData();
                        postData.WifiSsid = firstNetwork.Ssid.ToString();
                        postData.IpAddress = ipAddress.ToString();

                        httpClient.BaseAddress = new Uri("https://localhost:7095/api/Employees/");

                        /*var json = System.Text.Json.JsonSerializer.Serialize(postData);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");*/

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
                        Console.WriteLine("No available Wi-Fi networks detected.");
                    }
                    // Native Wifi end
                }
            }

            Console.WriteLine("Listening changes in network availability. Press any key to continue.");
            Console.ReadLine();

            NetworkChange.NetworkAvailabilityChanged -= OnNetworkAvailabilityChanged;
            
        }
    }
}
