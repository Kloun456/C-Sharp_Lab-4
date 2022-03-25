using System;
using Supabase;
using Postgrest.Attributes;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public class TableMessages : SupabaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("login")]
        public String Login { get; set; }

        [Column("message")]
        public String Messsage { get; set; }
    }
}
