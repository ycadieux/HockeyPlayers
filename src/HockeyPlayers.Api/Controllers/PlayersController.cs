using HockeyPlayers.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HockeyPlayers.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        readonly IOptions<AppSettings> _appSettings;
        readonly TemplateContext _context;

        public PlayersController(IOptions<AppSettings> appSettings, TemplateContext context)
        {
            _appSettings = appSettings;
            _context = context;
        }

        [HttpGet("")]
        public IList<Player> List()
        {
            var entities = _context.Players.ToList();

            return entities;
        }

        [HttpGet("{id}")]
        public ActionResult<Player> Get(int id)
        {
            var entity = _context.Players.FirstOrDefault(x => x.Id == id);

            if (entity != null)
            {
                return entity;
            }

            return NotFound();
        }
    }
}
