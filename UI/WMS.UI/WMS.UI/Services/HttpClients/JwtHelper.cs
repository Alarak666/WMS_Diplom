//using System.Security.Claims;
//using System.Text;

//namespace WMS.UI.Services.HttpClients;

//public class JwtHelper
//{
//    private readonly IConfiguration _configuration;

//    public JwtHelper(IConfiguration configuration)
//    {
//        _configuration = configuration;
//    }

//    public string GetJwtToken()
//    {
//        // Получение значений конфигураций из файла appsettings.json
//        var jwtKey = _configuration["jwt:key"];
//        var jwtIssuer = _configuration["jwt:issuer"];
//        var jwtAudience = _configuration["jwt:audience"];

//        // Создание токена
//        var tokenHandler = new JwtSecurityTokenHandler();
//        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
//        var tokenDescriptor = new SecurityTokenDescriptor
//        {
//            Subject = new ClaimsIdentity(new[]
//            {
//                new(ClaimTypes.Name, "tester"),
//                new Claim(ClaimTypes.Role, "admin")
//            }),
//            Expires = DateTime.UtcNow.AddMinutes(30),
//            SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
//        };

//        tokenDescriptor.Issuer = jwtIssuer;
//        tokenDescriptor.Audience = jwtAudience;

//        var token = tokenHandler.CreateToken(tokenDescriptor);
//        return tokenHandler.WriteToken(token);
//    }
//}