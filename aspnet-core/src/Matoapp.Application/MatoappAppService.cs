using System;
using System.Collections.Generic;
using System.Text;
using Matoapp.Localization;
using Volo.Abp.Application.Services;

namespace Matoapp;

/* Inherit your application services from this class.
 */
public abstract class MatoappAppService : ApplicationService
{
    protected MatoappAppService()
    {
        LocalizationResource = typeof(MatoappResource);
    }
}
