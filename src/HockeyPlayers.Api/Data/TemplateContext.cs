using HockeyPlayers.Api.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyPlayers.Api.Data
{
    public class TemplateContext : DbContext
    {
        public TemplateContext(DbContextOptions<TemplateContext> options) : base(options) { }
        public TemplateContext() { }

        public DbSet<Player> Players { get; set; }
    }
}
