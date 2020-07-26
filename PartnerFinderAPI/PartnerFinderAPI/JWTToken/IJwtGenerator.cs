using PartnerFinderAPI.DTO;
using PartnerFinderAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerFinderAPI.JWTToken
{
    public interface IJwtGenerator
    {
        public LoginResponseDTO CreateToken(AppUser user, string[] role);
    }
}
