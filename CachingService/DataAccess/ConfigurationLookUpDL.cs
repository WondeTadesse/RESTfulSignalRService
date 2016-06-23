//|---------------------------------------------------------------|
//|                     CACHING SERVICE                           |
//|---------------------------------------------------------------|
//|                     Developed by Wonde Tadesse                |
//|                        Copyright ©2015 - Present              |
//|---------------------------------------------------------------|
//|                     CACHING SERVICE                           |
//|---------------------------------------------------------------|
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using CachingService.DTO;


namespace CachingService.DataAccess
{

    /// <summary>
    /// ConfigurationLookUp data access class
    /// </summary>
    public class ConfigurationLookUpDL
    {
        #region Private Variable 

        private static string SignalRBroadCasterDBConnectionstring = "SignalRBroadCasterDBConnectionstring";

        #endregion

        #region Public Methods 

        /// <summary>
        /// Get Configuration LookUps
        /// </summary>
        /// <returns>ConfigurationLookUp list</returns>
        public static List<ConfigurationLookup> GetConfigurationLookUps()
        {
            List<ConfigurationLookup> configurationLookups = new List<ConfigurationLookup>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings[SignalRBroadCasterDBConnectionstring].ConnectionString))
                {
                    SqlCommand command = new SqlCommand(
                      "SELECT ID, Name, Value FROM ConfigurationLookup;", connection);
                    
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int id = -1;
                                int.TryParse(reader["ID"].ToString(), out id);
                                string name = reader["Name"] != null ? reader["Name"].ToString() : string.Empty;
                                string value = reader["Value"] != null ? reader["Value"].ToString() : string.Empty;
                                if (id != -1)
                                {
                                    configurationLookups.Add(new ConfigurationLookup()
                                    {
                                        ID = id,
                                        Name = name,
                                        Value = value
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Log exception
            }
            return configurationLookups;
        }
        
        #endregion
    }
}