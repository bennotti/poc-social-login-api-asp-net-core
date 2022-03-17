using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleProject.Core.Dto;
using SampleProject.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace SampleProject.Api.Controllers
{
    [ApiController]
    [Route("api/credencial/google")]
    public class GoogleController : ControllerBase
    {
        private readonly GoogleApiAuthSettings _googleApiAuthSettings;
        public GoogleController(GoogleApiAuthSettings googleApiAuthSettings)
        {
            _googleApiAuthSettings = googleApiAuthSettings;
        }
        [HttpPost]
        [Route("validar")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterPorCodigo([FromBody]CredencialGoogleValidarRequestDto bodyRequest)
        {
            await Task.CompletedTask;
            Payload payload = null;
            try
            {
                payload = await ValidateAsync(bodyRequest.IdToken, new ValidationSettings
                {
                    Audience = new[] { _googleApiAuthSettings.ClientId }
                });
                // It is important to add your ClientId as an audience in order to make sure
                // that the token is for your application!
            }
            catch
            {
                // Invalid token
            }
            if (payload == null)
            {
                return BadRequest();
            }

            return Ok(payload);
        }
        public async Task<string> GenerateToken(UserDto user)
        {
            await Task.CompletedTask;
            //var claims = await GetUserClaims(user);
            return "";
        }
        public async Task<UserDto> GetOrCreateExternalLoginUser(string provider, string key, string email, string firstName, string lastName)
        {
            await Task.CompletedTask;
            return new UserDto {
                Name = ($"{firstName} {lastName}").Trim(),
            };
        }
    }
}
