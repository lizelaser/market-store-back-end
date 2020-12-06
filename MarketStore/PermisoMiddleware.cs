using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketStore
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class PermisoMiddleware
    {
        private readonly RequestDelegate _next;

        public PermisoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, MARKETSTOREContext context)
        {
            try
            {
                string controller = httpContext.Request.RouteValues["controller"] as string;
                string action = httpContext.Request.RouteValues["action"] as string;

                Permiso permisoRequerido = await context.Permiso
                    .Where(x => x.Controlador == controller && x.Accion == action)
                    .FirstOrDefaultAsync();

                if (permisoRequerido != null)
                {
                    if (!permisoRequerido.Protegido)
                    {
                        await _next(httpContext);
                    }
                    else
                    {
                        if (httpContext.User.Identity != null)
                        {
                            int usuarioId = int.Parse(httpContext.User.Identity.Name ?? string.Empty);
                            Usuario usuario = await context.Usuario.FindAsync(usuarioId);

                            List<Permiso> permisosUsuario = await context.RolPermiso
                                .Include(x => x.Permiso)
                                .Where(x => x.RolId == usuario.RolId)
                                .Select(x => x.Permiso)
                                .ToListAsync();

                            if (permisosUsuario.Contains(permisoRequerido))
                            {
                                await _next(httpContext);
                                return;
                            }

                            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        }
                        httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                    }
                }
                else
                {
                    httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                }
            }
            catch (Exception e)
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsync(e.Message);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class PermisoMiddlewareExtensions
    {
        public static IApplicationBuilder UsePermisoMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PermisoMiddleware>();
        }
    }
}
