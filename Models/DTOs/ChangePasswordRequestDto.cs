﻿namespace StudentTechShop.API.Models.DTOs
{
    public class ChangePasswordRequestDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
