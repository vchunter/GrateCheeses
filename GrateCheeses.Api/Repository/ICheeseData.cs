using GrateCheeses.Api.Models;
using System.Collections.Generic;

namespace GrateCheeses.Api.Repository
{
    public interface ICheeseData
    {
        IEnumerable<Cheese> GetAllCheeses();
        Cheese AddCheese(Cheese newCheese);
        Cheese GetCheeseByCheeseId(int cheeseId);
        Cheese UpdateCheese(Cheese updatedCheese);
        bool DeleteCheese(int cheeseId);

    }
}
