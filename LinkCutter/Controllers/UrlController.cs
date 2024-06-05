using LinkCutter.Application.DTOs;
using LinkCutter.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace LinkCutter.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class UrlController : ControllerBase
{
    private readonly IUrlService _urlService;
    private readonly ISecurityService _securityService;
    public UrlController(IUrlService urlService, ISecurityService securityService)
    {
        _securityService = securityService;
        _urlService = urlService;
    }

    [HttpGet("{rash}")]
    [SwaggerOperation(Summary = "Pega a url que está vinculada ao rash code.")]
    [SwaggerResponse(StatusCodes.Status201Created)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUrl([FromHeader][Required] string token, [Required] string rash)
    {
        var isValidToken = await _securityService.ValidateTokenAsync(token);
        if (!isValidToken) return Unauthorized();
        var result = await _urlService.GetUrlAsync(rash);
        return Ok(result);
    }


    [HttpPost]
    [SwaggerOperation(Summary = "Encurta uma url e retorna o rash dela.")]
    [SwaggerResponse(StatusCodes.Status201Created)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostUrl([FromBody] UrlDTO urlDTO, [FromHeader][Required] string token)
    {
        var isValidToken = await _securityService.ValidateTokenAsync(token);
        if (!isValidToken) return Unauthorized();
        var result = await _urlService.PostUrlAsync(urlDTO.OriginalUrl);
        return Ok(result);
    }

}
