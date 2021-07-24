using Microsoft.EntityFrameworkCore;
using PASR.Teams;
using PASR.Teams.Dto;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PASR.Tests.Teams
{
    public class TeamAppService_Tests : PASRTestBase
    {
        private readonly ITeamAppService _teamAppService;

        public TeamAppService_Tests()
        {
            _teamAppService = Resolve<ITeamAppService>();
        }

        [Fact]
        public async Task GetTeams_Test()
        {
            // Act
            var output = await _teamAppService.GetAllAsync(new PagedTeamResultRequestDto { MaxResultCount = 20, SkipCount = 0 });

            // Assert
            output.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task CreateLead_Test()
        {
            

            // Act
            await _teamAppService.CreateAsync(
                new CreateTeamDto
                {
                    TeamName = "TestTeam",

                });

            //Esse encapsulamento ajuda a usar o Tenant correto na hora de ver como o DBcontext será instanciado, pega da sessão ou usa o null
            //Também serve para centralizar o código que obtem o DBContext através do IoCManager em um lugar só
            await UsingDbContextAsync(async context =>
            {
                var matheusDosAnjos = await context.Leads.FirstOrDefaultAsync(l => l.IdentityCode == "48291929840");
                matheusDosAnjos.ShouldNotBeNull();
            });
        }
    }
}
