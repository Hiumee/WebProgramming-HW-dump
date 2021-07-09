﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore;
using AspCoreMVCEF.Models;

namespace AspCoreMVCEF.Data
{
	public class DBWpContext : DbContext
	{
		public DBWpContext(DbContextOptions<DBWpContext> options) : base(options)
		{
		}

		public DbSet<Article> Student { get; set; }
	}
}

