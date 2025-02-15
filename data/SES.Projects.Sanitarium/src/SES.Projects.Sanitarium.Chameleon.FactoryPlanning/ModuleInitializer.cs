﻿using Catel.IoC;
using Catel.Services;
using Gum.Drawing;
using SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ViewModels;
using SES.Projects.Sanitarium.Chameleon.FactoryPlanning.Views;

/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static class ModuleInitializer
{
    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        var serviceLocator = ServiceLocator.Default;

        Figure.IsErasingRequiredOnStylePropertyChanged = true;
    }
}
