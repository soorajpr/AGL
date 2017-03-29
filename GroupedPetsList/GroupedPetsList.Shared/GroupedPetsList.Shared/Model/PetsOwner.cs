using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GroupedPetsList.Shared
{
    [DataContract]
    public class PetsOwner
    {
        [DataMember(Name = "name")]
        public string OwnerName { get; set; }

        [DataMember(Name = "gender")]
        public string Gender { get; set; }

        [DataMember(Name = "age")]
        public int Age { get; set; }

        [DataMember(Name = "pets")]
        public List<Pet> Pets { get; set; }
    }

    [DataContract]
    public class Pet
    {
        [DataMember(Name = "name")]
        public string PetName { get; set; }

        [DataMember(Name = "type")]
        public string PetType { get; set; }
    }
}
