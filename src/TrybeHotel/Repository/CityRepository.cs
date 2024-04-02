using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 2. Desenvolva o endpoint GET /city
        public IEnumerable<CityDto> GetCities()
        {
            try
            {
                var res = _context.Cities.Select(c => new CityDto()
                {
                    cityId = c.CityId,
                    name = c.Name
                }).ToList();
                return res;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        // 3. Desenvolva o endpoint POST /city
        public CityDto AddCity(City city)
        {
            try
            {
                _context.Cities.Add(city);
                _context.SaveChanges();
                return new CityDto()
                {
                    cityId = city.CityId,
                    name = city.Name
                };
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

    }
}