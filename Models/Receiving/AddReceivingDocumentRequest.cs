﻿using System.ComponentModel.DataAnnotations;

namespace CarpenterAPI.Models.Receiving
{
    public class AddReceivingDocumentRequest
    {
        [Required, MinLength(1)]
        public ProductQuantity[] ProductQuantities { get; set;}
    }
}
