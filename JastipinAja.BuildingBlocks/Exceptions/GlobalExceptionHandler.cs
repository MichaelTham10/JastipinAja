using JastipinAja.BuildingBlocks.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JastipinAja.BuildingBlocks.Exceptions
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, Exception exception, CancellationToken ct)
        {
            var (status, title) = exception switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, "Resource tidak ditemukan"),
                DomainException => (StatusCodes.Status409Conflict, "Aturan bisnis dilanggar"),
                FluentValidation.ValidationException => (StatusCodes.Status400BadRequest, "Validasi gagal"),
                _ => (StatusCodes.Status500InternalServerError, "Terjadi kesalahan")
            };

            var problem = new ProblemDetails
            {
                Status = status,
                Title = title,
                // pesan teknis hanya ditampilkan untuk error yang aman; 500 disembunyikan
                Detail = status == StatusCodes.Status500InternalServerError
                    ? "Silakan coba lagi nanti."
                    : exception.Message,
                Type = $"https://httpstatuses.io/{status}"
            };

            httpContext.Response.StatusCode = status;
            await httpContext.Response.WriteAsJsonAsync(problem, ct);
            return true;
        }
    }
}
