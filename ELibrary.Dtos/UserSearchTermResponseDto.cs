using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Dtos
{
    public class UserSearchTermResponseDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
    }
}
