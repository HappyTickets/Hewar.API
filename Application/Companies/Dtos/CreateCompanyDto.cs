﻿namespace Application.Companies.Dtos
{
    public class CreateCompanyDto
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Address { get; set; }
    }
}