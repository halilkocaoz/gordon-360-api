﻿using System;
/// <summary>
/// Namespace where we will define custom exceptions to be throwl later on.
/// </summary>
namespace Gordon360.Exceptions.CustomExceptions
{
    public class ResourceNotFoundException : Exception
    {
        public string ExceptionMessage { get; set; }
    }
    
}