namespace Investoras_Backend.Data.Dto;

public record CategoryDto(
    int CategoryId,
    string Name,
    bool IsIncome,
    string Description 
    );
public record CreateCategoryDto(
    string Name,
    bool IsIncome,
    string Description
    );
public record UpdateCategoryDto(
    string Name,
    bool IsIncome,
    string Description
    );