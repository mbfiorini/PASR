using Microsoft.EntityFrameworkCore;
using PASR.Leads;
using PASR.Leads.Dto;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PASR.Tests.Leads
{
    public class LeadAppService_Tests : PASRTestBase
    {
        private readonly ILeadAppService _leadAppService;

        public LeadAppService_Tests()
        {
            _leadAppService = Resolve<ILeadAppService>();
        }

        [Fact]
        public async Task GetLeads_Test()
        {
            // Act
            var output = await _leadAppService.GetAllAsync(new PagedLeadResultRequestDto { MaxResultCount = 20, SkipCount = 0, Keyword = "try" });

            // Assert
            output.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task CreateLead_Test()
        {
            var AddressList = new List<AddressDto>
            {
                new AddressDto
                {
                    FederalUnity = "SP",
                    City = "Jundiaí",
                    District = "Vila Rami",
                    Number = "50",
                    Street = "Rua Sem Saída"
                }
            };

            // Act
            await _leadAppService.CreateAsync(
                new CreateLeadDto
                {
                    Name = "Matheus",
                    LastName = "dos Anjos",
                    IdentityCode = "48291929840",
                    EmailAddress = "matheus.bf.dosanjos@gmail.com",
                    Addresses = AddressList,
                    PhoneNumber = "11998908899",
                    Priority = Lead.LeadPriority.Min
                });

            await UsingDbContextAsync(async context =>
            {
                var matheusDosAnjos = await context.Leads.FirstOrDefaultAsync(l => l.IdentityCode == "48291929840");
                matheusDosAnjos.ShouldNotBeNull();
            });
        }
    }
}
