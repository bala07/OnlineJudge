﻿using System.Collections.Generic;
using System.IO;
using System.Web;

using DataAccessLayer;
using DataAccessLayer.RepositoryInterfaces;

using Domain.Models;

using Newtonsoft.Json;

using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Service
{
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository problemRepository;

        private readonly IFileService fileService;

        private readonly IPathService pathService;

        public ProblemService()
        {
            problemRepository = new ProblemRepository();
            fileService = new FileService();
            pathService = new PathService();
        }

        public Problem GetProblem(string name)
        {
            var problem = problemRepository.Get(name);

            return problem;
        }

        public IList<Problem> GetAllProblems()
        {
            var problems = problemRepository.GetAll();

            return problems;
        }

        public Problem GetProblemWithStatement(string problemCode)
        {
            var problem = this.GetProblem(problemCode);
            problem.ProblemStatement = GetProblemStatement(problemCode);

            return problem;
        }

        public IList<Problem> GetAllProblemsWithStatements()
        {
            var problems = this.GetAllProblems();

            foreach (var problem in problems)
            {
                problem.ProblemStatement = this.GetProblemStatement(problem.Code);
            }

            return problems;
        }

        private ProblemStatement GetProblemStatement(string problemCode)
        {
            var baseDir = pathService.GetAppDataPath();
            var problemFilePath = baseDir + "\\problems\\" + problemCode + ".json";

            return JsonConvert.DeserializeObject<ProblemStatement>(File.ReadAllText(problemFilePath));
        }


    }
}
