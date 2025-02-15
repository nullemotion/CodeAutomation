﻿namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ProjectManagement
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Configuration;
    using Catel.Logging;
    using Catel.Services;
    using CsvHelper.Configuration;
    using Gum.ProjectManagement;
    using MethodTimer;
    using Orc.Csv;
    using Orc.FileSystem;
    using Orc.ProjectManagement;
    using Models;
    using System.Windows;

    public class ProjectReader : Gum.ProjectManagement.ProjectReaderBase
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IPleaseWaitService _pleaseWaitService;
        private readonly ICsvReaderService _csvReaderService;
        private readonly IFileService _fileService;

        public ProjectReader(IPleaseWaitService pleaseWaitService, ICsvReaderService csvReaderService,
            IDirectoryService directoryService, IFileService fileService, IProjectSettingsSerializationService projectSettingsSerializationService,
            IConfigurationService configurationService)
            : base(directoryService, fileService, projectSettingsSerializationService, configurationService)
        {
            Argument.IsNotNull(() => pleaseWaitService);
            Argument.IsNotNull(() => csvReaderService);

            _pleaseWaitService = pleaseWaitService;
            _csvReaderService = csvReaderService;
            _fileService = fileService;
        }

        [Time]
        protected override async Task<IProject> ReadFromLocationAsync(string location, ProjectSettings projectSettings)
        {
            var project = new FactoryPlanningProject(location);

            // Note: in the future this should be a plxml with the input data etc

            int currentStep = 0;
            const int totalSteps = 2;

            await _csvReaderService.LoadProjectFilesAsync(project);

            _pleaseWaitService.UpdateStatus(currentStep++, totalSteps);

            _pleaseWaitService.UpdateStatus(currentStep++, totalSteps);

            //=============================================================================

            project.Initialise();

            //=============================================================================

            Debug.Assert(currentStep == totalSteps, $"Make sure to match total steps to '{currentStep}'");

            _pleaseWaitService.UpdateStatus(currentStep, totalSteps);

            return project;
        }
    }
}
