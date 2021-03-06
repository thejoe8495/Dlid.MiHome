﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Dlid.MiHome.Tests
{
    /// <summary>
    /// Tests toward an actual device
    /// Setup your environment variables to run the tests
    /// </summary>
    [TestClass]
    public class VacuumDeviceTests
    {
        string _ip;
        string _token;

        [TestInitialize]
        public void Init()
        {
            _ip = Environment.GetEnvironmentVariable("VACUUM_IP");
            _token = Environment.GetEnvironmentVariable("VACUUM_TOKEN");
        }

        [TestMethod]
        public void StartCleaning()
        {
            if (!string.IsNullOrEmpty(_ip))
            {
                using (var device = new MiDevice(_ip, _token))
                {
                    var response = device.Send("app_start");
                }
            }
        }


        [TestMethod]
        public void GetConsumables()
        {
            if (!string.IsNullOrEmpty(_ip))
            {
                using (var device = new MiVacuumDevice(_ip, _token))
                {
                    var response = device.GetConsumables();
                }
            }
        }

        [TestMethod]
        public void Test_CleanZone()
        {
            if (!string.IsNullOrEmpty(_ip))
            {
                using (var device = new MiDevice(_ip, _token))
                {
                    var response = device.Send("app_zoned_clean", new int[] { 20047, 24303, 24097, 25853, 1 });
                    var resultStrings = response.As<List<string>>();
                    Assert.IsNotNull(resultStrings);
                    Assert.AreEqual(1, resultStrings.Count);
                    Assert.AreEqual("ok", resultStrings.First());
                }
            }
        }

        [TestMethod]
        public void Test_GetStatus()
        {
            if (!string.IsNullOrEmpty(_ip))
            {
                using (var device = new MiDevice(_ip, _token))
                {
                    var response = device.Send("get_status");

                    // in cleaning 3 ==    spot cleaning?
                    // in cleaning 2 == ✔ zone 
                    // in cleaning 1 == ✔ general 

                    /*
                     * 
                     * 
                     * {"result":[{"msg_ver":2,"msg_seq":67,"state":8,"battery":100,"clean_time":16,"clean_area":157500,"error_code":0,"map_present":1,"in_cleaning":0,"in_returning":0,"in_fresh_state":1,"lab_status":1,"fan_power":60,"dnd_enabled":1}],"id":-976019150}*/

                }
            }
        }
    }
}
