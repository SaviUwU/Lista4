using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PessoaController : ControllerBase
{
    private static List<Pessoa> pessoas = new List<Pessoa>();

    
    [HttpPost]
    public IActionResult AdicionarPessoa([FromBody] Pessoa novaPessoa)
    {
        pessoas.Add(novaPessoa);
        return Ok(novaPessoa);
    }

   
    [HttpPut("{cpf}")]
    public IActionResult AtualizarPessoa(string cpf, [FromBody] Pessoa pessoaAtualizada)
    {
        var pessoa = pessoas.FirstOrDefault(p => p.Cpf == cpf);
        if (pessoa == null)
        {
            return NotFound("Pessoa não encontrada");
        }

        pessoa.Nome = pessoaAtualizada.Nome;
        pessoa.Peso = pessoaAtualizada.Peso;
        pessoa.Altura = pessoaAtualizada.Altura;

        return Ok(pessoa);
    }

    [HttpDelete("{cpf}")]
    public IActionResult RemoverPessoa(string cpf)
    {
        var pessoa = pessoas.FirstOrDefault(p => p.Cpf == cpf);
        if (pessoa == null)
        {
            return NotFound("Pessoa não encontrada");
        }

        pessoas.Remove(pessoa);
        return Ok();
    }

   
    [HttpGet]
    public IActionResult BuscarTodasPessoas()
    {
        return Ok(pessoas);
    }

    
    [HttpGet("{cpf}")]
    public IActionResult BuscarPessoaPorCpf(string cpf)
    {
        var pessoa = pessoas.FirstOrDefault(p => p.Cpf == cpf);
        if (pessoa == null)
        {
            return NotFound("Pessoa não encontrada");
        }

        return Ok(pessoa);
    }

    
    [HttpGet("imc/bom")]
    public IActionResult BuscarPessoasComIMCBom()
    {
        var pessoasComIMCBom = pessoas.Where(p => p.CalcularIMC() >= 18 && p.CalcularIMC() <= 24).ToList();
        return Ok(pessoasComIMCBom);
    }

   
    [HttpGet("buscar-por-nome/{nome}")]
    public IActionResult BuscarPessoasPorNome(string nome)
    {
        var pessoasComNome = pessoas.Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();
        return Ok(pessoasComNome);
    }
}
