using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using APL_Test2.Models;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.SqlClient;

namespace APL_Test2.Controllers
{
    public class Home : Controller
    {
        // AzureBlob Connection string.
        private string azureBlob = "DefaultEndpointsProtocol=https;AccountName=aplrecruitment;AccountKey=jL5IsCg1kPqLvFlOdEb7X9awlW1csdOoE8NGxlgfex6ktpFJBxI2wDMzLLWojVN9RtJGlq/szGll+AStYQ7UqA==;EndpointSuffix=core.windows.net";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Image img)
        {
            BlobContainerClient blobContainerClient = new BlobContainerClient(azureBlob, "DefaultContainer");

            var filePath = Path.GetFullPath(img.File.FileName);
            var fileExt = Path.GetExtension(img.File.FileName);
            var allowedExt = new[] { "jpg", "png" };
            try
            {
                if (img.File.Length > 0)
                {
                    if (allowedExt.Contains(fileExt))
                    {
                        // create file in local folder
                        using (var stream = new MemoryStream())
                        {
                            await blobContainerClient.UploadBlobAsync(img.File.FileName, stream);
                            SaveImage(filePath);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return new StatusCodeResult(500);
            }
            return View();
        }

        public void SaveImage(String filename)
        {
            string conString = "Data Source=DESKTOP-L75EJ5K\\SQLEXPRESS;Initial Catalog=Image;Integrated Security=True";
            string query = "INSERT INTO Image (FilePath) VALUES(" + "'" + filename + "'" + ");";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand sc = new SqlCommand(query, con);
            sc.ExecuteNonQuery();
            con.Close();
        }
    }
}
