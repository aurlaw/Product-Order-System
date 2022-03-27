using System.ComponentModel.DataAnnotations;

namespace AurSystem.Framework.Models.Dto;

public class CustomerBalanceDto
{
    [Required] public Guid Id { get; set; } = default;

    [Required] public double Balance { get; set; } = default;

}