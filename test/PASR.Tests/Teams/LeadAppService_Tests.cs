using Microsoft.EntityFrameworkCore;
using PASR.Leads;
using PASR.Leads.Dto;
using PASR.Teams;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PASR.Tests.Leads
{
    public class TeamAppService_Tests : PASRTestBase
    {
        private readonly ITeamAppService _teamAppService;

        public TeamAppService_Tests()
        {
            _teamAppService = Resolve<ITeamAppService>();
        }

        [Fact]
        public async Task GetLeads_Test()
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
                    Name = "Matheus",
                    LastName = "dos Anjos",
                    Cgc = "48291929840",
                    Addresses = AddressList,
                    PhoneNumber = "11998908899"

                });

            await UsingDbContextAsync(async context =>
            {
                var matheusDosAnjos = await context.Leads.FirstOrDefaultAsync(l => l.Cgc == "48291929840");
                matheusDosAnjos.ShouldNotBeNull();
            });
        }
    }
}
