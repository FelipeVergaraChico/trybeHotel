using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 4. Desenvolva o endpoint GET /hotel
        public IEnumerable<HotelDto> GetHotels()
        {
            try
            {
                var res = _context.Hotels.Select(h => new HotelDto()
                {
                    hotelId = h.HotelId,
                    name = h.Name,
                    address = h.Address,
                    cityId = h.CityId,
                    cityName = h.City.Name
                }).ToList();
                return res;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }
        
        // 5. Desenvolva o endpoint POST /hotel
        public HotelDto AddHotel(Hotel hotel)
        {
            try
            {
                var city = _context.Cities.Find(hotel.CityId);
                if (city == null)
                {
                    throw new Exception("City not found");
                }
                _context.Hotels.Add(hotel);
                _context.SaveChanges();
                var nh = _context.Hotels.Last();
                return new HotelDto()
                {
                    hotelId = hotel.HotelId,
                    name = hotel.Name,
                    address = hotel.Address,
                    cityId = hotel.CityId,
                    cityName = nh.City.Name,
                };
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }
    }
}