using System.ComponentModel.DataAnnotations;
namespace Investoras_Backend.Data.Dto;

public record CategoryDto(
    int CategoryId,
    string Name,
    bool IsIncome,
    string Description
    );
public record CreateCategoryDto(
    [Required(ErrorMessage = "Введите название категории.")]
    [StringLength(30, ErrorMessage = "Слишком длинное название категории.")]
    string Name,

    [Required(ErrorMessage = "Укажите тип категории.")]
    bool IsIncome,

    [Required(ErrorMessage = "Введите описание категории.")]
    [StringLength(100, ErrorMessage = "Слишком длинное описание категории.")]
    string Description
    );
public record UpdateCategoryDto(
    [Required(ErrorMessage = "Введите название категории.")]
    [StringLength(30, ErrorMessage = "Слишком длинное название категории.")]
    string Name,

    [Required(ErrorMessage = "Укажите тип категории.")]
    bool IsIncome,

    [Required(ErrorMessage = "Введите описание категории.")]
    [StringLength(100, ErrorMessage = "Слишком длинное описание категории.")]
    string Description
    );