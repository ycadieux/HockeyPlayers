using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyPlayers.Api.Data
{
    public class Player
    {
        public int Id { get; set; }
        public int JerseyNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Position Position { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }

    public enum Position
    {
        C,
        LW,
        RW,
        D,
        G
    }
}
