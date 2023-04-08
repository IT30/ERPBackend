﻿using System.ComponentModel.DataAnnotations;

namespace Farma.DTO
{
    public class UserUpdateDTO
    {
        [Required(ErrorMessage = "User must have an ID.")]
        [MinLength(36, ErrorMessage = "GUID must be in following format (0x): xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx.")]
        [MaxLength(36, ErrorMessage = "GUID must be in following format (0x): xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx.")]
        public string IDUser { get; set; } = string.Empty!;
        public string? ProfilePictureURL { get; set; }

        [Required(ErrorMessage = "User must have an email.")]
        [MaxLength(40, ErrorMessage = "User email must be less than 40 characters.")]
        public string Email { get; set; } = string.Empty!;

        [Required(ErrorMessage = "User must have a password for logging.")]
        [MaxLength(25, ErrorMessage = "User password must be less than 25 characters.")]
        public string UserPassword { get; set; } = string.Empty!;

        [Required(ErrorMessage = "User must have a first name.")]
        [MaxLength(30, ErrorMessage = "User last name must be less than 30 characters.")]
        public string FirstName { get; set; } = string.Empty!;

        [Required(ErrorMessage = "User must have a last name.")]
        [MaxLength(30, ErrorMessage = "User last name name must be less than 30 characters.")]
        public string LastName { get; set; } = string.Empty!;

        [Required(ErrorMessage = "User must have an address.")]
        [MaxLength(50, ErrorMessage = "User address must be less than 50 characters.")]
        public string Address { get; set; } = string.Empty!;

        [Required(ErrorMessage = "User must be from a place.")]
        [MaxLength(30, ErrorMessage = "User phone number must be less than 30 characters.")]
        public string City { get; set; } = string.Empty!;

        [Required(ErrorMessage = "User must have a phone number.")]
        [MaxLength(12, ErrorMessage = "User username must be less than 12 characters.")]
        public string Phone { get; set; } = string.Empty!;

        [Required(ErrorMessage = "User must have a role.")]
        [MaxLength(6, ErrorMessage = "User type must be less than 6 characters.")]
        public string UserRole { get; set; } = string.Empty!;
    }
}
