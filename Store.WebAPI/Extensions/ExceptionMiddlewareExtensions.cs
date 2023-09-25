using Microsoft.AspNetCore.Diagnostics;
using Store.Business.Abstract;
using Store.DAL.ErrorModel;
using Store.DAL.Exceptions;
using System.Net;

namespace Store.WebAPI.Extensions
{
	public static class ExceptionMiddlewareExtensions
	{
		public static void ConfigureExceptionHandler(this WebApplication app,
			ILoggerService logger)
		{
			app.UseExceptionHandler(appError =>
			{
				appError.Run(async context =>
				{
					
					context.Response.ContentType = "application/json";
					var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
					if (contextFeature is not null) 
					{
						context.Response.StatusCode = contextFeature.Error switch
						{
							NotFoundException => StatusCodes.Status404NotFound,
							_ => StatusCodes.Status500InternalServerError,

						};


						context.Response.StatusCode = contextFeature.Error switch
						{
						  InvalidException => StatusCodes.Status400BadRequest,
						  _ =>StatusCodes.Status400BadRequest,
						};
						



						logger.LogError($"Birşeyler ters gitti : {contextFeature.Error}");
						await context.Response.WriteAsync(new ErrorDetails()
						{
							StatusCode = context.Response.StatusCode,
							Message = contextFeature.Error.Message
						}.ToString());
					}
				});
			});
		}
	}
}
