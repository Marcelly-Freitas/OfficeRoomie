# OfficeRoomie API - CRUD Tutorial

Este tutorial passo-a-passo orienta sobre como criar e adicionar componentes para um CRUD de entidades em um sistema utilizando ASP.NET Core com Razor Pages para o frontend e Controllers para a API REST.

## Estrutura do Projeto

O projeto contém as seguintes camadas:
- **Models**: Definem as entidades do banco de dados.
- **ViewModels**: Dados a serem exibidos nas páginas.
- **DTOs**: Transferência de dados via API.
- **Repositories**: Lógica de acesso a dados.
- **Controllers**: Endpoints da API REST.
- **Razor Pages**: Frontend para interação do usuário.

## Passo-a-passo

### 1. Criar o Model

Crie o **Model** que representa a entidade no banco de dados:

```csharp
public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
}
```

### 2. Criar o ViewModel

Crie o **ViewModel** para exibir dados:

```csharp
public class ClienteViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
}
```

### 3. Criar o DTO

Crie o **DTO** para transferir dados via API:

```csharp
public class ClienteDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
}
```

### 4. Criar o Repositório

Crie a **interface do repositório**:

```csharp
public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<Cliente> GetByIdAsync(int id);
    Task AddAsync(Cliente cliente);
    Task UpdateAsync(Cliente cliente);
    Task DeleteAsync(int id);
}
```

Implemente o **repositório**:

```csharp
public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task<Cliente> GetByIdAsync(int id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task AddAsync(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente != null)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
```

### 5. Criar o Controller

Crie o **Controller** para a API REST:

```csharp
[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase
{
    private readonly IClienteRepository _clienteRepository;

    public ClientesController(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clientes = await _clienteRepository.GetAllAsync();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ClienteDto clienteDto)
    {
        var cliente = new Cliente { Nome = clienteDto.Nome, Email = clienteDto.Email };
        await _clienteRepository.AddAsync(cliente);
        return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ClienteDto clienteDto)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        if (cliente == null) return NotFound();
        cliente.Nome = clienteDto.Nome;
        cliente.Email = clienteDto.Email;
        await _clienteRepository.UpdateAsync(cliente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _clienteRepository.DeleteAsync(id);
        return NoContent();
    }
}
```

### 6. Configurações no `Program.cs`

Adicione as configurações no arquivo `Program.cs`:

```csharp
builder.Services.AddControllers();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>(); // Registro do repositório
```

### 7. Configurar o Swagger

Para testar as APIs com Swagger:

```csharp
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OfficeRoomie API", Version = "v1" });
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OfficeRoomie API V1");
    c.RoutePrefix = string.Empty;  // Acesso na raiz (/)
});
```

### 8. Executar o Projeto

Compile e execute a aplicação, e acesse o Swagger para testar os endpoints da API:

```
https://localhost:{porta}/swagger
```

---

### Tecnologias Utilizadas

- **ASP.NET Core**
- **Entity Framework Core**
- **JWT Authentication**
- **Swagger**
- **Razor Pages**
