using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace personapi_dotnet.Controllers;

public class BaseController : Controller
{
    protected static string DbErrorMessage(DbUpdateException ex)
    {
        var sql = ex.InnerException as Microsoft.Data.SqlClient.SqlException;
        return sql?.Number switch
        {
            547  => "No se puede completar la operación: verifique que los registros referenciados existan y que no haya datos dependientes.",
            2627 => "Ya existe un registro con ese identificador.",
            2601 => "Ya existe un registro con ese valor único.",
            _    => "Error al guardar en la base de datos. Intente de nuevo."
        };
    }
}
