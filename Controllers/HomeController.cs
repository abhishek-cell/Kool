using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KoolApplicationMain.Models;
using MySql.Data.MySqlClient;


namespace KoolApplicationMain.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("ProductDetails");
        }
        public IActionResult ProductDetail()
        {
            Search search = new Search();
            var model = new List<Product>();
            using (MySqlConnection conn = search.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select XXIBM_PRODUCT_SKU.Item_number,XXIBM_PRODUCT_SKU.description,XXIBM_PRODUCT_PRICING.List_price,XXIBM_PRODUCT_PRICING.In_stock from XXIBM_PRODUCT_SKU JOIN XXIBM_PRODUCT_PRICING ON XXIBM_PRODUCT_SKU.Item_number=XXIBM_PRODUCT_PRICING.Item_number AND XXIBM_PRODUCT_SKU.description ", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.Add(new Product()
                        {
                            ItemNumber = Convert.ToInt32(reader["Item_number"]),
                            Description = reader["description"].ToString(),
                            Price = Convert.ToDouble(reader["List_price"]),
                            Stock = reader["In_stock"].ToString()

                        });
                    }
                }
            }
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
