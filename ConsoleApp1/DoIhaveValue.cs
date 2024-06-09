using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSVtoLINQ
{
    class Program
    {
        class SaleItem
        {
            public DateTime Date { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public int QuantitySold { get; set; }
            public double PricePerUnit { get; set; }
        }

        static void Main(string[] args)
        {
            string filePath = $"C:\\Users\\yitzy\\RiderProjects\\ConsoleApp1\\ConsoleApp1\\sales_data.csv";
            var salesData = ReadCsv(filePath);

            var productSales = CalculateProductSales(salesData);
            
            var topProducts = GetTopProducts(productSales, 5);
            var categorySales = CalculateCategorySales(salesData);

            SaveSummaryReport(productSales, "summary_report.csv");

            // Print the results (optional, for verification)
            PrintTopProducts(topProducts);
            PrintCategorySales(categorySales);
        }


        static List<SaleItem> ReadCsv(string filePath)
        {
            var sales = new List<SaleItem>();
            var reader = new StreamReader(filePath);
            string line;
            reader.ReadLine();
            while ((line = reader.ReadLine()) != null)
            {
                var splitLine = line.Split(',');
                var sale = new SaleItem()
                {
                    Date = DateTime.Parse(splitLine[0]),
                    ID = splitLine[1],
                    Name = splitLine[2],
                    Category = splitLine[3],
                    QuantitySold = int.Parse(splitLine[4]),
                    PricePerUnit = double.Parse(splitLine[5])
                };
                sales.Add(sale);
                Console.WriteLine(sale.PricePerUnit);
            }

            return sales;
        }

        static Dictionary<string, double> CalculateProductSales(List<SaleItem> salesData)
        {
            var productSales = new Dictionary<string, double>();
            foreach (var saleItem in salesData)
            {
                if (productSales.ContainsKey(saleItem.Name))
                {
                    productSales[saleItem.Name] += saleItem.PricePerUnit * saleItem.QuantitySold;
                }
                else
                {
                    productSales.Add(saleItem.Name, saleItem.PricePerUnit * saleItem.QuantitySold);
                }
            }

            foreach (var hi in productSales)
            {
                Console.WriteLine(hi);
            }
            return productSales;
        }

        static List<KeyValuePair<string, double>> GetTopProducts(Dictionary<string, double> productSales, int topN)
        {
            // Implement logic to get top N products by sales
            return productSales.OrderByDescending(kvp => kvp.Value).Take(topN).ToList();
        }

        
        static Dictionary<string, double> CalculateCategorySales(List<SaleItem> salesData)
        {
            var categorySales = new Dictionary<string, double>();
            foreach (var saleItem in salesData)
            {
                if (categorySales.ContainsKey(saleItem.Category))
                {
                    categorySales[saleItem.Category] += saleItem.PricePerUnit * saleItem.QuantitySold;
                }
                else
                {
                    categorySales.Add(saleItem.Category, saleItem.PricePerUnit * saleItem.QuantitySold);
                }
            }

            foreach (var hi in categorySales)
            {
                Console.WriteLine(hi);
            }

            // Implement the logic to calculate total sales for each category

            return categorySales;
        }

        static void SaveSummaryReport(Dictionary<string, double> productSales, string filePath)
        {
            // Implement logic to save the summary report as a CSV file
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Product,TotalSales");
                foreach (var product in productSales)
                {
                    writer.WriteLine($"{product.Key},{product.Value}");
                }
            }
        }

        static void PrintTopProducts(List<KeyValuePair<string, double>> topProducts)
        {
            foreach(var product in topProducts)
            {
               Console.WriteLine(product.Value +" "+ product.Key); 
            }
            // Implement logic to print top products
        }

        static void PrintCategorySales(Dictionary<string, double> categorySales)
        {
            foreach(var product in categorySales)
            {
                Console.WriteLine(product.Value +" "+ product.Key); 
            }
            // Implement logic to print category sales
        }
    }
}