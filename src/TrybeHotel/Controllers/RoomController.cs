using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("room")]
    public class RoomController : Controller
    {
        private readonly IRoomRepository _repository;
        public RoomController(IRoomRepository repository)
        {
            _repository = repository;
        }

        // 6. Desenvolva o endpoint GET /room/:hotelId
        [HttpGet("{HotelId}")]
        public IActionResult GetRoom(int HotelId){
            var res = _repository.GetRooms(HotelId);
            return Ok(res);
        }

        // 7. Desenvolva o endpoint POST /room
        [HttpPost]
        public IActionResult PostRoom([FromBody] Room room){
            var newRoom = _repository.AddRoom(room);
            return Created("", newRoom);
        }

        // 8. Desenvolva o endpoint DELETE /room/:roomId
        [HttpDelete("{RoomId}")]
        public IActionResult Delete(int RoomId)
        {
            _repository.DeleteRoom(RoomId);
            return NoContent();
        }
    }
}