﻿using System.Net;

namespace StructureOfUniversity.Models.Exceptions;

public class StatusCodeException(string message, HttpStatusCode statusCode) : Exception(message)
{
    public HttpStatusCode HttpStatus { get; } = statusCode;
}
