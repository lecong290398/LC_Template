using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSQLlite.Model
{
    [Table("LC_User")]
    public class LC_User
	{
        [PrimaryKey]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Fullname")]
        public string Fullname { get; set; }

        [Column("Username")]
        public string Username { get; set; }

        [Column("Password")]
        public string Passoword { get; set; }

    }
}
