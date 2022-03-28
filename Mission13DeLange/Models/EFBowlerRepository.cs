using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13DeLange.Models
{
    public class EFBowlerRepository : IBowlerRepository
    {
        private BowlerDbContext _context { get; set; }
        public EFBowlerRepository(BowlerDbContext temp)
        {
            _context = temp;
        }
        public IQueryable<Bowler> Bowlers => _context.Bowlers;

        public IQueryable<Team> Teams => _context.Teams;
    }
}
