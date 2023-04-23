using Microsoft.AspNetCore.Mvc;

namespace Gama.Application.DataContracts.Commands.UserManagement;

public class UpdatePasswordCommand
{
    public string Login { get; set; }
    
    public string NewPassword { get; set; }
    
    public string OldPassword { get; set; }
}