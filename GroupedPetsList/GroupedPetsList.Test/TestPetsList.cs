using NUnit.Framework;
using GroupedPetsList.Shared;
using System.Collections.Generic;

namespace GroupedPetsList.Test
{
    [TestFixture]
    public class TestPetsList
    {
        List<PetsOwner> groupedMockData;
        [SetUp]
        public void Setup()
        {
            var mockData = new List<PetsOwner>()
            {
                new PetsOwner()
                {
                    OwnerName="Bob",
                    Age=23,
                    Gender="Male",
                    Pets=new List<Pet>()
                    {
                        new Pet()
                        {
                            PetName="Garfield",
                            PetType="Cat"
                        },
                        new Pet()
                        {
                            PetName="Fido",
                            PetType="Dog"
                        }
                    }
                },
                new PetsOwner()
                {
                    OwnerName="Jennifer",
                    Gender="Female",
                    Age=18,
                    Pets=new List<Pet>()
                    {
                        new Pet()
                        {
                            PetName="Garfield",
                            PetType="Cat"
                        }
                    }
                },
                new PetsOwner()
                {
                    OwnerName="Steve",
                    Gender="Male",
                    Age=45,
                    Pets=null
                },
                new PetsOwner()
                {
                    OwnerName="Fred",
                    Gender="Male",
                    Age=40,
                    Pets=new List<Pet>()
                    {
                        new Pet()
                        {
                            PetName="Tom",
                            PetType="Cat"
                        },
                        new Pet()
                        {
                            PetName="Max",
                            PetType="Cat"
                        },
                        new Pet()
                        {
                            PetName="Sam",
                            PetType="Dog"
                        },
                        new Pet()
                        {
                            PetName="Jim",
                            PetType="Cat"
                        }
                    }
                },
                new PetsOwner()
                {
                    OwnerName="Samantha",
                    Gender="Female",
                    Age=40,
                    Pets=new List<Pet>()
                    {
                        new Pet()
                        {
                            PetName="Tabby",
                            PetType="Cat"
                        }
                    }
                },
                new PetsOwner()
                {
                    OwnerName="Alice",
                    Gender="Female",
                    Age=64,
                    Pets=new List<Pet>()
                    {
                        new Pet()
                        {
                            PetName="Simba",
                            PetType="Cat"
                        },
                        new Pet()
                        {
                            PetName="Nemo",
                            PetType="Fish"
                        }
                    }
                }
            };

            groupedMockData = PetsServiceHandler.Instance.GetGroupedPetsList(mockData);
        }


        [TearDown]
        public void Tear() { }

        [Test]
        public async void Test_ServiceDataIsNotNull()
        {
            var data = await PetsServiceHandler.Instance.GetPetsOwnerListAsync();
            Assert.NotNull(data);
        }

        [Test]
        public async void Test_ServiceDataGroupCount()
        {
            var data = await PetsServiceHandler.Instance.GetPetsOwnerListAsync();
            Assert.True(data.Count==2);
        }

        [Test]
        public async void Test_ServiceDataGroupGroupName()
        {
            var data = await PetsServiceHandler.Instance.GetPetsOwnerListAsync();
            Assert.True(data[0].Gender.Equals("Male") && data[1].Gender.Equals("Female"));
        }

        [Test]
        public async void Test_ServiceDataPetsType()
        {
            var data = await PetsServiceHandler.Instance.GetPetsOwnerListAsync();
            Assert.False(data[0].Pets.Exists(x=>x.PetType.ToLower()!= "cat") || data[1].Pets.Exists(x => x.PetType.ToLower() != "cat"));
        }

        [Test]
        public void Test_GroupedMockDataIsNotNull()
        {
            Assert.NotNull(groupedMockData);
        }

        [Test]
        public void Test_GroupedMockDataGroupCount()
        {
            Assert.True(groupedMockData.Count == 2);
        }

        [Test]
        public void Test_GroupedMockDataGroupGroupName()
        {
            Assert.True(groupedMockData[0].Gender.Equals("Male") && groupedMockData[1].Gender.Equals("Female"));
        }

        [Test]
        public void Test_GroupedMockDataPetsType()
        {
            Assert.False(groupedMockData[0].Pets.Exists(x => x.PetType.ToLower() != "cat") || groupedMockData[1].Pets.Exists(x => x.PetType.ToLower() != "cat"));
        }
    }
}