﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CodeAutomationConsole
{
    public class CodeBuilder
    {
        private AutomationSettings _settings;

        public CodeBuilder(AutomationSettings settings)
        {
            _settings = settings;
        }

        public void Run()
        {
            ISettingsProcessor settingsProcessor = new SettingsProcessor();
            _settings = settingsProcessor.Run(_settings);

            var context = _settings.Config.FixTypes();
            var solutionTree = new SolutionTree(_settings.TemplatesPath, context);

            solutionTree = BuildCode(_settings, solutionTree);

            solutionTree.Save(_settings.OutputPath); // save generated code

            _settings.Save(Path.Combine(_settings.OutputPath, "config.yaml")); // save updated settings
        }

        private SolutionTree BuildCode(AutomationSettings settings, SolutionTree solutionTree)
        {
            solutionTree.RenderTemplate();

            return solutionTree;
        }
    }
}
