using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FunctionAppVotacao.Models;

namespace FunctionAppVotacao;

public static class Resultado
{
    [FunctionName("Resultado")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
        ILogger log,
        [Sql("SELECT Tecnologia, COUNT(1) AS QtdVotos FROM dbo.VotoTecnologia GROUP BY Tecnologia",
            CommandType = System.Data.CommandType.Text,
            ConnectionStringSetting = "BaseVotacao")]
            IEnumerable<ResultadoTecnologia> resultadoTecnologias)
    {
        log.LogInformation(
            $"Resultado parcial = {JsonSerializer.Serialize(resultadoTecnologias)}");
        return new OkObjectResult(resultadoTecnologias);
    }
}