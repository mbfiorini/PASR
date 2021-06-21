//using Microsoft.EntityFrameworkCore;
//using Shouldly;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace PASR.Tests.Calls
//{
//    public class CallAppService_Tests : PASRTestBase
//    {
//        private readonly ICallAppService _callAppService;

//        public CallAppService_Tests()
//        {
//            _callAppService = Resolve<ILeadAppService>();
//        }

//        [Fact]
//        public async Task GetLeads_Test()
//        {
//            // Act
//            var output = await _callAppService.GetAllAsync(new PagedCallResultRequestDto { MaxResultCount = 20, SkipCount = 0 });

//            // Assert
//            output.Items.Count.ShouldBeGreaterThan(0);
//        }

//        [Fact]
//        public async Task CreateLead_Test()
//        {
//            var AddressList = new List<AddressDto>();

//            AddressList.Add(new AddressDto
//            {
//                FederalUnity = "SP",
//                City = "Jundiaí",
//                District = "Vila Rami",
//                Number = "50",
//                Street = "Rua Sem Saída"
//            });

//            // Act
//            await _callAppService.CreateAsync(
//                new CreateLeadDto
//                {
//                    Name = "Matheus",
//                    LastName = "dos Anjos",
//                    IdentityCode = "48291929840",
//                    Addresses = AddressList,
//                    PhoneNumber = "11998908899"

//                });

//            await UsingDbContextAsync(async context =>
//            {
//                var matheusDosAnjos = await context.Leads.FirstOrDefaultAsync(l => l.IdentityCode == "48291929840");
//                matheusDosAnjos.ShouldNotBeNull();
//            });
//        }
//    }
//}
