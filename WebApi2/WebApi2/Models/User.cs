﻿using System;
using System.Data;

namespace WebApi2.Models
{
	public class User : IUser
	{
			public int Id { get; set; }
			public string Username { get; set; }
			public string Password { get; set; }
			public string Role { get; set; }
    }
}

