using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Collections;

namespace FastBuildGen.VisualStudio
{
    [DebuggerDisplay("{ProjectName}, {RelativePath}, {ProjectGuid}")]
    public class SolutionProject
    {
        static readonly Type s_ProjectInSolution;
        static readonly PropertyInfo s_ProjectInSolution_ProjectName;
        static readonly PropertyInfo s_ProjectInSolution_RelativePath;
        static readonly PropertyInfo s_ProjectInSolution_ProjectGuid;
        static readonly PropertyInfo s_ProjectInSolution_Dependencies;
        static readonly PropertyInfo s_ProjectInSolution_AbsolutePath;
        static readonly PropertyInfo s_ProjectInSolution_ParentProjectGuid;
        static readonly PropertyInfo s_ProjectInSolution_Extension;
        
        static SolutionProject()
        {
            s_ProjectInSolution = Type.GetType("Microsoft.Build.Construction.ProjectInSolution, Microsoft.Build, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", false, false);
            if (s_ProjectInSolution != null)
            {
                s_ProjectInSolution_ProjectName = s_ProjectInSolution.GetProperty("ProjectName", BindingFlags.NonPublic | BindingFlags.Instance);
                s_ProjectInSolution_RelativePath = s_ProjectInSolution.GetProperty("RelativePath", BindingFlags.NonPublic | BindingFlags.Instance);
                s_ProjectInSolution_ProjectGuid = s_ProjectInSolution.GetProperty("ProjectGuid", BindingFlags.NonPublic | BindingFlags.Instance);
                s_ProjectInSolution_Dependencies = s_ProjectInSolution.GetProperty("Dependencies", BindingFlags.NonPublic | BindingFlags.Instance);
                s_ProjectInSolution_AbsolutePath = s_ProjectInSolution.GetProperty("AbsolutePath", BindingFlags.NonPublic | BindingFlags.Instance);
                s_ProjectInSolution_ParentProjectGuid = s_ProjectInSolution.GetProperty("ParentProjectGuid", BindingFlags.NonPublic | BindingFlags.Instance);
                s_ProjectInSolution_Extension = s_ProjectInSolution.GetProperty("Extension", BindingFlags.NonPublic | BindingFlags.Instance);
            }
        }

        public string ProjectName { get; private set; }
        public string RelativePath { get; private set; }
        public string ProjectGuid { get; private set; }
        public IEnumerable<string> Dependencies { get; private set; }
        public string AbsolutePath { get; private set; }
        public string ParentProjectGuid { get; private set; }
        public string Extension { get; private set; }

        public SolutionProject(object solutionProject)
        {
            this.ProjectName = s_ProjectInSolution_ProjectName.GetValue(solutionProject, null) as string;
            this.RelativePath = s_ProjectInSolution_RelativePath.GetValue(solutionProject, null) as string;
            this.ProjectGuid = s_ProjectInSolution_ProjectGuid.GetValue(solutionProject, null) as string;
            this.Dependencies = (s_ProjectInSolution_Dependencies.GetValue(solutionProject, null) as ArrayList).OfType<string>().ToArray();
            //this.AbsolutePath = s_ProjectInSolution_AbsolutePath.GetValue(solutionProject, null) as string;
            this.ParentProjectGuid = s_ProjectInSolution_ParentProjectGuid.GetValue(solutionProject, null) as string;
            this.Extension = s_ProjectInSolution_Extension.GetValue(solutionProject, null) as string;
        }
    }
}
