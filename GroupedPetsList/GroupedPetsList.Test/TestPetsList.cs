using NUnit.Framework;
using GroupedPetsList.Shared;
namespace GroupedPetsList.Test
{
    [TestFixture]
    public class TestPetsList
    {

        [SetUp]
        public void Setup() { }


        [TearDown]
        public void Tear() { }

        [Test]
        public async void Test_PetsListIsNotNull()
        {
            var data = await PetsServiceHandler.Instance.GetPetListAsync();
            Assert.IsNotNull(data);
        }

        [Test]
        public async void Test_PetsListCount()
        {
            var data = await PetsServiceHandler.Instance.GetPetListAsync();
            Assert.True(data.Count>0);
        }

        [Test]
        public async void Test_PetsListGroupName()
        {
            var data = await PetsServiceHandler.Instance.GetPetListAsync();
            Assert.True(data[0].Gender.Equals("Male") && data[1].Gender.Equals("Female"));
        }

        [Test]
        public async void Test_PetsType()
        {
            var data = await PetsServiceHandler.Instance.GetPetListAsync();
            Assert.False(data[0].Pets.Exists(x=>x.PetType.ToLower()!= "cat") || data[1].Pets.Exists(x => x.PetType.ToLower() != "cat"));
        }
    }
}