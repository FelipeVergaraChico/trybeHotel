using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        public RoomRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 6. Desenvolva o endpoint GET /room/:hotelId
        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
            try
            {
                var res =(from room in _context.Rooms
                          join hotel in _context.Hotels on room.HotelId equals hotel.HotelId
                          select new RoomDto()
                          {
                            roomId = room.RoomId,
                            name = room.Name,
                            capacity = room.Capacity,
                            image = room.Image,
                            hotel = new HotelDto()
                            {
                                hotelId = hotel.HotelId,
                                name = hotel.Name,
                                address = hotel.Address,
                                cityId = hotel.City.CityId,
                                cityName = hotel.City.Name
                            }
                          }).ToList();
                return res;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        // 7. Desenvolva o endpoint POST /room
        public RoomDto AddRoom(Room room) {
            try
            {
                _context.Rooms.Add(room);
                _context.SaveChanges();
                var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == room.HotelId);
                if (hotel != null) {
                    var city = _context.Cities.FirstOrDefault(c => c.CityId == hotel.CityId);
                    if (city != null) {
                        return new RoomDto() {
                            roomId = room.RoomId,
                            name = room.Name,
                            capacity = room.Capacity,
                            image = room.Image,
                            hotel = new HotelDto() {
                                hotelId = hotel.HotelId,
                                name = hotel.Name,
                                address = hotel.Address,
                                cityId = city.CityId,
                                cityName = city.Name
                            }
                        };
                    } else {
                        throw new Exception("City not found");
                    }
                } else {
                    throw new Exception("Hotel not found");
                }
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        // 8. Desenvolva o endpoint DELETE /room/:roomId
        public void DeleteRoom(int RoomId) {
            try
            {
                var room = _context.Rooms.First(r => r.RoomId == RoomId);
                _context.Rooms.Remove(room);
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }
    }
}