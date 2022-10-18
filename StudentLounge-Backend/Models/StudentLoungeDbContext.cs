using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StudentLounge_Backend.Models
{
    public class StudentLoungeDbContext : IdentityDbContext<StudentLoungeUser>
    {

    }
}
