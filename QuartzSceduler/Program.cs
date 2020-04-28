using Newtonsoft.Json;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml;

namespace QuartzSceduler
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of SQLServer
            var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IVR-db;Integrated Security=True");
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(connection, compiler);
            // You can register the QueryFactory in the IoC container

            dynamic user = db.Query("ASGCalls").Select("AssessorId", "AccountSid").FirstOrDefault();


            var json = @"{
                       ""employees"": [
                       { ""firstName"":""John"" , ""lastName"":""Doe"" },
                       { ""firstName"":""Anna"" , ""lastName"":""Smith"" },
                       { ""firstName"":""Peter123"" , ""lastName"":""Jones"" },
                       { ""firstName"":""Peter"" , ""lastName"":""Jones"" }
                       ]
                       }";
            jsonStringToCSV(json);

            Console.WriteLine("Hello World!");
        }

        public static void jsonStringToCSV(string jsonContent)
        {
            //used NewtonSoft json nuget package
            XmlNode xml = JsonConvert.DeserializeXmlNode("{records:{record:" + jsonContent + "}}");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml.InnerXml);
            XmlReader xmlReader = new XmlNodeReader(xml);
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlReader);
            var dataTable = dataSet.Tables[1];

            //Datatable to CSV
            var lines = new List<string>();
            string[] columnNames = dataTable.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();
            var header = string.Join(",", columnNames);
            lines.Add(header);
            var valueLines = dataTable.AsEnumerable()
                               .Select(row => string.Join(",", row.ItemArray));
            lines.AddRange(valueLines);
            File.WriteAllLines(@"D:/Export.csv", lines);
        }
    }
}
