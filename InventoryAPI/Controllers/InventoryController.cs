using InventoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace InventoryAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController : ControllerBase
{
   
    private readonly ILogger<InventoryController> _logger;
    private readonly InventoryRepository userRepository;

    private readonly IConfiguration configuration;
    public InventoryController( IConfiguration config, ILogger<InventoryController> logger)
    {
      configuration=config;
        _logger = logger;
        userRepository = new InventoryRepository(configuration.GetConnectionString("DefaultConnection"));
    }

    [HttpGet]
    [Route("getAllUsers")]
    public ActionResult GetAllUsers(){
      var isReturned= userRepository.getAllUsers(); 
      if (isReturned.Any()){
         return Ok(isReturned);
      }
      else{
         return NoContent();
      }
   }

   [HttpGet]
   [Route("getUser")]

   public ActionResult GetUser([FromBody]int userId){
      var isReturned = userRepository.getUser(userId);
      if (isReturned == null){
         return NoContent();
      }
      return Ok(isReturned);
      }
   

    [HttpPost]
    [Route("addUser")]
   public ActionResult AddUsers([FromBody] User user){
      var isAdded = userRepository.addUsers(user);
      if (isAdded == true){
         return Ok();
      }
      else{
         return NoContent();
      }
   }

   [HttpDelete]
   [Route("deleteUser")]

   public ActionResult DeleteUser([FromBody] int userId){
      var isDeleted = userRepository.deleteUsers(userId);
      if(isDeleted == true){
         return Ok();
      }
      else{
         return NoContent();
      }
         }
}
