using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IoC
{

    //tüm projelerde kullanabilmek için(bu proje dışında)
    public interface ICoreModule
    {
        void Load(IServiceCollection serviceCollection);
    }
}
