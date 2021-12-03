using Microsoft.EntityFrameworkCore;
using PASR.Authorization.Roles;
using PASR.Calls;
using PASR.Leads;
using PASR.Localization;
using PASR.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.EntityFrameworkCore.Seed.Testing
{
    internal class DefultWorkflowBuilder
    {
        private readonly PASRDbContext _context;

        public DefultWorkflowBuilder(PASRDbContext context)
        {
            _context = context;
        }

        public void Create() {
        
            CreateLead();
            CreateCall();
            CreateTeam();
        
        }

        void CreateLead()
        {
            //Leads
            var lead = _context.Leads.IgnoreQueryFilters().FirstOrDefault(l => l.IdentityCode == "48291929840");

            if (lead == null)
            {
                lead = new Lead(
                    "Matheus",
                    "Fiorini",
                    "(11)99890-8899",
                    "48291929840",
                    "Geniality",
                    "matheus.bf.dosanjos@gmail.com",
                    Lead.LeadPriority.Max);
                lead = _context.Leads.Add(lead).Entity;
                _context.SaveChanges();
            }

            if (lead.Address == null)
            {   
                lead.Address = new Address("Rua 12 de Outubro", "40", "Vila Rami", "Jundiaí", "SP");
            }

            _context.SaveChanges();
        }

        void CreateCall()
        {
            //Leads
            var lead = _context.Leads.IgnoreQueryFilters().FirstOrDefault(l => l.Id == 1);

            var adminRoleId = _context.Roles.FirstOrDefault(r => r.Name == "admin")?.Id;

            var user = _context.Users
                            .IgnoreQueryFilters()
                            .FirstOrDefault(u => u.Roles
                                .Any(r => 
                                    r.Id == adminRoleId));

            if (lead != null && user != null)
            {
                var call = new Call(user, 
                                    lead, 
                                    DateTime.Today, 
                                    DateTime.Now,
                                    CallResult.ScheduledMeeting,
                                    ResultReason.Necessity);
                
                call.CallNotes = "Reunião Agendada";

                _context.Calls.Add(call);
            }

            _context.SaveChanges();
        }
        

        void CreateTeam()
        {
            // var sdmRoleId = _roleManager.GetRoleByName("SDM")?.Id;
            //Leads
            var team = _context.Teams.IgnoreQueryFilters().FirstOrDefault(t => t.Name == "Team1");

            var sdmRoleId = _context.Roles.FirstOrDefault(r => r.Name == "SDM")?.Id;

            var user = _context.Users
                            .IgnoreQueryFilters()
                            .FirstOrDefault(u => u.Roles.Any(r => 
                                    r.Id == sdmRoleId));

            if (team == null && user != null)
            {
                var team1 = new Team("Team1","Test Team, only for fun", user);
                
                _context.Teams.Add(team1);
            }

            _context.SaveChanges();
        }
    }
}
