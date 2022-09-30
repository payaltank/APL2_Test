using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Data.SqlClient;

namespace APL_Test2.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public IFormFile File { get; set; }
            
    }

}
