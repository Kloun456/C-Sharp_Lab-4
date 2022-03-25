using System;
using Supabase;
using Postgrest.Attributes;
using System.Collections.Generic;
using System.Text;

namespace Project.BD
{
    public class TableUsers : SupabaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("login")]
        public String Login { get; set; }
        
        [Column("password")]
        public String Password { get; set; }
    }
}