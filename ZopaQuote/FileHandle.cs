using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace ZopaQuote
{
    static class FileHandle
    {
        //if csv exists extracts data to list
        public static List<string> ExtractData(string filePath)
        {
            var dataLines = new List<string>();

            if (File.Exists(filePath))
            {
                var allLines = File.ReadAllLines(filePath);
                dataLines = allLines.Skip(1).ToList();
            }

            return dataLines;
        }

        //extracts data into Lenders
        public static List<Lenders> GetLendersData(List<string> fileData)
        {
            var allLenders = new List<Lenders>();

            foreach (string line in fileData)
            {
                var values = line.Split(',');

                var lender = new Lenders
                {
                    Name = values[0],
                    Rate = Convert.ToDouble(values[1]),
                    AvailableAmount = Int32.Parse(values[2])
                };

                allLenders.Add(lender);
            }

            return allLenders;
        }
    }
}
