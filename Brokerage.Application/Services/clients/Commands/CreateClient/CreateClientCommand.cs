using MediatR;

public record CreateClientCommand(
    string Username,
    string Name,
    string Email,
    string Password,
    string NationalID,
    string PhoneNumber,
    decimal Deposit)
    : IRequest<int>;