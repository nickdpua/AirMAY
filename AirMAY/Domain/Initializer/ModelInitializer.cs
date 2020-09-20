using System;
using System.Collections.Generic;
using System.Text;

namespace AirMAY.Domain.Initializer
{
    public static class ModelInitializer
    {
        public static void Initialize(AirMAYDataBaseContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}