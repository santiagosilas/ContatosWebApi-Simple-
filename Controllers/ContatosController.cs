using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ContatosWebApi.Models;
using System.Linq;

namespace ContatosWebApi.Controllers
{
  //[Route("api/Contatos")]
  [Route("api/[controller]")]
  public class ContatosController : Controller
  {
    private readonly DataContext _context;

    public ContatosController(DataContext context)
    {
      _context = context;

      if (_context.Contatos.Count() == 0)
      {
        _context.Contatos.Add(new Contato { Nome = "Arthur Dent", Telefone = "(85) 8765-4321" });
        _context.Contatos.Add(new Contato { Nome = "Ford Prefect", Telefone = "(85) 8765-4321" });
        _context.Contatos.Add(new Contato { Nome = "Trillian", Telefone = "(85) 8765-4321" });
        _context.Contatos.Add(new Contato { Nome = "Zaphod Beeblebrox", Telefone = "(85) 8765-4321" });
        _context.Contatos.Add(new Contato { Nome = "Marvin", Telefone = "(85) 8765-4321" });
        _context.SaveChanges();
      }
    }

    [HttpGet]
    public IEnumerable<Contato> GetAll()
    {
      return _context.Contatos.ToList();
    }

    [HttpGet("{id}", Name = "GetContato")]
    public IActionResult GetById(long id)
    {
      var item = _context.Contatos.FirstOrDefault(t => t.Id == id);
      if (item == null)
      {
        return NotFound();
      }
      return new ObjectResult(item);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Contato contato)
    {
      if (contato == null)
      {
        return BadRequest();
      }

      _context.Contatos.Add(contato);
      _context.SaveChanges();

      return CreatedAtRoute("GetContato", new { id = contato.Id }, contato);
    }

    [HttpPut("{id}")]
    public IActionResult Update(long id, [FromBody] Contato item)
    {
      if (item == null || item.Id != id)
      {
        return BadRequest();
      }

      var contato = _context.Contatos.FirstOrDefault(t => t.Id == id);
      if (contato == null)
      {
        return NotFound();
      }

      contato.Nome = item.Nome;
      contato.Telefone = item.Telefone;

      _context.Contatos.Update(contato);
      _context.SaveChanges();
      return new NoContentResult();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
      var todo = _context.Contatos.FirstOrDefault(t => t.Id == id);
      if (todo == null)
      {
        return NotFound();
      }

      _context.Contatos.Remove(todo);
      _context.SaveChanges();
      return new NoContentResult();
    }



  }
}