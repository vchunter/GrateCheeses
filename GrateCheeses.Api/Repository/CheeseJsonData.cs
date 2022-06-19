using GrateCheeses.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GrateCheeses.Api.Repository
{
    //TODO: This class is used to serve the cheese data from a json data file to start off with
    //      Future plans are to move the cheese data out of an embedded file and into a database
    //      The interface will help when the time comes to introduce the new database data source 
    //      and also with unit testing
    public class CheeseJsonData : ICheeseData
    {
        //TODO: Move this out to an environment variable at some point
        private const string cheeseDataFile = "Data/big-cheese.json";

        public Cheese AddCheese(Cheese newCheese)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteCheese(int cheeseId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Cheese> GetAllCheeses()
        {
            var cheeseData = ReadBigCheeseFile();

            if (String.IsNullOrEmpty(cheeseData))
                return new List<Cheese>();

            return JsonSerializer.Deserialize<List<Cheese>>(cheeseData);
            
        }

        public Cheese GetCheeseByCheeseId(int cheeseId)
        {
            throw new System.NotImplementedException();
        }

        public Cheese UpdateCheese(Cheese updatedCheese)
        {
            throw new System.NotImplementedException();
        }

        public string ReadBigCheeseFile()
        {
            return File.ReadAllText(cheeseDataFile);
        }

        public bool WriteBigCheeseFile()
        {
            throw new System.NotImplementedException();
        }
    }
}
