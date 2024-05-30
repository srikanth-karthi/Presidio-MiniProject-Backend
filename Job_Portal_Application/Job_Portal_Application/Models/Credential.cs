using Job_Portal_Application.Dto.Enums;
using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Models
{
    public class Credential
    {
           [Key]
         public Guid  CredentialId =Guid.NewGuid();

        public byte[] Password { get; set; }

        public byte[] HasCode { get; set; }

        public Roles Role {  get; set; }




    }
}
