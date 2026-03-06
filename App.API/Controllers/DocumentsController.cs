using App.API.CacheItems;
using App.API.DTOs;
using App.Repository;
using App.Repository.DocumentItems;
using App.Repository.DocumentStatusEnum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using static StackExchange.Redis.Role;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly DocumentService _documentService;
        public DocumentsController(AppDbContext context ,IWebHostEnvironment env, DocumentService documentService)
        {
            _env = env;
            _context = context;
            _documentService= documentService;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments(CancellationToken cancellationToken)
        {
            var documentAsJson = await _documentService.GetDocumentsFromCache(cancellationToken);

            if (!string.IsNullOrWhiteSpace(documentAsJson))
            {
                var cachedDocuments = JsonSerializer.Deserialize<List<GetDocumentDto>>(documentAsJson);

                if (cachedDocuments is not null)
                    return Ok(cachedDocuments);
            }

            var documents = await _context.Documents.ToListAsync(cancellationToken);
            return Ok(documents);
        }

        // GET: api/Documents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            return document;
        }

        // PUT: api/Documents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(int id, Document document)
        {
            if (id != document.Id)
            {
                return BadRequest();
            }

            _context.Entry(document).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Documents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostDocument([FromForm] DocumentUploadDto model, CancellationToken cancellationToken)
        {
            if (model.File == null)
                return BadRequest("Dosya bulunamadı");

            var uploadPath = Path.Combine(_env.ContentRootPath, "uploads");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(model.File.FileName);

            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }

            var document = new Document {
                UserId = model.UserId,
                Title = model.Title,
                FilePath = fileName,
                Status = DocumentStatus.Pending,
                UploadedDate = DateTime.Now
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            await _documentService.CreateCacheAsync(document, cancellationToken);

            return CreatedAtAction("GetDocument", new { id = document.Id }, document);
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
    }
}
