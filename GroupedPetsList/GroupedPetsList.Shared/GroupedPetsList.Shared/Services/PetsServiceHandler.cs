﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
namespace GroupedPetsList.Shared
{
    public class PetsServiceHandler : ControllerBase<PetsServiceHandler>
    {
        public async Task<List<PetsOwner>> GetPetsOwnerListAsync()
        {
            List<PetsOwner> data = null;

            var uri = new Uri(string.Format(Constants.PetsUrl, string.Empty));

            try
            {

                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {

                    var content = await response.Content.ReadAsStringAsync();
                    data = GetGroupedPetsList(JsonConvert.DeserializeObject<List<PetsOwner>>(content));

                }
            }
            catch (Exception ex)
            {
                var exception = new CustomException(ex);
                exception.RequestUrl = Constants.PetsUrl;
                exception.MethodName = "GetPetListAsync";
                ExceptionHandler.Instance.LogError(exception);
            }

            return data;
        }

        public List<PetsOwner> GetGroupedPetsList(List<PetsOwner> petsOwnerList)
        {
            if (null != petsOwnerList)
            {
                try
                {
                    var sortedPetsList = petsOwnerList.Where(t => t.Pets != null).GroupBy(t => t.Gender)
                              .Select(t => new PetsOwner
                              {
                                  Gender = t.Key,
                                  Pets = t.SelectMany(k => k.Pets).Where(m => m.PetType.ToLower() == "cat").
                                          OrderBy(k => k.PetName).ToList()
                              }).ToList();

                    return sortedPetsList;
                }
                catch(Exception ex)
                {
                    var exception = new CustomException(ex);
                    exception.MethodName = "GetGroupedPetsList";
                    ExceptionHandler.Instance.LogError(exception);
                }
            }
            return null;
        }
    }
}
