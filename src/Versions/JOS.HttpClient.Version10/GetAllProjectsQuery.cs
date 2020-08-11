﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JOSHttpClient.Common;
using JOSHttpClient.Common.Domain;

namespace JOSHttpClient.Version10
{
    public class GetAllProjectsQuery : IGetAllProjectsQuery
    {
        private readonly GitHubClientFactory _gitHubClientFactory;

        public GetAllProjectsQuery(GitHubClientFactory gitHubClientFactory)
        {
            _gitHubClientFactory = gitHubClientFactory ?? throw new ArgumentNullException(nameof(gitHubClientFactory));
        }

        public async Task<IReadOnlyCollection<Project>> Execute(CancellationToken cancellationToken)
        {
            var gitHubClient = _gitHubClientFactory.Create();
            var response = await gitHubClient.GetRepositories(cancellationToken).ConfigureAwait(false);
            return response.Select(x => new Project(x.Name, x.Url, x.Stars)).ToArray();
        }
    }
}
