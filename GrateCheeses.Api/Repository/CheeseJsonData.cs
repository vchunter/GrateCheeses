using GrateCheeses.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GrateCheeses.Api.Repository
{
    //TODO: This class is used to serve the cheese data from a json data file to start off with
    //      Future plans are to move the cheese data out of an embedded file and into a database
    //      maybe something like a NoSQL db like DynamoDb in AWS
    //      The interface will help when the time comes to introduce the new database data source 
    //      and also with unit testing
    public class CheeseJsonData : ICheeseData
    {
        //TODO: Move this out to an environment variable at some point
        private const string cheeseDataFile = "Data/big-cheese.json";

        //TODO: This would work better with a better data source
        public Cheese AddCheese(Cheese newCheese)
        {
            var cheeseList = GetAllCheeses().ToList();

            cheeseList.Add(newCheese);

            WriteNewBigCheeseFile(cheeseList);

            return GetCheeseByCheeseId(newCheese.CheeseId);
        }

        public bool DeleteCheese(int cheeseId)
        {
            var cheeseList = GetAllCheeses().ToList();
            var cheeseToRemove = cheeseList.FirstOrDefault<Cheese>(c => c.CheeseId == cheeseId);

            if (cheeseToRemove == null)
                return false;

            cheeseList.Remove(cheeseToRemove);

            WriteNewBigCheeseFile(cheeseList);

            return true;
        }

        public IEnumerable<Cheese> GetAllCheeses()
        {
            var cheeseData = ReadBigCheeseFile();

            if (String.IsNullOrEmpty(cheeseData))
                return new List<Cheese>();

            return JsonConvert.DeserializeObject<IEnumerable<Cheese>>(cheeseData).OrderBy(c => c.CheeseId);
            
        }

        public Cheese GetCheeseByCheeseId(int cheeseId)
        {
            return GetAllCheeses()
                .ToList()
                .FirstOrDefault(c => c.CheeseId == cheeseId);
        }

        //TODO: this needs further refinement to find a better way to update an existing record in the json file, for now, removing the existing record and re-adding the updated one is sufficient, just not efficient
        public Cheese UpdateCheese(Cheese updatedCheese)
        {
            var cheeseList = GetAllCheeses().ToList();

            var cheeseToRemove = cheeseList.FirstOrDefault<Cheese>(c => c.CheeseId == updatedCheese.CheeseId);

            cheeseList.Remove(cheeseToRemove);

            cheeseList.Add(updatedCheese);

            WriteNewBigCheeseFile(cheeseList);

            return GetCheeseByCheeseId(updatedCheese.CheeseId);
        }

        public string ReadBigCheeseFile()
        {
            return File.ReadAllText(cheeseDataFile);
        }

        //TODO: improve this in the future to make it better instead of rewriting the entire file
        public bool WriteNewBigCheeseFile(IEnumerable<Cheese> cheeseList)
        {
            var stringCheese = JsonConvert.SerializeObject(cheeseList);

            File.WriteAllText(cheeseDataFile, stringCheese);

            return true;
        }
    }
}
