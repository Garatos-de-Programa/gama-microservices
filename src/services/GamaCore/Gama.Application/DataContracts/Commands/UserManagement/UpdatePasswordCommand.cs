namespace Gama.Application.DataContracts.Commands.UserManagement;

public class UpdatePasswordCommand
{
    public string NewPassword { get; set; }
    
    public string OldPassword { get; set; }
}