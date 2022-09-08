using Badge2022EF.WebApi.Controllers.Crypto;
using System.Security.Cryptography;
using System.Text;

string encrypt = "U2FsdGVkX1+EuKMuCBWZ4qb/oP+HpliL+I9617ZDVmE=";


DeCrypToBusiness service = new("admin@admin.be");

string decrypt = service.AesDecrypt(Encoding.UTF8.GetBytes(encrypt));
Console.WriteLine(decrypt);