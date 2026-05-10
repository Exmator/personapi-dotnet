using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace personapi_dotnet.Models.Entities;

[ModelMetadataType(typeof(EstudioMetadata))]
public partial class Estudio { }

internal sealed class EstudioMetadata
{
    [ValidateNever]
    public Persona CcPerNavigation { get; set; } = default!;

    [ValidateNever]
    public Profesion IdProfNavigation { get; set; } = default!;
}
