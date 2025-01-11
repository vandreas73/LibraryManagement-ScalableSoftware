﻿using System.ComponentModel.DataAnnotations;

namespace UserManagementService.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Email { get; set; }
		public string Address { get; set; }
	}
}
