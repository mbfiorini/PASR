using Microsoft.EntityFrameworkCore;
using PASR.Leads;
using PASR.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.EntityFrameworkCore.Seed.Testing
{
    internal class DefaultLeadBuilder
    {
        private readonly PASRDbContext _context;
        private readonly int _tenantId;

        public DefaultLeadBuilder(PASRDbContext context)
        {
            _context = context;
        }

        public void Create() => CreateLead();

        void CreateLead()
        {
            //Leads
            var lead = _context.Leads.IgnoreQueryFilters().FirstOrDefault(l => l.Id == 1);

            if (lead == null)
            {
                lead = new Lead("Mathues", "Fiorini", "(11)99890-8899", "48291929840");
                _context.Leads.Add(lead);
            }

            var hasAddress = lead.Addresses.Any();

            if (!hasAddress)
            {
                lead.Addresses.Add(new Address("Rua 12 de Outubro", "40", "Vila Rami", "Jundiaí", "SP"));
            }

            _context.SaveChanges();
        }
    }
}
