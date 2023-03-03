// namespace ApiGateway
// {
//     using System.Threading.Tasks;
//     using Ocelot.Middleware;
//
//     public class CustomAuthenticationMiddleware
//     {
//         private readonly OcelotRequestDelegate _next;
//
//         public CustomAuthenticationMiddleware(OcelotRequestDelegate next)
//         {
//             _next = next;
//         }
//
//         public async Task Invoke(HttpContext context)
//         {
//             // TODO: Implement your authentication logic here
//
//             if (!IsAuthenticated(context))
//             {
//                 context.Response.StatusCode = 401;
//                 await context.Response.WriteAsync("Unauthorized");
//                 return;
//             }
//
//             await _next.Invoke(context);
//         }
//
//         private bool IsAuthenticated(HttpContext context)
//         {
//             // TODO: Implement your authentication logic here
//             return true;
//         }
//     }
// }
