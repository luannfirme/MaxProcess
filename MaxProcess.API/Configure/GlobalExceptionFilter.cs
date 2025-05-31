using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MaxProcess.API.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;

            if (ex is ValidationException validationException)
            {
                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                var problemDetails = new ValidationProblemDetails(errors)
                {
                    Status = 400,
                    Title = "Um ou mais campos estão inválidos.",
                    Detail = "Verifique o dicionário 'errors' para detalhes."
                };

                context.Result = new BadRequestObjectResult(problemDetails);
                context.ExceptionHandled = true;
                return;
            }

            if (ex is InvalidOperationException invalidOpEx)
            {
                var problem = new ProblemDetails
                {
                    Status = 400,
                    Title = "Operação inválida.",
                    Detail = invalidOpEx.Message
                };

                context.Result = new BadRequestObjectResult(problem);
                context.ExceptionHandled = true;
                return;
            }

            if (ex is ArgumentNullException argNullEx)
            {
                var problem = new ProblemDetails
                {
                    Status = 400,
                    Title = $"Parâmetro obrigatório '{argNullEx.ParamName}' não informado.",
                    Detail = argNullEx.Message
                };

                context.Result = new BadRequestObjectResult(problem);
                context.ExceptionHandled = true;
                return;
            }
            if (ex is ArgumentException argEx)
            {
                var problem = new ProblemDetails
                {
                    Status = 400,
                    Title = "Argumento inválido.",
                    Detail = argEx.Message
                };

                context.Result = new BadRequestObjectResult(problem);
                context.ExceptionHandled = true;
                return;
            }

            if (ex is DbUpdateException dbUpdateEx)
            {
                var inner = ex.InnerException is SqlException sqlEx
                    ? $"Erro no banco de dados: {sqlEx.Message}"
                    : "Erro ao atualizar o banco de dados.";

                var problem = new ProblemDetails
                {
                    Status = 500,
                    Title = "Falha ao persistir informação.",
                    Detail = inner
                };

                context.Result = new ObjectResult(problem) { StatusCode = 500 };
                context.ExceptionHandled = true;
                return;
            }
            if (ex is SqlException sqlException)
            {
                var problem = new ProblemDetails
                {
                    Status = 500,
                    Title = "Erro de conexão com o banco de dados.",
                    Detail = sqlException.Message
                };

                context.Result = new ObjectResult(problem) { StatusCode = 500 };
                context.ExceptionHandled = true;
                return;
            }

            {
                var problem = new ProblemDetails
                {
                    Status = 500,
                    Title = "Erro interno do servidor.",
                    Detail = "Ocorreu um erro inesperado. Tente novamente mais tarde."
                };

                context.Result = new ObjectResult(problem) { StatusCode = 500 };
                context.ExceptionHandled = true;
                return;
            }
        }
    }
}