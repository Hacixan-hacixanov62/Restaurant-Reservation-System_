﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Core.Attributes
{
    public class AllowedTypesAttribute: ValidationAttribute
    {
        private string[] _allowedTypes;
        public AllowedTypesAttribute(params string[] types)
        {
            _allowedTypes = types;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            List<IFormFile> files = new List<IFormFile>();
            if (value is List<IFormFile> fileList) files = fileList;
            if (value is IFormFile file) files.Add(file);

            foreach (var item in files)
            {
                if (!_allowedTypes.Contains(item.ContentType))
                {
                    string message = "File content types must be only .... ";
                    return new ValidationResult(message);
                }
            }

            return ValidationResult.Success;
        }
    }
}
